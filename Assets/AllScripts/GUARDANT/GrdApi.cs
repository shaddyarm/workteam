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

#pragma warning disable 169

#pragma warning restore 169

	/// <summary>
	/// Структура, определяющая хендл не указателем, а 64-разрядным целым числом; ее использование в функциях вместо IntPtr позволяет избежать unsafe-кода
	/// </summary>
	public struct Handle
	{
		/// <summary>
		/// Создает новый хендл, в качестве параметра принимается его адрес; если адрес задан 0, 
		/// </summary>
		/// <param name="handle"></param>
		public Handle(IntPtr handle)
		{
			GrdHandle = handle;
		}

		// Объявил закрытый конструктор, чтобы компилятор автоматически не объявил открытый
		private IntPtr GrdHandle;

		/// <summary>
		/// Указатель, представляющий собой хендл ключа
		/// </summary>
		public IntPtr Address
		{
			get { return GrdHandle; }
		}

		public static bool operator ==(Handle handle, IntPtr Address)
		{
			return handle.Address == Address;
		}

		public static bool operator !=(Handle handle, IntPtr Address)
		{
			return handle.Address != Address;
		}

		public override int GetHashCode()
		{
			return base.GetHashCode();
		}

		public override bool Equals(object obj)
		{
			return base.Equals(obj);
		}
	}

