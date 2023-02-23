using System;
using System.Runtime.InteropServices;
using System.Collections.Generic;
using System.Text;
using System.Resources;
using System.Reflection;
using System.IO;

using GrdHandle = Guardant.Handle;

namespace Guardant
{
	/// <summary>
	/// Класс для задания номера алгоритма 
	/// </summary>
	public class GrdAlgNum
	{
		int mValue;

		public static implicit operator GrdAlgNum(int i)
		{
			return new GrdAlgNum(i);
		}

		public static implicit operator GrdAlgNum(uint i)
		{
			return new GrdAlgNum((int)i);
		}

		public GrdAlgNum(int value)
		{
			mValue = value;
		}

		public GrdAlgNum(uint value)
		{
			mValue = (int)value;
		}

		public int Value
		{
			get { return mValue; }
		}
	}


		/// <summary>
	/// Класс для задания номера алгоритма 
	/// </summary>
	public class GrdPswd
	{
		int mValue;

		public static implicit operator GrdPswd(int i)
		{
			return new GrdPswd(i);
		}

		public static implicit operator GrdPswd(uint i)
		{
			return new GrdPswd((int)i);
		}

		public GrdPswd(int value)
		{
			mValue = value;
		}

		public GrdPswd(uint value)
		{
			mValue = (int)value;
		}

		public int Value
		{
			get { return mValue; }
		}
	}

	/// <summary>
	/// Пароли к демонстрационным алгоритмам Guardant GSII64
	/// </summary>
	/// 
	public class GrdAP : GrdPswd
	{
		/// <summary>
		/// Демо GSII64 пароль для активации 
		/// </summary>
		public static GrdAP GSII64_DEMO_ACTIVATION = new GrdAP(0xAAAAAAAA);

		/// <summary>
		/// Демо GSII64 пароль для деактивации 
		/// </summary>
		public static GrdAP GSII64_DEMO_DEACTIVATION = new GrdAP(0xDDDDDDDD);

		/// <summary>
		/// Демо GSII64 Пароль для чтения 
		/// </summary>
		public static GrdAP GSII64_DEMO_READ = new GrdAP(0xBBBBBBBB);

		/// <summary>
		/// Демо GSII64 пароль для обновления
		/// </summary>
		public static GrdAP GSII64_DEMO_UPDATE = new GrdAP(0xCCCCCCCC);

		/// <summary>
		/// Демо HASH64 пароль для активации
		/// </summary>
		public static GrdAP HASH64_DEMO_ACTIVATION = new GrdAP(0xAAAAAAAA);

		/// <summary>
		/// Демо HASH64 пароль для деактивации
		/// </summary>
		public static GrdAP HASH64_DEMO_DEACTIVATION = new GrdAP(0xDDDDDDDD);

		/// <summary>
		/// Демо HASH64 пароль для чтения
		/// </summary>
		public static GrdAP HASH64_DEMO_READ = new GrdAP(0xBBBBBBBB);

		/// <summary>
		/// Демо HASH64 пароль для обновления
		/// </summary>
		public static GrdAP HASH64_DEMO_UPDATE = new GrdAP(0xCCCCCCCC);

		public static implicit operator int(GrdAP grdAp)
		{
			return grdAp.Value;
		}

		public static implicit operator uint(GrdAP grdAp)
		{
			return (uint)grdAp.Value;
		}

		public GrdAP(int value)
			: base(value)
		{
		}

		public GrdAP(uint value)
			: base(value)
		{
		}
	}

	/// <summary>
	/// Номера алгоритмов и защищенных ячеек в прошивке Guardant Sign по умолчанию
	/// </summary>
	public class GrdAN : GrdAlgNum
	{
		/// <summary>
		/// GSII64 для автоматической защиты + используется в API
		/// </summary>
		public static GrdAN GSII64 = new GrdAN(0);

		/// <summary>
		/// HASH64 для автоматической защиты + используется в API
		/// </summary>
		public static GrdAN HASH64 = new GrdAN(1);

