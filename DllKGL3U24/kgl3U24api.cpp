#include "pch.h"
#include "kgl3U24api.h"

#include <Public.h>

#ifdef __cplusplus
	EXTERN_C_START
#endif // cplusplus

HANDLE Kgl3u24Open()
{
	HANDLE hDevice = CreateFile(USER_DEVICE_NAME,
		GENERIC_READ | GENERIC_WRITE,
		0,
		NULL,
		OPEN_EXISTING,
		0,
		NULL);

	if (hDevice == INVALID_HANDLE_VALUE)
	{
		hDevice = NULL;
	}


	return hDevice;
}

BOOL Kgl3u24Close(HANDLE deviceHandle)
{
	if (deviceHandle == INVALID_HANDLE_VALUE || deviceHandle == NULL)
	{
		return FALSE;
	}

	if (CloseHandle(deviceHandle))
	{
		return TRUE;
	}


	return FALSE;
}

BOOL Kgl3u24OutputSet(HANDLE deviceHandle, UINT16 param)
{
	SWITCH_WRITE_DATA swi;
	BOOL deviceCtlResult = FALSE;
	unsigned long returnedLength;

	if (deviceHandle == INVALID_HANDLE_VALUE || deviceHandle == NULL)
	{
		return FALSE;
	}

	swi.data1 = ~(UINT8)(param & 0x00FF);
	swi.data2 = ~(UINT8)((param >> 8) & 0x00FF);

	deviceCtlResult = DeviceIoControl(deviceHandle,
		IOCTRL_GPD_WRITE_SWITCH,
		&swi,
		sizeof(SWITCH_WRITE_DATA),
		NULL,
		0,
		&returnedLength,
		NULL);
	
	return deviceCtlResult;
}

#ifdef __cplusplus
	EXTERN_C_END
#endif // __cplusplus