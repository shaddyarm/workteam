using System;
using System.Runtime.InteropServices;
using System.Collections.Generic;
using System.Text;
using System.Resources;
using System.Reflection;
using System.IO;



namespace Guardant
{
	/// <summary>
	/// Структура описания формата времени.
	/// </summary>
	[StructLayout(LayoutKind.Sequential, Pack = 1)]
	public struct GrdSystemTime
	{
		/// <summary>
		/// Год (1601 - 30827)
		/// </summary>
		public ushort wYear;
		/// <summary>
		/// Месяц(Январь = 1, Февраль = 2, ...)
		/// </summary>
		public ushort wMonth;
		/// <summary>
		/// День недели (Воскресенье = 0, Понедельник = 1, ...)
		/// </summary>
		public ushort wDayOfWeek;
		/// <summary>
		/// День месяца (1-31)
		/// </summary>
		public ushort wDay;
		/// <summary>
		/// Час (0-23)
		/// </summary>
		public ushort wHour;
		/// <summary>
		/// Минуты (0-59)
		/// </summary>
		public ushort wMinute;
		/// <summary>
		/// Секунды(0-59)
		/// </summary>
		public ushort wSecond;
		/// <summary>
		/// Милисекунды(0-999)
		/// </summary>
		public ushort wMilliseconds;
	}


	/// <summary>
	/// Структура используемая в функции GrdApi.GrdFind для получения информации об электронном ключе
	/// </summary>
	[StructLayout(LayoutKind.Sequential, Pack = 1)]
	public unsafe struct FindInfo
	{
		/// <summary>
		/// Общий код доступа
		/// </summary>
		public uint dwPublicCode;
		/// <summary>
		/// Версия прошивки ключа
		/// </summary>
		public byte byHrwVersion;
		/// <summary>
		/// Максимальный сетевой ресурс
		/// </summary>
		public byte byMaxNetRes;
		/// <summary>
		/// Тип ключа
		/// </summary>
		public ushort wType;
		/// <summary>
		/// ID
		/// </summary>
		public uint dwID;
		// Following fields are available from UAM mode
		/// <summary>
		/// Номер продукта
		/// </summary>
		public byte byNProg;
		/// <summary>
		/// Версия продукта
		/// </summary>
		public byte byVer;
		/// <summary>
		/// Серийный номер
		/// </summary>
		public ushort wSN;
		/// <summary>
		/// Маска
		/// </summary>
		public ushort wMask;
		/// <summary>
		/// Счетчик запусков
		/// </summary>
		public ushort wGP;
		/// <summary>
		/// Назначенный сетевой ресурс ключа
		/// </summary>
		public ushort wRealNetRes;
		/// <summary>
		/// Индекс
		/// </summary>
		public uint dwIndex;

		// Информация только для Stealth III 
		/// <summary>
		/// Reserved memory for future use
		/// </summary>
		public fixed byte abyReservedISEE[28];
		/// <summary>
		/// Stealth III write protect address
		/// </summary>
		public ushort wWriteProtectS3;
		/// <summary>
		/// Stealth III read protect address
		/// </summary>
		public ushort wReadProtectS3;
		/// <summary>
		/// Global Stealth III flags. Reserved.
		/// </summary>
		public ushort wGlobalFlags;
		/// <summary>
		///  Dongle State. See GrdDSF_XXX definition 
		/// </summary>
		public uint dwDongleState;

		// Available since:
		// 1. Stealth Sign.(Firmware number >= 0x01000011h or 01.00.00.11)
		// 2. Guardant Code.
		/// <summary>
		/// Old firmware number(before SFU).
		/// </summary>
		public uint dwOldMPNum;
		/// <summary>
		/// Reserved memory for future use // 0xc0 192
		/// </summary>
		public fixed byte abyReservedH[188];

		// Reserved info from gsA
		// Driver info
		/// <summary>
		/// Платформа (Win32/Win64)
		/// </summary>
		public uint dwGrDrv_Platform;
		/// <summary>
		/// Версия драйвера
		/// </summary>
		public uint dwGrDrv_Vers;
		/// <summary>
		/// Сборка драйвера
		/// </summary>
		public uint dwGrDrv_Build;
		/// <summary>
		/// Зарезервированое значение
		/// </summary>
		public uint dwGrDrv_Reserved;