		/// <summary>
		/// RAND64 для автоматической защиты + используется в API
		/// </summary>
		public static GrdAN RAND64 = new GrdAN(2);

		/// <summary>
		/// Защищенная ячейка, только для считывания. Может быть обновлена с помощью Secured  Guardant Remote Update
		/// </summary>
		public static GrdAN READ_ONLY = new GrdAN(3);

		/// <summary>
		/// Защищенная ячейка, для считывания и записи. Может быть обновлена с помощью protected application runtime
		/// </summary>
		public static GrdAN READ_WRITE = new GrdAN(4);

		/// <summary>
		/// GSII64 демо алгоритм, используется в примерах
		/// </summary>
		public static GrdAN GSII64_DEMO = new GrdAN(5);

		/// <summary>
		/// HASH64 демо алгоритм, используется в примерах
		/// </summary>
		public static GrdAN HASH64_DEMO = new GrdAN(6);

		/// <summary>
		/// ECC160 демо алгоритм, используется в примерах
		/// </summary>
		public static GrdAN ECC160 = new GrdAN(8);

		/// <summary>
		/// AES128 демо алгоритм, используется в примерах
		/// </summary>
		public static GrdAN AES128 = new GrdAN(9);

		/// <summary>
		/// GSII64 (только зашифровывание) демо алгоритм, используется в примерах
		/// </summary>
		public static GrdAN GSII64_ENCRYPT = new GrdAN(10);

		/// <summary>
		/// GSII64 (только расшифровывание) демо алгоритм, используется в примерах
		/// </summary>
		public static GrdAN GrdAN_GSII64_DECRYPT = new GrdAN(11);


		public static implicit operator int(GrdAN grdAn)
		{
			return grdAn.Value;
		}

		public static implicit operator uint(GrdAN grdAn)
		{
			return (uint)grdAn.Value;
		}

		GrdAN(int value)
			: base(value)
		{
		}

		GrdAN(uint value)
			: base(value)
		{
		}

	}

	/// <summary>
	/// Номера программно-реализованных алгоритмов
	/// </summary>
	public class GrdSA : GrdAlgNum
	{
		/// <summary>
		/// Флаг аппаратно-реализованного алгоритма хеширования и шифрования
		/// </summary>
		static int SoftAlgo = unchecked((int)0x80000000);

		/// <summary>
		/// ECC160 asymmetric cryptoalgorithm number    
		/// </summary>
		public static GrdSA ECC160 = new GrdSA(SoftAlgo + 0);

		/// <summary>
		/// Программно-реализованный алгоритм вычисления CRC
		/// </summary>
		public static GrdSA CRC32 = new GrdSA(SoftAlgo + 0);

		/// <summary>
		/// Программно-реализованный алгоритм хеширования
		/// </summary>
		public static GrdSA SHA256 = new GrdSA(SoftAlgo + 1);

		/// <summary>
		/// Программно-реализованный криптографический алгоритм
		/// </summary>
		public static GrdSA AES256 = new GrdSA(SoftAlgo + 0);

		GrdSA(int value)
			: base(value)
		{
		}
	}

	/// <summary>
	// для софтверных алгоритмов и вычислений хеш
	/// </summary>
	public class GrdSC
	{
		/// <summary>
		/// Первый блок
		/// </summary>
		public static GrdSC First = new GrdSC(0x100);

		/// <summary>
		/// Следующий блок
		/// </summary>
		public static GrdSC Next = new GrdSC(0x200);

		/// <summary>
		/// Последний блок
		/// </summary>
		public static GrdSC Last = new GrdSC(0x400);

		/// <summary>
		/// Все блоки
		/// </summary>
		public static GrdSC All = new GrdSC(First.mValue + Next.mValue + Last.mValue);

		int mValue;

		GrdSC(int value)
		{
			mValue = value;
		}

		public int Value
		{
			get { return mValue; }
		}
	}

