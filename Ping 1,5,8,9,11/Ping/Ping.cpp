#include "stdafx.h"
#include <winsock2.h>
#include <iphlpapi.h>
#include <icmpapi.h>
#include <stdio.h>
#include <iostream>
#include <stdio.h>
#include <stdlib.h>
#include <string>

#pragma comment(lib, "iphlpapi.lib")
#pragma comment(lib, "ws2_32.lib")
#pragma warning(disable:4996) 

using namespace std;

int main() {
	//------------------------------------------------------------
	setlocale(LC_ALL, "Russian");

	HANDLE hIcmpFile;		
	unsigned long ipaddr = INADDR_NONE;
	DWORD dwRetVal = 0;
	char SendData[32] = "Data Buffer";
	LPVOID ReplyBuffer = NULL;
	DWORD ReplySize = 0;

	int start_firstByte = -1;
	int end_firstByte = -1;

	int start_secondByte = -1;
	int end_secondByte = -1;
	//------------------------------------------------------------
	while (start_firstByte > 255 || start_firstByte < 0 || end_firstByte > 255 || end_firstByte < 0
		|| start_secondByte > 255 || start_secondByte < 0 || end_secondByte > 255 || end_secondByte < 0)
	{
		printf("Введите диапазон адресов от 0 до 255: ");
		printf("\n 1) Старт: ");
		cin >> start_firstByte;
		printf("\n 1) Финиш: ");
		cin >> end_firstByte;
		printf("\n 2) Старт: ");
		cin >> start_secondByte;
		printf("\n 2) Финиш: ");
		cin >> end_secondByte;
	}

	ReplySize = sizeof(ICMP_ECHO_REPLY) + sizeof(SendData);
	ReplyBuffer = (VOID*)malloc(ReplySize);

	const char* pointerToAddres;	//указатель на текущий ip-адрес

	for (int i = start_firstByte; i <= end_firstByte; i++) {
		for (int j = start_secondByte; j <= end_secondByte; j++) {

			//------------------------------------------------------------
			char buffer1[3];
			char buffer2[3];

			char layout[16] = "192.168.";

			strcat(layout, itoa(i, buffer1, 10));

			strcat(layout, ".");

			strcat(layout, itoa(j, buffer2, 10));

			pointerToAddres = layout;

			//----------------------------------------------------------------
			printf("Поиск: %s \n", pointerToAddres);

			ipaddr = inet_addr(pointerToAddres);	//переводим адрес в нужный формат

			hIcmpFile = IcmpCreateFile();			//открываем соединение

			dwRetVal = IcmpSendEcho(hIcmpFile, ipaddr, SendData, sizeof(SendData), NULL, ReplyBuffer, ReplySize, 1000);  //посылаем запрос

			PICMP_ECHO_REPLY pEchoReply = (PICMP_ECHO_REPLY)ReplyBuffer;  //записываем ответ
			struct in_addr ReplyAddr;
			ReplyAddr.S_un.S_addr = pEchoReply->Address;

			printf("Адрес: %s ", inet_ntoa(ReplyAddr));			//выводим
			printf("Ping = %ld миллисекунд\n", pEchoReply->RoundTripTime);

			//------------------------------------------------------------
			IcmpCloseHandle(hIcmpFile);			//закрываем конект
		}
	}

	system("pause");
	return 0;
}