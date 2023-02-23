using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Guardant;

public class GuardantClass : MonoBehaviour
{
	public bool isOn = false;
	public GameObject BadCanvas;

	// Demo codes decreased by random value for better security;
	// This way they are not shown in the application
	static uint PublicCode = 0xC00D4E47;    
	static uint ReadCode = 0x53b370e0;      
	static uint WriteCode = 0x695f415b;     
	static uint MasterCode = 0xc134c9d2;    

	void Bad()
    {
		BadCanvas.SetActive(true);
	}
	
	// Start is called before the first frame update
	void Start()
    {
		if (isOn == false) return;
			 
        short type;
        byte modelID;
        int dongleId;
        GrdFMR RemoteMode = GrdFMR.Local;

        //Initialize this copy of GrdAPI
        GrdE nGrdE = GrdApi.GrdStartup(RemoteMode);
        ErrorHandling(nGrdE);

       	// -----------------------------------------------------------------
		// Creating Grd protected container & returning it's handle
		// -----------------------------------------------------------------
		Debug.Log("Create Guardant protected container : ");
		Handle grdHandle = GrdApi.GrdCreateHandle(GrdCHM.MultiThread);
		ErrorHandling(grdHandle, GrdE.OK);

		// -----------------------------------------------------------------
		// Store dongle codes in Guardant protected container
		// -----------------------------------------------------------------
		Debug.Log("Storing dongle codes in Guardant protected container : ");
		nGrdE = GrdApi.GrdSetAccessCodes(grdHandle, PublicCode, ReadCode, WriteCode);
		ErrorHandling(grdHandle, nGrdE);

		GrdFM DongleFlags = GrdFM.ALL;          // Operation mode flags
		uint ProgramNumber = 0;                 // Program number
		uint Version = 0;                       // Version
		uint SerialNumber = 0;                  // Serial number
		uint BitMask = 0;                       // Bit mask
		uint DongleID = 0;                      // DongelID number
		GrdDT DongleType = GrdDT.ALL;           // Dongle type
		GrdFMM DongleModel = GrdFMM.ALL;        // Dongle model
		GrdFMI DongleInterface = GrdFMI.ALL;    // Dongle interface

		FindInfo findInfo;
		uint grdDongleID;
		Debug.Log("Setting dongle search conditions : ");
		nGrdE = GrdApi.GrdSetFindMode(grdHandle, RemoteMode, DongleFlags, ProgramNumber, DongleID, SerialNumber, Version, BitMask, DongleType, DongleModel, DongleInterface);
		ErrorHandling(grdHandle, nGrdE);

		// -----------------------------------------------------------------
		// Search for all specified dongles and print ID's
		// -----------------------------------------------------------------
		nGrdE = GrdApi.GrdFind(grdHandle, GrdF.First, out DongleID, out findInfo);
		if (nGrdE != GrdE.OK)
		{
			Debug.Log("The Guardant Sign/Time dongle with this access codes not found!");
			CloseGuardantApi(grdHandle);
			Bad();
			return;
		}

		// Print table header if at least one dongle found
		Debug.Log("------------------------------------------------------------------------------");
		Debug.Log("  Public  HVer Net  Type DongleID Prog Ver    SN  Mask    GP NetRes    Index  ");
		Debug.Log("------------------------------------------------------------------------------");
		while (nGrdE == GrdE.OK)
		{
			string Str = string.Format("{0,8:X}", findInfo.dwPublicCode);  // Public code
			Str = Str + string.Format(" {0,5:X}", findInfo.byHrwVersion);  // Dongle hardware version
			Str = Str + string.Format(" {0,3:D}", findInfo.byMaxNetRes);   // Maximum LAN license limit
			Str = Str + string.Format(" {0,5:X}", findInfo.wType);         // Dongle type flags
			Str = Str + string.Format(" {0,8:X}", findInfo.dwID);          // Dongle's ID (unique)
			Str = Str + string.Format(" {0,4:D}", findInfo.byNProg);       // Program number
			Str = Str + string.Format(" {0,3:D}", findInfo.byVer);         // Version
			Str = Str + string.Format(" {0,5:D}", findInfo.wSN);           // Serial number
			Str = Str + string.Format(" {0,5:X}", findInfo.wMask);         // Bit mask
			Str = Str + string.Format(" {0,5:D}", findInfo.wGP);           // Executions GP counter/ License time counter
			Str = Str + string.Format(" {0,6:D}", findInfo.wRealNetRes);   // Current LAN license limit, must be <=kmLANRes
			Str = Str + string.Format(" {0,8:X}", findInfo.dwIndex);       // Index for remote programming
			Debug.Log(Str);

			nGrdE = GrdApi.GrdFind(grdHandle, GrdF.Next, out grdDongleID, out findInfo);
		}

		if (nGrdE == GrdE.AllDonglesFound)
		{
			// Search has been completed?
			Debug.Log("------------------------------------------------------------------------------");
			Debug.Log("Dongles search is complete with: ");
			Debug.Log("No errors");
		}
		else
		{
			ErrorHandling(grdHandle, nGrdE);
			CloseGuardantApi(grdHandle);
			Bad();
			return;
		}

		// Find only Guardant StealthII/Sign/Time dongle
		Debug.Log("Set find only Guardant StealthII/Sign/Time dongle: ");
		nGrdE = GrdApi.GrdSetFindMode(grdHandle, RemoteMode, DongleFlags, ProgramNumber, DongleID, SerialNumber, Version, BitMask, DongleType, GrdFMM.GS3S | GrdFMM.GS2U, DongleInterface);
		ErrorHandling(grdHandle, nGrdE);

		// All following Guardant API calls before next GrdCloseHandle()/GrdLogin() will use this dongle
		Debug.Log("Login to dongle with search conditions: ");
		nGrdE = GrdApi.GrdLogin(grdHandle, -1, GrdLM.PerStation);
		if (nGrdE != GrdE.OK)
		{
			Debug.Log("The Guardant StealthII/Sign/Time dongle with this access codes not found!");
			CloseGuardantApi(grdHandle);
			Bad();
			return;
		}
		else
		{
			ErrorHandling(grdHandle, nGrdE);
		}
			
		//------------------------------------------------------------------------------
		// Sample Read/Write operation
		/*
		{
			// Reading model field value
			Debug.Log("Reading Model value of the dongle via hGrd handle: ");
			nGrdE = GrdApi.GrdGetInfo(grdHandle, GrdGIL.Model, out modelID);
			ErrorHandling(grdHandle, nGrdE);

			// Check dongle model for this sample
			if (0 != PrintDongleModel((GrdDM)modelID))
			{
				CloseGuardantApi(grdHandle);
				//				return 1;
			}

			// Read ID field value from the dongle.
			Debug.Log("Reading dongle ID field value: ");
			nGrdE = GrdApi.GrdRead(grdHandle, GrdSAM.ID, out dongleId);
			ErrorHandling(grdHandle, nGrdE);
			Debug.Log("  Result: dwDongleID = " + string.Format(" {0,8:X}", dongleId));

			// Read Type field value from the dongle.
			Debug.Log("Reading Type field value: ");
			nGrdE = GrdApi.GrdRead(grdHandle, GrdSAM.Type, out type);
			ErrorHandling(grdHandle, nGrdE);

			Debug.Log("Locking dongle for read/write operations: ");
			uint TimeoutWaitForUnlock = 10000; // Max GrdAPI unlock waiting time. -1 == infinity. 0 == no waiting
			uint TimeoutAutoUnlock = 10000;    // Max dongle locking time in ms.  -1 == infinity. 0 == 10000 ms (10 sec)
											   // Prevent reading and writing from other threads and tasks (GrdRead() & GrdWrite())
			nGrdE = GrdApi.GrdLock(grdHandle, TimeoutWaitForUnlock, TimeoutAutoUnlock, GrdLockMode.Read | GrdLockMode.Write);
			ErrorHandling(grdHandle, nGrdE);

			// Read data from the dongle
			int index;
			Debug.Log("Reading Index: ");
			nGrdE = GrdApi.GrdRead(grdHandle, GrdUAM.Index, out index);
			ErrorHandling(grdHandle, nGrdE);
			Debug.Log("  Result: Read Index = " + index);

			// Increment value & write it back.
			Debug.Log("Increment value & write it back : ");
			nGrdE = GrdApi.GrdWrite(grdHandle, GrdUAM.Index, ++index);
			ErrorHandling(grdHandle, nGrdE);

			// Unlock dongle when transaction is completed
			Debug.Log("Unlocking dongle: ");
			nGrdE = GrdApi.GrdUnlock(grdHandle);
			ErrorHandling(grdHandle, nGrdE);
		}
		*/
		CloseGuardantApi(grdHandle);
	}

	


