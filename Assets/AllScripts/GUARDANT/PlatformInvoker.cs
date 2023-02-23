using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Reflection.Emit;
using Guardant;

public class PlatformInvoker : MarshalByRefObject
{
	private readonly AssemblyBuilder DynamicAssembly;
	private readonly ModuleBuilder DynamicModule;
	private readonly TypeBuilder DynamicType;
	private readonly Type DllImportType;

	public String message;
	public PlatformInvoker()
	{
		DynamicAssembly =
#if NETFRAMEWORK && !NET45 // .NET Framework lower than 4.5
            AppDomain.CurrentDomain.
#elif (NETFRAMEWORK && NET45) || NETCOREAPP || NETSTANDARD
            AssemblyBuilder.
#else
#error "Unknown .NET product: neither Framework, nor Core, nor Standard!"
            AssemblyBuilder.
#endif
			DefineDynamicAssembly(new AssemblyName("DynamicAssembly"), AssemblyBuilderAccess.Run);
		DynamicModule = DynamicAssembly.DefineDynamicModule("DynamicModule");
		DynamicType = DynamicModule.DefineType("DynamicType", TypeAttributes.Abstract |
			TypeAttributes.Sealed |
			TypeAttributes.AutoClass |
			TypeAttributes.BeforeFieldInit |
			TypeAttributes.Class);
		DllImportType = typeof(DllImportAttribute);
	}

	protected readonly MethodBuilder theNativeMethod;

	public PlatformInvoker(string functionName,
		string dllName,
		MethodAttributes attrs,
		CallingConventions conventions,
		Type returnType,
		Type[] parameters,
		CallingConvention nativeCallConv,
		CharSet chars,
		bool bestFitMapping,
		bool exactSpelling,
		bool preserveSig,
		bool setLastError,
		bool throwOnUnmappableChar)
		: this()
	{

		theNativeMethod = DynamicType.DefineMethod(functionName,
			attrs,
			returnType,
			parameters);


		ConstructorInfo ctor = DllImportType.GetConstructor(new Type[] { typeof(string) });

		theNativeMethod.SetCustomAttribute(
			new CustomAttributeBuilder(ctor,
			new object[] { dllName },
			new FieldInfo[] { DllImportType.GetField("CallingConvention"), 
                    DllImportType.GetField("BestFitMapping"),
                    DllImportType.GetField("CharSet"),
                    DllImportType.GetField("ExactSpelling"),
                    DllImportType.GetField("PreserveSig"),
                    DllImportType.GetField("SetLastError"),
                    DllImportType.GetField("ThrowOnUnmappableChar")},
			new object[] { nativeCallConv, bestFitMapping, chars, exactSpelling, preserveSig, setLastError, throwOnUnmappableChar }));

		DynamicType.CreateType();
		DynamicModule.CreateGlobalFunctions();
	}

	public PlatformInvoker(string functionName,
		string dllName,
		Type returnType,
		Type[] parameters,
		CallingConvention nativeCallConv,
		CharSet chars)
		: this(functionName, dllName, MethodAttributes.PinvokeImpl | MethodAttributes.Static | MethodAttributes.Public,
		CallingConventions.Standard, returnType, parameters, nativeCallConv, chars, false, false, true, true, false)
	{

	}

	public PlatformInvoker(string functionName,
		string dllName,
		Type returnType,
		Type[] parameters,
		CallingConvention nativeCallConv,
		CharSet chars,
		bool bestFitMapping,
		bool exactSpelling,
		bool preserveSig,
		bool setLastError,
		bool throwOnUnmappableChar)
		: this(functionName, dllName, MethodAttributes.PinvokeImpl | MethodAttributes.Static | MethodAttributes.Public,
		CallingConventions.Standard, returnType, parameters, nativeCallConv, chars, bestFitMapping, exactSpelling,
		preserveSig, setLastError, throwOnUnmappableChar)
	{
	}

	public ParameterInfo[] Parameters
	{
		get { return theNativeMethod.GetParameters(); }
	}

	public object Invoke(params object[] args)
	{
		try
		{
			MethodBase baseMethod = DynamicModule.GetType("DynamicType").GetMethod(FunctionName);
			return baseMethod.Invoke(null, args);
		}
		catch (Exception e)
		{
			Type InnerType = e.InnerException.GetType();
			message = e.InnerException.Message;

			if (InnerType == typeof(System.DllNotFoundException))
				return GrdE.NotFoundDLL;
			else if (InnerType == typeof(System.EntryPointNotFoundException))
				return GrdE.NotFoundFunction;

			return GrdE.ManageError;
		}
	}

	public string FunctionName
	{
		get { return theNativeMethod.Name; }
	}

	protected RuntimeMethodHandle GetDynamicMethodHandle()
	{
		return DynamicModule.GetType("DynamicType").GetMethod(FunctionName).MethodHandle;
	}
}

public class PlatformInvoker2<T> : PlatformInvoker
{
	private static readonly Type m_returnType;
	private static readonly Type[] m_parameters;
	private static readonly string m_functionName;

	static PlatformInvoker2()
	{
		Type genericInst = typeof(T);
		if (!genericInst.IsSubclassOf(typeof(Delegate)))
			throw new ArgumentException("Generic argument is not delegate");

		MethodInfo invokeMethod = genericInst.GetMethod("Invoke");
		m_returnType = invokeMethod.ReturnType;
		List<Type> _params = new List<Type>();
		foreach (ParameterInfo p in invokeMethod.GetParameters())
			_params.Add(p.ParameterType);

		m_parameters = _params.ToArray();
		m_functionName = genericInst.Name;
	}

	private readonly Delegate m_caller;

	public PlatformInvoker2(string dllName,
		CallingConvention nativeCallConv,
		CharSet chars)
		: base(m_functionName, dllName, m_returnType, m_parameters, nativeCallConv, chars)
	{
		MethodInfo surrogateMethod = (MethodInfo)MethodInfo.GetMethodFromHandle(GetDynamicMethodHandle());
		Type genericInst = typeof(T);
		m_caller = Delegate.CreateDelegate(genericInst, surrogateMethod);
	}

	public T Call
	{
		get { return (T)(object)m_caller; }
	}
}