	/// <summary>
	/// Режимы использования <b>GSII64 </b>в <b>Guardant Stealth</b>
	/// <br/>- bit 0-5 - режим использования
	/// <br/>- bit 7   - шифрование/расшифрование
	/// <br/>- bit 8-9 - позиция шифруемого блока
	/// </summary>
	public class GrdAM
	{
		/// <summary>
		/// Режим электронной кодовой книги
		/// </summary>
		/// <remarks>
		/// Это простейший режим работы алгоритма <b>GSII64</b>. В режиме <b>ECB </b>каждый 8-байтовый блок, подавемый на вход алгоритма, преобразуется с одним и тем же определителем в другой 8-байтовый блок. Поэтому преобразование двух одинаковых 8-байтовых блоков даст идентичный результат. 
		/// </remarks>
		public static GrdAM ECB = new GrdAM(0);

		/// <summary>
		/// Режим сцепления кодированных блоков
		/// </summary>
		/// <remarks> 
		/// В режиме <b>CBC</b>, как и в <b>ECB</b>, каждый 8-байтовый блок преобразуется в 8-байтовый блок. Преобразование в режиме <b>CBC </b>для всех блоков осуществляется с одним и тем же определителем. Режим <b>CBC </b>чаще используется и лучше подходит для преобразования блоков данных, превышающих по длине 8 байтов.
		/// </remarks>
		public static GrdAM CBC = new GrdAM(1);

		/// <summary>
		/// Режим с кодированной обратной связью
		/// </summary>
		/// <remarks>
		/// Режим <b>CFB</b> позволяет преобразовывать блоки данных произвольного размера, не обязательно кратного 8 байтам. Это избавляет от необходимости дополнять исходные данные до целого количества 8-байтовых блоков. В этом режиме длина закодированной последовательности будет равна длине исходной.
		/// <br/><br/>
		/// <b>Примечание.</b>
		/// <br/><br/>
		/// Если при декодировании указан неверный вектор инициализации, все данные, кроме первых 8 байт, все равно декодируются правильно. Если это критично для приложения, предпочтительно использовать режим OFB. 
		/// </remarks>
		public static GrdAM CFB = new GrdAM(2);

		/// <summary>
		/// Режим с обратной связью по выходу
		/// </summary>
		/// <remarks>
		/// Этот режим имеет много общего с режимом CFB.Главное отличие состоит в том, что для кодирования следующего блока используется не закодированный предыдущий блок, а результат преобразования вектора инициализации IV.
		/// </remarks>
		public static GrdAM OFB = new GrdAM(3);

		/// <summary>
		/// Кодирование
		/// </summary>
		public static GrdAM Encode = new GrdAM(0);

		/// <summary>
		/// Декодирование
		/// </summary>
		public static GrdAM Decode = new GrdAM(0x80);

		/// <summary>
		/// Синонимы
		/// </summary>
		public static GrdAM Encrypt = new GrdAM(Encode.mValue);

		/// <summary>
		/// Синонимы
		/// </summary>
		public static GrdAM Decrypt = new GrdAM(Decode.mValue);

		int mValue;

		GrdAM(int value)
		{
			mValue = value;
		}

		public int Value
		{
			get { return mValue; }
		}

		public static GrdAM operator +(GrdAM op1, GrdAM op2)
		{
			return new GrdAM(op1.Value + op2.Value);
		}

		public static GrdAM operator +(GrdAM op1, GrdSC op2)
		{
			return new GrdAM(op1.Value + op2.Value);
		}

		public static GrdAM operator +(GrdSC op1, GrdAM op2)
		{
			return new GrdAM(op1.Value + op2.Value);
		}
	}


	public class GrdInfo
	{
		int mValue;

		public static implicit operator GrdInfo(int i)
		{
			return new GrdInfo(i);
		}

		public GrdInfo(int value)
		{
			mValue = value;
		}

		public int Value
		{
			get { return mValue; }
		}
	}