		// dongle info
		/// <summary>
		/// Адрес пользователя
		/// </summary>
		public uint dwRkmUserAddr;
		/// <summary>
		/// Адрес алгоритма
		/// </summary>
		public uint dwRkmAlgoAddrW;
		/// <summary>
		/// Адрес порта (0, если USB)
		/// </summary>
		public uint dwPrnPort;
		/// <summary>
		/// Версия клиента
		/// </summary>
		public uint dwClientVersion;

		// SAP start
		/// <summary>
		/// Тип ключа
		/// </summary>
		public uint dwRFlags;
		/// <summary>
		/// Версия программы
		/// </summary>
		public uint dwRProgVer;
		/// <summary>
		/// 
		/// </summary>
		public uint dwRcn_rc;
		/// <summary>
		/// 
		/// </summary>
		public uint dwNcmps;
		/// <summary>
		/// 
		/// </summary>
		public uint dwNSKClientVersion;
		/// <summary>
		/// Модель ключа
		/// </summary>
		public uint dwModel;
		/// <summary>
		/// Тип MCU
		/// </summary>
		public uint dwMcuType;
		/// <summary>
		/// Тип памяти ключа
		/// </summary>
		public uint dwMemoryType;
        /// <summary>
        /// Версия программы
        /// </summary>
        public byte bFeatureFlags;

		// Reserved memory for future use
        public fixed byte abyReserved[215];
	}

	/// <summary>
	/// Структура, полей памяти ключа при использование SAM aдресации
	/// </summary>
	[StructLayout(LayoutKind.Sequential, Pack = 1)]
	public struct GrdStdFields
	{
		// Fields protection against nsc_Init, nsc_Protect, nsc_Write commands
		//    * - Partial protection: nsc_Protect can be executed only after nsc_Init
		//    X - Full protection
		// Protection against command:      Init Protect Write
		public byte byDongleModel;      //  0h   X     X     X    0=GS,1=GU,2=GF,
		public byte byDongleMemSize;    //  1h   X     X     X    0=0, 8=256 ( Memsize = 1 << byDongleMemSize )
		public byte byDongleProgVer;    //  2h   X     X     X
		public byte byDongleProtocol;   //  3h   X     X     X
		public ushort wClientVer;       //  4h   X     X     X    0x104=1.4
		public byte byDongleUserAddr;   //  6h   X     X     X
		public byte byDongleAlgoAddr;   //  7h   X     X     X
		public ushort wPrnport;         //  8h   X     X     X
		public ushort wWriteProtectS3;  //  Ah         *     X      // Stealth III write protect SAM address in bytes
		public ushort wReadProtectS3;   //  Ch         *     X      // Stealth III read  protect SAM address in bytes
		public uint dwPublicCode;       //  Eh   X     X     X
		public byte byVersion;          // 12h   X     X     X
		public byte byLANRes;           // 13h   X     X     X
		public ushort wType;            // 14h   X     X     X
		public uint dwID;               // 16h   X     X     X
		public byte byWriteProtect;     // 1Ah         *     X      // Stealth I & II write protect SAM address in words
		public byte byReadProtect;      // 1Bh         *     X      // Stealth I & II read  protect SAM address in words
		public byte byNumFunc;          // 1Ch         *     X
		public byte byTableLMS;         // 1Dh         *     X
		public byte byNProg;            // 1Eh   X     X
		public byte byVer;              // 1Fh   X     X
		public ushort wSN;              // 20h   X     X
		public ushort wMask;            // 22h   X     X
		public ushort wGP;              // 24h   X     X
		public ushort wRealLANRes;      // 26h   X     X
		public uint dwIndex;            // 28h   X     X
		public byte abyAlgoAddr;        // 2Ch
	}

