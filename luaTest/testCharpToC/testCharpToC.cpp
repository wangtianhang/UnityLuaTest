// testCharpToC.cpp : ���� DLL Ӧ�ó���ĵ���������
//

#include "stdafx.h"


extern "C"
{
__declspec(dllexport) int Add(int a, int b)
	{
		return a + b;
	}
__declspec(dllexport) int Length(const char * str)
{
	return strlen(str);
}
}