	// Update is called once per frame
	void Update()
    {
        
    }

    private  void ErrorHandling(GrdE nGrdE) //static
	{
        // print the result of last executed function
        PrintCode(nGrdE);
        if (nGrdE != GrdE.OK)
        {
			Bad();
		}
    }

    

	private  void ErrorHandling(Handle grdHandle, GrdE nGrdE) //static void
	{
		// print the result of last executed function
		PrintCode(nGrdE);
		if (nGrdE != GrdE.OK)
		{
			// Perform some cleanup operations if hGrd handle exists
			if (grdHandle.Address != null) // !=  IntPtr.Zero
			{
				// Close hGrd handle, log out from dongle/server, free allocated memory
				Debug.Log("Closing handle.\n");
				nGrdE = GrdApi.GrdCloseHandle(grdHandle);
				PrintCode(nGrdE);
			}

			// Deinitialize this copy of GrdAPI. GrdCleanup() must be called after last GrdAPI call before program termination
			Debug.Log("Deinitializing this copy of GrdAPI: ");
			nGrdE = GrdApi.GrdCleanup();
			PrintCode(nGrdE);
			// Terminate application
			Bad();
		}
	}

	private  void CloseGuardantApi(Handle grdHandle) //static
	{
		//------------------------------------------------------------------------------
		// Close hGrd handle. Log out from dongle/server & free allocated memory
		//------------------------------------------------------------------------------
		Debug.Log("Closing handle: ");
		GrdE nGrdE = GrdApi.GrdCloseHandle(grdHandle);
		ErrorHandling(nGrdE);

		//------------------------------------------------------------------------------
		// Deinitialize this copy of GrdAPI. GrdCleanup() 
		// must be called after last GrdAPI call before program termination
		//------------------------------------------------------------------------------
		Debug.Log("Deinitializing this copy of GrdAPI: ");
		nGrdE = GrdApi.GrdCleanup();
		ErrorHandling(nGrdE);
	}