	/// <summary>
	/// Структура данных возвращаемая функцией GrdApi.GrdCodeGetInfo
	/// </summary>
	[StructLayout(LayoutKind.Sequential, Pack = 1)]
	public unsafe struct GrdCodeInfo
	{
		/// <summary>
		/// Flash start address for User Firmware.
		/// </summary>
		public uint dwStartAddr;
		/// <summary>
		/// Flash size for User Firmware.
		/// </summary>
		public uint dwCodeSizeMax;
		/// <summary>
		/// Flash sector size for User Firmware.
		/// </summary>
		public uint dwCodeSectorSize;
		/// <summary>
		/// RAM start address for User Firmware.
		/// </summary>
		public uint dwStartRamAddr;
		/// <summary>
		/// RAM size for User Firmware.
		/// </summary>
		public uint dwRamSizeMax;
		/// <summary>
		/// Reserved.
		/// </summary>
		public uint dwReserved;
		/// <summary>
		/// public data .
		/// </summary>
		public GrdCodePublicData UFPublicData;
		/// <summary>
		/// Hash of loadable code.
		/// </summary>
		public fixed byte abHashLoadableCode[32];
		/// <summary>
		/// Reserved.
		/// </summary>
		public fixed byte abReserved[64];
	}

	/// <summary>
	/// Структура данных возвращаемая функцией GrdApi.GrdCodeGetInfo внутри стуктуры GrdCodeInfo
	/// </summary>
	[StructLayout(LayoutKind.Sequential, Pack = 1)]
	public struct GrdCodePublicData
	{
		/// <summary>
		/// User Firmware version.
		/// </summary>
		public byte bFirmwareVersion;
		/// <summary>
		/// Loading date.
		/// </summary>
		public byte bLoadingDate;
		/// <summary>
		/// User Firmware state.
		/// </summary>
		public byte bState;
		/// <summary>
		/// Reserved
		/// </summary>
		public byte bReserved;
		/// <summary>
		/// In/Out buffer size.
		/// </summary>
		public uint dwIOBufSize;
	}

	/// <summary>
	/// Структура данных дискриптора алгоритма загружаемого кода 
	/// </summary>
	[StructLayout(LayoutKind.Sequential, Pack = 1)]
	public unsafe struct GrdLoadableCodeData
	{
		public GrdCodePublicData PublicDataLoadableCode;
		public fixed byte abLoadableCodePublicKey4VerifySign[(int)GrdECC160.PUBLIC_KEY_SIZE];
		public fixed byte abLoadableCodePrivateKey4DecryptKey[(int)GrdECC160.PRIVATE_KEY_SIZE];
		/// <summary>
		/// The specified start address flash-memory for loadable code.
		/// </summary>
		public uint dwBegFlashAddr;
		/// <summary>
		/// The specified end address flash-memory for loadable code.
		/// </summary>
		public uint dwEndFlashAddr;
		/// <summary>
		/// The specified start address RAM-memory for loadable code.
		/// </summary>
		public uint dwBegMemAddr;
		/// <summary>
		/// The specified end address RAM-memory for loadable code.
		/// </summary>
		public uint dwEndMemAddr;
	}



	/// <summary>
	/// GrdTime
	/// </summary>
	[StructLayout(LayoutKind.Sequential, Pack = 1)]
	public struct GrdTime
	{
		/// <summary>
		/// Seconds     0 - 59
		/// </summary>
		public byte bSeconds;
		/// <summary>
		/// Minutes     0 - 59
		/// </summary>
		public byte bMinute;
		/// <summary>
		/// Hours       0 - 23
		/// </summary>
		public byte bHour;
		/// <summary>
		/// Days        1 - 31
		/// </summary>
		public byte bDay;
		/// <summary>
		/// Months      1 - 12
		/// </summary>
		public byte bMonth;
		/// <summary>
		/// Years       0 - 99 from 2000 (i.e. 0 is 2000 , 1 is 2001 etc...)
		/// </summary>
		public byte bYear;
	}

	/// <summary>
	/// GrdLifeTime
	/// </summary>
	[StructLayout(LayoutKind.Sequential, Pack = 1)]
	public struct GrdLifeTime
	{
		/// <summary>
		/// Difference of dates is Life Time
		/// </summary>
		public GrdTime LifeTime;
		/// <summary>
		/// 0 - has not been activated, 1 - activated
		/// </summary>
		public byte State;
		/// <summary>
		/// Reserved
		/// </summary>
		public byte ReservedForEven;
	}

	/// <summary>
	/// GrdFlipTime
	/// </summary>
	[StructLayout(LayoutKind.Sequential, Pack = 1)]
	public struct GrdFlipTime
	{
		/// <summary>
		/// Date and Time to start count
		/// </summary>
		public GrdTime rs_ChangeFlipTimeStart;
		/// <summary>
		/// Changes every 'rs_DaysGap' days from. Must be 1 - 255
		/// </summary>
		public byte rs_DaysGap;
		/// <summary>
		/// Reserved
		/// </summary>
		public byte ReservedForEven;
	}


