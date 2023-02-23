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

	#region Enumerations

	/// <summary>
	/// Флаги, задающие режим поиска локальных или удаленных ключей
	/// </summary>
	[Flags]
	public enum GrdFMR : int
	{
		/// <summary>
		/// Локальный ключ
		/// </summary>
		Local = 0x0001,

		/// <summary>
		/// Удаленный ключ
		/// </summary>
		Remote = 0x0002,

		/// <summary>
		/// Локальный или удаленный ключ
		/// </summary>
		ALL = 0x0003
	}

	/// <summary>
	/// Идентификатор языка для функции GrdFormatMessage
	/// </summary>
	public enum GrdLNG : int
	{
		/// <summary>
		/// Английский язык
		/// </summary>
		English = 0,

		/// <summary>
		/// Русский язык
		/// </summary>
		Russian = 7
	}

	/// <summary>
	/// rs_algo. Тип защищенной ячейки
	/// </summary>
	public enum rs_algo : int
	{
		GSII64 = 5,
		HASH64 = 6,
		RND64 = 7,
		PI = 8,
		Sim64Encode = 10,
		Sim64Decode = 11,
		ECC160 = 12,
		AES128 = 13,
		LoadableCode = 14,
		SHA256 = 15,
		AES128Encode = 16,
		AES128Decode = 17
	}

	/// <summary>
	/// Содержит константу Grd.StartCRC для вычисления 'последовательного' CRC
	/// </summary>
	public enum Grd : int
	{
		/// <summary>
		/// Стартовое значение для 'последовательного' CRC
		/// </summary>
		StartCRC = -1
	}

	/// <summary>
	/// Демонстрационные коды
	/// </summary>
	public enum GrdDC : uint
	{
		/// <summary>
		/// Демо public code
		/// </summary>
		DEMONVK = 0x519175b7,

		/// <summary>
		/// Демо private read code
		/// </summary>
		DEMORDO = 0x51917645,

		/// <summary>
		/// Демо private write code
		/// </summary>
		DEMOPRF = 0x51917603,

		/// <summary>
		/// Демо private master code
		/// </summary>
		DEMOMST = 0x5191758c
	}

	/// <summary>
	/// Стандартный набор ошибок
	/// </summary>
	public enum GrdE : int
	{
		/// <summary>
		/// Операция выполнена успешно
		/// </summary>
		OK = 0,

		/// <summary>
		/// Не найден ключ, отвечающий заданным условиям поиска
		/// </summary>
		DongleNotFound = 1,

		/// <summary>
		/// Указанный адрес слишком велик
		/// </summary> 
		AddressTooBig = 3,

		/// <summary>
		/// Счетчик запусков GP исчерпан (значение равно нулю)
		/// </summary>
		GPis0 = 5,

		/// <summary>
		/// Неверная команда обращения к ключу
		/// </summary>
		InvalidCommand = 6,

		/// <summary>
		/// Ошибка верификации при записи в память ключа
		/// </summary>
		VerifyError = 8,

		/// <summary>
		/// Сетевой протокол не найден
		/// </summary>
		NetProtocolNotFound = 9,

		/// <summary>
		/// Сетевой ресурс ключа Guardant Net исчерпан
		/// </summary>
		NetResourceExhaust = 10,

		/// <summary>
		/// Потеряно соединение с сервером Guardant Net
		/// </summary>
		NetConnectionLost = 11,

		/// <summary>
		/// Сервер Guardant Net не найден
		/// </summary>
		NetDongleNotFound = 12,

		/// <summary>
		/// Ошибка распределения памяти сервера Guardant Net
		/// </summary>
		NetServerMemory = 13,

		/// <summary>
		/// Ошибка DPMI
		/// </summary>
		DPMI = 14,

		/// <summary>
		/// Внутренняя ошибка сервера Guardant Net
		/// </summary>
		Internal = 15,

		/// <summary>
		/// Сервер Guardant Net был перезагружен
		/// </summary>
		NetServerReloaded = 16,

		/// <summary>
		/// Данная команда не поддерживается данной 
		/// версией ключа (ключ старой версии)
		/// </summary>
		VersionTooOld = 17,

		/// <summary>
		/// Необходим драйвер Windows NT
		/// </summary>
		BadDriver = 18,

		/// <summary>
		/// Ошибка сетевого протокола
		/// </summary>
		NetProtocol = 19,

		/// <summary>
		/// Получен сетевой пакет недопустимого формата
		/// </summary>
		NetPacket = 20,

		/// <summary>
		/// Необходима регистрация на сервере Guardant Net
		/// </summary>
		NeedLogin = 21,

		/// <summary>
		/// Необходимо снять регистрацию на сервере Guardant
		/// </summary>
		NeedLogout = 22,

		/// <summary>
		/// Ключ Guardant Net занят другим приложением
		/// </summary>
		DongleLocked = 23,

		/// <summary>
		/// Драйвер не может захватить порт
		/// </summary>
		DriverBusy = 24,

		/// <summary>
		/// Ошибка CRC при обращении к ключу
		/// </summary>
		CRCError = 30,

		/// <summary>
		/// Ошибка CRC при чтении данных из ключа
		/// </summary>
		CRCErrorRead = 31,

		/// <summary>
		/// Ошибка CRC при записи данных в ключ
		/// </summary>
		CRCErrorWrite = 32,

		/// <summary>
		/// Выход за границу памяти ключа
		/// </summary>
		Overbound = 33,

		/// <summary>
		/// Аппаратный алгоритм с таким номером в ключе не найден
		/// </summary>
		AlgoNotFound = 34,

		/// <summary>
		/// Ошибка CRC аппаратного алгоритма
		/// </summary>
		CRCErrorFunc = 35,

		/// <summary>
		/// Все ключи перебраны
		/// </summary>
		AllDonglesFound = 36,

		/// <summary>
		/// Слишком старая версия Guardant API
		/// </summary>
		ProtocolNotSup = 37,

		/// <summary>
		/// Задан несуществующий метод взаимообратного преобразования
		/// </summary>
		InvalidCnvType = 38,

		/// <summary>
		/// Неизвестная ошибка при работе с алгоритмом/ячейкой,
		/// операция могла не завершиться
		/// </summary>
		UnknownError = 39,

		/// <summary>
		/// Неверный пароль доступа к защищенной ячейке
		/// </summary>
		AccessDenied = 40,

		/// <summary>
		/// Статус защищенной ячейки изменить нельзя
		/// </summary>
		StatusUnchangeable = 41,

		/// <summary>
		/// Для алгоритма/ячейки сервис не предусмотрен 
		/// </summary>
		NoService = 42,

		/// <summary>
		/// Алгоритм/ячейка находятся в состоянии Inactive,
		/// команда не выполнена
		/// </summary>
		InactiveItem = 43,

		/// <summary>
		/// Попытка выполнить операцию, которую не поддерживает
		/// текущая версия сервера Guardant Net
		/// </summary>
		DongleServerTooOld = 44,

		/// <summary>
		/// В данный момент ключ не может выполнять никаких операций
		/// </summary>
		DongleBusy = 45,

		/// <summary>
		/// Задано недопустимое значение одного из аргументов функции
		/// </summary>
		InvalidArg = 46,

		/// <summary>
		/// Ошибка распределения памяти
		/// </summary>
		MemoryAllocation = 47,

		/// <summary>
		/// Недопустимый хендл
		/// </summary>
		InvalidHandle = 48,

		/// <summary>
		/// Этот защищенный контейнер уже используется
		/// </summary>
		ContainerInUse = 49,

		/// <summary>
		/// Зарезервировано
		/// </summary>
		Reserved50 = 50,

		/// <summary>
		/// Зарезервировано
		/// </summary>
		Reserved51 = 51,

		/// <summary>
		/// Зарезервировано
		/// </summary>
		Reserved52 = 52,

		/// <summary>
		/// Нарушена целостность системных данных
		/// </summary>
		SystemDataCorrupted = 53,

		/// <summary>
		/// Вопрос для удаленного обновления не был сгенерирован
		/// </summary>
		NoQuestion = 54,

		/// <summary>
		/// Недопустимый формат данных для удаленного обновления
		/// </summary>
		InvalidData = 55,

		/// <summary>
		/// Вопрос для удаленного обновления уже сгенерирован
		/// </summary>
		QuestionOK = 56,

		/// <summary>
		/// Процедура записи при удаленном обновлении не завершена
		/// </summary>
		UpdateNotComplete = 57,

		/// <summary>
		/// Неверное значение хеша данных удаленного обновления
		/// </summary>
		InvalidHash = 58,

		/// <summary>
		/// Внутренняя ошибка
		/// </summary>
		GenInternal = 59,

		/// <summary>
		/// Данная копия Guardant API уже инициализирована
		/// </summary>
		AlreadyInitialized = 60,

		/// <summary>
		/// Ошибка часов реального времени
		/// </summary>
		RTC_Error = 61,

		/// <summary>
		/// Батарея разряжена
		/// </summary>
		BatteryError = 62,

		/// <summary>
		/// Повтор объектов/имен алгаритмов
		/// </summary>
		DuplicateNames = 63,

		/// <summary>
		/// Выход за границу AAT таблицы
		/// </summary>
		AATFormatError = 64,

		/// <summary>
		/// Ошибка генерирования ключа
		/// </summary>
		SessionKeyNtGen = 65,

		/// <summary>
		/// Неверный общий код доступа
		/// </summary>
		InvalidPublicKey = 66,

		/// <summary>
		/// Неверная цифровая подпись
		/// </summary>
		InvalidDigitalSign = 67,

		/// <summary>
		/// Ошибка генерирования сессионного ключа
		/// </summary>
		SessionKeyGenError = 68,

		/// <summary>
		/// Неверный сессионный ключ
		/// </summary>
		InvalidSessionKey = 69,

		/// <summary>
		/// Сессионный ключ устарел
		/// </summary>
		SessionKeyTooOld = 70,

		/// <summary>
		/// Требуется инициализация
		/// </summary>
		NeedInitialization = 71,

		/// <summary>
		/// Ошибка при работе с функционалом 
		/// UserFirmware ключа Guardant Code
		/// Error while operating with functional of "Guardant Code"
		/// [for GrdCodeLoad] Адрес точки входа некорректен.
		/// [for GrdCodeLoad] Ошибка при проверке загружаемого кода 
		/// (При проверке кода во время его загрузки обнаружены 
		/// запрещенные команды или обращение к недопустимым адресам).
		/// </summary>
		gcProhibitCode = 72,

		/// <summary>
		/// Пользовательская программа зависла или выполняется 
		/// слишком долго
		/// </summary>
		gcUserFirmwareTimeOut = 73,

		/// <summary>
		/// В дескрипторе выделен недостаточный размер 
		/// flash-памяти для пользовательского приложения
		/// </summary>
		gcFlashSizeFromDescriptorTooSmall = 74,

		/// <summary>
		/// Зарезервировано #75
		/// </summary>
		Reserved75 = 75,
		/// <summary>
		/// Зарезервировано #76
		/// </summary>
		Reserved76 = 76,
		/// <summary>
		/// Зарезервировано #77
		/// </summary>
		Reserved77 = 77,
		/// <summary>
		/// Зарезервировано #78
		/// </summary>
		Reserved78 = 78,
		/// <summary>
		/// Зарезервировано #79
		/// </summary>
		Reserved79 = 79,
		/// <summary>
		/// Размер определителя меньше размера структуры GrdLoadableCodeData
		/// </summary>
		gcIncorrectMask = 80,

		/// <summary>
		/// Некорректно задана область RAM-памяти в загружаемом дескрипторе
		/// </summary>
		gcRamOverboundInProtect = 81,

		/// <summary>
		/// Некорректно задана область FLASH-памяти 
		/// в загружаемом дескрипторе
		/// </summary>
		gcFlashOverboundInProtect = 82,

		/// <summary>
		/// Пересечение областей FLASH-памяти заданных 
		/// в нескольких дескрипторах
		/// </summary>
		gcIntersectionOfCodeAreasInProtect = 83,

		/// <summary>
		/// Слишком длинный BMAP файл
		/// </summary>
		gcBmapFileTooBig = 84,

		/// <summary>
		/// Загрузка программы нулевого размера
		/// </summary>
		gcZeroLengthProgram = 85,

		/// <summary>
		/// Ошибка при проверке данных
		/// </summary>
		gcDataCorrupt = 86,

		/// <summary>
		/// Ошибка протокола при выполнении
		/// </summary>
		gcProtocolError = 87,

		/// <summary>
		/// Нет загруженной программы пользователя
		/// </summary>
		gcGCEXENotFound = 88,

		/// <summary>
		/// Объявленный в дескрипторе буфер ввода/вывода недостаточен
		/// для передачи/приема данных программе пользователя
		/// </summary>
		gcNotEnoughRAM = 89,

		/// <summary>
		/// При выполнении кода произошло нарушение защиты виртуальной среды
		/// </summary>
		gcException = 90,

		/// <summary>
		/// Буфер ввода/вывода, заданный в программе пользователя,
		/// выходит за допустимую область памяти
		/// </summary>
		gcRamOverboundInCodeLoad = 91,

		/// <summary>
		/// Выход за пределы допустимой области FLASH-памяти
		/// </summary>
		gcFlashOverboundInCodeLoad = 92,

		/// <summary>
		/// Адресное пространство загружаемой программы пользователя 
		/// пересекается с уже загруженной (требуется операция Init)
		/// </summary>
		gcIntersectionOfCodeAreasInCodeLoad = 93,

		/// <summary>
		/// Некорректный формат файла GCEXE
		/// </summary>
		gcGCEXEFormatError = 94,

		/// <summary>
		/// Incorrect RAM area specified in loadable code for GcaCodeRun
		/// </summary>
		gcRamAccessViolation = 95,

		/// <summary>
		/// Too many nested calls of GcaCodeRun.
		/// </summary>
		gcCallDepthOverflow = 96,

		/// <summary>
		/// Unable to create ini file during network API initialization
		/// </summary>
		UnableToCreateIniFile =97,
		/// <summary>
		/// Неизвестная ошибка
		/// </summary>
		LastError = 98,

		/// <summary>
		/// Not found function
		/// </summary>
		NotFoundFunction = 0x70000000,

		/// <summary>
		/// Not found dll
		/// </summary>
		NotFoundDLL = 0x70000001,

		/// <summary>
		/// Неизвестная ошибка
		/// </summary>
		ManageError = 0x70000002
	}

	/// <summary>
	/// Флаги, задающие режим создания контейнера для функции <b>GrdCreateHandle</b>
	/// </summary>
	public enum GrdCHM : int
	{
		/// <summary>
		/// Контейнер создается для работы в монопольном режиме
		/// </summary>
		SingleThread = 0x00000000,
		/// <summary>
		/// Контейнер может быть использован для одновременной работы с ним из нескольких потоков
		/// </summary>
		MultiThread = 0x00000001
	}

	/// <summary>
	/// Флаги, используемые в функции GrdSetFindMode, разрешающие использовать при поиске параметры, записанные в обязательных полях ключа
	/// </summary>
	[Flags]
	public enum GrdFM : int
	{
		/// <summary>
		/// Все параметры поиска игнорируются
		/// </summary>
		ALL = 0,

		/// <summary>
		/// Учитывать при поиске "Номер программы"
		/// </summary>
		NProg = 0x0001,

		/// <summary>
		/// Учитывать при поиске "ID"
		/// </summary>
		ID = 0x0002,

		/// <summary>
		/// Учитывать при поиске "SN"
		/// </summary>
		SN = 0x0004,

		/// <summary>
		/// Учитывать при поиске "Версию"
		/// </summary>     
		Ver = 0x0008,

		/// <summary>
		/// Учитывать при поиске "Маску"
		/// </summary>
		Mask = 0x0010,

		/// <summary>
		/// Учитывать при поиске "Тип"    
		/// </summary>
		Type = 0x0020
	}

	/// <summary>
	/// Модели ключей
	/// </summary>
	public enum GrdDM : int
	{

		/// <summary>
		/// Guardant Stealth LPT
		/// </summary>
		GS1L = 0,

		/// <summary>
		/// Guardant Stealth USB 
		/// </summary>
		GS1U = 1,

		/// <summary>
		/// Guardant Fidus LPT
		/// </summary>
		GF1L = 2,

		/// <summary>
		/// Guardant Stealth II LPT 
		/// </summary>
		GS2L = 3,

		/// <summary>
		/// Guardant Stealth II USB
		/// </summary>
		GS2U = 4,

		/// <summary>
		/// Guardant Stealth III USB
		/// </summary>
		GS3U = 5,

		/// <summary>
		/// Guardant Fidus USB
		/// </summary>
		GF1U = 6,

		/// <summary>
		/// Guardant StealthIII Sign/Time USB
		/// </summary>
		GS3SU = 7,

		/// <summary>
		/// Guardant Code USB
		/// </summary>
		GCU = 8,

		/// <summary>
		/// Guardant SP SOFTWARE
		/// </summary>
		GSP = 9,

		/// <summary>
		/// Guardant Code Pro USB
		/// </summary>
		GCPU = 10,

		/// <summary>
		/// Общее количество моделей ключей
		/// </summary>
		Total = 11,

		/// <summary>
		/// Guardant Stealth         LPT
		/// </summary>
		Stealth1LPT = GS1L,

		/// <summary>
		/// Guardant Stealth         USB
		/// </summary>
		Stealth1USB = GS1U,

		/// <summary>
		/// Guardant Fidus           LPT
		/// </summary>
		FidusLPT = GF1L,

		/// <summary>
		/// Guardant Stealth II      LPT
		/// </summary>
		Stealth2LPT = GS2L,

		/// <summary>
		/// Guardant Stealth II      USB
		/// </summary>
		Stealth2USB = GS2U,

		/// <summary>
		/// Guardant Stealth III     USB
		/// </summary>
		Stealth3USB = GS3U,

		/// <summary>
		/// Guardant Fidus           USB
		/// </summary>
		FidusUSB = GF1U,

		/// <summary>
		/// Guardant Stealth III Sign/Time USB
		/// </summary>
		SignUSB = GS3SU,

		/// <summary>
		/// Guardant Sign VIRTUAL
		/// </summary>
		Soft = GSP,

		/// <summary>
		/// Guardant Code USB
		/// </summary>
		CodeUSB = GCU,

		/// <summary>
		/// Guardant Code Pro USB
		/// </summary>
		CodeProUSB = GCPU
	}

	/// <summary>
	/// Способ доступа
	/// </summary>
	public enum GrdDR : int
	{
		/// <summary>
		/// Guardant driver
		/// </summary>
		GRD_DRV = 0,

		/// <summary>
		/// HID driver
		/// </summary>
        USB_HID = 1,

        /// <summary>
        /// WINUSB driver
        /// </summary>
        WINUSB  = 2
	}

	/// <summary>
	/// Интерфейсы ключей
	/// </summary>
	[Flags]
	public enum GrdDI : int
	{
		/// <summary>
		/// LPT порт
		/// </summary>
		LPT = 0,

		/// <summary>
		/// USB порт
		/// </summary>
		USB = 1,

		/// <summary>
		/// Virtual bus
		/// </summary>
		VIRTUAL = 2
	}

	/// <summary>
	/// Типы ключей
	/// </summary>
	[Flags]
	public enum GrdDT : int
	{
		/// <summary>
		/// Любой ключ
		/// </summary>
		ALL = 0x0000,

		/// <summary>
		/// Ключ поддерживает защиту приложений, созданных для работы в локальных сетях
		/// </summary>
		LAN = 0x0001,

		/// <summary>
		/// Ключ имеет возможность ограничивать время работы защищенного приложения
		/// </summary>
		Time = 0x0002,

		/// <summary>
		/// Ключ содержит алгоритм GSII64: ключи Guardant Stealth II / Net II, Stealth III / Net III
		/// </summary>
		GSII64 = 0x0008,

		/// <summary>
		/// Ключ поддерживает технологию защищенных ячеек Guardant Stealth III / Net III
		/// </summary>
		PI = 0x0010,

		/// <summary>
		/// Ключ поддерживает технологию защищенного удаленного программирования Trusted Remote Update Guardant Stealth III / Net III
		/// </summary>
		TRU = 0x0020,

		/// <summary>
		/// Ключ с часами реального времени
		/// </summary>
		RTC = 0x0040,

		/// <summary>
		/// Ключ поддерживает аппаратно алгоритм AES
		/// </summary>
		AES = 0x0080,

		/// <summary>
		/// Ключ поддерживает аппаратно алгоритм цифровой подписи на эллиптических кривых (ECC160)
		/// </summary>
		ECC = 0x0100,

		/// <summary>
		/// Ключ поддерживает пользовательские подпрограммы в ключе
		/// </summary>
		UserFirmware = 0x0400, // Support of User Firmware

		/// <summary>
		/// Зарезервировано
		/// </summary>
		Reserved = 0x0800,  //  Reserved for 'feature' functionality

		/// <summary>
		/// Ключ содержит извлекаемую MicroSD карту
		/// </summary>
		MSCRemovable = 0x1000  //  MSC removable. USB-connector with uSD.
	}

	/// <summary>
	/// Флаги, задающие список возможных моделей ключа, участвующих в поиске
	/// </summary>
	[Flags]
	public enum GrdFMM : int
	{
		/// <summary>
		/// Guardant Stealth LPT
		/// </summary>
		GS1L = (1 << (int)GrdDM.GS1L),

		/// <summary>
		/// Guardant Stealth USB
		/// </summary>
		GS1U = (1 << (int)GrdDM.GS1U),

		/// <summary>
		/// Guardant Fidus LPT
		/// </summary>
		GF1L = (1 << (int)GrdDM.GF1L),

		/// <summary>
		/// Guardant Stealth II LPT
		/// </summary>
		GS2L = (1 << (int)GrdDM.GS2L),

		/// <summary>
		/// Guardant Stealth II USB
		/// </summary>
		GS2U = (1 << (int)GrdDM.GS2U),

		/// <summary>
		/// Guardant Stealth III USB
		/// </summary>
		GS3U = (1 << (int)GrdDM.GS3U),

		/// <summary>
		/// Guardant Fidus USB
		/// </summary>
		GF1U = (1 << (int)GrdDM.GF1U),

		/// <summary>
		/// Guardant Sign/Time
		/// </summary>
		GS3SU = (1 << (int)GrdDM.GS3SU),

		/// <summary>
		/// Guardant Code USB
		/// </summary>
		GCU = (1 << (int)GrdDM.GCU),

		/// <summary>
		/// Guardant Code Pro USB
		/// </summary>
		GCPU = (1 << (int)GrdDM.GCPU),

		/// <summary>
		/// Guardant SP SOFTWARE
		/// </summary>
		GSP = (1 << (int)GrdDM.GSP),

		/// <summary>
		/// Guardant Stealth I of any interface
		/// </summary>
		GS1 = (GS1L | GS1U),

		/// <summary>
		/// Guardant Fidus of any interface
		/// </summary>
		GF = (GF1L | GF1U),

		/// <summary>
		/// Guardant Stealth II of any interface
		/// </summary>
		GS2 = (GS2L | GS2U),

		/// <summary>
		/// Guardant Stealth III of any interface
		/// </summary>
		GS3 = (GS3U),

		/// <summary>
		/// Guardant  Sign/Time of any interface
		/// </summary>
		GS3S = (GS3SU),

		/// <summary>
		/// Guardant Code с любым интерфейсом
		/// </summary>
		GC = (GrdFMM.GCU),

		/// <summary>
		/// Guardant Code Pro с любым интерфейсом
		/// </summary>
		GCP = (GrdFMM.GCPU),

		/// <summary>
		/// Любой ключ из семейства Guardant Stealth или Fidus
		/// </summary>
		ALL = 0,

		/// <summary>
		// SetFindMode dongle mode search bits
		/// </summary>

		/// <summary>
		/// Guardant Stealth LPT
		/// </summary>
		Stealth1LPT = (1 << (int)GrdDM.Stealth1LPT),

		/// <summary>
		/// Guardant Stealth USB
		/// </summary>
		Stealth1USB = (1 << (int)GrdDM.Stealth1USB),

		/// <summary>
		/// Guardant Fidus LPT
		/// </summary>
		FidusLPT = (1 << (int)GrdDM.FidusLPT),

		/// <summary>
		/// Guardant StealthII LPT
		/// </summary>
		Stealth2LPT = (1 << (int)GrdDM.Stealth2LPT),

		/// <summary>
		/// Guardant StealthII USB
		/// </summary>
		Stealth2USB = (1 << (int)GrdDM.Stealth2USB),

		/// <summary>
		/// Guardant StealthIII USB
		/// </summary>
		Stealth3USB = (1 << (int)GrdDM.Stealth3USB),

		/// <summary>
		/// Guardant Fidus USB
		/// </summary>
		FidusUSB = (1 << (int)GrdDM.FidusUSB),

		/// <summary>
		/// Guardant Sign/Time USB
		/// </summary>
		SignUSB = (1 << (int)GrdDM.SignUSB),


		/// <summary>
		/// Guardant SP SOFTWARE
		/// </summary>
		Soft = (1 << (int)GrdDM.Soft),

		/// <summary>
		/// Guardant Code USB
		/// </summary>
		CodeUSB = (1 << (int)GrdDM.CodeUSB),

		/// <summary>
		/// Guardant Code Pro USB
		/// </summary>
		CodeProUSB = (1 << (int)GrdDM.CodeProUSB),

		/// <summary>
		/// Guardant Stealth I   of any interface
		/// </summary>
		Stealth1 = (GrdFMM.Stealth1LPT | GrdFMM.Stealth1USB),

		/// <summary>
		/// Guardant Fidus I of any interface
		/// </summary>
		Fidus = (GrdFMM.FidusLPT | GrdFMM.FidusUSB),

		/// <summary>
		/// Guardant Stealth II of any interface
		/// </summary>
		Stealth2 = (GrdFMM.Stealth2LPT | GrdFMM.Stealth2USB),

		/// <summary>
		/// Guardant Stealth III of any interface
		/// </summary>
		Stealth3 = (GrdFMM.Stealth3USB),

		/// <summary>
		/// Guardant Stealth III Sign/Time of any interface
		/// </summary>
		Sign = (GrdFMM.SignUSB),

		/// <summary>
		/// Guardant Code of any interface
		/// </summary>
		Code = (GrdFMM.CodeUSB),

		/// <summary>
		/// Guardant Code Pro of any interface
		/// </summary>
		CodePro = (GrdFMM.CodeUSB)
	}

	/// <summary>
	/// Флаги, задающие список возможных интерфейсов ключей, участвующих в поиске
	/// </summary>
	[Flags]
	public enum GrdFMI : int
	{
		/// <summary>
		/// Любой из возможных для Guardant Stealth и Fidus интерфесов
		/// </summary>
		ALL = 0,

		/// <summary>
		/// LPT порт
		/// </summary>
		LPT = (1 << (int)GrdDI.LPT),

		/// <summary>
		/// USB порт 
		/// </summary>
		USB = (1 << (int)GrdDI.USB),

		/// <summary>
		/// Virtual bus
		/// </summary>
		VIRTUAL = (1 << (int)GrdDI.VIRTUAL)

	}

	/// <summary>
	/// Режим поиска для функции GrdFind
	/// </summary>
	public enum GrdF : int
	{
		/// <summary>
		/// Первый вызов
		/// </summary>
		First = 1,

		/// <summary>
		/// Все последующие вызовы
		/// </summary>
		Next = 0
	}

	/// <summary>
	/// Флаги для функций <b>GrdLogin</b>
	/// </summary>
	public enum GrdLM : int
	{
		/// <summary>
		/// Сетевые лицензии распределяются рабочим станциям, вне зависимости от количества запущенных на одной станции копий приложения
		/// </summary>
		PerStation = 0x00000000,

		/// <summary>
		/// Сетевые лицензии распределяются хэндлам. Каждый новый регистрируемый через GrdLogin хэндл получит отдельную лицензию, безотносительно того на одном или нескольких компьютерах они работают
		/// </summary>
		PerHandle = 0x00000001,


		/// <summary>
		/// Allocate Guardant Net license for each process (application copy)
		/// </summary>
		PerProcess = 0x00000002
	}

	/// <summary>
	/// Флаги для функций  <b>GrdLock</b>
	/// </summary>
	[Flags]
	public enum GrdLockMode : int
	{
		/// <summary>
		/// Блокируется только вызов GrdLock из другого потока, процесса или даже компьютера (при работе в сети)
		/// </summary>
		Nothing = 0x00000000,

		/// <summary>
		/// Блокируются операции Init
		/// </summary>
		Init = 0x00000001,

		/// <summary>
		/// Блокируются операции Protect
		/// </summary>
		Protect = 0x00000002,

		/// <summary>
		/// Блокируются операции Transform
		/// </summary>
		Transform = 0x00000004,

		/// <summary>
		/// Блокируются операции Read
		/// </summary>
		Read = 0x00000008,

		/// <summary>
		/// Блокируются операции Write
		/// </summary>
		Write = 0x00000010,

		/// <summary>
		/// Блокируются операции Activate
		/// </summary>
		Activate = 0x00000020,

		/// <summary>
		/// Блокируются операции Deactivate
		/// </summary>
		Deactivate = 0x00000040,

		/// <summary>
		/// Блокируются операции ReadItem
		/// </summary>
		ReadItem = 0x00000080,

		/// <summary>
		/// Блокируются операции UpdateItem
		/// </summary>
		UpdateItem = 0x00000100,

		/// <summary>
		/// Блокируются все вышеперечисленные операции
		/// </summary>
		All = unchecked((int)0xFFFFFFFF)
	}

	/// <summary>
	/// Режим работы, задается в функции GrdSetWorkMode
	/// </summary>
	public enum GrdWM : int
	{
		/// <summary>
		/// Задает режим адресации UAM (User Address Mode)в операциях чтения/записи. Режим по умолчанию
		/// </summary>
		UAM = 0x0000,

		/// <summary>
		/// Задает режим адресации SAM (System Address Mode) в операциях чтения/записи (по умолчанию - режим UAM)
		/// </summary>
		SAM = 0x0080
	}

	/// <summary>
	/// Guardant Stealth III: Размер вопроса к алгоритмам и защищенным ячейкам по умолчанию
	/// </summary>
	public enum GrdARS : int
	{
		/// <summary>
		/// GSII64 для автоматической защиты + используется в API
		/// </summary>
		GSII64 = 8,

		/// <summary>
		/// HASH64 для автоматической защиты + используется в API
		/// </summary>
		HASH64 = 8,

		/// <summary>
		/// RAND64 для автоматической защиты + используется в API
		/// </summary>
		RAND64 = 8,

		/// <summary>
		/// Защищенная ячейка, только для считывания. Может быть обновлена с помощью Secured  Guardant Remote Update
		/// </summary>
		READ_ONLY = 8,

		/// <summary>
		/// Защищенная ячейка, для считывания и записи. Может быть обновлена с помощью protected application runtime
		/// </summary>
		READ_WRITE = 8,

		/// <summary>
		/// GSII64 демо алгоритм, используется в примерах
		/// </summary>
		GSII64_DEMO = 8,

		/// <summary>
		/// HASH64 демо алгоритм, используется в примерах
		/// </summary>
		HASH64_DEMO = 8,

		/// <summary>
		/// Алгоритм для Guardant StealthIII Sign/Time USB
		/// </summary>
		ECC160 = 20,

		/// <summary>
		/// Алгоритм для Guardant StealthIII Sign/Time USB
		/// </summary>
		AES128 = 16,

		/// <summary>
		/// Алгоритм SHA256 для Guardant StealthIII Sign/Time/Code USB 
		/// </summary>
		HASH_SHA256 = 0
	}

	/// <summary>
	/// Guardant Stealth III: размеры определителей алгоритмов и защищенных ячеек по умолчанию
	/// </summary>
	public enum GrdADS : int
	{
		/// <summary>
		/// GSII64 для автоматической защиты + используется в API
		/// </summary>
		GSII64 = 16,

		/// <summary>
		/// HASH64 для автоматической защиты + используется в API
		/// </summary>
		HASH64 = 16,

		/// <summary>
		/// RAND64 для автоматической защиты + используется в API
		/// </summary>
		RAND64 = 16,

		/// <summary>
		/// Защищенная ячейка, только для считывания. Может быть обновлена с помощью Secured  Guardant Remote Update
		/// </summary>
		READ_ONLY = 8,

		/// <summary>
		/// Защищенная ячейка, для считывания и записи. Может быть обновлена с помощью protected application runtime
		/// </summary>
		READ_WRITE = 8,

		/// <summary>
		/// GSII64 демо алгоритм, используется в примерах
		/// </summary>
		GSII64_DEMO = 16,

		/// <summary>
		/// HASH64 демо алгоритм, используется в примерах
		/// </summary>
		HASH64_DEMO = 16,

		/// <summary>
		/// Алгоритм для Guardant StealthIII Sign/Time USB
		/// </summary>
		ECC160 = 20,

		/// <summary>
		/// Алгоритм для Guardant StealthIII Sign/Time USB
		/// </summary>
		AES128 = 16
	}





	/// <summary>
	/// Методы обновления защищенных ячеек
	/// </summary>
	public enum GrdUM : int
	{
		/// <summary>
		/// Данные из буфера <b>Data </b>заменяют старые данные 
		/// </summary>
		MOV = 0,

		/// <summary>
		/// Данные из буфера <b>Data </b>складываются со старыми данными по модулю 2 
		/// </summary>
		XOR = 1
	}

	/// <summary>
	/// Алгоритмы ключей Guardant Stealth
	/// </summary>
	public enum GrdAT : int
	{
		/// <summary>
		/// Базовый метод
		/// </summary>
		Algo0 = 0,

		/// <summary>
		/// Символьный метод
		/// </summary>
		AlgoASCII = 1,

		/// <summary>
		/// Файловый метод
		/// </summary>
		AlgoFile = 2
	}

	/// <summary>
	/// Режим работы GrdTRU
	/// </summary>
	public enum GrdTRU : int
	{
		/// <summary>
		/// Выполнять операцию Init перед обновлением памяти ключа.
		/// </summary>
		Flags_Init = 1,
		/// <summary>
		/// Выполнять операцию Protect после обновления памяти ключа.
		/// </summary>
		Flags_Protect = 2,
		/// <summary>
		/// Шифрование на базе GSII64 ( 8 байт), хеш на базе GSII64 (8 байт).
		/// </summary>
		CryptMode_GSII64 = 0,
		/// <summary>
		/// Шифрование на базе AES128(16 байт), хеш на базе SHA256(32 байт).
		/// </summary>
		CryptMode_AES128SHA256 = 1
	}


	public enum GrdGF : int
	{
		/// <summary>
		/// Блокировка вызова функции GrdSetTime. Автоматически выставляется при программировании ключа из GrdUtil. 
		/// Если данный флаг был выставлен, то изменить время микросхемы таймера невозможно без перезаписи маски.
		/// </summary>
		ProtectTime = 1,
		/// <summary>
		/// Ключ работает в HID-режиме.
		/// </summary>
		HID = 2,
		/// <summary>
		/// Единственный сессионный ключ для Guardant API. При установленном флаге будет работоспособна 
		/// только одна копия приложения, защищенного Guardant API.
		/// </summary>
		OnlyOneSessKey = 4,
		/// <summary>
		/// Единственный сессионный ключ для автозащиты. При установленном флаге будет работоспособна
		/// только одна копия приложения, накрытого автозащитой.
		/// </summary>
        SecondSessKey = 8,
        /// <summary>
        /// Ключ работает в режиме WinUsb (если установлен, то флаг HID игнорируется)
        /// </summary>
        WINUSB_FLG = 16
	}


	/// <summary>
	/// rs_LoFlags Младший байт флагов защищенной ячейки.
	/// </summary>
	public enum rs_LoFlags : int
	{
		/// <summary>
		/// // Not used in S/N III
		/// </summary>
		nsafl_ID = 1,
		/// <summary>
		/// Decrement counter
		/// </summary>
		nsafl_GP_dec = 2,
		/// <summary>
		/// Not used in S/N III
		/// </summary>
		nsafl_GP = 4,
		/// <summary>
		/// Must be enabled in case S/N III 
		/// </summary>
		nsafl_ST_III = 8,
		/// <summary>
		/// Enable activation service
		/// </summary>
		nsafl_ActivationSrv = 16,
		/// <summary>
		/// Enable deactivation service
		/// </summary>
		nsafl_DeactivationSrv = 32,
		/// <summary>
		/// Enable update-by-psswd service
		/// </summary>
		nsafl_UpdateSrv = 64,
		/// <summary>
		/// Deactivated item
		/// </summary>
		nsafl_InactiveFlag = 128
	}

	/// <summary>
	/// rs_HiFlags. Дополнительные флаги свойств защищенной ячейки
	/// </summary>
	public enum rs_HiFlags : int
	{
		/// <summary>
		// Enable read-service of rs_K[]
		nsafh_ReadSrv = 1,
		/// <summary>
		/// Check password for read-service
		/// </summary>
		nsafh_ReadPwd = 2,
		/// <summary>
		/// Use BirthTime mechanism
		/// </summary>
		nsafh_BirthTime = 4,
		/// <summary>
		/// Use DeadTime mechanism
		/// </summary>
		nsafh_DeadTime = 8,
		/// <summary>
		/// Use LifeTime mechanism
		/// </summary>
		nsafh_LifeTime = 16,
		/// <summary>
		/// Use FlipTime mechanism
		/// </summary>
		nsafh_FlipTime = 32
	}




	/// <summary>
	/// Определяет размер дайджеста при вычислении HASH64
	/// </summary>
	public enum GrdHASH64 : int
	{
		/// <summary>
		/// Размер дайджеста
		/// </summary>
		DIGEST_SIZE = 8,
		/// <summary>
		/// Должно быть >= 0x200
		/// </summary>
		CONTEXT_SIZE = 0x200

	}

	/// <summary>
	/// Определяет размер дайджеста при вычислении CRC
	/// </summary>
	public enum GrdCRC32 : int
	{
		/// <summary>
		/// Размер дайджеста
		/// </summary>
		DIGEST_SIZE = 4,
		/// <summary>
		/// software CRC32 context size
		/// </summary>
		CONTEXT_SIZE = 4
	}

	/// <summary>
	/// Определяет размер дайджеста и контекста для алгоритма SHA256
	/// </summary>
	public enum GrdSHA256 : int
	{
		/// <summary>
		/// Размер дайджеста
		/// </summary>
		DIGEST_SIZE = 32,

		/// <summary>
		/// Должно быть >=sizeof(SHA256_CONTEXT)
		/// </summary>
		CONTEXT_SIZE = 0x200
	}

	/// <summary>
	/// Определяет размер ключа и блока данных для алгоритма AES256
	/// </summary>
	public enum GrdAES256 : int
	{
		/// <summary>
		/// Размер ключа шифрования
		/// </summary>
		KEY_SIZE = 32,
		/// <summary>
		/// Размер шифруемого блока данных
		/// </summary>
		BLOCK_SIZE = 16,
		/// <summary>
		/// Должно быть >0x4000)
		/// </summary>
		CONTEXT_SIZE = 0x4000

	}

	/// <summary>
	/// Определяет размер контекста для программного алгоритма AES
	/// </summary>
	public enum GrdAES : int
	{
		/// <summary>
		/// Должно быть >= sizeof(AES_CONTEXT)
		/// </summary>
		CONTEXT_SIZE = 0x4000
	}

	/// <summary>
	/// Типы алгоритмов цифровой подписи (для использования в GrdVerifySign)
	/// </summary>
	public enum GrdVSC : int
	{
		/// <summary>
		/// Алгоритм цифровой подписи (для использования в<b> GrdVerifySign</b>) <b>Guardant StealthIII Sign/Time USB</b>
		/// </summary>
		ECC160 = 0
	}

	/// <summary>
	/// Константы проверки цифровой подписи (для использования в GrdVerifySign)
	/// </summary>
	public enum GrdECC160 : int
	{
		/// <summary>
		/// Размер публичного ключа <b>Guardant Sign/Time USB</b>
		/// </summary>
		PUBLIC_KEY_SIZE = 40,

		/// <summary>
		/// Размер закрытого ключа <b>Guardant Sign/Time USB</b>
		/// </summary>
		PRIVATE_KEY_SIZE = 20,

		/// <summary>
		/// Размер дайджеста <b> Guardant Sign/Time USB</b>
		/// </summary>
		DIGEST_SIZE = 40,

		/// <summary>
		/// Размер сообщения <b>Guardant Sign/Time USB</b>
		/// </summary>
		MESSAGE_SIZE = 20
	}

	/// <summary>
	/// Флаги часов реального времени
	/// </summary>
	public enum GrdDSF : int
	{
		/// <summary>
		/// Флаг ошибки часов реального времени
		/// </summary>
		GrdDSF_RTC_Quality = 1,
		/// <summary>
		/// Флаг разрядки батареи часов реального времени
		/// </summary>
		GrdDSF_RTC_Battery = 2
	}

	/// <summary>
	/// Значения GrdCodePublicData.bState
	/// </summary>
	public enum GrdCodeState : int
	{
		/// <summary>
		/// not loading
		/// </summary>
		CodeNotLoad = 0,
		/// <summary>
		/// loading now
		/// </summary>
		CodeStartLoad = 1,
		/// <summary>
		/// loaded now
		/// </summary>
		CodeOk = 2
	}


	/// <summary>
	/// Flags for GrdStartupEx()
	/// Remote client settings file path (path to gnclient.ini)
	/// </summary>
	public enum GrdRCS : int
	{
		/// <summary>
		/// User defined path,
		/// szNetworkClientIniPath parameter must contain full path to the filename or directory where the
		/// remote client settings file will be saved
		/// </summary>
		UserDefined = 0,
		/// <summary>
		/// User defined path relative to the ProgramData folder,
		/// szNetworkClientIniPath parameter must contain a relative path to the filename or directory where the
		/// remote client settings file will be saved
		/// </summary>
		ProgramData = unchecked((int)0x80000001),
		/// <summary>
		/// Detect path use environment variable,
		/// szNetworkClientIniPath parameter must contain the name of environment variable which points
		/// to the full pathname or directory where the remote client settings file will be savedloaded now
		/// </summary>
		GrdRCS_EnvVar = unchecked((int)0x80000002)
	}


	/// <summary>
	/// Значения параметра nGrdNotifyMessage callback функции 
	/// </summary>
	public enum GrdNotifyMessage : int
	{
		/// <summary>
		/// Обнаруженно подключение USB ключа
		/// </summary>
		DongleArrived = 0,
		/// <summary>
		/// Обнаруженно отключение USB ключа
		/// </summary>
		DongleRemoved = 1,
		/// <summary>
		/// Lost connection with the Guardant License Server
		/// </summary>
		ConnectionLost = 2,
		/// <summary>
		/// Restore connection with the Guardant License Server
		/// </summary>
		ConnectionRestore = 3
	}
	#endregion Enumerations
}
