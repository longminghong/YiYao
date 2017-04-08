
#include <windows.h>
#include <stdio.h>
#include <time.h>

#define WltUSB_API __declspec(dllexport)

//extern TERMB_API int PASCAL Read_Content(int active);
extern WltUSB_API int PASCAL GetBmp(char * Wlt_File);