	/// <summary>
	/// SBMAPFILE
	/// </summary>
	[StructLayout(LayoutKind.Sequential, Pack = 1)]
	public struct SBMAPFILE
	{
		public uint dwRes;
		public uint dwFlashStart;                   // Start FLASH
		public uint dwFlashSize;                    // FLASH size
		public uint dwFlashSizeReal;                // Really used FLASH size
		public uint dwRamStart;                     // Start RAM
		public uint dwRamSize;                      // RAM size
		public uint dwRamSizeReal;                  // Really used RAM size

		public uint dwMaptblOffset;                 // MAP-file ranges table offset
		public uint dwMaptblSize;                   // MAP-file ranges table size 
	}

	/// <summary>
	/// SignTimeCode_FixedItem
	/// </summary>
	[StructLayout(LayoutKind.Sequential, Pack = 1)]
	public unsafe struct SignTimeCode_FixedItem
	{
		public byte byLoFlags;                      // LoFlags
		public byte byAlgorithmCode;                // Algorithm's code
		public fixed byte rs_res[2];                // 2 bytes are reserved for even
		public uint dwHiFlags;                      // HiFlags
		public uint dwKeyLength;                    // Size of algorithm's key
		public uint dwBlockLength;                  // Request Block length
		public fixed byte rs_hash[8];               // 8 bytes are reserved for even
		public uint dwActivatePwd;                  // Activation service password
		public uint dwDeactivatePwd;                // Deactivation service password
		public uint dwReadPwd;                      // Read service password
		public uint dwUpdatePwd;                    // Update service password
		public GrdTime BirthTime;
		public GrdTime DeadTime;
		public GrdLifeTime LifeTime;
		public GrdFlipTime FlipTime;
		public uint dwGP_Counter;                   // Backward GP counter
		public uint dwErrorCounter;                 // Permissible incorrect password attempts
	}

	/// <summary>
	/// StealthII_FixedHeader
	/// </summary>
	[StructLayout(LayoutKind.Sequential, Pack = 1)]
	public struct StealthII_FixedHeader
	{
		/// <summary>
		/// LoFlags
		/// </summary>
		public byte byLoFlags;
		/// <summary>
		/// Algorithm code
		/// </summary>
		public byte byAlgorithmCode;
		/// <summary>
		/// GP counter
		/// </summary>
		public uint dwGP_Counter;
		/// <summary>
		/// Size of algorithm's key
		/// </summary>
		public byte byKeyLength;
		/// <summary>
		/// Request Block length
		/// </summary>
		public byte byBlockLength;
	}


	/// <summary>
	/// StealthIII_FixedHeader
	/// </summary>
	[StructLayout(LayoutKind.Sequential, Pack = 1)]
	public struct StealthIII_FixedHeader
	{
		/// <summary>
		/// LoFlags
		/// </summary>
		public byte byLoFlags;
		/// <summary>
		/// HiFlags
		/// </summary>
		public ushort wHiFlags;
		/// <summary>
		/// Algorithm code
		/// </summary>
		public byte byAlgorithmCode;
		/// <summary>
		/// Size of algorithm's key
		/// </summary>
		public byte byKeyLength;
		/// <summary>
		/// Request Block length
		/// </summary>
		public byte byBlockLength;
	}


	/// <summary>
	/// LMS_FixedHeader
	/// </summary>
	[StructLayout(LayoutKind.Sequential, Pack = 1)]
	public unsafe struct LMS_FixedHeader
	{
		/// <summary>
		/// Signature of LMS Table
		/// </summary>
		public ushort wSignature;
		/// <summary>
		/// Table's version, defined constant
		/// </summary>
		public ushort wVersion;
		/// <summary>
		/// CheckSum GrdCRC()
		/// </summary>
		public uint dwChecksum;
		/// <summary>
		/// Flags of LMS Table
		/// </summary>
		public ushort wFlags;
		/// <summary>
		/// Number of records in LMS Table
		/// </summary>
		public byte byElements;
		/// <summary>
		/// 2 bytes are reserved for even
		/// </summary>
		public fixed byte rs_res[5];
	}

}