	/// <summary>
	/// Класс содержит константы для получения информации с помощью функции GrdGetInfo.
	/// <br/>Режимы поиска и логина.
	/// </summary>
	public class GrdGIF : GrdInfo
	{
		/// <summary>
		/// Локальные и/или удаленные ключи (GrdFMR.Local + GrdFMR.Remote)
		/// </summary>
		public static GrdGIF Remote = new GrdGIF(0x2000);

		/// <summary>
		/// Флаги
		/// </summary>
		public static GrdGIF Flags = new GrdGIF(0x2001);

		/// <summary>
		/// Заданное в GrdSetFindMode() значение для поля"Номера программы"
		/// </summary>
		public static GrdGIF Prog = new GrdGIF(0x2002);

		/// <summary>
		/// Заданное в GrdSetFindMode() значение для поля "Уникального идентификатора ключа"
		/// </summary>
		public static GrdGIF ID = new GrdGIF(0x2003);

		/// <summary>
		/// Заданное в GrdSetFindMode() значение для поля "Серийного номера"
		/// </summary>
		public static GrdGIF SN = new GrdGIF(0x2004);

		/// <summary>
		/// Заданное в GrdSetFindMode() значение для поля "Версия"
		/// </summary>
		public static GrdGIF Ver = new GrdGIF(0x2005);

		/// <summary>
		/// Заданное в GrdSetFindMode() значение для поля "Битовая маска"
		/// </summary>
		public static GrdGIF Mask = new GrdGIF(0x2006);

		/// <summary>
		/// Заданное в GrdSetFindMode() значение для поля "Тип ключа"
		/// </summary>
		public static GrdGIF Type = new GrdGIF(0x2007);

		/// <summary>
		/// Заданное в GrdSetFindMode() значение для поле "Модель ключа" GrdDM_XXX 
		/// </summary>
		public static GrdGIF Model = new GrdGIF(0x2008);

		/// <summary>
		/// Заданное в GrdSetFindMode() значение для поле "Интерфейс" GrdDI_XXX  
		/// </summary>
		public static GrdGIF Interface = new GrdGIF(0x2009);

		GrdGIF(int value): base(value)
		{
		}
	}

	/// <summary>
	/// Класс содержит константы для получения информации с помощью функции GrdGetInfo.
	/// <br/>Информация об API.
	/// </summary>
	public class GrdGIV : GrdInfo
	{
		/// <summary>
		/// Версия API
		/// </summary>
		public static GrdGIV VerAPI = new GrdGIV(0x0000);

		GrdGIV(int value): base(value)
		{
		}
	}

	/// <summary>
	/// Класс содержит константы для получения информации с помощью функции GrdGetInfo.
	/// <br/>Информация о режиме.
	/// </summary>
	public class  GrdGIM : GrdInfo
	{
		/// <summary>
		/// Режим работы
		/// </summary>
		public static GrdGIM WorkMode = new GrdGIM(0x1000);

		/// <summary>
		/// Режим хендла
		/// </summary>
		public static GrdGIM HandleMode = new GrdGIM(0x1001);

		GrdGIM(int value): base(value)
		{
		}
	}

	/// <summary>
	/// Константы для получения информации с помощью функции GrdGetInfo.
	/// <br/>Информация о текущем ключе.
	/// </summary>
	public class GrdGIL : GrdInfo
	{
		/// <summary>
		/// Локальный или удаленный ключ
		/// </summary>
		public static GrdGIL Remote = new GrdGIL(0x3000);

		/// <summary>
		/// ID ключа
		/// </summary>
		public static GrdGIL ID = new GrdGIL(0x3001);

		/// <summary>
		/// Модель ключа
		/// </summary>
		public static GrdGIL Model = new GrdGIL(0x3002);

		/// <summary>
		/// Интерфейс ключа
		/// </summary>
		public static GrdGIL Interface = new GrdGIL(0x3003);

		/// <summary>
		/// Заблокировать значение счетчика ключа
		/// </summary>
		public static GrdGIL LockCounter = new GrdGIL(0x3005);

