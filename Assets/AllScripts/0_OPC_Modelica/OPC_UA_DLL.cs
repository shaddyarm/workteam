using System;
using System.Runtime.InteropServices;
using UnityEngine;

public class OPC_UA_DLL : MonoBehaviour
{

    const string dllname = "OpcTestDll.dll";


    [DllImport(dllname, CallingConvention = CallingConvention.Cdecl)]
    private static extern IntPtr CreateContext();

    [DllImport(dllname, CallingConvention = CallingConvention.Cdecl)]
    private static extern void DestroyContext(IntPtr _pContext);

    [DllImport(dllname, CallingConvention = CallingConvention.Cdecl)]
    private static extern void BrowseAll(IntPtr _pContext);

    [DllImport(dllname, CallingConvention = CallingConvention.Cdecl)]
    private static extern double GetValue(IntPtr _pContext, string str);

    [DllImport(dllname, CallingConvention = CallingConvention.Cdecl)]
    private static extern void SetValue(IntPtr _pContext, string str, double value);




    static OPC_UA_DLL()
    {
        Debug.Log("Plugin name: " + dllname);
    }

    void Start()
    {
        var context = CreateContext();
        BrowseAll(context);
        var test = GetValue(context, "voltageSensor.v");
        Debug.Log(test);
        SetValue(context, "OpenModelica.realTimeScalingFactor", 0.55);
        var test2 = GetValue(context, "OpenModelica.realTimeScalingFactor");
        Debug.Log(test2);
        DestroyContext(context);
    }
}