#pragma once
#include <ntddk.h>
#include <wdf.h>

#include "trace.h"
EXTERN_C_START

/**
* ���ڴ��� PCI�忨�ϵĿ��������
* 
* deviceObject ��Ҫ�����Ƶ��豸���
* 
* buffer		��������ݻ�����(��Ҫ����Free��)
* 
* len			��������Ч�ֽڳ���
*/
NTSTATUS
WriteSwitch(
	_In_ WDFDEVICE deviceObject,
	_In_ PVOID buffer,
	_In_	size_t len
);

EXTERN_C_END