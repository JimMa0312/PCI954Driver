/*++

Module Name:

    device.h

Abstract:

    This file contains the device definitions.

Environment:

    Kernel-mode Driver Framework

--*/

#include "public.h"
#include "GPIOCtl.h"

EXTERN_C_START

//window Nt �ں���ʹ�õ����� �豸�ļ�����
#define GPD_DEVICE_NAME L"\\Device\\3U24"

//window Dos�ں���ʹ�õ����� �豸�ļ�����
#define DOS_DEVICE_NAME L"\\DDosDevices\\3U24Dev"

#define COM_NUM         4

//����PCI�豸������ID����ƷID
#define PCI_954_VID 0x10B5
#define PCI_954_DID 0x9052

//
// The device context performs the same job as
// a WDM device extension in the driver frameworks
//
typedef struct _DEVICE_CONTEXT
{
    //TODO
    ULONG PrivateDeviceData;  // just a placeholder

    PUNICODE_STRING RegistryPath;
    PVOID           PortBase;
    ULONG           PortCount;
    ULONG           PortMemoryType;
    PDEVICE_OBJECT  DeviceObject;
    PDEVICE_OBJECT  NextLowerDriver;
    BOOLEAN         Started;
    BOOLEAN         Removed;
    BOOLEAN         PortWasMapped;
    IO_REMOVE_LOCK  RemoveLock;

    //���ж����
    PKINTERRUPT     InterruptObject;
    BOOLEAN         FoundInterrupt;
    KIRQL           InterruptLevel;
    ULONG           InterruptVector;
    KAFFINITY       InterruptAffinity;
    KINTERRUPT_MODE InterruptMode;
    ULONG           TestIsr;
    //����

    //��PCI��� 
    ULONG           BusNumber;
    PCI_SLOT_NUMBER SlotNumber;
    //����

    //���շ���صı���
    PVOID           ComBase[COM_NUM];
    
    COM_SET_MODE    ComState[COM_NUM];
    BOOLEAN         Inited[COM_NUM];
    
    KSEMAPHORE      BufSemaphore[2][COM_NUM];
    PVOID           BufBase[2][COM_NUM];
    PVOID           DataNumBase[2][COM_NUM];
    PVOID           PtrBuff[2][COM_NUM];
    //����
} DEVICE_CONTEXT, *PDEVICE_CONTEXT;

//
// This macro will generate an inline function called DeviceGetContext
// which will be used to get a pointer to the device context memory
// in a type safe manner.
//
WDF_DECLARE_CONTEXT_TYPE_WITH_NAME(DEVICE_CONTEXT, DeviceGetContext)

//
// Function to initialize the device and its callbacks
//
NTSTATUS
PCI954DriverCreateDevice(
    _Inout_ PWDFDEVICE_INIT DeviceInit
    );

EXTERN_C_END