		/// <summary>
		/// Адрес памяти ключа
		/// </summary>
		public static GrdGIL Seek = new GrdGIL(0x3006);

		/// <summary>
		/// Версия драйвера (0x04801234=4.80.12.34)
		/// </summary>
		public static GrdGIL DrvVers = new GrdGIL(0x4000);

		/// <summary>
		/// Сборка драйвера
		/// </summary>
		public static GrdGIL DrvBuild = new GrdGIL(0x4001);

		/// <summary>
		/// Адрес LPT порта (0 == USB)
		/// </summary>
		public static GrdGIL PortLPT = new GrdGIL(0x4002);

		/// <summary>
		/// Virtual dongle container file name unicode string
		/// </summary>
		public static GrdGIL VirtualFileName = new GrdGIL(0x4003);

		GrdGIL(int value): base(value)
		{
		}
	}

	/// <summary>
	/// Константы для получения информации с помощью функции GrdGetInfo.
	/// <br/>Информация о текущем сетевом ключе.
	/// </summary>
	public class GrdGIR : GrdInfo
	{
		/// <summary>
		/// Версия Guardant Net server 
		/// </summary>
		public static GrdGIR VerSrv= new GrdGIR(0x5000);

		/// <summary>
		/// Guardant Net локальный IP Адрес
		/// </summary>
		public static GrdGIR LocalIP = new GrdGIR(0x5001);

		/// <summary>
		/// Guardant Net локальный  IP порт
		/// </summary>
		public static GrdGIR LocalPort = new GrdGIR(0x5002);

		/// <summary>
		/// Guardant Net локальное имя NetBIOS
		/// </summary>
		public static GrdGIR LocalNB = new GrdGIR(0x5003);

		/// <summary>
		/// Guardant Net удаленный IP адрес
		/// </summary>
		public static GrdGIR RemoteIP = new GrdGIR(0x5004);

		/// <summary>
		/// Guardant Net удаленный IP порт
		/// </summary>
		public static GrdGIR RemotePort = new GrdGIR(0x5005);

		/// <summary>
		/// Guardant Net удаленное имя NetBIOS 
		/// </summary>
		public static GrdGIR RemoteNB = new GrdGIR(0x5006);

		/// <summary>
		/// Handle of internal heartbeat thread 
		/// </summary>
		public static GrdGIR HeartBeatThread = new GrdGIR(0x5007);

		/// <summary>
		/// Send operation timeout in seconds. Requires the API to be started up with GrdFMR_Remote flag.
		/// </summary>
		public static GrdGIR IniTimeOutSend = new GrdGIR(0x5008);

		/// <summary>
		/// Receive operation timeout in seconds. Requires the API to be started up with GrdFMR_Remote flag.
		/// </summary>
		public static GrdGIR IniTimeOutReceive = new GrdGIR(0x5009);

		/// <summary>
		/// Broadcasting search timeout in seconds. Requires the API to be started up with GrdFMR_Remote flag.
		/// </summary>
		public static GrdGIR IniTimeOutSearch = new GrdGIR(0x500A);

		/// <summary>
		/// Client's UDP port for sending of datagrams to a server. Requires the API to be started up with GrdFMR_Remote flag.
		/// </summary>
		public static GrdGIR IniClientUDPPort = new GrdGIR(0x500B);

		/// <summary>
		/// Server's UDP port for sending of replies to a client. Requires the API to be started up with GrdFMR_Remote flag.
		/// </summary>
		public static GrdGIR IniServerUDPPort = new GrdGIR(0x500C);

		/// <summary>
		/// Broadcasting address . Requires the API to be started up with GrdFMR_Remote flag.
		/// </summary>
		public static GrdGIR IniBroadcastAddress = new GrdGIR(0x500D);

		/// <summary>
		/// Initialization file name. Requires the API to be started up with GrdFMR_Remote flag.
		/// </summary>
		public static GrdGIR IniFileName = new GrdGIR(0x500E);