	private  void PrintCode(GrdE ErrorCode) //static
	{
		string errMessage;
		GrdE nGrdE = GrdApi.GrdFormatMessage(ErrorCode, GrdLNG.English, out errMessage);
		if (nGrdE != GrdE.OK)
		{
			Debug.Log("ERROR! " + ErrorCode);
		}
		else
		{
			if (ErrorCode == GrdE.OK)
				Debug.Log(errMessage);
			else
			{
				Debug.Log("ERROR! " + errMessage);
			}
		}
	}

	private  int PrintDongleModel(GrdDM modelID) //static
	{
		Debug.Log("  Result: Dongle Model - ");
		switch (modelID)
		{
			case GrdDM.GF1L:
			case GrdDM.GF1U:
				Debug.Log("Guardant Fidus");
				return 1;
			case GrdDM.GS1L:
			case GrdDM.GS1U:
				Debug.Log("Guardant StealthI");
				return 1;
			case GrdDM.GS2L:
			case GrdDM.GS2U:
				Debug.Log("Guardant StealthII");
				return 0;
			case GrdDM.GS3U:
				Debug.Log("Guardant StealthIII");
				return 0;
			case GrdDM.GS3SU:
				Debug.Log("Guardant Sign/Time");
				return 0;
			case GrdDM.GCU:
				Debug.Log("Guardant Code");
				return 1;
			case GrdDM.GSP:
				Debug.Log("Guardant SP");
				return 1;
			default:
				Debug.Log("Unknown Model");
				break;
		}
		return 1;
	}

}
