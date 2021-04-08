#pragma once
#include "pch.h"


typedef struct {
    uint8_t data1;
    uint8_t data2;
}SWITCH_WRITE_DATA;

//��3U24�������豸
HANDLE kgl3u24_open();
//���ݾ���ر�3U24�������豸
BOOL kgl3u24_close(HANDLE deviceHandle);
//���ƿ����������
//deviceHandle Ϊ �豸��Ч���
//param Ϊ����ĵ�16��ͨ�����״̬��1��ʾ��ͨ��0��ʾ�Ͽ���
BOOL kgl3u24_output_set(HANDLE deviceHandle, UINT16 param);