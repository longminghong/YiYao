extern "C"
{
	int __stdcall SDT_StartFindIDCard (
		int iPort,
		unsigned char * pucManaInfo,
		int iIfOpen
		);
	int __stdcall SDT_SelectIDCard (
		int iPort,
		unsigned char * pucManaMsg,
		int iIfOpen
		);
	int __stdcall SDT_ReadBaseMsg (
		int iPort,
		unsigned char * pucCHMsg,
		unsigned int * puiCHMsgLen,
		unsigned char * pucPHMsg,
		unsigned int * puiPHMsgLen,
		int iIfOpen
		);
}