/**
	@mainpage
	 <summary>
	 Интерфейс прикладного программирования "Guardant API .NET" - это набор функций, используемых прикладными программами на платформе .NET при выполнении операций с электронными ключами Guardant.
	 "Guardant API .NET" позволяет осуществить с ключами все действия, необходимые для создания системы защиты: 
	 <br/>
	 <list type="bullet">
	 <item><description>поиск и проверка наличия ключа с заданными параметрами</description></item>
	 <item><description>запись и считывание данных из памяти ключа</description></item>
	 <item><description>преобразование информации с помощью аппаратных алгоритмов</description></item>
	 <item><description>подсчет CRC</description></item>
	 <item><description>установка аппаратных запретов на чтение/запись памяти ключа и т. п. </description></item>
	 </list>
	 Для организации работы приложения с электронным ключом через "Guardant API .NET" нужно вставить вызовы функций API в исходные тексты программы, подключить к ней сборку Guardant.Api32ru.dll (а также обеспечить доступность библиотеки GrdAPI32.dll в рабочем каталоге программы) и организовать обработку ошибок. В результате защита будет интегрирована в тело программы.
	 <br/><br/>
	 <b>Замечание: </b>В целом "Guardant API .NET" повторяет функциональность Guardant API, но имеет некоторые особенности:
	 <br/>
	 <list type="bullet">
	 <item><description>Функции "Guardant API .NET" инкапсулируют в себе работу с указателями и позволяют разработчику избежать их использования и компиляции приложения с параметром /unsafe </description></item>
	 <item><description>Большинство функций имеют ряд перегруженных вариантов, для упрощения работы с наиболее распространенными типами данных, а также для использования параметров по умолчанию; поддерживается работа с управляемыми ссылочными типами данных (например, там где в native API требовалось выделить участок памяти и передать функции указатель на него, а также его длину, в данном API требуется всего лишь объявить массив и передать функции его имя)</description></item>
	 <item><description>Константы "Guardant API .NET" представлены в виде перечислений и классов, что упрощает работу с ними</description></item>
	 <item><description>Перечисления бывают двух типов: флаговые и обычные; при использовании флаговых перечислений необходимо обратить внимание на использование оператора ИЛИ (например, в native API для инициализации и работы с локальными и сетевыми ключами одновременно использовался бы синтаксис GrdStartup(GrdFMR_Remote + GrdFMR_Local), в данном API необходимо писать GrdStartup(GrdFMR.Remote|GrdFMR.Local)) </description></item>
	 </list>
	 </summary>
 */
	public sealed class GrdApi
	{
		private static string SystemErrorMsg = "";
		/// <summary>
		/// Имя dll назначается динамически 
		/// </summary>
		private static string GrdDllName = "";

		/// <summary>
		/// Делегат типа (int)(Handle GrdHandle, int nGrdNotifyMessage)
		/// </summary>
		public delegate int GrdDongleNotifyCallBackDelegate(IntPtr hAddress, GrdNotifyMessage notifyMessage);

		// список функций API Guradant API
		// инициализация в момент первого вызова
		private static PlatformInvoker GrdStartupInvoker;
		private static PlatformInvoker GrdStartupExInvoker;
		private static PlatformInvoker GrdCleanupInvoker;
		private static PlatformInvoker GrdCreateHandleInvoker;
		private static PlatformInvoker GrdCloseHandleInvoker;
		private static PlatformInvoker GrdIsValidHandleInvoker;
		private static PlatformInvoker GrdGetLastErrorInvoker;
		private static PlatformInvoker GrdFormatMessageInvoker;
		private static PlatformInvoker GrdSetAccessCodesInvoker;
		private static PlatformInvoker GrdSetFindModeInvoker;
		private static PlatformInvoker GrdFindInvoker;
		private static PlatformInvoker GrdLoginInvoker;
		private static PlatformInvoker GrdCheckInvoker;
		private static PlatformInvoker GrdLogoutInvoker;
		private static PlatformInvoker GrdGetInfoInvoker;
		private static PlatformInvoker GrdSetWorkModeInvoker;
		private static PlatformInvoker GrdDecGPInvoker;
		private static PlatformInvoker GrdSeekInvoker;
		private static PlatformInvoker GrdInitInvoker;
		private static PlatformInvoker GrdProtectInvoker;
		private static PlatformInvoker GrdReadInvoker;
		private static PlatformInvoker GrdWriteInvoker;
		private static PlatformInvoker GrdLockInvoker;
		private static PlatformInvoker GrdUnlockInvoker;
		private static PlatformInvoker GrdTransformInvoker;
		private static PlatformInvoker GrdCryptInvoker;
		private static PlatformInvoker GrdHashInvoker;
		private static PlatformInvoker GrdHashExInvoker;
		private static PlatformInvoker GrdCodeInitInvoker;
		private static PlatformInvoker GrdEnCodeInvoker;
		private static PlatformInvoker GrdDeCodeInvoker;
		private static PlatformInvoker GrdPI_ActivateInvoker;
		private static PlatformInvoker GrdPI_DeactivateInvoker;
		private static PlatformInvoker GrdPI_ReadInvoker;
		private static PlatformInvoker GrdPI_UpdateInvoker;

		private static PlatformInvoker GrdTRU_SetKeyInvoker;
		private static PlatformInvoker GrdTRU_GenerateQuestionInvoker;
		private static PlatformInvoker GrdTRU_GenerateQuestionExInvoker;
		private static PlatformInvoker GrdTRU_GenerateQuestionTimeInvoker;
		private static PlatformInvoker GrdTRU_GenerateQuestionTimeExInvoker;
		private static PlatformInvoker GrdTRU_DecryptQuestionInvoker;
		private static PlatformInvoker GrdTRU_DecryptQuestionExInvoker;
		private static PlatformInvoker GrdTRU_DecryptQuestionTimeInvoker;
		private static PlatformInvoker GrdTRU_DecryptQuestionTimeExInvoker;
		private static PlatformInvoker GrdTRU_SetAnswerPropertiesInvoker;
		private static PlatformInvoker GrdTRU_EncryptAnswerInvoker;
		private static PlatformInvoker GrdTRU_EncryptAnswerExInvoker;
		private static PlatformInvoker GrdTRU_ApplyAnswerInvoker;

		private static PlatformInvoker GrdSignInvoker;
		private static PlatformInvoker GrdVerifySignInvoker;
		private static PlatformInvoker GrdTransformExInvoker;
		private static PlatformInvoker GrdCryptExInvoker;
		private static PlatformInvoker GrdSetTimeInvoker;
		private static PlatformInvoker GrdGetTimeInvoker;
		private static PlatformInvoker GrdPI_GetTimeLimitInvoker;
		private static PlatformInvoker GrdPI_GetCounterInvoker;
		private static PlatformInvoker GrdMakeSystemTimeInvoker;
		private static PlatformInvoker GrdSplitSystemTimeInvoker;
		private static PlatformInvoker GrdCodeGetInfoInvoker;
		private static PlatformInvoker GrdCodeLoadInvoker;
		private static PlatformInvoker GrdCodeRunInvoker;
		private static PlatformInvoker GrdSetDriverModeInvoker;
		private static PlatformInvoker GrdCRCInvoker;
		private static PlatformInvoker GrdInitializeNotificationAPIInvoker;
		private static PlatformInvoker GrdRegisterDongleNotificationInvoker;
		private static PlatformInvoker GrdUnRegisterDongleNotificationInvoker;
		private static PlatformInvoker GrdUnInitializeNotificationAPIInvoker;

		private GrdApi()
		{
		}

		/// <summary>
		/// 
		/// </summary>
		/// <param name="pathToMobileSdk"></param>
		public static void SetPathToNativeLib(string pathToMobileSdk)
		{
			if (pathToMobileSdk.Length != 0)
			{
				char p = pathToMobileSdk[pathToMobileSdk.Length - 1];
				if (p != '\\' && p != '/')
					pathToMobileSdk += "\\";
			}

			if (IntPtr.Size == 8)
				GrdApi.GrdDllName = pathToMobileSdk + "GrdAPI64.dll";
			else
				GrdApi.GrdDllName = pathToMobileSdk + "GrdAPI32.dll";
		}

		/// <summary>
		/// 
		/// </summary>
		/// <param name="pathToNativeLib"></param>
		/// <param name="libname_x86"></param>
		/// <param name="libname_x64"></param>
		public static void SetPathToNativeLib(string pathToNativeLib, string libname_x86, string libname_x64)
		{
			if (pathToNativeLib.Length != 0)
			{
				char p = pathToNativeLib[pathToNativeLib.Length - 1];
				if (p != '\\' && p != '/')
					pathToNativeLib += "\\";
			}

			if (IntPtr.Size == 8)
				GrdApi.GrdDllName = pathToNativeLib + libname_x64;
			else
				GrdApi.GrdDllName = pathToNativeLib + libname_x86;
		}
		
		private static void SetApiPlatform()
		{
			if (GrdApi.GrdDllName == "")
			{
				if (IntPtr.Size == 8)
					GrdApi.GrdDllName = "GrdAPI64.dll";
				else
					GrdApi.GrdDllName = "GrdAPI32.dll";
			}
		}

		#region Функции инициализации и деинициализации Guardant API
		/// \defgroup init Функции инициализации и деинициализации Guardant API
		/// \{
		///

		/// <summary>
		/// Функция <b>GrdStartup</b> инициализирует данную копию "Guardant API".
		/// </summary>
		/// <param name="remoteMode"> комбинация флагов <see cref="GrdFMR"/>, задающих режим поиска локальных и/или удаленных ключей </param>
		/// <returns>Стандартный набор ошибок <seealso cref="GrdE"/> </returns>
		public static GrdE GrdStartup(GrdFMR remoteMode)
		{
			if (GrdApi.GrdStartupInvoker == null)
			{
				SetApiPlatform();
				Type[] parameters = { typeof(GrdFMR) };

				GrdApi.GrdStartupInvoker = new PlatformInvoker("GrdStartup", GrdDllName,
						typeof(GrdE), parameters, CallingConvention.Winapi, CharSet.Auto);
			}
			return (GrdE)GrdStartupInvoker.Invoke(remoteMode);
		}

		/// <summary>
		/// Функция <b>GrdStartupEx</b> инициализирует данную копию "Guardant API" и позволяет задать путь к файлу сетевых настроек клиента gnсlient.ini.
		/// </summary>
		/// <param name="remoteMode"> Комбинация флагов <see cref="GrdFMR"/>, задающих режим поиска локальных и/или удаленных ключей</param>
		/// <param name="networkClientIniPath"> Абсолютный путь, по которому должен располагаться gnclient.ini. Если параметр равен NULL, то приложение будет искать файл gnclient.ini в директории исполняемого файла, как при использовании GrdStartup.</param>
		/// <returns>Стандартный набор ошибок <seealso cref="GrdE"/> </returns>
		public static GrdE GrdStartupEx(GrdFMR remoteMode, string networkClientIniPath)
		{
			if (GrdApi.GrdStartupExInvoker == null)
			{
				SetApiPlatform();
				Type[] parameters = { typeof(GrdFMR), typeof(IntPtr), typeof(int) };

				GrdApi.GrdStartupExInvoker = new PlatformInvoker("GrdStartupEx", GrdDllName,
					typeof(GrdE), parameters, CallingConvention.Winapi, CharSet.Auto);
			}

			IntPtr pPathToFile = Marshal.StringToHGlobalAnsi(networkClientIniPath);
			GrdE nGrdE = (GrdE)GrdStartupExInvoker.Invoke(remoteMode, pPathToFile, 0);
			Marshal.FreeHGlobal(pPathToFile);
			return nGrdE;
		}


		/// <summary>
		/// Функция <b>GrdStartupEx</b> инициализирует данную копию "Guardant API" и позволяет задать путь к файлу сетевых настроек клиента gnсlient.ini.
		/// </summary>
		/// <param name="remoteMode"> Комбинация флагов <see cref="GrdFMR"/>, задающих режим поиска локальных и/или удаленных ключей</param>
		/// <param name="networkClientIniPath"> Абсолютный путь, по которому должен располагаться gnclient.ini. Если параметр равен NULL, то приложение будет искать файл gnclient.ini в директории исполняемого файла, как при использовании GrdStartup.</param>
		/// <param name="rcsMode"> Режим</param>
		/// <returns>Стандартный набор ошибок <seealso cref="GrdE"/> </returns>
		public static GrdE GrdStartupEx(GrdFMR remoteMode, string networkClientIniPath, GrdRCS rcsMode )
		{
			if (GrdApi.GrdStartupExInvoker == null)
			{
				SetApiPlatform();
				Type[] parameters = { typeof(GrdFMR), typeof(IntPtr), typeof(GrdRCS) };

				GrdApi.GrdStartupExInvoker = new PlatformInvoker("GrdStartupEx", GrdDllName,
					typeof(GrdE), parameters, CallingConvention.Winapi, CharSet.Auto);
			}

			IntPtr pPathToFile = Marshal.StringToHGlobalAnsi(networkClientIniPath);
			GrdE nGrdE = (GrdE)GrdStartupExInvoker.Invoke(remoteMode, pPathToFile, rcsMode);
			Marshal.FreeHGlobal(pPathToFile);
			return nGrdE;
		}

		/// <summary>
		/// Функция <b>GrdCleanup</b>
		/// </summary>
		/// <returns></returns>
		public static GrdE GrdCleanup()
		{
			if (GrdApi.GrdCleanupInvoker == null)
			{
				GrdApi.GrdCleanupInvoker = new PlatformInvoker("GrdCleanup", GrdDllName,
					typeof(GrdE), null, CallingConvention.Winapi, CharSet.Auto);
			}
			return (GrdE)GrdCleanupInvoker.Invoke();
		}

		/// <summary>
		/// Функция <b>GrdCreateHandle</b>
		/// </summary>
		/// <param name="Mode"></param>
		/// <returns></returns>
		public static Handle GrdCreateHandle(GrdCHM Mode)
		{
			if (GrdApi.GrdCreateHandleInvoker == null)
			{
                Type[] parameters = { typeof(IntPtr), typeof(GrdCHM), typeof(IntPtr) };

				GrdApi.GrdCreateHandleInvoker = new PlatformInvoker("GrdCreateHandle", GrdDllName,
					typeof(IntPtr), parameters, CallingConvention.Winapi, CharSet.Auto);
			}

			return new Handle((IntPtr)GrdApi.GrdCreateHandleInvoker.Invoke(IntPtr.Zero, Mode, IntPtr.Zero));
		}

		/// <summary>
		/// Функция <b>GrdCloseHandle</b>
		/// </summary>
		/// <param name="grdHandle"></param>
		/// <returns></returns>
		public static GrdE GrdCloseHandle(Handle grdHandle)
		{
			if (GrdApi.GrdCloseHandleInvoker == null)
			{
				Type[] parameters = { typeof(IntPtr) };

				GrdApi.GrdCloseHandleInvoker = new PlatformInvoker("GrdCloseHandle", GrdDllName,
					typeof(GrdE), parameters, CallingConvention.Winapi, CharSet.Auto);
			}

			return (GrdE)GrdCloseHandleInvoker.Invoke(grdHandle.Address);
		}

		/// <summary>
		/// Функция <b>GrdSetAccessCodes</b>
		/// </summary>
		/// <param name="grdHandle"></param>
		/// <param name="publicCode"></param>
		/// <returns></returns>
		public static GrdE GrdSetAccessCodes(Handle grdHandle, uint publicCode)
		{
			return GrdSetAccessCodes(grdHandle.Address, publicCode, 0, 0, 0);
		}

		/// <summary>
		/// Функция <b>GrdSetAccessCodes</b>
		/// </summary>
		/// <param name="grdHandle"></param>
		/// <param name="publicCode"></param>
		/// <param name="privateRD"></param>
		/// <returns></returns>
		public static GrdE GrdSetAccessCodes(Handle grdHandle, uint publicCode, uint privateRD)
		{
			return GrdSetAccessCodes(grdHandle.Address, publicCode, privateRD, 0, 0);
		}

		/// <summary>
		/// Функция <b>GrdSetAccessCodes</b>
		/// </summary>
		/// <param name="grdHandle"></param>
		/// <param name="publicCode"></param>
		/// <param name="privateRD"></param>
		/// <param name="privateWR"></param>
		/// <returns></returns>
		public static GrdE GrdSetAccessCodes(Handle grdHandle, uint publicCode, uint privateRD, uint privateWR)
		{
			return GrdSetAccessCodes(grdHandle.Address, publicCode, privateRD, privateWR, 0);
		}

		/// <summary>
		/// Функция <b>GrdSetAccessCodes</b>
		/// </summary>
		/// <param name="grdHandle"></param>
		/// <param name="publicCode"></param>
		/// <param name="privateRD"></param>
		/// <param name="privateWR"></param>
		/// <param name="privateMST"></param>
		/// <returns></returns>
		public static GrdE GrdSetAccessCodes(Handle grdHandle, uint publicCode, uint privateRD, uint privateWR, uint privateMST)
		{
			return GrdSetAccessCodes(grdHandle.Address, publicCode, privateRD, privateWR, privateMST);
		}

		private static GrdE GrdSetAccessCodes(IntPtr hAddress, uint publicCode, uint privateRD, uint privateWR, uint privateMST)
		{
			if (GrdApi.GrdSetAccessCodesInvoker == null)
			{
				Type[] parameters = { typeof(IntPtr), typeof(uint), typeof(uint), typeof(uint), typeof(uint) };

				GrdApi.GrdSetAccessCodesInvoker = new PlatformInvoker("GrdSetAccessCodes", GrdDllName,
					typeof(GrdE), parameters, CallingConvention.Winapi, CharSet.Auto);
			}

			return (GrdE)GrdSetAccessCodesInvoker.Invoke(hAddress, publicCode, privateRD, privateWR, privateMST);
		}

		/// <summary>
		/// Функция <b>GrdLogin</b>
		/// </summary>
		/// <param name="grdHandle"></param>
		/// <param name="loginFlags"></param>
		/// <returns></returns>
		public static GrdE GrdLogin(Handle grdHandle, GrdLM loginFlags)
		{
			return GrdLogin(grdHandle, -1, loginFlags);
		}

		/// <summary>
		/// Функция <b>GrdLogin</b>
		/// </summary>
		/// <param name="grdHandle"></param>
		/// <param name="moduleLMS"></param>
		/// <param name="loginFlags"></param>
		/// <returns></returns>
		public static GrdE GrdLogin(Handle grdHandle, int moduleLMS, GrdLM loginFlags)
		{
			if (GrdApi.GrdLoginInvoker == null)
			{
				Type[] parameters = { typeof(IntPtr), typeof(int), typeof(GrdLM) };

				GrdApi.GrdLoginInvoker = new PlatformInvoker("GrdLogin", GrdDllName,
					typeof(GrdE), parameters, CallingConvention.Winapi, CharSet.Auto);
			}

			return (GrdE)GrdLoginInvoker.Invoke(grdHandle.Address, moduleLMS, loginFlags);
		}

		/// <summary>
		/// Функция <b>GrdLogout</b>
		/// </summary>
		/// <param name="grdHandle"></param>
		/// <returns></returns>
		public static GrdE GrdLogout(Handle grdHandle)
		{
			return GrdLogout(grdHandle, 0);
		}

		/// <summary>
		/// Функция <b>GrdLogout</b>
		/// </summary>
		/// <param name="grdHandle"></param>
		/// <param name="mode"></param>
		/// <returns></returns>
		public static GrdE GrdLogout(Handle grdHandle, int mode)
		{
			if (GrdApi.GrdLogoutInvoker == null)
			{
				Type[] parameters = { typeof(IntPtr), typeof(int) };

				GrdApi.GrdLogoutInvoker = new PlatformInvoker("GrdLogout", GrdDllName,
					typeof(GrdE), parameters, CallingConvention.Winapi, CharSet.Auto);
			}
			return (GrdE)GrdLogoutInvoker.Invoke(grdHandle.Address, mode);
		}

		/// \}
		#endregion //Функции инициализации и деинициализации Guardant API

		#region Функции Guardant API общего назначения
		/// \defgroup union Функции Guardant API общего назначения
		/// \{

		/// <summary>
		/// Функция <b>GrdFormatMessage</b>
		/// </summary>
		/// <param name="errorCode"></param>
		/// <param name="language"></param>
		/// <param name="errorMsg"></param>
		/// <returns></returns>
		public static GrdE GrdFormatMessage(GrdE errorCode, GrdLNG language, out string errorMsg)
		{
			return GrdFormatMessage(IntPtr.Zero, (int)errorCode, (int)language, out errorMsg);
		}

		/// <summary>
		/// Функция <b>GrdFormatMessage</b>
		/// </summary>
		/// <param name="grdHandle"></param>
		/// <param name="errorCode"></param>
		/// <param name="language"></param>
		/// <param name="errorMsg"></param>
		/// <returns></returns>
		public static GrdE GrdFormatMessage(Handle grdHandle, GrdE errorCode, GrdLNG language, out string errorMsg)
		{
			return GrdFormatMessage(grdHandle.Address, (int)errorCode, (int)language, out errorMsg);
		}

		private static unsafe GrdE GrdFormatMessage(IntPtr hAddress, int errorCode, int language, out string errorMsg)
		{
			if (GrdApi.GrdFormatMessageInvoker == null)
			{
				Type[] parameters = { typeof(IntPtr), typeof(int), typeof(int),
					typeof(IntPtr), typeof(int), typeof(IntPtr) };

				GrdApi.GrdFormatMessageInvoker = new PlatformInvoker(
#if NETFRAMEWORK
																	 "GrdFormatMessage"
#elif NETCOREAPP || NETSTANDARD
																	 "GrdFormatMessageUtf8"
#else
#error "Unknown .NET product: neither Framework, nor Core, nor Standard!"
																	 "GrdFormatMessageUtf8"
#endif
																	 , GrdDllName,
					typeof(GrdE), parameters, CallingConvention.Winapi, CharSet.Auto);
			}

			GrdE nGrdE;
			byte[] szErrorMsg = new byte[0x100];
			fixed (byte* pErrorMsg = &szErrorMsg[0])
				nGrdE = (GrdE)GrdFormatMessageInvoker.Invoke(hAddress, errorCode, language, new IntPtr(pErrorMsg), szErrorMsg.Length, IntPtr.Zero);

			if (nGrdE == GrdE.NotFoundDLL || nGrdE == GrdE.NotFoundFunction || nGrdE == GrdE.ManageError)
			{
				errorMsg = GrdApi.GrdFormatMessageInvoker.message;
				return GrdE.OK;
			}
			else
			{
				Encoding e = Encoding.GetEncoding(
#if NETFRAMEWORK // .NET Framework (any) => CP-1251 (for compatibility)
						1251
#elif NETCOREAPP || NETSTANDARD // .NET Core and .NET Standard => UTF-8
						65001
#else
#error "Unknown .NET product: neither Framework, nor Core, nor Standard!"
						0
#endif
					);
				String strError = e.GetString(szErrorMsg);
				errorMsg = strError.TrimEnd('\0');
			}
			return nGrdE;
		}

		/// <summary>
		/// Функция <b>GrdSetFindMode</b>
		/// </summary>
		/// <param name="grdHandle"></param>
		/// <param name="remoteMode"></param>
		/// <param name="flags"></param>
		/// <param name="prog"></param>
		/// <param name="id"></param>
		/// <param name="sn"></param>
		/// <param name="ver"></param>
		/// <param name="mask"></param>
		/// <param name="type"></param>
		/// <param name="models"></param>
		/// <param name="interfaces"></param>
		/// <returns></returns>
		/// 
		public static GrdE GrdSetFindMode(Handle grdHandle, GrdFMR remoteMode, GrdFM flags,
			uint prog, uint id, uint sn, uint ver, uint mask,
			GrdDT type, GrdFMM models, GrdFMI interfaces)
		{
			if (GrdApi.GrdSetFindModeInvoker == null)
			{
				Type[] parameters = { typeof(IntPtr), typeof(GrdFMR), typeof(GrdFM),
					typeof(uint), typeof(uint), typeof(uint), typeof(uint), typeof(uint),
					typeof(GrdDT), typeof(GrdFMM), typeof(GrdFMI) };

				GrdApi.GrdSetFindModeInvoker = new PlatformInvoker("GrdSetFindMode", GrdDllName,
					typeof(GrdE), parameters, CallingConvention.Winapi, CharSet.Auto);
			}
			return (GrdE)GrdSetFindModeInvoker.Invoke(grdHandle.Address, remoteMode, flags,
														prog, id, sn, ver, mask, type, models, interfaces);
		}

		/// <summary>
		/// Функция <b>GrdFind</b>
		/// </summary>
		/// <param name="grdHandle"></param>
		/// <param name="mode"></param>
		/// <param name="id"></param>
		/// <param name="findInfo"></param>
		/// <returns></returns>
		public static GrdE GrdFind(Handle grdHandle, GrdF mode, out uint id, out FindInfo findInfo)
		{
			return GrdFind(grdHandle.Address, (int)mode, out id, out findInfo);
		}

		private static unsafe GrdE GrdFind(IntPtr hAddress, int mode, out uint id, out FindInfo findInfo)
		{
			if (GrdApi.GrdFindInvoker == null)
			{
				Type[] parameters = { typeof(IntPtr), typeof(int), typeof(IntPtr), typeof(IntPtr) };

				GrdApi.GrdFindInvoker = new PlatformInvoker("GrdFind", GrdDllName,
					typeof(GrdE), parameters, CallingConvention.Winapi, CharSet.Auto);
			}

			fixed (uint* pID = &id)
			fixed (FindInfo* pFindInfo = &findInfo)
				return (GrdE)GrdFindInvoker.Invoke(hAddress, mode, new IntPtr(pID), new IntPtr(pFindInfo));
		}

		/// <summary>
		/// Функция <b>GrdLoсk</b>
		/// </summary>
		/// <param name="grdHandle"></param>
        /// <param name="timeoutWaitForUnlock"></param>
        /// <param name="timeoutAutoUnlock"></param>
		/// <param name="mode"></param>
		/// <returns></returns>
        public static GrdE GrdLock(Handle grdHandle, uint timeoutWaitForUnlock, uint timeoutAutoUnlock, GrdLockMode mode)
		{
			if (GrdApi.GrdLockInvoker == null)
			{
				Type[] parameters = { typeof(IntPtr), typeof(uint), typeof(uint), typeof(GrdLockMode) };

				GrdApi.GrdLockInvoker = new PlatformInvoker("GrdLock", GrdDllName,
					typeof(GrdE), parameters, CallingConvention.Winapi, CharSet.Auto);
			}

            return (GrdE)GrdLockInvoker.Invoke(grdHandle.Address, timeoutWaitForUnlock, timeoutAutoUnlock, mode);
		}

		/// <summary>
		/// Функция <b>GrdUnlock</b>
		/// </summary>
		/// <param name="grdHandle"></param>
		/// <returns></returns>
		public static GrdE GrdUnlock(Handle grdHandle)
		{
			if (GrdApi.GrdUnlockInvoker == null)
			{
				Type[] parameters = { typeof(IntPtr) };

				GrdApi.GrdUnlockInvoker = new PlatformInvoker("GrdUnlock", GrdDllName,
					typeof(GrdE), parameters, CallingConvention.Winapi, CharSet.Auto);
			}

			return (GrdE)GrdUnlockInvoker.Invoke(grdHandle.Address);
		}

		/// <summary>
		/// Функция <b>GrdGetLastError</b>
		/// </summary>
		/// <param name="grdHandle"></param>
		/// <returns></returns>
		public static GrdE GrdGetLastError(Handle grdHandle)
		{
			if (GrdApi.GrdGetLastErrorInvoker == null)
			{
				Type[] parameters = { typeof(IntPtr), typeof(IntPtr) };
				GrdApi.GrdGetLastErrorInvoker = new PlatformInvoker("GrdGetLastError", GrdDllName,
					typeof(GrdE), parameters, CallingConvention.Winapi, CharSet.Auto);
			}
			return (GrdE)GrdGetLastErrorInvoker.Invoke(grdHandle.Address, IntPtr.Zero);
		}

		/// <summary>
		/// Функция <b>GrdIsValidHandle</b>
		/// </summary>
		/// <param name="grdHandle"></param>
		/// <returns></returns>
		public static Boolean GrdIsValidHandle(Handle grdHandle)
		{
			if (GrdApi.GrdIsValidHandleInvoker == null)
			{
				Type[] parameters = { typeof(IntPtr) };

				GrdApi.GrdIsValidHandleInvoker = new PlatformInvoker("GrdIsValidHandle", GrdDllName,
					typeof(Boolean), parameters, CallingConvention.Winapi, CharSet.Auto);
			}

			return (Boolean)GrdApi.GrdIsValidHandleInvoker.Invoke(grdHandle.Address);
		}

		/// <summary>
		/// Функция <b>GrdCheck</b>
		/// </summary>
		/// <param name="grdHandle"></param>
		/// <returns></returns>
		public static GrdE GrdCheck(Handle grdHandle)
		{
			if (GrdApi.GrdCheckInvoker == null)
			{
				Type[] parameters = { typeof(IntPtr) };

				GrdApi.GrdCheckInvoker = new PlatformInvoker("GrdCheck", GrdDllName,
					typeof(GrdE), parameters, CallingConvention.Winapi, CharSet.Auto);
			}
			return (GrdE)GrdCheckInvoker.Invoke(grdHandle.Address);
		}

		/// <summary>
		/// Функция <b>GrdDecGP</b>
		/// </summary>
		/// <param name="grdHandle"></param>
		/// <returns></returns>
		public static GrdE GrdDecGP(Handle grdHandle)
		{
			if (GrdApi.GrdDecGPInvoker == null)
			{
				Type[] parameters = { typeof(IntPtr) };

				GrdApi.GrdDecGPInvoker = new PlatformInvoker("GrdDecGP", GrdDllName,
					typeof(GrdE), parameters, CallingConvention.Winapi, CharSet.Auto);
			}

			return (GrdE)GrdDecGPInvoker.Invoke(grdHandle.Address);
		}

		/// <summary>
		/// Функция <b>GrdSetWorkMode</b>
		/// </summary>
		/// <param name="grdHandle"></param>
		/// <param name="flagsWork"></param>
		/// <returns></returns>
		public static GrdE GrdSetWorkMode(Handle grdHandle, GrdWM flagsWork)
		{
			if (GrdApi.GrdSetWorkModeInvoker == null)
			{
				Type[] parameters = { typeof(IntPtr), typeof(GrdWM), typeof(int) };

				GrdApi.GrdSetWorkModeInvoker = new PlatformInvoker("GrdSetWorkMode", GrdDllName,
					typeof(GrdE), parameters, CallingConvention.Winapi, CharSet.Auto);
			}

			return (GrdE)GrdSetWorkModeInvoker.Invoke(grdHandle.Address, flagsWork, 0);
		}

		/// <summary>
		/// Функция <b>GrdSeek</b>
		/// </summary>
		/// <param name="grdHandle"></param>
		/// <param name="addr"></param>
		/// <returns></returns>
		public static GrdE GrdSeek(Handle grdHandle, uint addr)
		{
			if (GrdApi.GrdSeekInvoker == null)
			{
				Type[] parameters = { typeof(IntPtr), typeof(uint) };

				GrdApi.GrdSeekInvoker = new PlatformInvoker("GrdSeek", GrdDllName,
					typeof(GrdE), parameters, CallingConvention.Winapi, CharSet.Auto);
			}

			return (GrdE)GrdSeekInvoker.Invoke(grdHandle.Address, addr);
		}

		#region Функции записи данных в ключ
		/// \defgroup GRD_WRITE Функции записи данных в ключ
		/// \{

		/// Функция <b>GrdWrite</b>
		/// </summary>
		/// <param name="grdHandle"></param>
		/// <param name="addr"></param>
		/// <param name="data"></param>
		/// <returns></returns>
		public static GrdE GrdWrite(Handle grdHandle, GrdSAM addr, byte[] data)
		{
			GrdSetWorkMode(grdHandle, GrdWM.SAM);
			return GrdWrite(grdHandle.Address, addr.Value, data);
		}

		/// <summary>
		/// Функция <b>GrdWrite</b>
		/// </summary>
		/// <param name="grdHandle"></param>
		/// <param name="addr"></param>
		/// <param name="data"></param>
		/// <returns></returns>
		public static GrdE GrdWrite(Handle grdHandle, GrdUAM addr, byte[] data)
		{
			GrdSetWorkMode(grdHandle, GrdWM.UAM);
			return GrdWrite(grdHandle.Address, addr.Value, data);
		}

		/// <summary>
		/// Функция <b>GrdWrite</b>
		/// </summary>
		/// <param name="grdHandle"></param>
		/// <param name="addr"></param>
		/// <param name="data"></param>
		/// <returns></returns>
		public static GrdE GrdWrite(Handle grdHandle, GrdSAM addr, byte data)
		{
			byte[] _data = new byte[1];
			_data[0] = data;
			return GrdWrite(grdHandle, addr, _data);
		}

		/// <summary>
		/// Функция <b>GrdWrite</b>
		/// </summary>
		/// <param name="grdHandle"></param>
		/// <param name="addr"></param>
		/// <param name="data"></param>
		/// <returns></returns>
		public static GrdE GrdWrite(Handle grdHandle, GrdUAM addr, byte data)
		{
			byte[] _data = new byte[1];
			_data[0] = data;
			return GrdWrite(grdHandle, addr, _data);
		}

		/// <summary>
		/// Функция <b>GrdWrite</b>
		/// </summary>
		/// <param name="grdHandle"></param>
		/// <param name="addr"></param>
		/// <param name="data"></param>
		/// <returns></returns>
		public static GrdE GrdWrite(Handle grdHandle, GrdSAM addr, short data)
		{
			byte[] _data = BitConverter.GetBytes(data);
			return GrdWrite(grdHandle, addr, _data);
		}

		/// <summary>
		/// Функция <b>GrdWrite</b>
		/// </summary>
		/// <param name="grdHandle"></param>
		/// <param name="addr"></param>
		/// <param name="data"></param>
		/// <returns></returns>
		public static GrdE GrdWrite(Handle grdHandle, GrdUAM addr, short data)
		{
			byte[] _data = BitConverter.GetBytes(data);
			return GrdWrite(grdHandle, addr, _data);
		}

		/// <summary>
		/// Функция <b>GrdWrite</b>
		/// </summary>
		/// <param name="grdHandle"></param>
		/// <param name="addr"></param>
		/// <param name="data"></param>
		/// <returns></returns>
		public static GrdE GrdWrite(Handle grdHandle, GrdSAM addr, ushort data)
		{
			byte[] _data = BitConverter.GetBytes(data);
			return GrdWrite(grdHandle, addr, _data);
		}

		/// <summary>
		/// Функция <b>GrdWrite</b>
		/// </summary>
		/// <param name="grdHandle"></param>
		/// <param name="addr"></param>
		/// <param name="data"></param>
		/// <returns></returns>
		public static GrdE GrdWrite(Handle grdHandle, GrdUAM addr, ushort data)
		{
			byte[] _data = BitConverter.GetBytes(data);
			return GrdWrite(grdHandle, addr, _data);
		}

		/// <summary>
		/// Функция <b>GrdWrite</b>
		/// </summary>
		/// <param name="grdHandle"></param>
		/// <param name="addr"></param>
		/// <param name="data"></param>
		/// <returns></returns>
		public static GrdE GrdWrite(Handle grdHandle, GrdSAM addr, int data)
		{
			byte[] _data = BitConverter.GetBytes(data);
			return GrdWrite(grdHandle, addr, _data);
		}

		/// <summary>
		/// Функция <b>GrdWrite</b>
		/// </summary>
		/// <param name="grdHandle"></param>
		/// <param name="addr"></param>
		/// <param name="data"></param>
		/// <returns></returns>
		public static GrdE GrdWrite(Handle grdHandle, GrdUAM addr, int data)
		{
			byte[] _data = BitConverter.GetBytes(data);
			return GrdWrite(grdHandle,addr, _data);
		}

		/// <summary>
		/// Функция <b>GrdWrite</b>
		/// </summary>
		/// <param name="grdHandle"></param>
		/// <param name="addr"></param>
		/// <param name="data"></param>
		/// <returns></returns>
		public static GrdE GrdWrite(Handle grdHandle, GrdSAM addr, uint data)
		{
			byte[] _data = BitConverter.GetBytes(data);
			return GrdWrite(grdHandle, addr, _data);
		}

		/// <summary>
		/// Функция <b>GrdWrite</b>
		/// </summary>
		/// <param name="grdHandle"></param>
		/// <param name="addr"></param>
		/// <param name="data"></param>
		/// <returns></returns>
		public static GrdE GrdWrite(Handle grdHandle, GrdUAM addr, uint data)
		{
			byte[] _data = BitConverter.GetBytes(data);
			return GrdWrite(grdHandle, addr, _data);
		}

		/// <summary>
		/// Функция <b>GrdWrite</b>
		/// </summary>
		/// <param name="grdHandle"></param>
		/// <param name="addr"></param>
		/// <param name="data"></param>
		/// <returns></returns>
		public static GrdE GrdWrite(Handle grdHandle, GrdSAM addr, long data)
		{
			byte[] _data = BitConverter.GetBytes(data);
			return GrdWrite(grdHandle, addr, _data);
		}

		/// <summary>
		/// Функция <b>GrdWrite</b>
		/// </summary>
		/// <param name="grdHandle"></param>
		/// <param name="addr"></param>
		/// <param name="data"></param>
		/// <returns></returns>
		public static GrdE GrdWrite(Handle grdHandle, GrdUAM addr, long data)
		{
			byte[] _data = BitConverter.GetBytes(data);
			return GrdWrite(grdHandle, addr, _data);
		}

		/// <summary>
		/// Функция <b>GrdWrite</b>
		/// </summary>
		/// <param name="grdHandle"></param>
		/// <param name="addr"></param>
		/// <param name="data"></param>
		/// <returns></returns>
		public static GrdE GrdWrite(Handle grdHandle, GrdSAM addr, ulong data)
		{
			byte[] _data = BitConverter.GetBytes(data);
			return GrdWrite(grdHandle, addr, _data);
		}

		/// <summary>
		/// Функция <b>GrdWrite</b>
		/// </summary>
		/// <param name="grdHandle"></param>
		/// <param name="addr"></param>
		/// <param name="data"></param>
		/// <returns></returns>
		public static GrdE GrdWrite(Handle grdHandle, GrdUAM addr, ulong data)
		{
			byte[] _data = BitConverter.GetBytes(data);
			return GrdWrite(grdHandle, addr, _data);
		}

		private static unsafe GrdE GrdWrite(IntPtr hAddress, uint addr, byte[] data)
		{
			if (!CheckArg(data))
				return GrdE.InvalidArg;

			if (GrdApi.GrdWriteInvoker == null)
			{
				Type[] parameters = { typeof(IntPtr), typeof(uint), typeof(int), typeof(IntPtr), typeof(IntPtr) };

				GrdApi.GrdWriteInvoker = new PlatformInvoker("GrdWrite", GrdDllName,
					typeof(GrdE), parameters, CallingConvention.Winapi, CharSet.Auto);
			}
			fixed (byte* pData = &data[0])
				return (GrdE)GrdWriteInvoker.Invoke(hAddress, addr, data.Length, new IntPtr(pData), IntPtr.Zero);
		}

		/// \}
		#endregion // Функции записи данных в ключ

		#region Функции чтение данных из ключа
		/// \defgroup GRD_READ Функции чтение данных из ключа
		/// \{

		/// <summary>
		/// Функция <b>GrdRead</b>
		/// </summary>
		/// <param name="grdHandle"></param>
		/// <param name="addr"></param>
		/// <param name="data"></param>
		/// <returns></returns>
		public static GrdE GrdRead(Handle grdHandle, GrdSAM addr, byte[] data)
		{
			GrdE nGrdE = GrdSetWorkMode(grdHandle, GrdWM.SAM);
			if (nGrdE != GrdE.OK)
				return nGrdE;

			return GrdRead(grdHandle.Address, addr.Value, data);
		}

		/// <summary>
		/// Функция <b>GrdRead</b>
		/// </summary>
		/// <param name="grdHandle"></param>
		/// <param name="addr"></param>
		/// <param name="data"></param>
		/// <returns></returns>
		public static GrdE GrdRead(Handle grdHandle, GrdUAM addr, byte[] data)
		{
			GrdE nGrdE = GrdSetWorkMode(grdHandle, GrdWM.UAM);
			if (nGrdE != GrdE.OK)
				return nGrdE;

			return GrdRead(grdHandle.Address, addr.Value, data);
		}

		/// <summary>
		/// Функция <b>GrdRead</b>
		/// </summary>
		/// <param name="grdHandle"></param>
		/// <param name="addr"></param>
		/// <param name="lng"></param>
		/// <param name="data"></param>
		/// <returns></returns>
		public static GrdE GrdRead(Handle grdHandle, GrdSAM addr, int lng, out byte[] data)
		{
			data = new byte[lng];
			return GrdRead(grdHandle, addr, data);
		}

		/// <summary>
		/// Функция <b>GrdRead</b>
		/// </summary>
		/// <param name="grdHandle"></param>
		/// <param name="addr"></param>
		/// <param name="lng"></param>
		/// <param name="data"></param>
		/// <returns></returns>
		public static GrdE GrdRead(Handle grdHandle, GrdUAM addr, int lng, out byte[] data)
		{
			data = new byte[lng];
			return GrdRead(grdHandle, addr, data);
		}


		/// <summary>
		/// Функция <b>GrdRead</b>
		/// </summary>
		/// <param name="grdHandle"></param>
		/// <param name="addr"></param>
		/// <param name="data"></param>
		/// <returns></returns>
		public static GrdE GrdRead(Handle grdHandle, GrdSAM addr, out byte data)
		{
			byte[] _data = new byte[1];
			GrdE nGrdE = GrdRead(grdHandle, addr, _data);
			data = _data[0];
			return nGrdE;
		}

		/// <summary>
		/// Функция <b>GrdRead</b>
		/// </summary>
		/// <param name="grdHandle"></param>
		/// <param name="addr"></param>
		/// <param name="data"></param>
		/// <returns></returns>
		public static GrdE GrdRead(Handle grdHandle, GrdUAM addr, out byte data)
		{
			byte[] _data = new byte[1];
			GrdE nGrdE = GrdRead(grdHandle, addr, _data);
			data = _data[0];
			return nGrdE;
		}

		/// <summary>
		/// Функция <b>GrdRead</b>
		/// </summary>
		/// <param name="grdHandle"></param>
		/// <param name="addr"></param>
		/// <param name="data"></param>
		/// <returns></returns>
		public static GrdE GrdRead(Handle grdHandle, GrdSAM addr, out short data)
		{
			byte[] _data = new byte[2];
			GrdE nGrdE = GrdRead(grdHandle, addr, _data);
			data = BitConverter.ToInt16(_data, 0);
			return nGrdE;
		}

		/// <summary>
		/// Функция <b>GrdRead</b>
		/// </summary>
		/// <param name="grdHandle"></param>
		/// <param name="addr"></param>
		/// <param name="data"></param>
		/// <returns></returns>
		public static GrdE GrdRead(Handle grdHandle, GrdUAM addr, out short data)
		{
			byte[] _data = new byte[2];
			GrdE nGrdE = GrdRead(grdHandle, addr, _data);
			data = BitConverter.ToInt16(_data, 0);
			return nGrdE;
		}

		/// <summary>
		/// Функция <b>GrdRead</b>
		/// </summary>
		/// <param name="grdHandle"></param>
		/// <param name="addr"></param>
		/// <param name="data"></param>
		/// <returns></returns>
		public static GrdE GrdRead(Handle grdHandle, GrdSAM addr, out ushort data)
		{
			byte[] _data = new byte[2];
			GrdE nGrdE = GrdRead(grdHandle, addr, _data);
			data = BitConverter.ToUInt16(_data, 0);
			return nGrdE;
		}

		/// <summary>
		/// Функция <b>GrdRead</b>
		/// </summary>
		/// <param name="grdHandle"></param>
		/// <param name="addr"></param>
		/// <param name="data"></param>
		/// <returns></returns>
		public static GrdE GrdRead(Handle grdHandle, GrdUAM addr, out ushort data)
		{
			byte[] _data = new byte[2];
			GrdE nGrdE = GrdRead(grdHandle, addr, _data);
			data = BitConverter.ToUInt16(_data, 0);
			return nGrdE;
		}

		/// <summary>
		/// Функция <b>GrdRead</b>
		/// </summary>
		/// <param name="grdHandle"></param>
		/// <param name="addr"></param>
		/// <param name="data"></param>
		/// <returns></returns>
		public static GrdE GrdRead(Handle grdHandle, GrdSAM addr, out int data)
		{
			byte[] _data = new byte[4];
			GrdE nGrdE = GrdRead(grdHandle, addr, _data);
			data = BitConverter.ToInt32(_data, 0);
			return nGrdE;
		}

		/// <summary>
		/// Функция <b>GrdRead</b>
		/// </summary>
		/// <param name="grdHandle"></param>
		/// <param name="addr"></param>
		/// <param name="data"></param>
		/// <returns></returns>
		public static GrdE GrdRead(Handle grdHandle, GrdUAM addr, out int data)
		{
			byte[] _data = new byte[4];
			GrdE nGrdE = GrdRead(grdHandle, addr, _data);
			data = BitConverter.ToInt32(_data, 0);
			return nGrdE;
		}

		/// <summary>
		/// Функция <b>GrdRead</b>
		/// </summary>
		/// <param name="grdHandle"></param>
		/// <param name="addr"></param>
		/// <param name="data"></param>
		/// <returns></returns>
		public static GrdE GrdRead(Handle grdHandle, GrdSAM addr, out uint data)
		{
			byte[] _data = new byte[4];
			GrdE nGrdE = GrdRead(grdHandle, addr, _data);
			data = BitConverter.ToUInt32(_data, 0);
			return nGrdE;
		}

		/// <summary>
		/// Функция <b>GrdRead</b>
		/// </summary>
		/// <param name="grdHandle"></param>
		/// <param name="addr"></param>
		/// <param name="data"></param>
		/// <returns></returns>
		public static GrdE GrdRead(Handle grdHandle, GrdUAM addr, out uint data)
		{
			byte[] _data = new byte[4];
			GrdE nGrdE = GrdRead(grdHandle, addr, _data);
			data = BitConverter.ToUInt32(_data, 0);
			return nGrdE;
		}


		/// <summary>
		/// Функция <b>GrdRead</b>
		/// </summary>
		/// <param name="grdHandle"></param>
		/// <param name="addr"></param>
		/// <param name="data"></param>
		/// <returns></returns>
		public static GrdE GrdRead(Handle grdHandle, GrdSAM addr, out long data)
		{
			byte[] _data = new byte[8];
			GrdE nGrdE = GrdRead(grdHandle, addr, _data);
			data = BitConverter.ToInt64(_data, 0);
			return nGrdE;
		}

		/// <summary>
		/// Функция <b>GrdRead</b>
		/// </summary>
		/// <param name="grdHandle"></param>
		/// <param name="addr"></param>
		/// <param name="data"></param>
		/// <returns></returns>
		public static GrdE GrdRead(Handle grdHandle, GrdUAM addr, out long data)
		{
			byte[] _data = new byte[8];
			GrdE nGrdE = GrdRead(grdHandle, addr, _data);
			data = BitConverter.ToInt64(_data, 0);
			return nGrdE;
		}

		/// <summary>
		/// Функция <b>GrdRead</b>
		/// </summary>
		/// <param name="grdHandle"></param>
		/// <param name="addr"></param>
		/// <param name="data"></param>
		/// <returns></returns>
		public static GrdE GrdRead(Handle grdHandle, GrdSAM addr, out ulong data)
		{
			byte[] _data = new byte[8];
			GrdE nGrdE = GrdRead(grdHandle, addr, _data);
			data = BitConverter.ToUInt64(_data, 0);
			return nGrdE;
		}

		/// <summary>
		/// Функция <b>GrdRead</b>
		/// </summary>
		/// <param name="grdHandle"></param>
		/// <param name="addr"></param>
		/// <param name="data"></param>
		/// <returns></returns>
		public static GrdE GrdRead(Handle grdHandle, GrdUAM addr, out ulong data)
		{
			byte[] _data = new byte[8];
			GrdE nGrdE = GrdRead(grdHandle, addr, _data);
			data = BitConverter.ToUInt64(_data, 0);
			return nGrdE;
		}

		private static unsafe GrdE GrdRead(IntPtr hAddress, uint addr, byte[] data)
		{
			if (!CheckArg(data))
				return GrdE.InvalidArg;


			if (GrdApi.GrdReadInvoker == null)
			{
				Type[] parameters = { typeof(IntPtr), typeof(uint), typeof(int), typeof(IntPtr), typeof(IntPtr) };

				GrdApi.GrdReadInvoker = new PlatformInvoker("GrdRead", GrdDllName,
					typeof(GrdE), parameters, CallingConvention.Winapi, CharSet.Auto);
			}
			fixed (byte* pData = &data[0])
				return (GrdE)GrdReadInvoker.Invoke(hAddress, addr, data.Length, new IntPtr(pData), IntPtr.Zero);
		}
		/// \}
		#endregion // Функции чтение данных из ключа

		#region Функции получения информацииo
		/// \defgroup GRD_GET_INFO Функции получения информации
		/// \{
		/// Функция GrdGetInfo получает информацию из защищенного контейнера по указанному коду.

		/// <summary>
		/// Функция <b>GrdGetInfo</b>
		/// </summary>
		/// <param name="grdHandle"></param>
		/// <param name="infoCode"></param>
		/// <param name="infoData"></param>
		/// <returns></returns>
		public static GrdE GrdGetInfo(Handle grdHandle, GrdInfo infoCode, out byte infoData)
		{
			byte[] _infoData = new byte[1];
			GrdE nGrdE = GrdGetInfo(grdHandle.Address, infoCode.Value, _infoData);
			infoData = _infoData[0];
			return nGrdE;
		}


		/// <summary>
		/// Функция <b>GrdGetInfo</b>
		/// </summary>
		/// <param name="grdHandle"></param>
		/// <param name="infoCode"></param>
		/// <param name="infoData"></param>
		/// <returns></returns>
		public static GrdE GrdGetInfo(Handle grdHandle, GrdInfo infoCode, out ushort infoData)
		{
			byte[] _infoData = new byte[2];
			GrdE nGrdE = GrdGetInfo(grdHandle.Address, infoCode.Value, _infoData);
			infoData = BitConverter.ToUInt16(_infoData, 0);
			return nGrdE;
		}

		/// <summary>
		/// Функция <b>GrdGetInfo</b>
		/// </summary>
		/// <param name="grdHandle"></param>
		/// <param name="infoCode"></param>
		/// <param name="infoData"></param>
		/// <returns></returns>
		public static GrdE GrdGetInfo(Handle grdHandle, GrdInfo infoCode, out uint infoData)
		{
			byte[] _infoData = new byte[4];
			GrdE nGrdE = GrdGetInfo(grdHandle.Address, infoCode.Value, _infoData);
			infoData = BitConverter.ToUInt32(_infoData, 0);
			return nGrdE;
		}

		/// <summary>
		/// Функция <b>GrdGetInfo</b>
		/// </summary>
		/// <param name="grdHandle"></param>
		/// <param name="infoCode"></param>
		/// <param name="infoData"></param>
		/// <returns></returns>
		public static GrdE GrdGetInfo(Handle grdHandle, GrdInfo infoCode, out ulong infoData)
		{
			byte[] _infoData = new byte[8];
			GrdE nGrdE = GrdGetInfo(grdHandle.Address, infoCode.Value, _infoData);
			infoData = BitConverter.ToUInt64(_infoData, 0);
			return nGrdE;
		}

		/// <summary>
		/// Функция <b>GrdGetInfo</b>
		/// </summary>
		/// <param name="grdHandle"></param>
		/// <param name="infoCode"></param>
		/// <param name="infoData"></param>
		/// <returns></returns>
		public static GrdE GrdGetInfo(Handle grdHandle, GrdInfo infoCode, byte[] infoData)
		{
			return GrdGetInfo(grdHandle.Address, infoCode.Value, infoData);
		}

		/// <summary>
		/// Функция <b>GrdGetInfo</b>
		/// </summary>
		/// <param name="grdHandle"></param>
		/// <param name="infoCode"></param>
		/// <param name="infoData"></param>
		/// <returns></returns>
		public static GrdE GrdGetInfo(Handle grdHandle, GrdGIR infoCode, out byte[] infoData)
		{
			uint dataSize = 0;

			if (GrdGIR.LocalPort.Value == infoCode.Value ||
				GrdGIR.RemotePort.Value == infoCode.Value )
			{
				dataSize = 2;
			}else if (GrdGIR.VerSrv.Value == infoCode.Value)
			{
				dataSize = 8;
			}else if (GrdGIR.LocalMACAddress.Value == infoCode.Value)
			{
				dataSize = 21;
			}else if (GrdGIR.LocalIP.Value   == infoCode.Value ||
					  GrdGIR.RemoteIP.Value == infoCode.Value  ||
					  GrdGIR.IniBroadcastAddress.Value == infoCode.Value)
			{
				dataSize = 32;
			}else
				dataSize = 255;

			infoData = new byte[dataSize];
			return GrdGetInfo(grdHandle.Address, infoCode.Value, infoData);
		}

		private static unsafe GrdE GrdGetInfo(IntPtr hAddress, int infoCode, byte[] infoData)
		{
			if (!CheckArg(infoData))
				return GrdE.InvalidArg;

			if (GrdApi.GrdGetInfoInvoker == null)
			{
				Type[] parameters = { typeof(IntPtr), typeof(int), typeof(IntPtr), typeof(int) };

				GrdApi.GrdGetInfoInvoker = new PlatformInvoker("GrdGetInfo", GrdDllName,
					typeof(GrdE), parameters, CallingConvention.Winapi, CharSet.Auto);
			}

			fixed (byte* pInfoData = &infoData[0])
				return (GrdE)GrdGetInfoInvoker.Invoke(hAddress, infoCode, new IntPtr(pInfoData), infoData.Length);
		}
		/// \}
		#endregion Функции GrdGetInfo

		#region Функции для вычисления CRC
		/// \defgroup GRD_CRC Функции для вычисления CRC
		/// \{
		/// Набор функций GrdCRC позволяет подсчитать 32-битный CRC участка памяти

		/// <summary>
		/// Функция <b>GrdCRC</b>
		/// </summary>
		/// <param name="data"></param>
		/// <returns></returns>
		public static unsafe uint GrdCRC(byte[] data)
		{
			fixed (byte* pData = data)
				return GrdCRC(new IntPtr(pData), data.Length * sizeof(byte), (int)Grd.StartCRC);
		}

		/// <summary>
		/// Функция <b>GrdCRC</b>
		/// </summary>
		/// <param name="data"></param>
		/// <param name="prevCRC"></param>
		/// <returns></returns>
		public static unsafe uint GrdCRC(byte[] data, int prevCRC)
		{
			fixed (byte* pData = data)
				return GrdCRC(new IntPtr(pData), data.Length * sizeof(byte), (int)prevCRC);
		}

		public static unsafe uint GrdCRC(byte[] data, uint prevCRC)
		{
			fixed (byte* pData = data)
				return GrdCRC(new IntPtr(pData), data.Length * sizeof(byte), (int)prevCRC);
		}

		/// <summary>
		/// Функция <b>GrdCRC</b>
		/// </summary>
		/// <param name="data"></param>
		/// <returns></returns>
		public static unsafe uint GrdCRC(short[] data)
		{
			fixed (short* pData = data)
				return GrdCRC(new IntPtr(pData), data.Length * sizeof(short), (int)Grd.StartCRC);
		}

		/// <summary>
		/// Функция <b>GrdCRC</b>
		/// </summary>
		/// <param name="data"></param>
		/// <param name="prevCRC"></param>
		/// <returns></returns>
		public static unsafe uint GrdCRC(short[] data, int prevCRC)
		{
			fixed (short* pData = data)
				return GrdCRC(new IntPtr(pData), data.Length * sizeof(short), prevCRC);
		}

		public static unsafe uint GrdCRC(short[] data, uint prevCRC)
		{
			fixed (short* pData = data)
				return GrdCRC(new IntPtr(pData), data.Length * sizeof(short), prevCRC);
		}

		/// <summary>
		/// Функция <b>GrdCRC</b>
		/// </summary>
		/// <param name="data"></param>
		/// <returns></returns>
		public static unsafe uint GrdCRC(ushort[] data)
		{
			fixed (ushort* pData = data)
				return GrdCRC(new IntPtr(pData), data.Length * sizeof(ushort), (int)Grd.StartCRC);
		}

		/// <summary>
		/// Функция <b>GrdCRC</b>
		/// </summary>
		/// <param name="data"></param>
		/// <param name="prevCRC"></param>
		/// <returns></returns>
		public static unsafe uint GrdCRC(ushort[] data, int prevCRC)
		{
			fixed (ushort* pData = data)
				return GrdCRC(new IntPtr(pData), data.Length * sizeof(ushort), prevCRC);
		}

		public static unsafe uint GrdCRC(ushort[] data, uint prevCRC)
		{
			fixed (ushort* pData = data)
				return GrdCRC(new IntPtr(pData), data.Length * sizeof(ushort), prevCRC);
		}

		/// <summary>
		/// Функция <b>GrdCRC</b>
		/// </summary>
		/// <param name="data"></param>
		/// <returns></returns>
		public static unsafe uint GrdCRC(int[] data)
		{
			fixed (int* pData = data)
				return GrdCRC(new IntPtr(pData), data.Length * sizeof(int), (int)Grd.StartCRC);
		}

		/// <summary>
		/// Функция <b>GrdCRC</b>
		/// </summary>
		/// <param name="data"></param>
		/// <param name="prevCRC"></param>
		/// <returns></returns>
		public static unsafe uint GrdCRC(int[] data, int prevCRC)
		{
			fixed (int* pData = data)
				return GrdCRC(new IntPtr(pData), data.Length * sizeof(int), prevCRC);
		}

		public static unsafe uint GrdCRC(int[] data, uint prevCRC)
		{
			fixed (int* pData = data)
				return GrdCRC(new IntPtr(pData), data.Length * sizeof(int), prevCRC);
		}

		/// <summary>
		/// Функция <b>GrdCRC</b>
		/// </summary>
		/// <param name="data"></param>
		/// <returns></returns>
		public static unsafe uint GrdCRC(uint[] data)
		{
			fixed (uint* pData = data)
				return GrdCRC(new IntPtr(pData), data.Length * sizeof(uint), (int)Grd.StartCRC);
		}

		/// <summary>
		/// Функция <b>GrdCRC</b>
		/// </summary>
		/// <param name="data"></param>
		/// <param name="prevCRC"></param>
		/// <returns></returns>
		public static unsafe uint GrdCRC(uint[] data, int prevCRC)
		{
			fixed (uint* pData = data)
				return GrdCRC(new IntPtr(pData), data.Length * sizeof(uint), prevCRC);
		}

		public static unsafe uint GrdCRC(uint[] data, uint prevCRC)
		{
			fixed (uint* pData = data)
				return GrdCRC(new IntPtr(pData), data.Length * sizeof(uint), prevCRC);
		}

		/// <summary>
		/// Функция <b>GrdCRC</b>
		/// </summary>
		/// <param name="data"></param>
		/// <returns></returns>
		public static unsafe uint GrdCRC(long[] data)
		{
			fixed (long* pData = data)
				return GrdCRC(new IntPtr(pData), data.Length * sizeof(long), (int)Grd.StartCRC);
		}

		/// <summary>
		/// Функция <b>GrdCRC</b>
		/// </summary>
		/// <param name="data"></param>
		/// <param name="prevCRC"></param>
		/// <returns></returns>
		public static unsafe uint GrdCRC(long[] data, int prevCRC)
		{
			fixed (long* pData = data)
				return GrdCRC(new IntPtr(pData), data.Length * sizeof(long), prevCRC);
		}

		public static unsafe uint GrdCRC(long[] data, uint prevCRC)
		{
			fixed (long* pData = data)
				return GrdCRC(new IntPtr(pData), data.Length * sizeof(long), prevCRC);
		}

		/// <summary>
		/// Функция <b>GrdCRC</b>
		/// </summary>
		/// <param name="data"></param>
		/// <returns></returns>
		public static unsafe uint GrdCRC(ulong[] data)
		{
			fixed (ulong* pData = data)
				return GrdCRC(new IntPtr(pData), data.Length * sizeof(ulong), (int)Grd.StartCRC);
		}

		/// <summary>
		/// Функция <b>GrdCRC</b>
		/// </summary>
		/// <param name="data"></param>
		/// <param name="prevCRC"></param>
		/// <returns></returns>
		public static unsafe uint GrdCRC(ulong[] data, int prevCRC)
		{
			fixed (ulong* pData = data)
				return GrdCRC( new IntPtr(pData), data.Length * sizeof(ulong), prevCRC);
		}

		public static unsafe uint GrdCRC(ulong[] data, uint prevCRC)
		{
			fixed (ulong* pData = data)
				return GrdCRC( new IntPtr(pData), data.Length * sizeof(ulong), prevCRC);
		}

		private static unsafe uint GrdCRC(IntPtr pData, int dataLength, int prevCRC)
		{
			Type[] parameters = { typeof(IntPtr), typeof(int), typeof(int) };
			if (GrdApi.GrdCRCInvoker == null)
			{
				GrdApi.GrdCRCInvoker = new PlatformInvoker("GrdCRC", GrdDllName,
					typeof(uint), parameters, CallingConvention.Winapi, CharSet.Auto);
			}
			return (uint)GrdApi.GrdCRCInvoker.Invoke(pData, dataLength, prevCRC);
		}

		private static unsafe uint GrdCRC(IntPtr pData, int dataLength, uint prevCRC)
		{
			Type[] parameters = { typeof(IntPtr), typeof(int), typeof(int) };
			if (GrdApi.GrdCRCInvoker == null)
			{
				GrdApi.GrdCRCInvoker = new PlatformInvoker("GrdCRC", GrdDllName,
					typeof(uint), parameters, CallingConvention.Winapi, CharSet.Auto);
			}
			return (uint)GrdApi.GrdCRCInvoker.Invoke(pData, dataLength, (int)prevCRC);
		}

		/// \}
		#endregion //Overload GrcCRC

		/// \}
		#endregion // Функции Guardant API общего назначения

		#region Функции для програмирование ключа
		/// \defgroup DONLGE_INIT Функции для програмирование ключа
		/// \{

		/// <summary>
		/// Функция <b>GrdInit</b>
		/// </summary>
		/// <param name="grdHandle"></param>
		/// <returns></returns>
		public static GrdE GrdInit(Handle grdHandle)
		{
			if (GrdApi.GrdInitInvoker == null)
			{
				Type[] parameters = { typeof(IntPtr) };

				GrdApi.GrdInitInvoker = new PlatformInvoker("GrdInit", GrdDllName,
					typeof(GrdE), parameters, CallingConvention.Winapi, CharSet.Auto);
			}
			return (GrdE)GrdInitInvoker.Invoke(grdHandle.Address);
		}

		/// <summary>
		/// Функция <b>GrdProtect</b>
		/// </summary>
		/// <param name="grdHandle"></param>
		/// <param name="wrProt"></param>
		/// <param name="rdProt"></param>
		/// <param name="numFunc"></param>
		/// <param name="tableLMS"></param>
		/// <returns></returns>
		public static GrdE GrdProtect(Handle grdHandle, uint wrProt, uint rdProt, uint numFunc, uint tableLMS)
		{
			return GrdProtect(grdHandle, wrProt, rdProt, numFunc, tableLMS, 0);
		}

		/// <summary>
		/// Функция <b>GrdProtect</b>
		/// </summary>
		/// <param name="grdHandle"></param>
		/// <param name="wrProt"></param>
		/// <param name="rdProt"></param>
		/// <param name="numFunc"></param>
		/// <param name="tableLMS"></param>
		/// <param name="globalFlags"></param>
		/// <returns></returns>
		public static GrdE GrdProtect(Handle grdHandle, uint wrProt, uint rdProt, uint numFunc, uint tableLMS, uint globalFlags)
		{
			if (GrdApi.GrdProtectInvoker == null)
			{
				Type[] parameters = { typeof(IntPtr), typeof(uint), typeof(uint), typeof(uint), 
					typeof(uint), typeof(uint), typeof(IntPtr) };

				GrdApi.GrdProtectInvoker = new PlatformInvoker("GrdProtect", GrdDllName,
					typeof(GrdE), parameters, CallingConvention.Winapi, CharSet.Auto);
			}

			return (GrdE)GrdProtectInvoker.Invoke(grdHandle.Address, wrProt, rdProt, numFunc, tableLMS, globalFlags, IntPtr.Zero);
		}


		// \}
		#endregion Функции для програмирование ключа

		#region Функции для работы с алгоритмами
		/// \defgroup GRD_ALG Функции для работы с алгоритмами
		/// \{
		/// Для преобразований данных приложения существуют специальные функции Guardant API, которые обращаются к аппаратным алгоритмам ключа или програмным алгоритмам "Guardant Api"

		/// <summary>
		/// Функция <b>GrdCryptEx</b>
		/// Использовать только для аппартных алгоритмов GSII64/AES128 или програмного алгоритма AES256
		/// Размер вектора инициализации для GSII64 8 байт
		/// Размер вектора инициализации для AES128/AES256 16 байт
		/// </summary>
		/// <param name="grdHandle"></param>
		/// <param name="algNum"></param>
		/// <param name="data"></param>
		/// <param name="method"></param>
		/// <param name="iv"></param>
		/// <returns></returns>
		public static GrdE GrdCryptEx(Handle grdHandle, GrdAlgNum algNum, byte[] data, GrdAM method, byte[] iv)
		{
			return GrdCryptEx(grdHandle.Address, algNum.Value, data, method.Value, iv, null, null);
		}

		/// <summary>
		/// Функция <b>GrdCryptEx</b>
		/// Использовать только для аппартных алгоритмов GSII64/AES128 или програмного алгоритма AES256
		/// Размер вектора инициализации для GSII64 8 байт
		/// Размер вектора инициализации для AES128/AES256 16 байт
		/// </summary>
		/// <param name="grdHandle"></param>
		/// <param name="algNum"></param>
		/// <param name="data"></param>
		/// <param name="method"></param>
		/// <param name="iv"></param>
		/// <param name="key"></param>
		/// <returns></returns>
		public static GrdE GrdCryptEx(Handle grdHandle, GrdAlgNum algNum, byte[] data, GrdAM method, byte[] iv, byte[] key)
		{
			return GrdCryptEx(grdHandle.Address, algNum.Value, data, method.Value, iv, key, null);
		}

		/// <summary>
		/// Функция <b>GrdCryptEx</b>
		/// Использовать только для аппартных алгоритмов GSII64/AES128 или програмного алгоритма AES256
		/// Размер вектора инициализации для GSII64 8 байт
		/// Размер вектора инициализации для AES128/AES256 16 байт
		/// </summary>
		/// <param name="grdHandle"></param>
		/// <param name="algNum"></param>
		/// <param name="data"></param>
		/// <param name="method"></param>
		/// <param name="iv"></param>
		/// <param name="key"></param>
		/// <param name="context"></param>
		/// <returns></returns>
		public static GrdE GrdCryptEx(Handle grdHandle, GrdAlgNum algNum, byte[] data, GrdAM method, byte[] iv, byte[] key, byte[] context)
		{
			return GrdCryptEx(grdHandle.Address, algNum.Value, data, method.Value, iv, key, context);
		}

		private static unsafe GrdE GrdCryptEx(IntPtr hAddress, int algNum, byte[] data, int method, byte[] iv, byte[] key, byte[] context)
		{
			if (GrdApi.GrdCryptExInvoker == null)
			{
				Type[] parameters = { typeof(IntPtr), typeof(int), typeof(int), typeof(IntPtr), typeof(int),
				typeof(int), typeof(IntPtr), typeof(IntPtr), typeof(IntPtr), typeof(IntPtr) };

				//Создаём экземпляр PlatformInvoker для вызова GrdCrypt
				GrdApi.GrdCryptExInvoker = new PlatformInvoker("GrdCryptEx", GrdDllName,
					typeof(GrdE), parameters, CallingConvention.Winapi, CharSet.Auto);
			}

			if (data == null)
				data = new byte[0];

			if (iv == null)
				iv = new byte[0];

			if (key == null)
				key = new byte[0];

			if (context == null)
				context = new byte[0];

			fixed (byte* pData = data)
			fixed (byte* pIV = iv)
			fixed (byte* pKey = key)
			fixed (byte* pСontext = context)
				return (GrdE)GrdCryptExInvoker.Invoke(hAddress, algNum,
					data.Length, new IntPtr(pData), method, iv.Length, new IntPtr(pIV), new IntPtr(pKey), new IntPtr(pСontext), IntPtr.Zero);
		}

		/// <summary>
		/// Функция <b>GrdTransformEx</b>
		/// Использовать только для аппартных алгоритмов GSII64/AES128
		/// Размер вектора инициализации для GSII64 8 байт
		/// Размер вектора инициализации для AES128 16 байт
		/// </summary>
		/// <param name="grdHandle"></param>
		/// <param name="algNum"></param>
		/// <param name="data"></param>
		/// <param name="method"></param>
		/// <param name="iv"></param>
		/// <returns></returns>
		public static GrdE GrdTransformEx(Handle grdHandle, GrdAlgNum algNum, byte[] data, GrdAM method, byte[] iv)
		{
			return GrdTransformEx(grdHandle.Address, algNum.Value, data, method.Value, iv);
		}

		private static unsafe GrdE GrdTransformEx(IntPtr hAddress, int algNum, byte[] data, int method, byte[] iv)
		{
			Type[] parameters = { typeof(IntPtr), typeof(int), typeof(int), typeof(IntPtr),
				typeof(int), typeof(int), typeof(IntPtr), typeof(IntPtr) };

			if (GrdApi.GrdTransformExInvoker == null)
			{
				GrdApi.GrdTransformExInvoker = new PlatformInvoker("GrdTransformEx", GrdDllName,
					typeof(GrdE), parameters, CallingConvention.Winapi, CharSet.Auto);
			}

			if (data == null)
				data = new byte[0];

			if (iv == null)
				iv = new byte[0];

			fixed (byte* pData = data)
			fixed (byte* pIV = iv)
				return (GrdE)GrdTransformExInvoker.Invoke(hAddress, algNum, data.Length, new IntPtr(pData), method,
				iv.Length, new IntPtr(pIV), IntPtr.Zero);
		}

		/// <summary>
		/// Функция <b>GrdSign</b>
		/// </summary>
		/// <param name="grdHandle"></param>
		/// <param name="algNum"></param>
		/// <param name="data"></param>
		/// <param name="digestSign"></param>
		/// <returns></returns>
		public static GrdE GrdSign(Handle grdHandle, GrdAlgNum algNum, byte[] data, out byte[] digestSign)
		{
			digestSign = new byte[(int)GrdECC160.DIGEST_SIZE];
			return GrdSign(grdHandle.Address, algNum.Value, data, digestSign);
		}

		private static unsafe GrdE GrdSign(IntPtr hAddress, int algNum, byte[] data, byte[] digestSign)
		{
			Type[] parameters = { typeof(IntPtr), typeof(int), typeof(int), typeof(IntPtr),
				typeof(int), typeof(IntPtr), typeof(IntPtr) };

			if (GrdApi.GrdSignInvoker == null)
			{
				GrdApi.GrdSignInvoker = new PlatformInvoker("GrdSign", GrdDllName,
					typeof(GrdE), parameters, CallingConvention.Winapi, CharSet.Auto);
			}

			if (data == null)
				data = new byte[0];

			if (digestSign == null)
				digestSign = new byte[0];

			fixed (byte* pData = data)
			fixed (byte* pDigestSign = digestSign)
				return (GrdE)GrdSignInvoker.Invoke(hAddress, algNum, data.Length, new IntPtr(pData), digestSign.Length, new IntPtr(pDigestSign), IntPtr.Zero);
		}

		/// <summary>
		/// Функция <b>GrdVerifySign</b>
		/// </summary>
		/// <param name="grdHandle"></param>
		/// <param name="publicKey"></param>
		/// <param name="data"></param>
		/// <param name="digestSign"></param>
		/// <returns></returns>
		public static GrdE GrdVerifySign(Handle grdHandle, byte[] publicKey, byte[] data, byte[] digestSign)
		{
			return GrdVerifySign(grdHandle.Address, (int)GrdVSC.ECC160, publicKey, data, digestSign);
		}

		private static unsafe GrdE GrdVerifySign(IntPtr hAddress, int algNum, byte[] publicKey, byte[] data, byte[] digestSign)
		{
			Type[] parameters = { typeof(IntPtr), typeof(int), typeof(int), typeof(IntPtr), 
				typeof(int), typeof(IntPtr), typeof(int), typeof(IntPtr), typeof(IntPtr) };
			if (GrdApi.GrdVerifySignInvoker == null)
			{
				GrdApi.GrdVerifySignInvoker = new PlatformInvoker("GrdVerifySign", GrdDllName,
					typeof(GrdE), parameters, CallingConvention.Winapi, CharSet.Auto);
			}

			if (publicKey == null)
				publicKey = new byte[0];

			if (data == null)
				data = new byte[0];

			if (digestSign == null)
				digestSign = new byte[0];

			fixed (byte* pPublicKey = publicKey)
			fixed (byte* pData = data)
			fixed (byte* pDigestSign = digestSign)
				return (GrdE)GrdVerifySignInvoker.Invoke(hAddress, algNum, publicKey.Length, new IntPtr(pPublicKey),
					data.Length, new IntPtr(pData), digestSign.Length, new IntPtr(pDigestSign), IntPtr.Zero);
		}

		/// <summary>
		/// Функция <b>GrdHashEx</b>
		/// </summary>
		/// <param name="grdHandle"></param>
		/// <param name="hashNum"></param>
		/// <param name="data"></param>
		/// <param name="method"></param>
		/// <param name="digest"></param>
		/// <returns></returns>
		public static GrdE GrdHashEx(Handle grdHandle, GrdAlgNum hashNum, byte[] data, GrdSC method, byte[] digest)
		{
			return GrdHashEx(grdHandle.Address, hashNum.Value, data, method.Value, digest, null);
		}

		/// <summary>
		/// Функция <b>GrdHashEx</b>
		/// </summary>
		/// <param name="grdHandle"></param>
		/// <param name="hashNum"></param>
		/// <param name="data"></param>
		/// <param name="method"></param>
		/// <param name="digest"></param>
		/// <param name="context"></param>
		/// <returns></returns>
		public static GrdE GrdHashEx(Handle grdHandle, GrdAlgNum hashNum, byte[] data, GrdSC method, byte[] digest, byte[] context)
		{
			return GrdHashEx(grdHandle.Address, hashNum.Value, data, method.Value, digest, context);
		}

		private static unsafe GrdE GrdHashEx(IntPtr hAddress, int algNum, byte[] data, int method, byte[] digest, byte[] context)
		{
			if (GrdApi.GrdHashExInvoker == null)
			{
				Type[] parameters = { typeof(IntPtr), typeof(int), typeof(int), typeof(IntPtr), typeof(int), 
					typeof(int), typeof(IntPtr), typeof(int),typeof(IntPtr), typeof(int), typeof(IntPtr), typeof(IntPtr) };

				//Создаём экземпляр PlatformInvoker для вызова GrdHash
				GrdApi.GrdHashExInvoker = new PlatformInvoker("GrdHashEx", GrdDllName,
					typeof(GrdE), parameters, CallingConvention.Winapi, CharSet.Auto);
			}

			if (data == null)
				data = new byte[0];

			if (digest == null)
				digest = new byte[0];

			if (context == null)
				context = new byte[0];

			fixed (byte* pData = data)
			fixed (byte* pDigest = digest)
			fixed (byte* pContext = context)
				return (GrdE)GrdHashExInvoker.Invoke(hAddress, algNum, data.Length, new IntPtr(pData), method,
					digest.Length, new IntPtr(pDigest), 0, IntPtr.Zero, context.Length, new IntPtr(pContext), IntPtr.Zero);
		}


		#region Устаревшие функции для работы с алгоритмами
		/// \defgroup GRD_ALG_OLG Устаревшие функции для работы с алгоритмами
		/// \{


		/// <summary>
		/// Функция <b>GrdCrypt</b>
		/// Использовать только для аппартного алгоритма GSII64
		/// Размер вектора инициализации для GSII64 8 байт
		/// </summary>
		/// <param name="grdHandle"></param>
		/// <param name="algNum"></param>
		/// <param name="data"></param>
		/// <param name="method"></param>
		/// <param name="iv"></param>
		/// <returns></returns>
		public static GrdE GrdCrypt(Handle grdHandle, GrdAlgNum algNum, byte[] data, GrdAM method, ref long iv)
		{
			byte[] _iv = BitConverter.GetBytes(iv);
			GrdE nGrdE = GrdCrypt(grdHandle.Address, algNum.Value, data, method.Value, _iv, null, null);
			iv = BitConverter.ToInt64(_iv, 0);
			return nGrdE;
		}

		/// <summary>
		/// Функция <b>GrdCrypt</b>
		/// Использовать только для аппартного алгоритма GSII64
		/// Размер вектора инициализации для GSII64 8 байт
		/// </summary>
		/// <param name="grdHandle"></param>
		/// <param name="algNum"></param>
		/// <param name="data"></param>
		/// <param name="method"></param>
		/// <param name="iv"></param>
		/// <returns></returns>
		public static GrdE GrdCrypt(Handle grdHandle, GrdAlgNum algNum, byte[] data, GrdAM method, byte[] iv)
		{
			return GrdCrypt(grdHandle.Address, algNum.Value, data, method.Value, iv, null, null);
		}

		/// <summary>
		/// Функция <b>GrdCrypt</b>
		/// Использовать только для аппартного алгоритма GSII64 или програмного алгоритма AES256
		/// Размер вектора инициализации для GSII64 8 байт
		/// Размер вектора инициализации для AES256 16 байт
		/// </summary>
		/// <param name="grdHandle"></param>
		/// <param name="algNum"></param>
		/// <param name="data"></param>
		/// <param name="method"></param>
		/// <param name="iv"></param>
		/// <param name="key"></param>
		/// <returns></returns>
		public static GrdE GrdCrypt(Handle grdHandle, GrdAlgNum algNum, byte[] data, GrdAM method, byte[] iv, byte[] key)
		{
			return GrdCrypt(grdHandle.Address, algNum.Value, data, method.Value, iv, key, null);
		}

		/// <summary>
		/// Функция <b>GrdCrypt</b>
		/// Использовать только для аппартного алгоритма GSII64 или програмного алгоритма AES256
		/// Размер вектора инициализации для GSII64 8 байт
		/// Размер вектора инициализации для AES256 16 байт
		/// </summary>
		/// <param name="grdHandle"></param>
		/// <param name="algNum"></param>
		/// <param name="data"></param>
		/// <param name="method"></param>
		/// <param name="iv"></param>
		/// <param name="key"></param>
		/// <param name="context"></param>
		/// <returns></returns>
		public static GrdE GrdCrypt(Handle grdHandle, GrdAlgNum algNum, byte[] data, GrdAM method, byte[] iv, byte[] key, byte[] context)
		{
			return GrdCrypt(grdHandle.Address, algNum.Value, data, method.Value, iv, key, context);
		}


		private static unsafe GrdE GrdCrypt(IntPtr hAddress, int algNum, byte[] data, int method, byte[] iv, byte[] key, byte[] context)
		{
			if (GrdApi.GrdCryptInvoker == null)
			{
				Type[] parameters = { typeof(IntPtr), typeof(int), typeof(int), typeof(IntPtr), typeof(int), 
					typeof(IntPtr), typeof(IntPtr), typeof(IntPtr) };

				//Создаём экземпляр PlatformInvoker для вызова GrdCrypt
				GrdApi.GrdCryptInvoker = new PlatformInvoker("GrdCrypt", GrdDllName,
					typeof(GrdE), parameters, CallingConvention.Winapi, CharSet.Auto);
			}

			if (data == null)
				data = new byte[0];

			if (iv == null)
				iv = new byte[0];

			if (key == null)
				key = new byte[0];

			if (context == null)
				context = new byte[0];

			fixed (byte* pData = data)
			fixed (byte* pIV = iv)
			fixed (byte* pKey = key)
			fixed (byte* pСontext = context)
				return (GrdE)GrdCryptInvoker.Invoke(hAddress, algNum,
					data.Length, new IntPtr(pData), method, new IntPtr(pIV), new IntPtr(pKey), new IntPtr(pСontext));
		}

		/// <summary>
		/// Функция <b>GrdTransform</b>
		/// Использовать только для аппартного алгоритма GSII64 
		/// </summary>
		/// <param name="grdHandle"></param>
		/// <param name="algNum"></param>
		/// <param name="data"></param>
		/// <param name="method"></param>
		/// <param name="iv"></param>
		/// <returns></returns>
		public static GrdE GrdTransform(Handle grdHandle, GrdAlgNum algNum, byte[] data, GrdAM method, ref long iv)
		{
			byte[] _iv = BitConverter.GetBytes(iv);
			GrdE nGrdE = GrdTransform(grdHandle.Address, algNum.Value, data, method.Value, _iv);
			iv = BitConverter.ToInt64(_iv, 0);
			return nGrdE;
		}

		/// <summary>
		/// Функция <b>GrdTransform</b>
		/// Использовать только для аппартного алгоритма GSII64 
		/// </summary>
		/// <param name="grdHandle"></param>
		/// <param name="algNum"></param>
		/// <param name="data"></param>
		/// <param name="method"></param>
		/// <param name="iv"></param>
		/// <returns></returns>
		public static GrdE GrdTransform(Handle grdHandle, GrdAlgNum algNum, byte[] data, GrdAM method, byte[] iv)
		{
			return GrdTransform(grdHandle.Address, algNum.Value, data, method.Value, iv);
		}

		private static unsafe GrdE GrdTransform(IntPtr hAddress, int algNum, byte[] data, int method, byte[] iv)
		{
			if (GrdApi.GrdTransformInvoker == null)
			{
				Type[] parameters = { typeof(IntPtr), typeof(int), typeof(int), typeof(IntPtr), typeof(int), typeof(IntPtr) };

				//Создаём экземпляр PlatformInvoker для вызова GrdTransform
				GrdApi.GrdTransformInvoker = new PlatformInvoker("GrdTransform", GrdDllName,
					typeof(GrdE), parameters, CallingConvention.Winapi, CharSet.Auto);
			}

			if (data == null)
				data = new byte[0];

			if (iv == null)
				iv = new byte[0];

			fixed (byte* pData = data)
			fixed (byte* pIV = iv)
				return (GrdE)GrdTransformInvoker.Invoke(hAddress, algNum, data.Length, new IntPtr(pData), method, new IntPtr(pIV));
		}

		/// <summary>
		/// Функция <b>GrdHash</b>
		/// </summary>
		/// <param name="grdHandle"></param>
		/// <param name="hashNum"></param>
		/// <param name="data"></param>
		/// <param name="method"></param>
		/// <param name="digest"></param>
		/// <returns></returns>
		public static GrdE GrdHash(Handle grdHandle, GrdAlgNum hashNum, byte[] data, GrdSC method, byte[] digest)
		{
			return GrdHash(grdHandle.Address, hashNum.Value, data, method.Value, digest, null);
		}

		/// <summary>
		/// Функция <b>GrdHash</b>
		/// </summary>
		/// <param name="grdHandle"></param>
		/// <param name="hashNum"></param>
		/// <param name="data"></param>
		/// <param name="method"></param>
		/// <param name="digest"></param>
		/// <param name="context"></param>
		/// <returns></returns>
		public static GrdE GrdHash(Handle grdHandle, GrdAlgNum hashNum, byte[] data, GrdSC method, byte[] digest, byte[] context)
		{
			return GrdHash(grdHandle.Address, hashNum.Value, data, method.Value, digest, context);
		}

		private static unsafe GrdE GrdHash(IntPtr hAddress, int algNum, byte[] data, int method, byte[] digest, byte[] context)
		{
			if (GrdApi.GrdHashInvoker == null)
			{
				Type[] parameters = { typeof(IntPtr), typeof(int), typeof(int), typeof(IntPtr), 
					typeof(int), typeof(IntPtr), typeof(IntPtr), typeof(IntPtr) };

				//Создаём экземпляр PlatformInvoker для вызова GrdHash
				GrdApi.GrdHashInvoker = new PlatformInvoker("GrdHash", GrdDllName,
					typeof(GrdE), parameters, CallingConvention.Winapi, CharSet.Auto);
			}

			if (data == null)
				data = new byte[0];

			if (digest == null)
				digest = new byte[0];

			if (context == null)
				context = new byte[0];

			fixed (byte* pData = data)
			fixed (byte* pDigest = digest)
			fixed (byte* pСontext = context)
				return (GrdE)GrdHashInvoker.Invoke(hAddress, algNum, data.Length, new IntPtr(pData),
					method, new IntPtr(pDigest), IntPtr.Zero, new IntPtr(pСontext));
		}

		/// <summary>
		/// Функция <b>GrdCodeInit</b>
		/// </summary>
		/// <param name="grdHandle"></param>
		/// <param name="cnvType"></param>
		/// <param name="addr"></param>
		/// <param name="key"></param>
		/// <returns></returns>
		public static GrdE GrdCodeInit(Handle grdHandle, GrdAT cnvType, uint addr, byte[] key)
		{
			return GrdCodeInit(grdHandle.Address, cnvType, addr, key);
		}

		private static unsafe GrdE GrdCodeInit(IntPtr hAddress, GrdAT cnvType, uint addr, byte[] key)
		{
			if (GrdApi.GrdCodeInitInvoker == null)
			{
				Type[] parameters = { typeof(IntPtr), typeof(GrdAT), typeof(uint), typeof(IntPtr) };

				GrdApi.GrdCodeInitInvoker = new PlatformInvoker("GrdCodeInit", GrdDllName,
					typeof(GrdE), parameters, CallingConvention.Winapi, CharSet.Auto);
			}

			if (key == null)
				key = new byte[0];

			fixed (byte* pKey = key)
				return (GrdE)GrdCodeInitInvoker.Invoke(hAddress, cnvType, addr, new IntPtr(pKey));
		}

		/// <summary>
		/// Функция <b>GrdEnCode</b>
		/// </summary>
		/// <param name="cnvType"></param>
		/// <param name="key"></param>
		/// <param name="data"></param>
		/// <returns></returns>
		public static GrdE GrdEnCode(GrdAT cnvType, byte[] key, byte[] data)
		{
			return GrdEnCode((int)cnvType, key, data);
		}

		private static unsafe GrdE GrdEnCode(int cnvType, byte[] key, byte[] data)
		{
			Type[] parameters = { typeof(int), typeof(IntPtr), typeof(IntPtr), typeof(int) };
			if (GrdApi.GrdEnCodeInvoker == null)
			{
				//Создаём экземпляр PlatformInvoker для вызова GrdEnCode
				GrdApi.GrdEnCodeInvoker = new PlatformInvoker("GrdEnCode", GrdDllName,
					typeof(GrdE), parameters, CallingConvention.Winapi, CharSet.Auto);
			}

			if (key == null)
				key = new byte[0];

			if (data == null)
				data = new byte[0];

			fixed (byte* pKey = key)
			fixed (byte* pData = data)
				return (GrdE)GrdEnCodeInvoker.Invoke(cnvType, new IntPtr(pKey), new IntPtr(pData), data.Length);
		}

		/// <summary>
		/// Функция <b>GrdDeCode</b>
		/// </summary>
		/// <param name="cnvType"></param>
		/// <param name="key"></param>
		/// <param name="data"></param>
		/// <returns></returns>
		public static GrdE GrdDeCode(GrdAT cnvType, byte[] key, byte[] data)
		{
			return GrdDeCode((int)cnvType, key, data);
		}

		private static unsafe GrdE GrdDeCode(int cnvType, byte[] key, byte[] data)
		{
			Type[] parameters = { typeof(int), typeof(IntPtr), typeof(IntPtr), typeof(int) };
			if (GrdApi.GrdDeCodeInvoker == null)
			{
				//Создаём экземпляр PlatformInvoker для вызова GrdDeCode
				GrdApi.GrdDeCodeInvoker = new PlatformInvoker("GrdDeCode", GrdDllName,
					typeof(GrdE), parameters, CallingConvention.Winapi, CharSet.Auto);
			}

			if (key == null)
				key = new byte[0];

			if (data == null)
				data = new byte[0];

			fixed (byte* pKey = key)
			fixed (byte* pData = data)
				return (GrdE)GrdDeCodeInvoker.Invoke(cnvType, new IntPtr(pKey), new IntPtr(pData), data.Length);
		}

		/// \}
		#endregion //Устаревшие функции для работы с алгоритмами
		/// \defgroup GRD_ALG_OLG Устаревшие функции для работы с алгоритмами

		/// \}
		#endregion //Функции для работы с алгоритмами

		#region Функции для работы с защищенными ячейками
		/// \defgroup GRD_PI Функции работы с защищенными ячейками
		/// \{
		/// Ключи <b>Guardant Time/Time Net/Code Time</b> обладают встроенными часами реального времени (Real-Time Clock, RTC), что позволяет ограничивать астрономическое время работы приложения путем установки временных зависимостей от аппаратных алгоритмов ключа. Для работы с возможностями, которые присущи только ключам с RTC, существуют специальные функции Guardant API.

		/// <summary>
		/// Функция <b>GrdPI_Activate</b>
		/// </summary>
		/// <param name="grdHandle"></param>
		/// <param name="algNum"></param>
		/// <param name="activatePsw"></param>
		/// <returns></returns>
		public static GrdE GrdPI_Activate(Handle grdHandle, GrdAlgNum algNum, GrdPswd activatePsw)
		{
			Type[] parameters = { typeof(IntPtr), typeof(int), typeof(int) };
			if (GrdApi.GrdPI_ActivateInvoker == null)
			{
				//Создаём экземпляр PlatformInvoker для вызова GrdPI_Activate
				GrdApi.GrdPI_ActivateInvoker = new PlatformInvoker("GrdPI_Activate", GrdDllName,
					typeof(GrdE), parameters, CallingConvention.Winapi, CharSet.Auto);
			}

			return (GrdE)GrdPI_ActivateInvoker.Invoke( grdHandle.Address, algNum.Value, activatePsw.Value);
		}

		/// <summary>
		/// Функция <b>GrdPI_Deactivate</b>
		/// </summary>
		/// <param name="grdHandle"></param>
		/// <param name="algNum"></param>
		/// <param name="deactivatePsw"></param>
		/// <returns></returns>
		public static GrdE GrdPI_Deactivate(Handle grdHandle, GrdAlgNum algNum, GrdPswd deactivatePsw)
		{
			Type[] parameters = { typeof(IntPtr), typeof(int), typeof(int) };
			if (GrdApi.GrdPI_DeactivateInvoker == null)
			{
				//Создаём экземпляр PlatformInvoker для вызова GrdPI_Deactivate
				GrdApi.GrdPI_DeactivateInvoker = new PlatformInvoker("GrdPI_Deactivate", GrdDllName,
					typeof(GrdE), parameters, CallingConvention.Winapi, CharSet.Auto);
			}
			return (GrdE)GrdPI_DeactivateInvoker.Invoke(grdHandle.Address, algNum.Value, deactivatePsw.Value);
		}

		/// <summary>
		/// Функция <b>GrdPI_Read</b>
		/// </summary>
		/// <param name="grdHandle"></param>
		/// <param name="algNum"></param>
		/// <param name="addr"></param>
		/// <param name="data"></param>
		/// <returns></returns>
		public static GrdE GrdPI_Read(Handle grdHandle, GrdAlgNum algNum, uint addr, byte[] data)
		{
			return GrdPI_Read(grdHandle.Address, algNum.Value, addr, data, 0);
		}

		/// <summary>
		/// Функция <b>GrdPI_Read</b>
		/// </summary>
		/// <param name="grdHandle"></param>
		/// <param name="algNum"></param>
		/// <param name="addr"></param>
		/// <param name="data"></param>
		/// <param name="readPsw"></param>
		/// <returns></returns>
		public static GrdE GrdPI_Read(Handle grdHandle, GrdAlgNum algNum, uint addr, byte[] data, GrdPswd readPsw)
		{
			return GrdPI_Read(grdHandle.Address, algNum.Value, addr, data, readPsw.Value);
		}

		/// <summary>
		/// Функция <b>GrdPI_Read</b>
		/// </summary>
		/// <param name="grdHandle"></param>
		/// <param name="algNum"></param>
		/// <param name="addr"></param>
		/// <param name="lng"></param>
		/// <param name="data"></param>
		/// <returns></returns>
		public static GrdE GrdPI_Read(Handle grdHandle, GrdAlgNum algNum, uint addr, int lng, out byte[] data)
		{
			data = new byte[lng];
			return GrdPI_Read(grdHandle.Address, algNum.Value, addr, data, 0);
		}

		/// <summary>
		/// Функция <b>GrdPI_Read</b>
		/// </summary>
		/// <param name="grdHandle"></param>
		/// <param name="algNum"></param>
		/// <param name="addr"></param>
		/// <param name="lng"></param>
		/// <param name="data"></param>
		/// <param name="readPsw"></param>
		/// <returns></returns>

		public static GrdE GrdPI_Read(Handle grdHandle, GrdAlgNum algNum, uint addr, int lng, out byte[] data, GrdPswd readPsw)
		{
			data = new byte[lng];
			return GrdPI_Read(grdHandle.Address, algNum.Value, addr, data, readPsw.Value);
		}

		private static unsafe GrdE GrdPI_Read(IntPtr hAddress, int algNum, uint addr, byte[] data, int readPsw)
		{
			Type[] parameters = { typeof(IntPtr), typeof(int), typeof(uint), typeof(int), typeof(IntPtr), typeof(int), typeof(IntPtr) };
			if (GrdApi.GrdPI_ReadInvoker == null)
			{
				//Создаём экземпляр PlatformInvoker для вызова GrdPI_Read
				GrdApi.GrdPI_ReadInvoker = new PlatformInvoker("GrdPI_Read", GrdDllName,
					typeof(GrdE), parameters, CallingConvention.Winapi, CharSet.Auto);
			}

			if (data == null)
				data = new byte[0];

			fixed (byte* pData = data)
				return (GrdE)GrdPI_ReadInvoker.Invoke(hAddress, algNum, addr, data.Length, new IntPtr(pData), readPsw, IntPtr.Zero);
		}

		/// <summary>
		/// Функция <b>GrdPI_Update</b>
		/// </summary>
		/// <param name="grdHandle"></param>
		/// <param name="algNum"></param>
		/// <param name="addr"></param>
		/// <param name="data"></param>
		/// <param name="updatePsw"></param>
		/// <param name="method"></param>
		/// <returns></returns>
		public static GrdE GrdPI_Update(Handle grdHandle, GrdAlgNum algNum, uint addr, byte[] data, GrdPswd updatePsw, GrdUM method)
		{
			return GrdPI_Update(grdHandle.Address, algNum.Value, addr, data, updatePsw.Value, (int)method);
		}

		private static unsafe GrdE GrdPI_Update(IntPtr hAddress, int algNum, uint addr, byte[] data, int updatePsw, int method)
		{
			Type[] parameters = { typeof(IntPtr), typeof(int), typeof(uint), typeof(int), typeof(IntPtr), typeof(int), typeof(int), typeof(IntPtr) };
			if (GrdApi.GrdPI_UpdateInvoker == null)
			{
				//Создаём экземпляр PlatformInvoker для вызова GrdPI_Update
				GrdApi.GrdPI_UpdateInvoker = new PlatformInvoker("GrdPI_Update", GrdDllName,
					typeof(GrdE), parameters, CallingConvention.Winapi, CharSet.Auto);
			}

			if (data == null)
				data = new byte[0];

			fixed (byte* pData = data)
				return (GrdE)GrdApi.GrdPI_UpdateInvoker.Invoke(hAddress, algNum, addr, data.Length, new IntPtr(pData), updatePsw, method, IntPtr.Zero);
		}

		/// <summary>
		/// Функция <b>GrdPI_GetCounter</b>
		/// </summary>
		/// <param name="grdHandle"></param>
		/// <param name="algNum"></param>
		/// <param name="counter"></param>
		/// <returns></returns>
		public static GrdE GrdPI_GetCounter(Handle grdHandle, GrdAlgNum algNum, out uint counter)
		{
			return GrdPI_GetCounter(grdHandle.Address, algNum.Value, out counter);
		}

		private static unsafe GrdE GrdPI_GetCounter(IntPtr hAddress, int algNum, out uint counter)
		{
			Type[] parameters = { typeof(IntPtr), typeof(int), typeof(IntPtr), typeof(IntPtr) };

			if (GrdApi.GrdPI_GetCounterInvoker == null)
			{
				GrdApi.GrdPI_GetCounterInvoker = new PlatformInvoker("GrdPI_GetCounter", GrdDllName,
					typeof(GrdE), parameters, CallingConvention.Winapi, CharSet.Auto);
			}
			fixed (uint* pCounter = &counter)
				return (GrdE)GrdPI_GetCounterInvoker.Invoke(hAddress, algNum, new IntPtr(pCounter), IntPtr.Zero);
		}

		/// \}
		#endregion //Функции работы с защищенными ячейками

		#region Функции доверенного удаленного обновления 
		/// \defgroup GRD_TRU Функции доверенного удаленного обновления 
		/// \{
		/// Технология Trusted Remote Update может быть реализована не только при помощи утилит, входящих в Комплект разработчика. При желании разработчики могут встраивать поддержку этой технологии непосредственно в свои приложения, используя набор предназначенных для этой цели функций.

		/// <summary>
		/// Функция <b>GrdTRU_SetKey</b>
		/// </summary>
		/// <param name="grdHandle"></param>
		/// <param name="gsii64_Key128"> указатель на буфер, содержащий 128-битовый уникальный секретный ключ шифрования алгоритма GSII64/AES128</param>
		/// <returns></returns>
		public static GrdE GrdTRU_SetKey(Handle grdHandle, byte[] gsii64_Key128)
		{
			return GrdApi.GrdTRU_SetKey(grdHandle.Address, gsii64_Key128);
		}

		private static unsafe GrdE GrdTRU_SetKey(IntPtr hAddress, byte[] gsii64_Key128)
		{
			Type[] parameters = { typeof(IntPtr), typeof(IntPtr) };
			if (GrdApi.GrdTRU_SetKeyInvoker == null)
			{
				GrdApi.GrdTRU_SetKeyInvoker = new PlatformInvoker("GrdTRU_SetKey", GrdDllName,
					typeof(GrdE), parameters, CallingConvention.Winapi, CharSet.Auto);
			}

			if (gsii64_Key128 == null)
				gsii64_Key128 = new byte[0];

			fixed (byte* pGsii64_Key128 = gsii64_Key128)
				return (GrdE)GrdTRU_SetKeyInvoker.Invoke(hAddress, new IntPtr(pGsii64_Key128));
		}

		/// <summary>
		/// Функция <b>GrdTRU_GenerateQuestionEx</b>
		/// </summary>
		/// <param name="grdHandle"></param>
		/// <param name="question"></param>
		/// <param name="id"></param>
		/// <param name="publicCode"></param>
		/// <param name="hash"></param>
		/// <returns></returns>
		public static GrdE GrdTRU_GenerateQuestionEx(Handle grdHandle,out byte[] question, out uint id, out uint publicCode, out byte[] hash)
		{
			uint ModelID;
			id = 0;
			publicCode = 0;
			question = new byte[0];
			hash = new byte[0];

			GrdE nGrdE = GrdApi.GrdGetInfo(grdHandle, GrdGIL.Model, out ModelID);
			if (nGrdE != GrdE.OK)
				return nGrdE;

			if (ModelID == (uint)GrdDM.GCU)
			{
				question = new byte[16];
				hash = new byte[32];
			}
			else {
				question = new byte[16];
				hash = new byte[32];
			}

			return GrdTRU_GenerateQuestionEx(grdHandle.Address, question, out id, out publicCode, hash);
		}

		private static unsafe GrdE GrdTRU_GenerateQuestionEx(IntPtr hAddress, byte[] question, out uint id, out uint publicCode, byte[] hash)
		{
			Type[] parameters = { typeof(IntPtr), typeof(int), typeof(IntPtr), typeof(IntPtr), 
				typeof(IntPtr), typeof(int), typeof(IntPtr), typeof(int), typeof(IntPtr) };

			if (GrdApi.GrdTRU_GenerateQuestionExInvoker == null)
			{
				GrdApi.GrdTRU_GenerateQuestionExInvoker = new PlatformInvoker("GrdTRU_GenerateQuestionEx", GrdDllName,
					typeof(GrdE), parameters, CallingConvention.Winapi, CharSet.Auto);
			}

			if (question == null)
				question = new byte[0];

			if (hash == null)
				hash = new byte[0];

			fixed (byte* pQuestion = question)
			fixed (uint* pID = &id)
			fixed (uint* pPublic = &publicCode)
			fixed (byte* pHash = hash)
				return (GrdE)GrdTRU_GenerateQuestionExInvoker.Invoke(hAddress, question.Length, new IntPtr(pQuestion), new IntPtr(pID),
					new IntPtr(pPublic), hash.Length, new IntPtr(pHash), 0, IntPtr.Zero);
		}

		/// <summary>
		/// Функция <b>GrdTRU_DecryptQuestionEx</b>
		/// </summary>
		/// <param name="grdHandle"></param>
		/// <param name="algNumDecrypt"></param>
		/// <param name="algNumHash"></param>
		/// <param name="question"></param>
		/// <param name="id"></param>
		/// <param name="publicCode"></param>
		/// <param name="hash"></param>
		/// <param name="truMode"></param>
		/// <returns></returns>
		public static GrdE GrdTRU_DecryptQuestionEx(Handle grdHandle, GrdAlgNum algNumDecrypt, GrdAlgNum algNumHash, byte[] question,
			uint id, uint publicCode, byte[] hash, GrdTRU truMode)
		{
			return GrdTRU_DecryptQuestionEx(grdHandle.Address, algNumDecrypt.Value, algNumHash.Value, question, id, publicCode, hash, (int)truMode);
		}

		private static unsafe GrdE GrdTRU_DecryptQuestionEx(IntPtr hAddress, int algNumDecrypt, int algNumHash,
			byte[] question, uint id, uint publicCode, byte[] hash, int truMode)
		{
			Type[] parameters = { typeof(IntPtr), typeof(int), typeof(int), 
				typeof(int), typeof(IntPtr), typeof(uint), typeof(uint),
				typeof(int), typeof(IntPtr), typeof(int), typeof(int), typeof(IntPtr)};

			if (GrdApi.GrdTRU_DecryptQuestionExInvoker == null)
			{
				GrdApi.GrdTRU_DecryptQuestionExInvoker = new PlatformInvoker("GrdTRU_DecryptQuestionEx", GrdDllName,
					typeof(GrdE), parameters, CallingConvention.Winapi, CharSet.Auto);
			}

			if (question == null)
				question = new byte[0];

			if (hash == null)
				hash = new byte[0];

			fixed (byte* pQuestion = question)
			fixed (byte* pHash = hash)
				return (GrdE)GrdTRU_DecryptQuestionExInvoker.Invoke(hAddress, algNumDecrypt, algNumHash,
				question.Length, new IntPtr(pQuestion), id, publicCode,
				hash.Length, new IntPtr(pHash), truMode, 0, IntPtr.Zero);
		}

		/// <summary>
		/// Функция <b>GrdTRU_EncryptAnswerEx</b>
		/// </summary>
		/// <param name="grdHandle"></param>
		/// <param name="addr"></param>
		/// <param name="data"></param>
		/// <param name="question"></param>
		/// <param name="algNumEncrypt"></param>
		/// <param name="algNumHash"></param>
		/// <param name="answer"></param>
		/// <param name="truMode"></param>
		/// <returns></returns>
		public static GrdE GrdTRU_EncryptAnswerEx(Handle grdHandle, uint addr, byte[] data, byte[] question,
			GrdAlgNum algNumEncrypt, GrdAlgNum algNumHash, out byte[] answer, GrdTRU truMode)
		{
			return GrdTRU_EncryptAnswerEx(grdHandle.Address, addr, data, question,
				algNumEncrypt.Value, algNumHash.Value, out answer, (int)truMode);
		}

		private static unsafe GrdE GrdTRU_EncryptAnswerEx(IntPtr hAddress, uint addr, byte[] data, byte[] question,
			int algNumEncrypt, int algNumHash, out byte[] answer, int truMode)
		{
			Type[] parameters = { typeof(IntPtr), typeof(uint), typeof(int), typeof(IntPtr),
				typeof(int), typeof(IntPtr), typeof(int), typeof(int), typeof(IntPtr), typeof(IntPtr),
				typeof(int), typeof(int), typeof(IntPtr)};

			if (GrdApi.GrdTRU_EncryptAnswerExInvoker == null)
			{
				GrdApi.GrdTRU_EncryptAnswerExInvoker = new PlatformInvoker("GrdTRU_EncryptAnswerEx", GrdDllName,
					typeof(GrdE), parameters, CallingConvention.Winapi, CharSet.Auto);
			}

			if (data == null)
				data = new byte[0];

			if (question == null)
				question = new byte[0];

			GrdE nGrdE;
			int answerSize = data.Length * 3 + 128;
			byte[] _answer = new byte[answerSize];

			fixed (byte* pData = data)
			fixed (byte* pQuestion = question)
			fixed (byte* pAnswer = _answer)
				nGrdE = (GrdE)GrdTRU_EncryptAnswerExInvoker.Invoke(hAddress, addr, data.Length, new IntPtr(pData),
					question.Length, new IntPtr(pQuestion), algNumEncrypt, algNumHash, new IntPtr(pAnswer), new IntPtr(&answerSize),
					truMode, 0, IntPtr.Zero);

			answer = new byte[answerSize];
			Array.Copy(_answer, answer, answerSize);
			return nGrdE;
		}


		/// <summary>
		/// Функция <b>GrdTRU_SetAnswerProperties</b>
		/// </summary>
		/// <param name="grdHandle"></param>
		/// <param name="truFlags"></param>
		/// <param name="wrProt"></param>
		/// <param name="rdProt"></param>
		/// <param name="numAlg"></param>
		/// <param name="tableLMS"></param>
		/// <param name="globalFlags"></param>
		/// <returns></returns>
		public static GrdE GrdTRU_SetAnswerProperties(Handle grdHandle, GrdTRU truFlags, uint wrProt, uint rdProt,
			int numAlg, int tableLMS, GrdGF globalFlags)
		{
			Type[] parameters = { typeof(IntPtr), typeof(GrdTRU), typeof(int), 
				typeof(uint), typeof(uint), typeof(int), typeof(int), typeof(GrdGF), typeof(IntPtr) };
			if (GrdApi.GrdTRU_SetAnswerPropertiesInvoker == null)
			{
				GrdApi.GrdTRU_SetAnswerPropertiesInvoker = new PlatformInvoker("GrdTRU_SetAnswerProperties", GrdDllName,
					typeof(GrdE), parameters, CallingConvention.Winapi, CharSet.Auto);

			}
			return (GrdE)GrdTRU_SetAnswerPropertiesInvoker.Invoke(grdHandle.Address, truFlags, 0,
				wrProt, rdProt, numAlg, tableLMS, globalFlags, IntPtr.Zero);
		}

		/// <summary>
		/// Функция <b>GrdTRU_ApplyAnswer</b>
		/// </summary>
		/// <param name="grdHandle"></param>
		/// <param name="answer"></param>
		/// <returns></returns>
		public static GrdE GrdTRU_ApplyAnswer(Handle grdHandle, byte[] answer)
		{
			return GrdTRU_ApplyAnswer(grdHandle.Address, answer);
		}

		private static unsafe GrdE GrdTRU_ApplyAnswer(IntPtr hAddress, byte[] answer)
		{
			Type[] parameters = { typeof(IntPtr), typeof(IntPtr), typeof(int) };
			if (GrdApi.GrdTRU_ApplyAnswerInvoker == null)
			{
				GrdApi.GrdTRU_ApplyAnswerInvoker = new PlatformInvoker("GrdTRU_ApplyAnswer", GrdDllName,
					typeof(GrdE), parameters, CallingConvention.Winapi, CharSet.Auto);
			}

			if (answer == null)
				answer = new byte[0];

			fixed (byte* pAnswer = answer)
				return (GrdE)GrdTRU_ApplyAnswerInvoker.Invoke(hAddress, new IntPtr(pAnswer), answer.Length);
		}

		#region Функции доверенного удаленного обновления для ключей Time
		/// \defgroup GRD_TRU_TIME Функции доверенного удаленного обновления для ключей Time
		/// \{
		/// Функции, реализующие технологию <b>Trusted Remote Update</b> для ключей с часами реального времени (Guardant Time, Guardant Time Net, Guardant Code Time) имеют некоторые отличия от своих аналогов для обычных ключей, и поэтому выделяются в особую подгруппу

		/// <summary>
		/// Функция <b>GrdTRU_GenerateQuestionTimeEx</b>
		/// </summary>
		/// <param name="grdHandle"></param>
		/// <param name="question"></param>
		/// <param name="id"></param>
		/// <param name="publicCode"></param>
		/// <param name="dongleTime"></param>
		/// <param name="deadTimes"></param>
		/// <param name="deadTimesNumbers"></param>
		/// <param name="hash"></param>
		/// <returns></returns>

		public static GrdE GrdTRU_GenerateQuestionTimeEx(Handle grdHandle, out byte[] question, out uint id, out uint publicCode,
			out ulong dongleTime, ulong[] deadTimes, out int deadTimesNumbers, out byte[] hash)
		{
			uint ModelID;
			id = 0;
			publicCode = 0;
			dongleTime = 0;
			deadTimesNumbers = 0;
			question = new byte[0];
			hash = new byte[0];

			GrdE nGrdE = GrdApi.GrdGetInfo(grdHandle, GrdGIL.Model, out ModelID);
			if (nGrdE != GrdE.OK)
				return nGrdE;

			if (ModelID == (uint)GrdDM.GCU)
			{
				question = new byte[16];
				hash = new byte[32];
			}
			else
			{
				question = new byte[8];
				hash = new byte[8];
			}

			return GrdTRU_GenerateQuestionTimeEx(grdHandle.Address, question, out id, out publicCode,
				out dongleTime, deadTimes, out deadTimesNumbers, hash);
		}

		private static unsafe GrdE GrdTRU_GenerateQuestionTimeEx(IntPtr hAddress, byte[] question, out uint id, out uint publicCode,
			out ulong dongleTime, ulong[] deadTimes, out int deadTimesNumbers, byte[] hash)
		{
			Type[] parameters = { typeof(IntPtr), typeof(int), typeof(IntPtr),
				typeof(IntPtr), typeof(IntPtr), typeof(IntPtr), typeof(int), typeof(IntPtr),
				typeof(IntPtr), typeof(int), typeof(IntPtr), typeof(int), typeof(IntPtr) };

			if (GrdApi.GrdTRU_GenerateQuestionTimeExInvoker == null)
			{
				GrdApi.GrdTRU_GenerateQuestionTimeExInvoker = new PlatformInvoker("GrdTRU_GenerateQuestionTimeEx", GrdDllName,
					typeof(GrdE), parameters, CallingConvention.Winapi, CharSet.Auto);
			}

			if (question == null)
				question = new byte[0];

			if (hash == null)
				hash = new byte[0];

			if (deadTimes == null)
				deadTimes = new ulong[0];

			fixed (byte* pQuestion = question)
			fixed (uint* pID = &id)
			fixed (uint* pPublicCode = &publicCode)
			fixed (ulong* pDongleTime = &dongleTime)
			fixed (ulong* pDeadTimes = deadTimes)
			fixed (int* pDeadTimesNumbers = &deadTimesNumbers)
			fixed (byte* pHash = hash)
				return (GrdE)GrdTRU_GenerateQuestionTimeExInvoker.Invoke(hAddress, question.Length, new IntPtr(pQuestion),
				new IntPtr(pID), new IntPtr(pPublicCode), new IntPtr(pDongleTime), deadTimes.Length, new IntPtr(pDeadTimes),
				new IntPtr(pDeadTimesNumbers), hash.Length, new IntPtr(pHash), 0, IntPtr.Zero);
		}

		/// <summary>
		/// Функция <b>GrdTRU_DecryptQuestionTimeEx</b>
		/// </summary>
		/// <param name="grdHandle"></param>
		/// <param name="algNumDecrypt"></param>
		/// <param name="algNumHash"></param>
		/// <param name="question"></param>
		/// <param name="id"></param>
		/// <param name="publicCode"></param>
		/// <param name="dongleTime"></param>
		/// <param name="deadTimes"></param>
		/// <param name="deadTimesNumbers"></param>
		/// <param name="hash"></param>
		/// <param name="truMode"></param>
		/// <returns></returns>
		public static GrdE GrdTRU_DecryptQuestionTimeEx(Handle grdHandle, GrdAlgNum algNumDecrypt, GrdAlgNum algNumHash, byte[] question,
			uint id, uint publicCode, ref ulong dongleTime, ulong[] deadTimes, int deadTimesNumbers, byte[] hash, GrdTRU truMode)
		{
			return GrdTRU_DecryptQuestionTimeEx(grdHandle.Address, algNumDecrypt.Value, algNumHash.Value, question,
				id, publicCode, ref dongleTime, deadTimes, deadTimesNumbers, hash, (int)truMode);
		}

		private static unsafe GrdE GrdTRU_DecryptQuestionTimeEx(IntPtr hAddress, int algNumDecrypt, int algNumHash, byte[] question,
			uint id, uint publicCode, ref ulong dongleTime, ulong[] deadTimes, int deadTimesNumbers, byte[] hash, int truMode)
		{
			Type[] parameters = { typeof(IntPtr), typeof(int), typeof(int), 
				typeof(int), typeof(IntPtr), typeof(uint), typeof(uint), typeof(IntPtr), typeof(IntPtr), 
				typeof(int), typeof(int), typeof(IntPtr), typeof(int), typeof(int),typeof(IntPtr)};

			if (GrdApi.GrdTRU_DecryptQuestionTimeExInvoker == null)
			{
				GrdApi.GrdTRU_DecryptQuestionTimeExInvoker = new PlatformInvoker("GrdTRU_DecryptQuestionTimeEx", GrdDllName,
					typeof(GrdE), parameters, CallingConvention.Winapi, CharSet.Auto);
			}

			if (question == null)
				question = new byte[0];

			if (hash == null)
				hash = new byte[0];

			if (deadTimes == null)
				deadTimes = new ulong[0];

			fixed (byte* pQuestion = question)
			fixed (ulong* pDongleTime = &dongleTime)
			fixed (ulong* pDeadTimes = deadTimes)
			fixed (byte* pHash = hash)
				return (GrdE)GrdTRU_DecryptQuestionTimeExInvoker.Invoke(hAddress, algNumDecrypt, algNumHash,
				question.Length, new IntPtr(pQuestion), id, publicCode, new IntPtr(pDongleTime), new IntPtr(pDeadTimes),
				deadTimesNumbers, hash.Length, new IntPtr(pHash), truMode,0, IntPtr.Zero);
		}

		/// \}
		#endregion //Функции доверенного удаленного обновления для ключей Time


		#region Устаревшие функции доверенного удаленного обновления
		/// \defgroup GRD_TRU_OLD Устаревшие функции доверенного удаленного обновления
		/// \{

		/// <summary>
		/// Функция <b>GrdTRU_GenerateQuestion</b>
		/// </summary>
		/// <param name="hGrd"></param>
		/// <param name="question"></param>
		/// <param name="id"></param>
		/// <param name="publicCode"></param>
		/// <param name="hash"></param>
		/// <returns></returns>
		public static GrdE GrdTRU_GenerateQuestion(Handle grdHandle, out byte[] question, out uint id, out uint publicCode, out byte[] hash)
		{
			question = new byte[8];
			hash = new byte[8];
			return GrdTRU_GenerateQuestion(grdHandle.Address, question, out id, out publicCode, hash);
		}

		private static unsafe GrdE GrdTRU_GenerateQuestion(IntPtr hAddress, byte[] question, out uint id, out uint publicCode, byte[] hash)
		{
			Type[] parameters = { typeof(IntPtr), typeof(IntPtr), typeof(IntPtr), typeof(IntPtr), typeof(IntPtr) };
			if (GrdApi.GrdTRU_GenerateQuestionInvoker == null)
			{
				GrdApi.GrdTRU_GenerateQuestionInvoker = new PlatformInvoker("GrdTRU_GenerateQuestion", GrdDllName,
					typeof(GrdE), parameters, CallingConvention.Winapi, CharSet.Auto);
			}

			if (question == null)
				question = new byte[0];

			if (hash == null)
				hash = new byte[0];

			fixed (byte* pQuestion = question)
			fixed (uint* pID = &id)
			fixed (uint* pPublic = &publicCode)
			fixed (byte* pHash = hash)
				return (GrdE)GrdTRU_GenerateQuestionInvoker.Invoke(hAddress, new IntPtr(pQuestion),
					new IntPtr(pID), new IntPtr(pPublic), new IntPtr(pHash));
		}

		/// <summary>
		/// Функция <b>GrdTRU_GenerateQuestionTime</b>
		/// Эту функцию неподходит для ключей Guardant Code Time. Используйте <b>GrdTRU_GenerateQuestionTimeEx</b>.
		/// </summary>
		/// <param name="grdHandle"></param>
		/// <param name="question"></param>
		/// <param name="id"></param>
		/// <param name="publicCode"></param>
		/// <param name="dongleTime"></param>
		/// <param name="deadTimes"></param>
		/// <param name="deadTimesNumbers"></param>
		/// <param name="hash"></param>
		/// <returns></returns>
		public static GrdE GrdTRU_GenerateQuestionTime(Handle grdHandle, out byte[] question, out uint id, out uint publicCode,
			out ulong dongleTime, ulong[] deadTimes, out int deadTimesNumbers, out byte[] hash)
		{
			question = new byte[8];
			hash = new byte[8];

			return GrdTRU_GenerateQuestionTime(grdHandle.Address, question, out id, out publicCode,
				out dongleTime, deadTimes, out deadTimesNumbers, hash);
		}

		private static unsafe GrdE GrdTRU_GenerateQuestionTime(IntPtr hAddress, byte[] question, out uint id, out uint publicCode,
			out ulong dongleTime, ulong[] deadTimes, out int deadTimesNumbers, byte[] hash)
		{
			Type[] parameters = { typeof(IntPtr), typeof(IntPtr), typeof(IntPtr), typeof(IntPtr),
				typeof(IntPtr), typeof(int), typeof(IntPtr),typeof(IntPtr), typeof(IntPtr), typeof(IntPtr) };

			if (GrdApi.GrdTRU_GenerateQuestionTimeInvoker == null)
			{
				GrdApi.GrdTRU_GenerateQuestionTimeInvoker = new PlatformInvoker("GrdTRU_GenerateQuestionTime", GrdDllName,
					typeof(GrdE), parameters, CallingConvention.Winapi, CharSet.Auto);
			}

			if (question == null)
				question = new byte[0];

			if (hash == null)
				hash = new byte[0];

			if (deadTimes == null)
				deadTimes = new ulong[0];

			fixed (byte* pQuestion = question)
			fixed (uint* pID = &id)
			fixed (uint* pPublicCode = &publicCode)
			fixed (ulong* pDongleTime = &dongleTime)
			fixed (ulong* pDeadTimes = deadTimes)
			fixed (int* pDeadTimesNumbers = &deadTimesNumbers)
			fixed (byte* pHash = hash)
				return (GrdE)GrdTRU_GenerateQuestionTimeInvoker.Invoke(hAddress, new IntPtr(pQuestion), new IntPtr(pID), new IntPtr(pPublicCode),
					new IntPtr(pDongleTime), deadTimes.Length * 8, new IntPtr(pDeadTimes), new IntPtr(pDeadTimesNumbers), new IntPtr(pHash), IntPtr.Zero);
		}

		/// <summary>
		/// Функция <b>GrdTRU_DecryptQuestion</b>
		/// Эту функцию неподходит для ключей Guardant Code Time. Используйте <b>GrdTRU_DecryptQuestionEx</b>.
		/// </summary>
		/// <param name="grdHandle"></param>
		/// <param name="algNum_GSII64"></param>
		/// <param name="algNum_HashS3"></param>
		/// <param name="question"></param>
		/// <param name="id"></param>
		/// <param name="publicCode"></param>
		/// <param name="hash"></param>
		/// <returns></returns>
		public static GrdE GrdTRU_DecryptQuestion(Handle grdHandle, GrdAlgNum algNum_GSII64, GrdAlgNum algNum_HashS3, byte[] question,
			uint id, uint publicCode, byte[] hash)
		{
			return GrdTRU_DecryptQuestion(grdHandle.Address, algNum_GSII64.Value, algNum_HashS3.Value, question, id, publicCode, hash);
		}

		private static unsafe GrdE GrdTRU_DecryptQuestion(IntPtr hAddress, int algNum_GSII64, int algNum_HashS3, byte[] question,
			uint id, uint publicCode, byte[] hash)
		{
			Type[] parameters = { typeof(IntPtr), typeof(int), typeof(int), typeof(IntPtr), 
				typeof(uint), typeof(uint), typeof(IntPtr) };

			if (GrdApi.GrdTRU_DecryptQuestionInvoker == null)
			{
				GrdApi.GrdTRU_DecryptQuestionInvoker = new PlatformInvoker("GrdTRU_DecryptQuestion", GrdDllName,
					typeof(GrdE), parameters, CallingConvention.Winapi, CharSet.Auto);
			}

			if (question == null)
				question = new byte[0];

			if (hash == null)
				hash = new byte[0];

			fixed (byte* pQuestion = question)
			fixed (byte* pHash = hash)
				return (GrdE)GrdTRU_DecryptQuestionInvoker.Invoke(hAddress, algNum_GSII64, algNum_HashS3, new IntPtr(pQuestion),
					id, publicCode, new IntPtr(pHash));
		}

		/// <summary>
		/// Функция <b>GrdTRU_DecryptQuestionTime</b>
		/// </summary>
		/// <param name="grdHandle"></param>
		/// <param name="algNum_GSII64"></param>
		/// <param name="algNum_HashS3"></param>
		/// <param name="question"></param>
		/// <param name="id"></param>
		/// <param name="publicCode"></param>
		/// <param name="dongleTime"></param>
		/// <param name="deadTimes"></param>
		/// <param name="deadTimesNumbers"></param>
		/// <param name="hash"></param>
		/// <returns></returns>
		public static GrdE GrdTRU_DecryptQuestionTime(Handle grdHandle, GrdAlgNum algNum_GSII64, GrdAlgNum algNum_HashS3, byte[] question,
			uint id, uint publicCode, ref ulong dongleTime, ulong[] deadTimes, int deadTimesNumbers, byte[] hash)
		{
			return GrdTRU_DecryptQuestionTime(grdHandle.Address, algNum_GSII64.Value, algNum_HashS3.Value, question,
				id, publicCode, ref dongleTime, deadTimes, deadTimesNumbers, hash);
		}

		private static unsafe GrdE GrdTRU_DecryptQuestionTime(IntPtr hAddress, int algNum_GSII64, int algNum_HashS3, byte[] question,
			uint id, uint publicCode, ref ulong dongleTime, ulong[] deadTimes, int deadTimesNumbers, byte[] hash)
		{
			Type[] parameters = { typeof(IntPtr), typeof(int), typeof(int), 
				typeof(IntPtr), typeof(uint), typeof(uint), typeof(IntPtr), typeof(IntPtr), 
				typeof(int), typeof(IntPtr)};

			if (GrdApi.GrdTRU_DecryptQuestionTimeInvoker == null)
			{
				GrdApi.GrdTRU_DecryptQuestionTimeInvoker = new PlatformInvoker("GrdTRU_DecryptQuestionTime", GrdDllName,
					typeof(GrdE), parameters, CallingConvention.Winapi, CharSet.Auto);
			}

			if (question == null)
				question = new byte[0];

			if (hash == null)
				hash = new byte[0];

			if (deadTimes == null)
				deadTimes = new ulong[0];

			fixed (byte* pQuestion = question)
			fixed (ulong* pDongleTime = &dongleTime)
			fixed (ulong* pDeadTimes = deadTimes)
			fixed (byte* pHash = hash)
				return (GrdE)GrdTRU_DecryptQuestionTimeInvoker.Invoke(hAddress, algNum_GSII64, algNum_HashS3,
				new IntPtr(pQuestion), id, publicCode, new IntPtr(pDongleTime), new IntPtr(pDeadTimes),
				deadTimesNumbers, new IntPtr(pHash));
		}


		/// <summary>
		/// Функция <b>GrdTRU_EncryptAnswer</b>
		/// </summary>
		/// <param name="grdHandle"></param>
		/// <param name="addr"></param>
		/// <param name="data"></param>
		/// <param name="question"></param>
		/// <param name="algNum_GSII64"></param>
		/// <param name="algNum_HashS3"></param>
		/// <param name="answer"></param>
		/// <returns></returns>
		public static GrdE GrdTRU_EncryptAnswer(Handle grdHandle, uint addr, byte[] data, byte[] question,
			int algNum_GSII64, int algNum_HashS3, out byte[] answer)
		{
			return GrdTRU_EncryptAnswer(grdHandle.Address, addr, data, question, algNum_GSII64, algNum_HashS3, out answer);
		}

		private static unsafe GrdE GrdTRU_EncryptAnswer(IntPtr hAddress, uint addr, byte[] data, byte[] question,
			int algNum_GSII64, int algNum_HashS3, out byte[] answer)
		{
			Type[] parameters = { typeof(IntPtr), typeof(uint), typeof(int), typeof(IntPtr),
				typeof(IntPtr), typeof(int), typeof(int), typeof(IntPtr), typeof(IntPtr) };

			if (GrdApi.GrdTRU_EncryptAnswerInvoker == null)
			{
				GrdApi.GrdTRU_EncryptAnswerInvoker = new PlatformInvoker("GrdTRU_EncryptAnswer", GrdDllName,
					typeof(GrdE), parameters, CallingConvention.Winapi, CharSet.Auto);
			}

			if (data == null)
				data = new byte[0];

			if (question == null)
				question = new byte[0];

			GrdE nGrdE;
			int answerSize = data.Length * 3 + 128;
			byte[] _answer = new byte[answerSize];

			fixed (byte* pData = data)
			fixed (byte* pQuestion = question)
			fixed (byte* pAnswer = _answer)
				nGrdE = (GrdE)GrdTRU_EncryptAnswerInvoker.Invoke(hAddress, addr, data.Length, new IntPtr(pData),
					new IntPtr(pQuestion), algNum_GSII64, algNum_HashS3, new IntPtr(pAnswer), new IntPtr(&answerSize));

			answer = new byte[answerSize];
			Array.Copy(_answer, answer, answerSize);
			return nGrdE;
		}

		/// \}
		#endregion //Устаревшие функции доверенного удаленного обновления

		/// \}
		#endregion //Функции доверенного удаленного обновления

		#region Функции для ключей Time
		/// \defgroup grdTime Функции для ключей Time 
		/// \{
		/// Ключи "Guardant Time/Time Net/Code Time" обладают встроенными часами реального времени (Real-Time Clock, RTC), что позволяет ограничивать астрономическое время работы приложения путем установки временных зависимостей от аппаратных алгоритмов ключа. Для работы с возможностями, которые присущи только ключам с RTC, существуют специальные функции "Guardant API"

		/// <summary>
		/// Функция <b>GrdSetTime</b>
		/// </summary>
		/// <param name="hGrd"></param>
		/// <param name="systemTime"></param>
		/// <returns></returns>
		public static GrdE GrdSetTime(Handle grdHandle, GrdSystemTime systemTime)
		{
			return GrdSetTime(grdHandle.Address, systemTime);
		}

		private static unsafe GrdE GrdSetTime(IntPtr hAddress, GrdSystemTime systemTime)
		{
			Type[] parameters = { typeof(IntPtr), typeof(IntPtr), typeof(IntPtr) };
			if (GrdApi.GrdSetTimeInvoker == null)
			{
				GrdApi.GrdSetTimeInvoker = new PlatformInvoker("GrdSetTime", GrdDllName,
					typeof(GrdE), parameters, CallingConvention.Winapi, CharSet.Auto);
			}
			return (GrdE)GrdSetTimeInvoker.Invoke(hAddress, new IntPtr(&systemTime), IntPtr.Zero);
		}

		/// <summary>
		/// Функция <b>GrdGetTime</b>
		/// </summary>
		/// <param name="grdHandle"></param>
		/// <param name="systemTime"></param>
		/// <returns></returns>
		public static GrdE GrdGetTime(Handle grdHandle, out GrdSystemTime systemTime)
		{
			return GrdGetTime(grdHandle.Address, out systemTime);
		}

		private static unsafe GrdE GrdGetTime(IntPtr hAddress, out GrdSystemTime systemTime)
		{
			Type[] parameters = { typeof(IntPtr), typeof(IntPtr), typeof(IntPtr) };
			if (GrdApi.GrdGetTimeInvoker == null)
			{
				GrdApi.GrdGetTimeInvoker = new PlatformInvoker("GrdGetTime", GrdDllName,
					typeof(GrdE), parameters, CallingConvention.Winapi, CharSet.Auto);
			}
			fixed (GrdSystemTime* pSystemTime = &systemTime)
				return (GrdE)GrdGetTimeInvoker.Invoke(hAddress, new IntPtr(pSystemTime), IntPtr.Zero);
		}

		/// <summary>
		/// Функция <b>GrdPI_GetTimeLimit</b>
		/// </summary>
		/// <param name="grdHandle"></param>
		/// <param name="algNum"></param>
		/// <param name="systemTime"></param>
		/// <returns></returns>
		public static GrdE GrdPI_GetTimeLimit(Handle grdHandle, GrdAlgNum algNum, out GrdSystemTime systemTime)
		{
			return GrdPI_GetTimeLimit(grdHandle.Address, algNum.Value, out systemTime);
		}

		private static unsafe GrdE GrdPI_GetTimeLimit(IntPtr hAddress, int algNum, out GrdSystemTime systemTime)
		{
			Type[] parameters = { typeof(IntPtr), typeof(int), typeof(IntPtr), typeof(IntPtr) };

			if (GrdApi.GrdPI_GetTimeLimitInvoker == null)
			{
				GrdApi.GrdPI_GetTimeLimitInvoker = new PlatformInvoker("GrdPI_GetTimeLimit", GrdDllName,
					typeof(GrdE), parameters, CallingConvention.Winapi, CharSet.Auto);
			}
			fixed (GrdSystemTime* pSystemTime = &systemTime)
				return (GrdE)GrdPI_GetTimeLimitInvoker.Invoke(hAddress, algNum, new IntPtr(pSystemTime), IntPtr.Zero);
		}

		/// <summary>
		/// Функция <b>GrdMakeSystemTime</b>
		/// </summary>
		/// <param name="grdHandle"></param>
		/// <param name="year"></param>
		/// <param name="month"></param>
		/// <param name="dayOfWeek"></param>
		/// <param name="day"></param>
		/// <param name="hour"></param>
		/// <param name="minute"></param>
		/// <param name="second"></param>
		/// <param name="milliseconds"></param>
		/// <param name="systemTime"></param>
		/// <returns></returns>
		public static GrdE GrdMakeSystemTime(Handle grdHandle, ushort year, ushort month, ushort dayOfWeek, ushort day,
			ushort hour, ushort minute, ushort second, ushort milliseconds, out GrdSystemTime systemTime)
		{
				return GrdMakeSystemTime(grdHandle.Address, year, month, dayOfWeek, day,
					hour, minute, second, milliseconds, out systemTime);
		}

		private static unsafe GrdE GrdMakeSystemTime(IntPtr hAddress, ushort year, ushort month, ushort dayOfWeek, ushort day,
			ushort hour, ushort minute, ushort second, ushort milliseconds, out GrdSystemTime systemTime)
		{
			Type[] parameters = { typeof(IntPtr), typeof(ushort), typeof(ushort), typeof(ushort),
				typeof(ushort), typeof(ushort), typeof(ushort), typeof(ushort), typeof(ushort), typeof(IntPtr) };

			if (GrdApi.GrdMakeSystemTimeInvoker == null)
			{
				GrdApi.GrdMakeSystemTimeInvoker = new PlatformInvoker("GrdMakeSystemTime", GrdDllName,
					typeof(GrdE), parameters, CallingConvention.Winapi, CharSet.Auto);
			}

			fixed (GrdSystemTime* pSystemTime = &systemTime)
				return (GrdE)GrdMakeSystemTimeInvoker.Invoke( hAddress, year, month, dayOfWeek, day, 
					hour, minute, second, milliseconds, new IntPtr(pSystemTime));
		}

		/// <summary>
		/// Функция <b>GrdSplitSystemTime</b>
		/// </summary>
		/// <param name="grdHandle"></param>
		/// <param name="systemTime"></param>
		/// <param name="year"></param>
		/// <param name="month"></param>
		/// <param name="dayOfWeek"></param>
		/// <param name="day"></param>
		/// <param name="hour"></param>
		/// <param name="minute"></param>
		/// <param name="second"></param>
		/// <param name="milliseconds"></param>
		/// <returns></returns>
		public static GrdE GrdSplitSystemTime(Handle grdHandle, GrdSystemTime systemTime, out ushort year, out ushort month, out ushort dayOfWeek, out ushort day,
			out ushort hour, out ushort minute, out ushort second, out ushort milliseconds)
		{
			return GrdSplitSystemTime(grdHandle.Address, systemTime, out year, out month, out dayOfWeek, out day,
				out hour, out minute, out second, out milliseconds);
		}

		private static unsafe GrdE GrdSplitSystemTime(IntPtr hAddress, GrdSystemTime systemTime, out ushort year, out ushort month, out ushort dayOfWeek, out ushort day,
			out ushort hour, out ushort minute, out ushort second, out ushort milliseconds)
		{
			Type[] parameters = { typeof(IntPtr), typeof(IntPtr), typeof(IntPtr),
				typeof(IntPtr), typeof(IntPtr), typeof(IntPtr), typeof(IntPtr),
				typeof(IntPtr), typeof(IntPtr), typeof(IntPtr) };

			if (GrdApi.GrdSplitSystemTimeInvoker == null)
			{
				GrdApi.GrdSplitSystemTimeInvoker = new PlatformInvoker("GrdSplitSystemTime", GrdDllName,
					typeof(GrdE), parameters, CallingConvention.Winapi, CharSet.Auto);
			}
			fixed (ushort* pYear = &year)
			fixed (ushort* pMonth = &month)
			fixed (ushort* pDayOfWeek = &dayOfWeek)
			fixed (ushort* pDay = &day)
			fixed (ushort* pHour = &hour)
			fixed (ushort* pMinute = &minute)
			fixed (ushort* pSecond = &second)
			fixed (ushort* pMilliseconds = &milliseconds)
				return (GrdE)GrdApi.GrdSplitSystemTimeInvoker.Invoke(hAddress, new IntPtr(&systemTime),
					new IntPtr(pYear), new IntPtr(pMonth), new IntPtr(pDayOfWeek), new IntPtr(pDay),
					new IntPtr(pHour), new IntPtr(pMinute), new IntPtr(pSecond), new IntPtr(pMilliseconds));
		}

		/// \}
		#endregion //Функции для ключей Time

		#region Функции для ключа Guardant Code
		/// \defgroup GrdCode Функции для ключа Guardant Code
		/// \{
		/// Технология <b>Загружаемый Код</b> может быть реализована не только при помощи утилит, входящих в комплект разработчика. При желании разработчики могут встраивать поддержку этой технологии непосредственно в свои приложения, используя набор предназначенных для этой цели функций.

		/// <summary>
		/// Функция <b>GrdCodeGetInfo</b>
		/// </summary>
		/// <param name="grdHandle"></param>
		/// <param name="algNum"></param>
		/// <param name="grdCodeInfo"></param>
		/// <returns></returns>
		public static GrdE GrdCodeGetInfo(Handle grdHandle, GrdAlgNum algNum, out GrdCodeInfo grdCodeInfo)
		{
			return GrdCodeGetInfo(grdHandle.Address, algNum.Value, out grdCodeInfo);
		}

		private static unsafe GrdE GrdCodeGetInfo(IntPtr hAddress, int algNum, out GrdCodeInfo grdCodeInfo)
		{
			Type[] parameters = { typeof(IntPtr), typeof(int), typeof(int), typeof(IntPtr), typeof(IntPtr) };

			if (GrdApi.GrdCodeGetInfoInvoker == null)
			{
				GrdApi.GrdCodeGetInfoInvoker = new PlatformInvoker("GrdCodeGetInfo", GrdDllName,
					typeof(GrdE), parameters, CallingConvention.Winapi, CharSet.Auto);
			}

			GrdCodeInfo _grdCodeInfo;
			GrdE nGrdE = (GrdE)GrdCodeGetInfoInvoker.Invoke(hAddress, algNum, sizeof(GrdCodeInfo), new IntPtr(&_grdCodeInfo), IntPtr.Zero);
			grdCodeInfo = _grdCodeInfo;
			return nGrdE;
		}

		/// <summary>
		/// Функция <b>GrdCodeLoad</b>
		/// </summary>
		/// <param name="grdHandle"></param>
		/// <param name="algNum"></param>
		/// <param name="fileData"></param>
		/// <returns></returns>
		public static GrdE GrdCodeLoad(Handle grdHandle, GrdAlgNum algNum, byte[] fileData)
		{
			return GrdCodeLoad(grdHandle.Address, algNum.Value, fileData);
		}

		private static unsafe GrdE GrdCodeLoad(IntPtr hAddress, int algNum, byte[] fileData)
		{
			Type[] parameters = { typeof(IntPtr), typeof(int), typeof(int), typeof(IntPtr), typeof(IntPtr) };

			if (GrdApi.GrdCodeLoadInvoker == null)
			{
				GrdApi.GrdCodeLoadInvoker = new PlatformInvoker("GrdCodeLoad", GrdDllName,
					typeof(GrdE), parameters, CallingConvention.Winapi, CharSet.Auto);
			}

			if (fileData == null)
				fileData = new byte[0];

			fixed (byte* pFileData = fileData)
				return (GrdE)GrdCodeLoadInvoker.Invoke( hAddress, algNum, fileData.Length, new IntPtr(pFileData), IntPtr.Zero);
		}

		/// <summary>
		/// Функция <b>GrdCodeRun</b>
		/// </summary>
		/// <param name="grdHandle"></param>
		/// <param name="algNum"></param>
		/// <param name="p1"></param>
		/// <param name="retCode"></param>
		/// <returns></returns>
		public static GrdE GrdCodeRun(Handle grdHandle, GrdAlgNum algNum, uint p1, out uint retCode)
		{
			return GrdCodeRun(grdHandle.Address, algNum.Value, p1, out retCode, 0, IntPtr.Zero, 0, IntPtr.Zero);
		}


		/// <summary>
		/// Функция <b>GrdCodeRun</b>
		/// </summary>
		/// <param name="grdHandle"></param>
		/// <param name="algNum"></param>
		/// <param name="p1"></param>
		/// <param name="retCode"></param>
		/// <param name="dataToDongle"></param>
		/// <returns></returns>
		public static unsafe GrdE GrdCodeRun(Handle grdHandle, GrdAlgNum algNum, uint p1, out uint retCode, byte[] dataToDongle)
		{
			if (dataToDongle == null)
				dataToDongle = new byte[0];

			fixed (byte* pDataToDongle = dataToDongle)
				return GrdCodeRun(grdHandle.Address, algNum.Value, p1, out retCode, 0, IntPtr.Zero, dataToDongle.Length, new IntPtr(pDataToDongle));
		}

		/// <summary>
		/// Функция <b>GrdCodeRun</b>
		/// </summary>
		/// <param name="grdHandle"></param>
		/// <param name="algNum"></param>
		/// <param name="p1"></param>
		/// <param name="retCode"></param>
		/// <param name="dataFromDongle"></param>
		/// <param name="dataToDongle"></param>
		/// <returns></returns>
		public static unsafe GrdE GrdCodeRun(Handle grdHandle, GrdAlgNum algNum, uint p1, out uint retCode, byte[] dataFromDongle, byte[] dataToDongle)
		{
			if (dataToDongle == null)
				dataToDongle = new byte[0];

			if (dataFromDongle == null)
				dataFromDongle = new byte[0];

			fixed (byte* pDataToDongle = dataToDongle)
			fixed (byte* pDataFromDongle = dataFromDongle)
				return GrdCodeRun(grdHandle.Address, algNum.Value, p1, out retCode,
					dataFromDongle.Length, new IntPtr(pDataFromDongle), dataToDongle.Length, new IntPtr(pDataToDongle));
		}

		/// <summary>
		/// Функция <b>GrdCodeRun</b>
		/// </summary>
		/// <param name="grdHandle"></param>
		/// <param name="algNum"></param>
		/// <param name="p1"></param>
		/// <param name="retCode"></param>
		/// <param name="dataFromDongleLng"></param>
		/// <param name="dataFromDongle"></param>
		/// <returns></returns>
		public static unsafe GrdE GrdCodeRun(Handle grdHandle, GrdAlgNum algNum, uint p1, out uint retCode, int dataFromDongleLng, out byte[] dataFromDongle)
		{
			dataFromDongle = new byte[dataFromDongleLng];

			fixed (byte* pDataFromDongle = dataFromDongle)
				return GrdCodeRun(grdHandle.Address, algNum.Value, p1, out retCode,
					dataFromDongle.Length, new IntPtr(pDataFromDongle), 0, IntPtr.Zero);
		}

		/// <summary>
		/// Функция <b>GrdCodeRun</b>
		/// </summary>
		/// <param name="grdHandle"></param>
		/// <param name="algNum"></param>
		/// <param name="p1"></param>
		/// <param name="retCode"></param>
		/// <param name="dataFromDongleLng"></param>
		/// <param name="dataFromDongle"></param>
		/// <param name="dataToDongle"></param>
		/// <returns></returns>
		public static unsafe GrdE GrdCodeRun(Handle grdHandle, GrdAlgNum algNum, uint p1, out uint retCode, int dataFromDongleLng, out byte[] dataFromDongle, byte[] dataToDongle)
		{
			dataFromDongle = new byte[dataFromDongleLng];

			if (dataToDongle == null)
				dataToDongle = new byte[0];

			fixed (byte* pDataToDongle = dataToDongle)
			fixed (byte* pDataFromDongle = dataFromDongle)
				return GrdCodeRun(grdHandle.Address, algNum.Value, p1, out retCode,
					dataFromDongle.Length, new IntPtr(pDataFromDongle), dataToDongle.Length, new IntPtr(pDataToDongle));
		}

		/// <summary>
		/// Функция <b>GrdCodeRun</b>
		/// </summary>
		/// <param name="grdHandle"></param>
		/// <param name="algNum"></param>
		/// <param name="p1"></param>
		/// <param name="retCode"></param>
		/// <param name="dataFromDongleLng"></param>
		/// <param name="dataFromDongle"></param>
		/// <param name="dataToDongle"></param>
		/// <returns></returns>
		public static unsafe GrdE GrdCodeRun(Handle grdHandle, GrdAlgNum algNum, uint p1, out uint retCode, int dataFromDongleLng, out short[] dataFromDongle, short[] dataToDongle)
		{
			dataFromDongle = new short[dataFromDongleLng];

			if (dataToDongle == null)
				dataToDongle = new short[0];

			fixed (short* pDataToDongle = dataToDongle)
			fixed (short* pDataFromDongle = dataFromDongle)
				return GrdCodeRun(grdHandle.Address, algNum.Value, p1, out retCode,
					dataFromDongle.Length*sizeof(short), new IntPtr(pDataFromDongle), dataToDongle.Length*sizeof(short), new IntPtr(pDataToDongle));
		}

		/// <summary>
		/// Функция <b>GrdCodeRun</b>
		/// </summary>
		/// <param name="grdHandle"></param>
		/// <param name="algNum"></param>
		/// <param name="p1"></param>
		/// <param name="retCode"></param>
		/// <param name="dataFromDongleLng"></param>
		/// <param name="dataFromDongle"></param>
		/// <param name="dataToDongle"></param>
		/// <returns></returns>
		public static unsafe GrdE GrdCodeRun(Handle grdHandle, GrdAlgNum algNum, uint p1, out uint retCode, int dataFromDongleLng, out int[] dataFromDongle, int[] dataToDongle)
		{
			dataFromDongle = new int[dataFromDongleLng];

			if (dataToDongle == null)
				dataToDongle = new int[0];

			fixed (int* pDataToDongle = dataToDongle)
			fixed (int* pDataFromDongle = dataFromDongle)
				return GrdCodeRun(grdHandle.Address, algNum.Value, p1, out retCode,
					dataFromDongle.Length*sizeof(int), new IntPtr(pDataFromDongle), dataToDongle.Length*sizeof(int), new IntPtr(pDataToDongle));
		}

		/// <summary>
		/// Функция <b>GrdCodeRun</b>
		/// </summary>
		/// <param name="grdHandle"></param>
		/// <param name="algNum"></param>
		/// <param name="p1"></param>
		/// <param name="retCode"></param>
		/// <param name="dataFromDongleLng"></param>
		/// <param name="dataFromDongle"></param>
		/// <param name="dataToDongle"></param>
		/// <returns></returns>
		public static unsafe GrdE GrdCodeRun(Handle grdHandle, GrdAlgNum algNum, uint p1, out uint retCode, int dataFromDongleLng, out long[] dataFromDongle, long[] dataToDongle)
		{
			dataFromDongle = new long[dataFromDongleLng];

			if (dataToDongle == null)
				dataToDongle = new long[0];

			fixed (long* pDataToDongle = dataToDongle)
			fixed (long* pDataFromDongle = dataFromDongle)
				return GrdCodeRun(grdHandle.Address, algNum.Value, p1, out retCode,
					dataFromDongle.Length*sizeof(long), new IntPtr(pDataFromDongle), dataToDongle.Length*sizeof(long), new IntPtr(pDataToDongle));
		}

		/// <summary>
		/// Функция <b>GrdCodeRun</b>
		/// </summary>
		/// <param name="grdHandle"></param>
		/// <param name="algNum"></param>
		/// <param name="p1"></param>
		/// <param name="retCode"></param>
		/// <param name="dataFromDongleLng"></param>
		/// <param name="dataFromDongle"></param>
		/// <param name="dataToDongle"></param>
		/// <returns></returns>
		public static unsafe GrdE GrdCodeRun(Handle grdHandle, GrdAlgNum algNum, uint p1, out uint retCode, int dataFromDongleLng, out double[] dataFromDongle, double[] dataToDongle)
		{
			dataFromDongle = new double[dataFromDongleLng];

			if (dataToDongle == null)
				dataToDongle = new double[0];

			fixed (double* pDataToDongle = dataToDongle)
			fixed (double* pDataFromDongle = dataFromDongle)
				return GrdCodeRun(grdHandle.Address, algNum.Value, p1, out retCode,
					dataFromDongle.Length*sizeof(double), new IntPtr(pDataFromDongle), dataToDongle.Length * sizeof(double), new IntPtr(pDataToDongle));
		}

		private static unsafe GrdE GrdCodeRun(IntPtr hAddress, int algNum, uint p1, out uint retCode, 
			int dataFromDongleLng, IntPtr pDataFromDongle, int dataToDongleLng, IntPtr pDataToDongle)
		{
			Type[] parameters = { typeof(IntPtr), typeof(int), typeof(uint),  typeof(IntPtr),
				typeof(int), typeof(IntPtr), typeof(int), typeof(IntPtr) ,typeof(IntPtr)};

			if (GrdApi.GrdCodeRunInvoker == null)
			{
				GrdApi.GrdCodeRunInvoker = new PlatformInvoker("GrdCodeRun", GrdDllName,
					typeof(GrdE), parameters, CallingConvention.Winapi, CharSet.Auto);
			}

			fixed(uint* pRetCode = &retCode)
				return (GrdE)GrdCodeRunInvoker.Invoke(hAddress, algNum, p1, new IntPtr(pRetCode),
					dataFromDongleLng, pDataFromDongle, dataToDongleLng, pDataToDongle, IntPtr.Zero);
		}

		/// <summary>
        /// Функция <b>GrdSetDriverMode</b> задает USB-режим работы ключей Guardant Code/Code Time: через драйвер HID, WINUSB или Guardant
		/// </summary>
		/// <param name="grdHandle"></param>
		/// <param name="mode"></param>
		/// <returns></returns>
		public static GrdE GrdSetDriverMode(Handle grdHandle, GrdDR mode)
		{
			Type[] parameters = { typeof(IntPtr), typeof(GrdDR), typeof(IntPtr) };

			if (GrdApi.GrdSetDriverModeInvoker == null)
			{
				GrdApi.GrdSetDriverModeInvoker = new PlatformInvoker("GrdSetDriverMode", GrdDllName,
					typeof(GrdE), parameters, CallingConvention.Winapi, CharSet.Auto);

			}
			return (GrdE)GrdSetDriverModeInvoker.Invoke(grdHandle.Address, mode, IntPtr.Zero);
		}

		/// \}
		#endregion //Функции для ключа Guardant Code

		#region Guardant Dongle Notification API
		/// \defgroup GrdCode Guardant Dongle Notification API
		/// \{

		/// <summary>
		/// Функция <b>GrdInitializeNotificationAPI </b>Функция инициализации Notification API.</b>.
		/// </summary>
		/// <returns></returns>
		public static GrdE GrdInitializeNotificationAPI()
		{
			try
			{
				if (GrdInitializeNotificationAPIInvoker == null)
				{
					SetApiPlatform();
					GrdInitializeNotificationAPIInvoker = new PlatformInvoker("GrdInitializeNotificationAPI", GrdDllName,
						typeof(GrdE), null, CallingConvention.Winapi, CharSet.Auto);
				}
				return (GrdE)GrdInitializeNotificationAPIInvoker.Invoke();
			}
			catch (Exception e)
			{
				return GrdException(e);
			}
		}

		/// <summary>
		/// Функция <b>GrdRegisterDongleNotification</b>Функция регистрации callback функции.</b>.
		/// </summary>
		/// <param name="grdHandle"></param>
		/// <param name="dongleNotifyCallBackDelegate"></param>
		/// <returns></returns>
		public static GrdE GrdRegisterDongleNotification(Handle grdHandle, GrdDongleNotifyCallBackDelegate dongleNotifyCallBackDelegate)
		{
			try
			{
				IntPtr functionPointer = Marshal.GetFunctionPointerForDelegate(dongleNotifyCallBackDelegate);

				if (GrdRegisterDongleNotificationInvoker == null)
				{
					Type[] parameters = { typeof(IntPtr), typeof(IntPtr) };

					GrdRegisterDongleNotificationInvoker = new PlatformInvoker("GrdRegisterDongleNotification",
						GrdDllName, typeof(GrdE), parameters, CallingConvention.Winapi, CharSet.Auto);
				}
				return (GrdE)GrdRegisterDongleNotificationInvoker.Invoke(grdHandle.Address, functionPointer);
			}
			catch (Exception e)
			{
				return GrdException(e);
			}
		}

		/// <summary>
		/// Функция <b>GrdUnRegisterDongleNotification</b>
		/// </summary>
		/// <param name="grdHandle"></param>
		/// <returns></returns>
		public static GrdE GrdUnRegisterDongleNotification(Handle grdHandle)
		{
			try
			{
				if (GrdUnRegisterDongleNotificationInvoker == null)
				{
					Type[] parameters = { typeof(IntPtr) };

					GrdUnRegisterDongleNotificationInvoker = new PlatformInvoker("GrdUnRegisterDongleNotification",
						GrdDllName, typeof(GrdE), parameters, CallingConvention.Winapi, CharSet.Auto);
				}
				return (GrdE)GrdUnRegisterDongleNotificationInvoker.Invoke(grdHandle.Address);
			}
			catch (Exception e)
			{
				return GrdException(e);
			}
		}

		/// <summary>
		/// Функция <b>GrdUnInitializeNotificationAPI</b>
		/// </summary>
		/// <returns></returns>
		public static GrdE GrdUnInitializeNotificationAPI()
		{
			try
			{
				if (GrdUnInitializeNotificationAPIInvoker == null)
				{
					GrdUnInitializeNotificationAPIInvoker = new PlatformInvoker("GrdUnInitializeNotificationAPI", GrdDllName,
						typeof(GrdE), null, CallingConvention.Winapi, CharSet.Auto);
				}

				return (GrdE)GrdUnInitializeNotificationAPIInvoker.Invoke();
			}
			catch (Exception e)
			{
				return GrdException(e);
			}
		}

		/// \}
		#endregion ///Guardant Dongle Notification API


		private static bool CheckArg(byte[] data)
		{
			if (data.Length == 0)
				return false;
			return true;
		}

		/// <summary>
		/// 
		/// </summary>
		/// <param name="ErrorCode"></param>
		/// <returns></returns>
		public static string PrintResult(GrdE ErrorCode)
		{
			string outMessage;
			GrdFormatMessage(IntPtr.Zero, (int)ErrorCode, (int)GrdLNG.English, out outMessage);
			return outMessage;
		}

		/// <summary>
		/// 
		/// </summary>
		/// <param name="ErrorCode"></param>
		/// <param name="Language"></param>
		/// <returns></returns>
		public static string PrintResult(GrdE ErrorCode, GrdLNG Language)
		{
			string outMessage;
			GrdFormatMessage(IntPtr.Zero, (int)ErrorCode, (int)Language, out outMessage);
			return outMessage;
		}

		public static GrdE GrdException(Exception e)
		{
			Type InnerType = e.InnerException.GetType();

			if (InnerType == typeof(System.DllNotFoundException))
				return GrdE.NotFoundDLL;
			else if (InnerType == typeof(System.EntryPointNotFoundException))
				return GrdE.NotFoundFunction;

			return GrdE.ManageError;
		}
	}


	public class GuardantException : Exception
	{
		private GrdE mError;

		public GuardantException(GrdE nError)
		{
			mError = nError;
		}
		public int ErrorCode { get { return (int) mError; } }
	}

}