		/// <summary>
		/// MAC address of the local network adapter. Requires the API to be started up with GrdFMR_Remote flag.
		/// </summary>
		public static GrdGIR LocalMACAddress = new GrdGIR(0x500F);

		/// <summary>
		/// Full name of the local host. Requires the API to be started up with GrdFMR_Remote flag.
		/// </summary>
		public static GrdGIR FullHostName = new GrdGIR(0x5010);

		/// <summary>
		/// Server IP address or host name. Requires the API to be started up with GrdFMR_Remote flag.
		/// </summary>
		public static GrdGIR IniServerIPName = new GrdGIR(0x5011);

		public static implicit operator GrdGIR(int i)
		{
			return new GrdGIR(i);
		}

		GrdGIR(int value): base(value)
		{
		}
	}

	/// <summary>
	/// Класс UAM адресов полей данных для использования в функциях GrdRead и GrdWrite
	/// </summary>
	public class GrdUAM
	{
		/// <summary>
		/// 00h Номер программы
		/// </summary>
		public static GrdUAM NProg = new GrdUAM(30 - 30);

		/// <summary>
		/// 01h Версия
		/// </summary>
		public static GrdUAM Ver = new GrdUAM(31 - 30);

		/// <summary>
		/// 02h Серийный номер
		/// </summary>
		public static GrdUAM SN = new GrdUAM(32 - 30);

		/// <summary>
		/// 04h Маска
		/// </summary>
		public static GrdUAM Mask = new GrdUAM(34 - 30);

		/// <summary>
		/// 06h Счетчик #1 (GP)
		/// </summary>
		public static GrdUAM GP = new GrdUAM(36 - 30);

		/// <summary>
		/// 08h Реальный сетевой ресурс
		/// </summary>
		public static GrdUAM RealLANRes = new GrdUAM(38 - 30);

		/// <summary>
		/// 0Ah Индекс
		/// </summary>
		public static GrdUAM Index = new GrdUAM(40 - 30);

		/// <summary>
		/// 0Eh Адрес алгоритма
		/// </summary>
		public static GrdUAM AlgoAddr = new GrdUAM(44 - 30);

		uint mValue;

		public static implicit operator GrdUAM(int i)
		{
			return new GrdUAM((uint)i);
		}

		public static implicit operator GrdUAM(uint i)
		{
			return new GrdUAM(i);
		}

		public GrdUAM(uint value)
		{
			mValue = value;
		}

		public uint Value
		{
			get { return mValue; }
		}
	}

	/// <summary>
	/// Класс SAM адресов полей данных для использования в функциях GrdRead и GrdWrite
	/// </summary>
	public class GrdSAM 
	{
		/// <summary>
		/// 0h, неизменяемое поле (значение прошивается на стадии производства)
		/// </summary>
		public static GrdSAM KeyModelAddr = new GrdSAM(0);

		/// <summary>
		/// 1h, неизменяемое поле (значение прошивается на стадии производства)
		/// </summary>
		public static GrdSAM KeyMemSizeAddr = new GrdSAM(1);

		/// <summary>
		/// 2h, неизменяемое поле (значение прошивается на стадии производства)
		/// </summary>
		public static GrdSAM KeyProgVerAddr = new GrdSAM(2);

		/// <summary>
		/// 3h, неизменяемое поле (значение прошивается на стадии производства)
		/// </summary>
		public static GrdSAM KeyProtocolAddr = new GrdSAM(3);

		/// <summary>
		/// 4h, неизменяемое поле (значение прошивается на стадии производства)
		/// Example: 0x104=1.4
		/// </summary>
		public static GrdSAM ClientVerAddr = new GrdSAM(4);

		/// <summary>
		/// 6h, неизменяемое поле (значение прошивается на стадии производства)
		/// </summary>
		public static GrdSAM KeyUserAddrAddr = new GrdSAM(6);

