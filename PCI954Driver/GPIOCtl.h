#pragma once
EXTERN_C_START

typedef struct _COM_SET_MODE
{
	UCHAR ComNum;		//���ں�
	ULONG Baud;			//������
	UCHAR Data;			//����λ
	UCHAR Stop;			//ֹͣλ
	UCHAR Parity;		//У��λ
	UCHAR Count;		//Fifo�жϼ���
	UCHAR Mode;			//����ģʽ
}COM_SET_MODE, *PCOM_SET_MODE;

EXTERN_C_END