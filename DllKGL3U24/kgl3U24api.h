#pragma once
#include "pch.h"


typedef struct {
    uint8_t data1;
    uint8_t data2;
}SWITCH_WRITE_DATA;

//��3U24�������豸
HANDLE __stdcall Kgl3u24Open();
//���ݾ���ر�3U24�������豸
BOOL __stdcall Kgl3u24Close(HANDLE deviceHandle);
//���ƿ����������
//deviceHandle Ϊ �豸��Ч���
//param Ϊ����ĵ�16��ͨ�����״̬��1��ʾ��ͨ��0��ʾ�Ͽ���
BOOL __stdcall Kgl3u24OutputSet(HANDLE deviceHandle, UINT16 param);