		/// <summary>
		/// 7h, неизменяемое поле (значение прошивается на стадии производства)
		/// </summary>
		public static GrdSAM KeyAlgoAddrAddr = new GrdSAM(7);

		/// <summary>
		/// 8h, неизменяемое поле (значение прошивается на стадии производства)
		/// </summary>
		public static GrdSAM PrnportAddr = new GrdSAM(8);

		/// <summary>
		/// Ah, 
		/// </summary>
		public static GrdSAM WriteProtectS3 = new GrdSAM(10);

		/// <summary>
		/// Ch, 
		/// </summary>
		public static GrdSAM ReadProtectS3 = new GrdSAM(12);

		/// <summary>
		/// Eh, неизменяемое поле (значение прошивается на стадии производства)
		/// </summary>
		public static GrdSAM PublicCode = new GrdSAM(14);

		/// <summary>
		/// 12h, неизменяемое поле (значение прошивается на стадии производства)
		/// </summary>
		public static GrdSAM Version = new GrdSAM(16);

		/// <summary>
		/// 13h, неизменяемое поле (значение прошивается на стадии производства)
		/// </summary>
		public static GrdSAM LANRes = new GrdSAM(19);

		/// <summary>
		/// 14h, неизменяемое поле (значение прошивается на стадии производства)
		/// </summary>
		public static GrdSAM Type = new GrdSAM(20);

		/// <summary>
		/// 16h, неизменяемое поле (значение прошивается на стадии производства)
		/// </summary>
		public static GrdSAM ID = new GrdSAM(22);

		/// <summary>
		/// 1Ah, данное поле устанавливается только функцией GrdProtect после выполнения GrdInit
		/// </summary>
		public static GrdSAM WriteProtect = new GrdSAM(26);

		/// <summary>
		/// 1Bh, данное поле устанавливается только функцией GrdProtect после выполнения GrdInit
		/// </summary>
		public static GrdSAM ReadProtect = new GrdSAM(27);

		/// <summary>
		/// 1Ch, данное поле устанавливается только функцией GrdProtect после выполнения GrdInit
		/// </summary>
		public static GrdSAM NumFunc = new GrdSAM(28);

		/// <summary>
		/// 1Dh, данное поле устанавливается только функцией GrdProtect после выполнения GrdInit
		/// </summary>
		public static GrdSAM TableLMS = new GrdSAM(29);

		/// <summary>
		/// 1Eh, стртовый адрес при UAM адресации
		/// </summary>
		public static GrdSAM UAM = new GrdSAM(30);

		/// <summary>
		/// 1Eh, данное поле может быть перезаписано функцией GrdWrite
		/// </summary>
		public static GrdSAM NProg = new GrdSAM(30);

		/// <summary>
		/// 1Fh, данное поле может быть перезаписано функцией GrdWrite
		/// </summary>
		public static GrdSAM Ver = new GrdSAM(31);

		/// <summary>
		/// 20h, данное поле может быть перезаписано функцией GrdWrite
		/// </summary>
		public static GrdSAM SN = new GrdSAM(32);

		/// <summary>
		/// 22h, данное поле может быть перезаписано функцией GrdWrite
		/// </summary>
		public static GrdSAM Mask = new GrdSAM(34);

		/// <summary>
		/// 24h, данное поле может быть перезаписано функцией GrdWrite
		/// </summary>
		public static GrdSAM GP = new GrdSAM(36);

		/// <summary>
		/// 26h, данное поле может быть перезаписано функцией GrdWrite
		/// </summary>
		public static GrdSAM RealLANRes = new GrdSAM(38);

		/// <summary>
		/// 28h, данное поле может быть перезаписано функцией GrdWrite
		/// </summary>
		public static GrdSAM Index = new GrdSAM(40);

		/// <summary>
		/// 2Ch
		/// </summary>
		public static GrdSAM AlgoAddr = new GrdSAM(44);

		uint mValue;

		GrdSAM(uint value)
		{
			mValue = value;
		}

		public uint Value
		{
			get { return mValue; }
		}
	}

}