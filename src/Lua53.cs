using System.Runtime.InteropServices;

using size_t = System.UInt64;
using lua_Number = System.Double;
using lua_Integer = System.Int64;
using lua_Unsigned = System.UInt64;

namespace LuaNET.Lua53;

public struct lua_State : IEquatable<lua_State>
{
	public nuint Handle;
	
	public readonly bool IsNull => Handle == 0;
	public readonly bool IsNotNull => Handle != 0;
	
	public static bool operator !(lua_State state) => state.Handle == 0;
	public static bool operator ==(lua_State state1, lua_State state2) => state1.Handle == state2.Handle;
	public static bool operator ==(lua_State state1, int handle) => state1.Handle == (nuint) handle;
	public static bool operator !=(lua_State state1, lua_State state2) => state1.Handle != state2.Handle;
	public static bool operator !=(lua_State state1, int handle) => state1.Handle != (nuint) handle;
	
	public readonly bool Equals(lua_State other) => Handle == other.Handle;
	public override readonly bool Equals(object? other) => other is lua_State state && Equals(state);
	public override readonly int GetHashCode() => Handle.GetHashCode();
}

public struct lua_KContext
{
	public nint Handle;
}

public static class Lua
{
	
	private const string DllName = "lua536";
	private const CallingConvention Convention = CallingConvention.Cdecl;
	
	[StructLayout(LayoutKind.Sequential, Pack = 1, CharSet = CharSet.Ansi)]
	public struct lua_Debug {
		public int _event;
		public string name;
		public string namewhat;
		public string what;
		public string source;
		public int currentline;
		public int linedefined;
		public int lastlinedefined;
		public byte nups;
		public byte nparams;
		public sbyte isvararg;
		public sbyte istailcall;
		[MarshalAs(UnmanagedType.ByValArray, SizeConst = LUA_IDSIZE)]
		public sbyte[] short_src;
		public nint i_ci;
	};
	
	[StructLayout(LayoutKind.Sequential, Pack = 1, CharSet = CharSet.Ansi)]
	public struct luaL_Reg {
		public string name;
		public nint func;
	};
	
	[StructLayout(LayoutKind.Sequential, Pack = 1, CharSet = CharSet.Ansi)]
	public struct luaL_Buffer {
		public nint b;
		public size_t size;
		public size_t n;
		public lua_State L;
		[MarshalAs(UnmanagedType.ByValArray, SizeConst = LUAL_BUFFERSIZE)]
		public sbyte[] initb;
	};
	
	/// <summary>
	///  Type for C functions. In order to communicate properly with Lua, a C function must use the following protocol, which defines the way parameters and results are passed: a C function receives its arguments from Lua in its stack in direct order (the first argument is pushed first). So, when the function starts, lua_gettop(L) returns the number of arguments received by the function. The first argument (if any) is at index 1 and its last argument is at index lua_gettop(L). To return values to Lua, a C function just pushes them onto the stack, in direct order (the first result is pushed first), and returns the number of results. Any other value in the stack below the results will be properly discarded by Lua. Like a Lua function, a C function called by Lua can also return many results. 
	/// </summary>
	public delegate int lua_CFunction(lua_State L);
	public delegate int lua_KFunction(lua_State L, int status, lua_KContext ctx);
	public delegate nint lua_Reader(lua_State L, nuint ud, ref size_t sz);
	public delegate int lua_Writer(lua_State L, nuint p, size_t sz, nuint ud);
	public delegate nuint lua_Alloc(nuint ud, nuint ptr, size_t osize, size_t nsize);
	public delegate void lua_Hook(lua_State L, lua_Debug ar);
	
	public static unsafe luaL_Reg AsLuaLReg(string name, delegate*unmanaged<lua_State, int> func) => new() { name  = name, func = (nint) func };
	
	public const int LUAI_BITSINT = 32;
	
	public const int LUA_INT_INT = 1;
	public const int LUA_INT_LONG = 2;
	public const int LUA_INT_LONGLONG = 3;
	
	public const int LUA_FLOAT_FLOAT = 1;
	public const int LUA_FLOAT_DOUBLE = 2;
	public const int LUA_FLOAT_LONGDOUBLE = 3;
	
	public const int LUA_INT_TYPE = LUA_INT_LONGLONG;
	public const int LUA_FLOAT_TYPE = LUA_FLOAT_DOUBLE;
	
	public const string LUA_PATH_SEP = ";";
	public const string LUA_PATH_MARK = "?";
	public const string LUA_EXEC_DIR = "!";
	
	public const string LUA_VDIR = LUA_VERSION_MAJOR + "." + LUA_VERSION_MINOR;
	
	public const string LUA_LDIR = "!\\lua\\";
	public const string LUA_CDIR = "!\\";
	public const string LUA_SHRDIR = "!\\..\\share\\lua\\" + LUA_VDIR + "\\";
	
	public const string LUA_PATH_DEFAULT = LUA_LDIR + "?.lua;" + LUA_LDIR + "?\\init.lua;" + LUA_CDIR + "?.lua;" + LUA_CDIR + "?\\init.lua;" + LUA_SHRDIR + "?.lua;" + LUA_SHRDIR + "?\\init.lua;" + ".\\?lua;" + ".\\?\\init.lua";
	public const string LUA_CPATH_DEFAULT = LUA_CDIR + "?.dll;" + LUA_CDIR + "..\\lib\\lua\\" + LUA_VDIR + "\\?.dll;" + LUA_CDIR + "loadall.dll;" + ".\\?.dll";
	
	public const string LUA_DIRSEP = "\\";
	
	public const int LUAI_MAXSTACK = 1000000;
	
	public const int LUA_EXTRASPACE = 8;
	
	public const int LUA_IDSIZE = 60;
	
	public const int LUAL_BUFFERSIZE = 8192;
	
	public const long LUA_MAXINTEGER = lua_Integer.MaxValue;
	public const long LUA_MININTEGER = lua_Integer.MinValue;
	
	public const ulong LUA_MAXUNSIGNED = lua_Unsigned.MaxValue;
	
	public static string LUA_QL(string x)
	{
		return "'" + x + "'";
	}
	
	public const string LUA_QS = "'%s'";
	
	public const string LUA_VERSION_MAJOR = "5";
	public const string LUA_VERSION_MINOR = "3";
	public const int LUA_VERSION_NUM = 503;
	public const string LUA_VERSION_RELEASE = "6";
	
	public const string LUA_VERSION = "Lua " + LUA_VERSION_MAJOR + "." + LUA_VERSION_MINOR;
	public const string LUA_RELEASE = LUA_VERSION + "." + LUA_VERSION_RELEASE;
	public const string LUA_COPYRIGHT = LUA_RELEASE + "  Copyright (C) 1994-2020 Lua.org, PUC-Rio";
	public const string LUA_AUTHORS = "R. Ierusalimschy, L. H. de Figueiredo, W. Celes";
	
	public const string LUA_SIGNATURE = "\x1bLua";
	
	public const int LUA_MULTRET = -1;
	
	public const int LUA_REGISTRYINDEX = -LUAI_MAXSTACK - 1000;
	
	public static int lua_upvalueindex(int i)
	{
		return LUA_REGISTRYINDEX - i;
	}
	
	public const int LUA_OK = 0;
	public const int LUA_YIELD = 1;
	public const int LUA_ERRRUN = 2;
	public const int LUA_ERRSYNTAX = 3;
	public const int LUA_ERRMEM = 4;
	public const int LUA_ERRGCMM = 5;
	public const int LUA_ERRERR = 6;
	
	public const int LUA_TNONE = -1;
	public const int LUA_TNIL = 0;
	public const int LUA_TBOOLEAN = 1;
	public const int LUA_TLIGHTUSERDATA = 2;
	public const int LUA_TNUMBER = 3;
	public const int LUA_TSTRING = 4;
	public const int LUA_TTABLE = 5;
	public const int LUA_TFUNCTION = 6;
	public const int LUA_TUSERDATA = 7;
	public const int LUA_TTHREAD = 8;
	
	public const int LUA_NUMTAGS = 9;
	
	public const int LUA_MINSTACK = 20;
	
	public const int LUA_RIDX_MAINTHREAD = 1;
	public const int LUA_RIDX_GLOBALS = 2;
	public const int LUA_RIDX_LAST = LUA_RIDX_GLOBALS;
	
	[DllImport(DllName, CallingConvention = Convention, EntryPoint = "lua_newstate")]
	private static extern lua_State _lua_newstate(nint f, nuint ud);
	public static lua_State lua_newstate(lua_Alloc? f, nuint ud)
	{
		return _lua_newstate(f == null ? 0 : Marshal.GetFunctionPointerForDelegate(f), ud);
	}
	
	/// <summary>
	/// <code> [-0, +0, –] </code> Destroys all objects in the given Lua state (calling the corresponding garbage-collection metamethods, if any) and frees all dynamic memory used by this state. In several platforms, you may not need to call this function, because all resources are naturally released when the host program ends. On the other hand, long-running programs that create multiple states, such as daemons or web servers, will probably need to close states as soon as they are not needed. 
	/// </summary>
	[DllImport(DllName, CallingConvention = Convention)]
	public static extern void lua_close(lua_State L);
	
	[DllImport(DllName, CallingConvention = Convention)]
	public static extern lua_State lua_newthread(lua_State L);
	
	[DllImport(DllName, CallingConvention = Convention, EntryPoint = "lua_atpanic")]
	private static extern nint _lua_atpanic(lua_State L, nint panicf);
	
	/// <summary>
	/// <code>[-0, +0, –]</code> Sets a new panic function and returns the old one (see §4.6).
	/// <param name="L">The Lua state.</param>
	/// <param name="panicf">The new panic function.</param>
	/// <returns>The old panic function.</returns>
	/// </summary>
	public static lua_CFunction? lua_atpanic(lua_State L, lua_CFunction? panicf)
	{
		nint panic = _lua_atpanic(L, panicf == null ? 0 : Marshal.GetFunctionPointerForDelegate(panicf));
		return panic == 0 ? null : Marshal.GetDelegateForFunctionPointer<lua_CFunction>(panic);
	}
	
	[DllImport(DllName, CallingConvention = Convention, EntryPoint = "lua_version")]
	private static extern nint _lua_version(lua_State L);
	public static double lua_version(lua_State L)
	{
		nint mem = _lua_version(L);
		if (mem == 0)
			return 0;
		byte[] arr = new byte[8];
		for (int i=0; i<arr.Length; i++)
			arr[i] = Marshal.ReadByte(mem, i);
		return BitConverter.ToDouble(arr, 0);
	}
	
	/// <summary>
	/// Converts the acceptable index idx into an equivalent absolute index (that is, one that does not depend on the stack top).
	/// <para>
	/// [-0, +0, –]
	/// </para>
	/// </summary>
	[DllImport(DllName, CallingConvention = Convention)]
	public static extern int lua_absindex(lua_State L, int idx);
	
	/// <summary>
	/// 
	/// <para>
	/// 
	/// </para>
	/// </summary>
	/// <returns></returns>
	[DllImport(DllName, CallingConvention = Convention)]
	public static extern int lua_gettop(lua_State L);
	
	[DllImport(DllName, CallingConvention = Convention)]
	public static extern void lua_settop(lua_State L, int idx);
	
	[DllImport(DllName, CallingConvention = Convention)]
	public static extern void lua_pushvalue(lua_State L, int idx);
	
	[DllImport(DllName, CallingConvention = Convention)]
	public static extern void lua_rotate(lua_State L, int idx, int n);
	
	/// <summary>
	/// <code> [-0, +0, –] </code>  Copies the element at index fromidx into the valid index toidx, replacing the value at that position. Values at other positions are not affected. 
	/// </summary>
	[DllImport(DllName, CallingConvention = Convention)]
	public static extern void lua_copy(lua_State L, int fromidx, int toidx);
	
	/// <summary>
	/// <code> [-0, +0, –] </code>Ensures that the stack has space for at least n extra slots (that is, that you can safely push up to n values into it). It returns false if it cannot fulfill the request, either because it would cause the stack to be larger than a fixed maximum size (typically at least several thousand elements) or because it cannot allocate memory for the extra space. This function never shrinks the stack; if the stack already has space for the extra slots, it is left unchanged. 
	/// </summary>
	[DllImport(DllName, CallingConvention = Convention)]
	public static extern int lua_checkstack(lua_State L, int n);
	
	[DllImport(DllName, CallingConvention = Convention)]
	public static extern void lua_xmove(lua_State L, lua_State to, int n);
	
	[DllImport(DllName, CallingConvention = Convention)]
	public static extern int lua_isnumber(lua_State L, int idx);
	
	[DllImport(DllName, CallingConvention = Convention)]
	public static extern int lua_isstring(lua_State L, int idx);
	
	[DllImport(DllName, CallingConvention = Convention)]
	public static extern int lua_iscfunction(lua_State L, int idx);
	
	[DllImport(DllName, CallingConvention = Convention)]
	public static extern int lua_isinteger(lua_State L, int idx);
	
	[DllImport(DllName, CallingConvention = Convention)]
	public static extern int lua_isuserdata(lua_State L, int idx);
	
	[DllImport(DllName, CallingConvention = Convention)]
	public static extern int lua_type(lua_State L, int idx);
	
	[DllImport(DllName, CallingConvention = Convention, EntryPoint = "lua_typename")]
	private static extern nint _lua_typename(lua_State L, int tp);
	public static string? lua_typename(lua_State L, int tp)
	{
		return Marshal.PtrToStringAnsi(_lua_typename(L, tp));
	}
	
	[DllImport(DllName, CallingConvention = Convention)]
	public static extern lua_Number lua_tonumberx(lua_State L, int idx, ref int isnum);
	
	[DllImport(DllName, CallingConvention = Convention)]
	public static extern lua_Integer lua_tointegerx(lua_State L, int idx, ref int isnum);
	
	[DllImport(DllName, CallingConvention = Convention)]
	public static extern int lua_toboolean(lua_State L, int idx);
	
	[DllImport(DllName, CallingConvention = Convention, EntryPoint = "lua_tolstring")]
	private static extern nint _lua_tolstring(lua_State L, int idx, ref size_t len);
	public static string? lua_tolstring(lua_State L, int idx, ref size_t len)
	{
		return Marshal.PtrToStringAnsi(_lua_tolstring(L, idx, ref len));
	}
	
	[DllImport(DllName, CallingConvention = Convention)]
	public static extern size_t lua_rawlen(lua_State L, int idx);
	
	[DllImport(DllName, CallingConvention = Convention, EntryPoint = "lua_tocfunction")]
	private static extern nint _lua_tocfunction(lua_State L, int idx);
	public static lua_CFunction? lua_tocfunction(lua_State L, int idx)
	{
		nint ret = _lua_tocfunction(L, idx);
		return ret == 0 ? null : Marshal.GetDelegateForFunctionPointer<lua_CFunction>(ret);
	}
	
	[DllImport(DllName, CallingConvention = Convention)]
	public static extern nuint lua_touserdata(lua_State L, int idx);
	
	[DllImport(DllName, CallingConvention = Convention)]
	public static extern lua_State lua_tothread(lua_State L, int idx);
	
	[DllImport(DllName, CallingConvention = Convention)]
	public static extern nuint lua_topointer(lua_State L, int idx);
	
	public const int LUA_OPADD = 0;
	public const int LUA_OPSUB = 1;
	public const int LUA_OPMUL = 2;
	public const int LUA_OPMOD = 3;
	public const int LUA_OPPOW = 4;
	public const int LUA_OPDIV = 5;
	public const int LUA_OPIDIV = 6;
	public const int LUA_OPBAND = 7;
	public const int LUA_OPBOR = 8;
	public const int LUA_OPBXOR = 9;
	public const int LUA_OPSHL = 10;
	public const int LUA_OPSHR = 11;
	public const int LUA_OPUNM = 12;
	public const int LUA_OPBNOT = 13;
	
	/// <summary>
	/// <code>[-(2|1), +1, e]</code> Performs an arithmetic or bitwise operation over the two values (or one, in the case of negations) at the top of the stack, with the value at the top being the second operand. It pops these values and pushes the result of the operation. The function follows the semantics of the corresponding Lua operator (that is, it may call metamethods).
	/// </summary>
	[DllImport(DllName, CallingConvention = Convention)]
	public static extern void lua_arith(lua_State L, int op);
	
	public const int LUA_OPEQ = 0;
	public const int LUA_OPLT = 1;
	public const int LUA_OPLE = 2;
	
	[DllImport(DllName, CallingConvention = Convention)]
	public static extern int lua_rawequal(lua_State L, int idx1, int idx2);
	
	/// <summary>
	/// <code> [-0, +0, e] </code> Compares two Lua values. Returns 1 if the value at index index1 satisfies op when compared with the value at index index2, following the semantics of the corresponding Lua operator (that is, it may call metamethods). Otherwise returns 0. Also returns 0 if any of the indices is not valid. 
	/// </summary>
	[DllImport(DllName, CallingConvention = Convention)]
	public static extern int lua_compare(lua_State L, int idx1, int idx2, int op);
	
	[DllImport(DllName, CallingConvention = Convention)]
	public static extern void lua_pushnil(lua_State L);
	
	[DllImport(DllName, CallingConvention = Convention)]
	public static extern void lua_pushnumber(lua_State L, lua_Number n);
	
	[DllImport(DllName, CallingConvention = Convention)]
	public static extern void lua_pushinteger(lua_State L, lua_Integer n);
	
	[DllImport(DllName, CallingConvention = Convention, EntryPoint = "lua_pushlstring")]
	private static extern nint _lua_pushlstring(lua_State L, string s, size_t len);
	public static string? lua_pushlstring(lua_State L, string s, size_t len)
	{
		return Marshal.PtrToStringAnsi(_lua_pushlstring(L, s, len));
	}
	
	[DllImport(DllName, CallingConvention = Convention, EntryPoint = "lua_pushstring")]
	private static extern nint _lua_pushstring(lua_State L, string s);
	public static string? lua_pushstring(lua_State L, string s)
	{
		return Marshal.PtrToStringAnsi(_lua_pushstring(L, s));
	}
	
	// TODO:
	// [DllImport(DllName, CallingConvention = Convention)]
	// public static extern lua_State lua_pushvfstring(lua_State L, string fmt, va_list argp);
	
	// TODO:
	// [DllImport(DllName, CallingConvention = Convention, EntryPoint = "lua_pushfstring")]
	// private static extern nint _lua_pushfstring(lua_State L, string fmt, params string[] args);
	// public static string? lua_pushfstring(lua_State L, string fmt, params string[] args)
	// {
	// 	return Marshal.PtrToStringAnsi(_lua_pushfstring(L, fmt, args));
	// }
	
	[DllImport(DllName, CallingConvention = Convention, EntryPoint = "lua_pushcclosure")]
	private static extern void _lua_pushcclosure(lua_State L, nint fn, int n);
	public static void lua_pushcclosure(lua_State L, lua_CFunction? fn, int n)
	{
		_lua_pushcclosure(L, fn == null ? 0 : Marshal.GetFunctionPointerForDelegate(fn), n);
	}
	
	[DllImport(DllName, CallingConvention = Convention)]
	public static extern void lua_pushboolean(lua_State L, int b);
	
	[DllImport(DllName, CallingConvention = Convention)]
	public static extern void lua_pushlightuserdata(lua_State L, nuint p);
	
	[DllImport(DllName, CallingConvention = Convention)]
	public static extern int lua_pushthread(lua_State L);
	
	/// <summary>
	/// <code> [-0, +1, e] </code> Pushes onto the stack the value of the global name. Returns the type of that value. 
	/// </summary>
	[DllImport(DllName, CallingConvention = Convention)]
	public static extern int lua_getglobal(lua_State L, string name);
	
	[DllImport(DllName, CallingConvention = Convention)]
	public static extern int lua_gettable(lua_State L, int idx);
	
	[DllImport(DllName, CallingConvention = Convention)]
	public static extern int lua_getfield(lua_State L, int idx, string k);
	
	/// <summary>
	/// <code> [-0, +1, e] </code> Pushes onto the stack the value t[i], where t is the value at the given index. As in Lua, this function may trigger a metamethod for the "index" event (see <see href="https://www.lua.org/manual/5.3/manual.html#2.4">§2.4</see>).
	/// Returns the type of the pushed value. 
	/// </summary>
	[DllImport(DllName, CallingConvention = Convention)]
	public static extern int lua_geti(lua_State L, int idx, lua_Integer n);
	
	[DllImport(DllName, CallingConvention = Convention)]
	public static extern int lua_rawget(lua_State L, int idx);
	
	[DllImport(DllName, CallingConvention = Convention)]
	public static extern int lua_rawgeti(lua_State L, int idx, lua_Integer n);
	
	[DllImport(DllName, CallingConvention = Convention)]
	public static extern int lua_rawgetp(lua_State L, int idx, nuint p);
	
	/// <summary>
	/// <code> [-0, +1, m] </code>  Creates a new empty table and pushes it onto the stack. Parameter narr is a hint for how many elements the table will have as a sequence; parameter nrec is a hint for how many other elements the table will have. Lua may use these hints to preallocate memory for the new table. This preallocation is useful for performance when you know in advance how many elements the table will have. Otherwise you can use the function lua_newtable. 
	/// </summary>
	[DllImport(DllName, CallingConvention = Convention)]
	public static extern void lua_createtable(lua_State L, int narr, int nrec);
	
	[DllImport(DllName, CallingConvention = Convention)]
	public static extern nuint lua_newuserdata(lua_State L, size_t sz);
	
	/// <summary>
	/// <code> [-0, +(0|1), –] </code> If the value at the given index has a metatable, the function pushes that metatable onto the stack and returns 1. Otherwise, the function returns 0 and pushes nothing on the stack. 
	/// </summary>
	[DllImport(DllName, CallingConvention = Convention)]
	public static extern int lua_getmetatable(lua_State L, int objindex);
	
	[DllImport(DllName, CallingConvention = Convention)]
	public static extern int lua_getuservalue(lua_State L, int idx);
	
	[DllImport(DllName, CallingConvention = Convention)]
	public static extern void lua_setglobal(lua_State L, string? name);
	
	[DllImport(DllName, CallingConvention = Convention)]
	public static extern void lua_settable(lua_State L, int idx);
	
	[DllImport(DllName, CallingConvention = Convention)]
	public static extern void lua_setfield(lua_State L, int idx, string k);
	
	[DllImport(DllName, CallingConvention = Convention)]
	public static extern void lua_seti(lua_State L, int idx, lua_Integer n);
	
	[DllImport(DllName, CallingConvention = Convention)]
	public static extern void lua_rawset(lua_State L, int idx);
	
	[DllImport(DllName, CallingConvention = Convention)]
	public static extern void lua_rawseti(lua_State L, int idx, lua_Integer n);
	
	[DllImport(DllName, CallingConvention = Convention)]
	public static extern void lua_rawsetp(lua_State L, int idx, nuint p);
	
	[DllImport(DllName, CallingConvention = Convention)]
	public static extern int lua_setmetatable(lua_State L, int objindex);
	
	[DllImport(DllName, CallingConvention = Convention)]
	public static extern void lua_setuservalue(lua_State L, int idx);
	
	[DllImport(DllName, CallingConvention = Convention, EntryPoint = "lua_callk")]
	private static extern void _lua_callk(lua_State L, int nargs, int nresults, nint ctx, nint k);
	
	/// <summary>
	/// <code>[-(nargs + 1), +nresults, e]</code> This function behaves exactly like lua_call, but allows the called function to yield (see <see href="https://www.lua.org/manual/5.3/manual.html#4.7">§4.7</see>). 
	/// </summary>
	public static void lua_callk(lua_State L, int nargs, int nresults, lua_KContext? ctx, lua_KFunction? k)
	{
		_lua_callk(L, nargs, nresults, ctx == null ? 0 : ctx.Value.Handle, k == null ? 0 : Marshal.GetFunctionPointerForDelegate(k));
	}
	
	/// <summary>
	/// <code>[-(nargs+1), +nresults, e]</code>
	/// To call a function you must use the following protocol: first, the function to be called is pushed onto the stack; then, the arguments to the function are pushed in direct order; that is, the first argument is pushed first. Finally you call lua_call; nargs is the number of arguments that you pushed onto the stack. All arguments and the function value are popped from the stack when the function is called. The function results are pushed onto the stack when the function returns. The number of results is adjusted to nresults, unless nresults is LUA_MULTRET. In this case, all results from the function are pushed; Lua takes care that the returned values fit into the stack space, but it does not ensure any extra space in the stack. The function results are pushed onto the stack in direct order (the first result is pushed first), so that after the call the last result is on the top of the stack.
	/// Any error inside the called function is propagated upwards (with a longjmp).
	/// </summary>
	public static void lua_call(lua_State L, int n, int r)
	{
		lua_callk(L, n, r, null, null);
	}
	
	[DllImport(DllName, CallingConvention = Convention, EntryPoint = "lua_pcallk")]
	private static extern int _lua_pcallk(lua_State L, int nargs, int nresults, int errfunc, nint ctx, nint k);
	public static int lua_pcallk(lua_State L, int nargs, int nresults, int errfunc, lua_KContext? ctx, lua_KFunction? k)
	{
		return _lua_pcallk(L, nargs, nresults, errfunc, ctx == null ? 0 : ctx.Value.Handle, k == null ? 0 : Marshal.GetFunctionPointerForDelegate(k));
	}
	
	public static int lua_pcall(lua_State L, int n, int r, int f)
	{
		return lua_pcallk(L, n, r, f, null, null);
	}
	
	[DllImport(DllName, CallingConvention = Convention, EntryPoint = "lua_load")]
	private static extern int _lua_load(lua_State L, nint reader, nuint dt, string chunkname, string? mode);
	public static int lua_load(lua_State L, lua_Reader? reader, nuint dt, string chunkname, string? mode)
	{
		return _lua_load(L, reader == null ? 0 : Marshal.GetFunctionPointerForDelegate(reader), dt, chunkname, mode);
	}
	
	[DllImport(DllName, CallingConvention = Convention, EntryPoint = "lua_dump")]
	private static extern int _lua_dump(lua_State L, nint writer, nuint data, int strip);
	
	/// <summary>
	/// <code> [-0, +0, –] </code> Dumps a function as a binary chunk. Receives a Lua function on the top of the stack and produces a binary chunk that, if loaded again, results in a function equivalent to the one dumped. As it produces parts of the chunk, lua_dump calls function writer (see lua_Writer) with the given data to write them.
	/// If strip is true, the binary representation may not include all debug information about the function, to save space.
	/// The value returned is the error code returned by the last call to the writer; 0 means no errors.
	/// This function does not pop the Lua function from the stack. 
	/// </summary>
	public static int lua_dump(lua_State L, lua_Writer? writer, nuint data, int strip)
	{
		return _lua_dump(L, writer == null ? 0 : Marshal.GetFunctionPointerForDelegate(writer), data, strip);
	}
	
	[DllImport(DllName, CallingConvention = Convention, EntryPoint = "lua_yieldk")]
	private static extern int _lua_yieldk(lua_State L, int nresults, nint ctx, nint k);
	public static int lua_yieldk(lua_State L, int nresults, lua_KContext? ctx, lua_KFunction? k)
	{
		return _lua_yieldk(L, nresults, ctx == null ? 0 : ctx.Value.Handle, k == null ? 0 : Marshal.GetFunctionPointerForDelegate(k));
	}
	
	[DllImport(DllName, CallingConvention = Convention)]
	public static extern int lua_resume(lua_State L, lua_State from, int narg);
	
	[DllImport(DllName, CallingConvention = Convention)]
	public static extern int lua_status(lua_State L);
	
	[DllImport(DllName, CallingConvention = Convention)]
	public static extern int lua_isyieldable(lua_State L);
	
	public static int lua_yield(lua_State L, int n)
	{
		return lua_yieldk(L, n, null, null);
	}
	
	public const int LUA_GCSTOP = 0;
	public const int LUA_GCRESTART = 1;
	public const int LUA_GCCOLLECT = 2;
	public const int LUA_GCCOUNT = 3;
	public const int LUA_GCCOUNTB = 4;
	public const int LUA_GCSTEP = 5;
	public const int LUA_GCSETPAUSE = 6;
	public const int LUA_GCSETSTEPMUL = 7;
	public const int LUA_GCISRUNNING = 9;
	
	/// <summary>
	/// <code> [-0, +0, m] </code> Controls the garbage collector. For more details about these options, see <see href="https://www.lua.org/manual/5.3/manual.html#pdf-collectgarbage">collectgarbage</see>.
	/// </summary>
	[DllImport(DllName, CallingConvention = Convention)]
	public static extern int lua_gc(lua_State L, int what, int args);
	
	/// <summary>
	/// <code> [-1, +0, v] </code> Generates a Lua error, using the value at the top of the stack as the error object. This function does a long jump, and therefore never returns (see <see href="https://www.lua.org/manual/5.3/manual.html#luaL_error">luaL_error</see>). 
	/// </summary>
	[DllImport(DllName, CallingConvention = Convention)]
	public static extern int lua_error(lua_State L);
	
	[DllImport(DllName, CallingConvention = Convention)]
	public static extern int lua_next(lua_State L, int idx);
	
	/// <summary>
	/// <code> [-n, +1, e] </code> Concatenates the n values at the top of the stack, pops them, and leaves the result at the top. If n is 1, the result is the single value on the stack (that is, the function does nothing); if n is 0, the result is the empty string. Concatenation is performed following the usual semantics of Lua (see §3.4.6). 
	/// </summary>
	[DllImport(DllName, CallingConvention = Convention)]
	public static extern void lua_concat(lua_State L, int n);
	
	[DllImport(DllName, CallingConvention = Convention)]
	public static extern void lua_len(lua_State L, int idx);
	
	[DllImport(DllName, CallingConvention = Convention)]
	public static extern size_t lua_stringtonumber(lua_State L, string s);
	
	/// <summary>
	/// <code> [-0, +0, –] </code> Pushes onto the stack the value t[k], where t is the value at the given index. As in Lua, this function may trigger a metamethod for the "index" event (see <see href="https://www.lua.org/manual/5.3/manual.html#2.4">§2.4</see>). 
	/// </summary>
	[DllImport(DllName, CallingConvention = Convention)]
	public static extern lua_Alloc lua_getallocf(lua_State L, out nuint ud);
	
	[DllImport(DllName, CallingConvention = Convention, EntryPoint = "lua_setallocf")]
	private static extern void _lua_setallocf(lua_State L, nint f, nuint ud);
	public static void lua_setallocf(lua_State L, lua_Alloc? f, nuint ud)
	{
		_lua_setallocf(L, f == null ? 0 : Marshal.GetFunctionPointerForDelegate(f), ud);
	}
	
	/// <summary>
	/// <code> [-0, +0, –] </code> Returns a pointer to a raw memory area associated with the given Lua state. The application can use this area for any purpose; Lua does not use it for anything.
	/// Each new thread has this area initialized with a copy of the area of the main thread.
	/// By default, this area has the size of a pointer to void, but you can recompile Lua with a different size for this area. (See LUA_EXTRASPACE in luaconf.h.) 
	/// </summary>
	public static nuint lua_getextraspace(lua_State L)
	{
		return L.Handle - 8;
	}
	
	public static lua_Number lua_tonumber(lua_State L, int i)
	{
		int temp = 0; // NOP
		return lua_tonumberx(L, i, ref temp);
	}
	
	public static lua_Integer lua_tointeger(lua_State L, int i)
	{
		int temp = 0; // NOP
		return lua_tointegerx(L, i, ref temp);
	}
	
	public static void lua_pop(lua_State L, int n)
	{
		lua_settop(L, -n-1);
	}
	
	public static void lua_newtable(lua_State L)
	{
		lua_createtable(L, 0, 0);
	}
	
	public static void lua_register(lua_State L, string n, lua_CFunction? f)
	{
		lua_pushcclosure(L, f, 0);
		lua_setglobal(L, n);
	}
	
	public static void lua_pushcfunction(lua_State L, lua_CFunction? f)
	{
		lua_pushcclosure(L, f, 0);
	}
	
	public static int lua_isfunction(lua_State L, int n)
	{
		return (lua_type(L, n) == LUA_TFUNCTION) ? 1 : 0;
	}
	
	public static int lua_istable(lua_State L, int n)
	{
		return (lua_type(L, n) == LUA_TTABLE) ? 1 : 0;
	}
	
	public static int lua_islightuserdata(lua_State L, int n)
	{
		return (lua_type(L, n) == LUA_TLIGHTUSERDATA) ? 1 : 0;
	}
	
	public static int lua_isnil(lua_State L, int n)
	{
		return (lua_type(L, n) == LUA_TNIL) ? 1 : 0;
	}
	
	public static int lua_isboolean(lua_State L, int n)
	{
		return (lua_type(L, n) == LUA_TBOOLEAN) ? 1 : 0;
	}
	
	public static int lua_isthread(lua_State L, int n)
	{
		return (lua_type(L, n) == LUA_TTHREAD) ? 1 : 0;
	}
	
	public static int lua_isnone(lua_State L, int n)
	{
		return (lua_type(L, n) == LUA_TNONE) ? 1 : 0;
	}
	
	public static int lua_isnoneornil(lua_State L, int n)
	{
		return (lua_type(L, n) <= 0) ? 1 : 0;
	}
	
	public static string? lua_pushliteral(lua_State L, string s)
	{
		return lua_pushstring(L, s);
	}
	
	public static void lua_pushglobaltable(lua_State L)
	{
		lua_rawgeti(L, LUA_REGISTRYINDEX, LUA_RIDX_GLOBALS);
	}
	
	public static string? lua_tostring(lua_State L, int i)
	{
		ulong temp = 0; // NOP
		return lua_tolstring(L, i, ref temp);
	}
	
	public static void lua_insert(lua_State L, int idx)
	{
		lua_rotate(L, idx, 1);
	}
	
	public static void lua_remove(lua_State L, int idx)
	{
		lua_rotate(L, idx, -1);
		lua_pop(L, 1);
	}
	
	public static void lua_replace(lua_State L, int idx)
	{
		lua_copy(L, -1, idx);
		lua_pop(L, 1);
	}
	
	public const int LUA_HOOKCALL = 0;
	public const int LUA_HOOKRET = 1;
	public const int LUA_HOOKLINE = 2;
	public const int LUA_HOOKCOUNT = 3;
	public const int LUA_HOOKTAILCALL = 4;
	
	public const int LUA_MASKCALL = 1 << LUA_HOOKCALL;
	public const int LUA_MASKRET = 1 << LUA_HOOKRET;
	public const int LUA_MASKLINE = 1 << LUA_HOOKLINE;
	public const int LUA_MASKCOUNT = 1 << LUA_HOOKCOUNT;
	
	[DllImport(DllName, CallingConvention = Convention)]
	public static extern int lua_getstack(lua_State L, int level, lua_Debug ar);
	
	[DllImport(DllName, CallingConvention = Convention)]
	public static extern int lua_getinfo(lua_State L, string what, lua_Debug ar);
	
	[DllImport(DllName, CallingConvention = Convention, EntryPoint = "lua_getlocal")]
	private static extern nint _lua_getlocal(lua_State L, lua_Debug ar, int n);
	public static string? lua_getlocal(lua_State L, lua_Debug ar, int n)
	{
		return Marshal.PtrToStringAnsi(_lua_getlocal(L, ar, n));
	}
	
	[DllImport(DllName, CallingConvention = Convention, EntryPoint = "lua_setlocal")]
	private static extern nint _lua_setlocal(lua_State L, lua_Debug ar, int n);
	public static string? lua_setlocal(lua_State L, lua_Debug ar, int n)
	{
		return Marshal.PtrToStringAnsi(_lua_setlocal(L, ar, n));
	}
	
	[DllImport(DllName, CallingConvention = Convention, EntryPoint = "lua_getupvalue")]
	private static extern nint _lua_getupvalue(lua_State L, int funcindex, int n);
	public static string? lua_getupvalue(lua_State L, int funcindex, int n)
	{
		return Marshal.PtrToStringAnsi(_lua_getupvalue(L, funcindex, n));
	}
	
	[DllImport(DllName, CallingConvention = Convention, EntryPoint = "lua_setupvalue")]
	private static extern nint _lua_setupvalue(lua_State L, int funcindex, int n);
	public static string? lua_setupvalue(lua_State L, int funcindex, int n)
	{
		return Marshal.PtrToStringAnsi(_lua_setupvalue(L, funcindex, n));
	}
	
	[DllImport(DllName, CallingConvention = Convention)]
	public static extern nuint lua_upvalueid(lua_State L, int fidx, int n);
	
	[DllImport(DllName, CallingConvention = Convention)]
	public static extern void lua_upvaluejoin(lua_State L, int fidx1, int n1, int fidx2, int n2);
	
	[DllImport(DllName, CallingConvention = Convention, EntryPoint = "lua_sethook")]
	private static extern void _lua_sethook(lua_State L, nint func, int mask, int count);
	public static void lua_sethook(lua_State L, lua_Hook? func, int mask, int count)
	{
		_lua_sethook(L, func == null ? 0 : Marshal.GetFunctionPointerForDelegate(func), mask, count);
	}
	
	[DllImport(DllName, CallingConvention = Convention, EntryPoint = "lua_gethook")]
	private static extern nint _lua_gethook(lua_State L);
	public static lua_Hook? lua_gethook(lua_State L)
	{
		nint ret = _lua_gethook(L);
		return ret == 0 ? null : Marshal.GetDelegateForFunctionPointer<lua_Hook>(ret);
	}
	
	[DllImport(DllName, CallingConvention = Convention)]
	public static extern int lua_gethookmask(lua_State L);
	
	[DllImport(DllName, CallingConvention = Convention)]
	public static extern int lua_gethookcount(lua_State L);
	
	public const int LUA_ERRFILE = LUA_ERRERR + 1;
	
	public const string LUA_LOADED_TABLE = "_LOADED";
	public const string LUA_RELOAD_TABLE = "_PRELOAD";
	
	public const int LUAL_NUMSIZES = 136;
	
	[DllImport(DllName, CallingConvention = Convention)]
	public static extern void luaL_checkversion_(lua_State L, lua_Number ver, size_t sz);
	
	public static void luaL_checkversion(lua_State L)
	{
		luaL_checkversion_(L, LUA_VERSION_NUM, LUAL_NUMSIZES);
	}
	
	[DllImport(DllName, CallingConvention = Convention)]
	public static extern int luaL_getmetafield(lua_State L, int obj, string e);
	
	[DllImport(DllName, CallingConvention = Convention)]
	public static extern int luaL_callmeta(lua_State L, int obj, string e);
	
	[DllImport(DllName, CallingConvention = Convention, EntryPoint = "luaL_tolstring")]
	private static extern nint _luaL_tolstring(lua_State L, int idx, ref size_t len);
	public static string? luaL_tolstring(lua_State L, int idx, ref size_t len)
	{
		return Marshal.PtrToStringAnsi(_luaL_tolstring(L, idx, ref len));
	}
	
	[DllImport(DllName, CallingConvention = Convention)]
	public static extern int luaL_argerror(lua_State L, int arg, string extramsg);
	
	[DllImport(DllName, CallingConvention = Convention, EntryPoint = "luaL_checklstring")]
	private static extern nint _luaL_checklstring(lua_State L, int arg, ref size_t l);
	public static string? luaL_checklstring(lua_State L, int arg, ref size_t l)
	{
		return Marshal.PtrToStringAnsi(_luaL_checklstring(L, arg, ref l));
	}
	
	[DllImport(DllName, CallingConvention = Convention, EntryPoint = "luaL_optlstring")]
	private static extern nint _luaL_optlstring(lua_State L, int arg, string def, ref size_t l);
	public static string? luaL_optlstring(lua_State L, int arg, string def, ref size_t l)
	{
		return Marshal.PtrToStringAnsi(_luaL_optlstring(L, arg, def, ref l));
	}
	
	[DllImport(DllName, CallingConvention = Convention)]
	public static extern lua_Number luaL_checknumber(lua_State L, int arg);
	
	[DllImport(DllName, CallingConvention = Convention)]
	public static extern lua_Number luaL_optnumber(lua_State L, int arg, lua_Number def);
	
	[DllImport(DllName, CallingConvention = Convention)]
	public static extern lua_Integer luaL_checkinteger(lua_State L, int arg);
	
	[DllImport(DllName, CallingConvention = Convention)]
	public static extern lua_Integer luaL_optinteger(lua_State L, int arg, lua_Integer def);
	
	[DllImport(DllName, CallingConvention = Convention)]
	public static extern void luaL_checkstack(lua_State L, int sz, string msg);
	
	[DllImport(DllName, CallingConvention = Convention)]
	public static extern void luaL_checktype(lua_State L, int arg, int t);
	
	[DllImport(DllName, CallingConvention = Convention)]
	public static extern void luaL_checkany(lua_State L, int arg);
	
	[DllImport(DllName, CallingConvention = Convention)]
	public static extern int luaL_newmetatable(lua_State L, string tname);
	
	[DllImport(DllName, CallingConvention = Convention)]
	public static extern void luaL_setmetatable(lua_State L, string tname);
	
	[DllImport(DllName, CallingConvention = Convention)]
	public static extern nuint luaL_testudata(lua_State L, int ud, string tname);
	
	[DllImport(DllName, CallingConvention = Convention)]
	public static extern nuint luaL_checkudata(lua_State L, int ud, string tname);
	
	[DllImport(DllName, CallingConvention = Convention)]
	public static extern void luaL_where(lua_State L, int lvl);
	
	// TODO: I dont think params works
	[DllImport(DllName, CallingConvention = Convention)]
	public static extern int luaL_error(lua_State L, string fmt, params string[] args);
	
	[DllImport(DllName, CallingConvention = Convention)]
	public static extern int luaL_checkoption(lua_State L, int arg, string def, string[] lst);
	
	[DllImport(DllName, CallingConvention = Convention)]
	public static extern int luaL_fileresult(lua_State L, int stat, string fname);
	
	[DllImport(DllName, CallingConvention = Convention)]
	public static extern int luaL_execresult(lua_State L, int stat);
	
	public const int LUA_NOREF = -2;
	public const int LUA_REFNIL = -1;
	
	[DllImport(DllName, CallingConvention = Convention)]
	public static extern int luaL_ref(lua_State L, int t);
	
	[DllImport(DllName, CallingConvention = Convention)]
	public static extern void luaL_unref(lua_State L, int t, int _ref);
	
	[DllImport(DllName, CallingConvention = Convention)]
	public static extern int luaL_loadfilex(lua_State L, string filename, string? mode);
	
	public static int luaL_loadfile(lua_State L, string f)
	{
		return luaL_loadfilex(L, f, null);
	}
	
	[DllImport(DllName, CallingConvention = Convention)]
	public static extern int luaL_loadbufferx(lua_State L, string buff, size_t sz, string name, string? mode);
	
	[DllImport(DllName, CallingConvention = Convention)]
	public static extern int luaL_loadstring(lua_State L, string s);
	
	[DllImport(DllName, CallingConvention = Convention)]
	public static extern lua_State luaL_newstate();
	
	[DllImport(DllName, CallingConvention = Convention)]
	public static extern lua_Integer luaL_len(lua_State L, int idx);
	
	[DllImport(DllName, CallingConvention = Convention, EntryPoint = "luaL_gsub")]
	private static extern nint _luaL_gsub(lua_State L, string s, string p, string r);
	public static string? luaL_gsub(lua_State L, string s, string p, string r)
	{
		return Marshal.PtrToStringAnsi(_luaL_gsub(L, s, p, r));
	}
	
	[DllImport(DllName, CallingConvention = Convention)]
	public static extern void luaL_setfuncs(lua_State L, luaL_Reg[] l, int nup);
	
	[DllImport(DllName, CallingConvention = Convention)]
	public static extern int luaL_getsubtable(lua_State L, int idx, string fname);
	
	[DllImport(DllName, CallingConvention = Convention)]
	public static extern void luaL_traceback(lua_State L, lua_State L1, string msg, int level);
	
	[DllImport(DllName, CallingConvention = Convention, EntryPoint = "luaL_requiref")]
	private static extern void _luaL_requiref(lua_State L, string modname, nint openf, int glb);
	public static void luaL_requiref(lua_State L, string modname, lua_CFunction? openf, int glb)
	{
		_luaL_requiref(L, modname, openf == null ? 0 : Marshal.GetFunctionPointerForDelegate(openf), glb);
	}
	
	public static void luaL_newlibtable(lua_State L, luaL_Reg[] l)
	{
		lua_createtable(L, 0, l.Length - 1);
	}
	
	public static void luaL_newlib(lua_State L, luaL_Reg[] l)
	{
		luaL_checkversion(L);
		luaL_newlibtable(L, l);
		luaL_setfuncs(L, l, 0);
	}
	
	public static void luaL_argcheck(lua_State L, bool cond, int arg, string extramsg)
	{
		if (cond == false)
			luaL_argerror(L, arg, extramsg);
	}
	
	public static string? luaL_checkstring(lua_State L, int n)
	{
		size_t temp = 0; // NOP
		return luaL_checklstring(L, n, ref temp);
	}
	
	public static string? luaL_optstring(lua_State L, int n, string d)
	{
		size_t temp = 0; // NOP
		return luaL_optlstring(L, n, d, ref temp);
	}
	
	public static string? luaL_typename(lua_State L, int i)
	{
		return lua_typename(L, lua_type(L, i));
	}
	
	public static int luaL_dofile(lua_State L, string fn)
	{
		int status = luaL_loadfile(L, fn);
		if (status > 0)
			return status;
		return lua_pcall(L, 0, LUA_MULTRET, 0);
	}
	
	public static int luaL_dostring(lua_State L, string s)
	{
		int status = luaL_loadstring(L, s);
		if (status > 0)
			return status;
		return lua_pcall(L, 0, LUA_MULTRET, 0);
	}
	
	public static int luaL_getmetatable(lua_State L, string n)
	{
		return lua_getfield(L, LUA_REGISTRYINDEX, n);
	}
	
	public delegate T luaL_Function<T>(lua_State L, int n);
	
	public static T luaL_opt<T>(lua_State L, luaL_Function<T> f, int n, T d)
	{
		return lua_isnoneornil(L, n) > 0 ? d : f(L, n);
	}
	
	public static int luaL_loadbuffer(lua_State L, string s, size_t sz, string n)
	{
		return luaL_loadbufferx(L, s, sz, n, null);
	}
	
	public static void luaL_addchar(luaL_Buffer B, byte c)
	{
		if (B.n >= B.size)
			luaL_prepbuffsize(B, 1);
		Marshal.WriteByte(B.b + (nint) B.n, c);
		B.n += 1;
	}
	
	public static void luaL_addsize(luaL_Buffer B, long s)
	{
		B.n = (size_t) ((long) B.n + s);
	}
	
	[DllImport(DllName, CallingConvention = Convention)]
	public static extern void luaL_buffinit(lua_State L, luaL_Buffer B);
	
	[DllImport(DllName, CallingConvention = Convention)]
	public static extern nint luaL_prepbuffsize(luaL_Buffer B, size_t sz);
	
	[DllImport(DllName, CallingConvention = Convention)]
	public static extern void luaL_addlstring(luaL_Buffer B, string s, size_t l);
	
	[DllImport(DllName, CallingConvention = Convention)]
	public static extern void luaL_addstring(luaL_Buffer B, string s);
	
	[DllImport(DllName, CallingConvention = Convention)]
	public static extern void luaL_addvalue(luaL_Buffer B);
	
	[DllImport(DllName, CallingConvention = Convention)]
	public static extern void luaL_pushresult(luaL_Buffer B);
	
	[DllImport(DllName, CallingConvention = Convention)]
	public static extern void luaL_pushresultsize(luaL_Buffer B, size_t sz);
	
	[DllImport(DllName, CallingConvention = Convention)]
	public static extern nint luaL_buffinitsize(lua_State L, luaL_Buffer B, size_t sz);
	
	public static nint luaL_prepbuffer(luaL_Buffer B)
	{
		return luaL_prepbuffsize(B, LUAL_BUFFERSIZE);
	}
	
	public const string LUA_FILEHANDLE = "FILE*";
	
	public const string LUA_VERSUFFIX = "_" + LUA_VERSION_MAJOR + "_" + LUA_VERSION_MINOR;
	
	public const string LUA_COLIBNAME = "coroutine";
	public const string LUA_TABLIBNAME = "table";
	public const string IOLIBNAME = "io";
	public const string OSLIBNAME = "os";
	public const string LUA_STRLIBNAME = "string";
	public const string LUA_UTF8LIBNAME = "utf8";
	public const string LUA_BITLIBNAME = "bit32";
	public const string LUA_MATHLIBNAME = "math";
	public const string DBLIBNAME = "debug";
	public const string LOADLIBNAME = "package";
	
	[DllImport(DllName, CallingConvention = Convention)]
	public static extern int luaopen_base(lua_State L);
	
	[DllImport(DllName, CallingConvention = Convention)]
	public static extern int luaopen_coroutine(lua_State L);
	
	[DllImport(DllName, CallingConvention = Convention)]
	public static extern int luaopen_table(lua_State L);
	
	[DllImport(DllName, CallingConvention = Convention)]
	public static extern int luaopen_io(lua_State L);
	
	[DllImport(DllName, CallingConvention = Convention)]
	public static extern int luaopen_os(lua_State L);
	
	[DllImport(DllName, CallingConvention = Convention)]
	public static extern int luaopen_string(lua_State L);
	
	[DllImport(DllName, CallingConvention = Convention)]
	public static extern int luaopen_utf8(lua_State L);
	
	[DllImport(DllName, CallingConvention = Convention)]
	public static extern int luaopen_bit32(lua_State L);
	
	[DllImport(DllName, CallingConvention = Convention)]
	public static extern int luaopen_math(lua_State L);
	
	[DllImport(DllName, CallingConvention = Convention)]
	public static extern int luaopen_debug(lua_State L);
	
	[DllImport(DllName, CallingConvention = Convention)]
	public static extern int luaopen_package(lua_State L);
	
	[DllImport(DllName, CallingConvention = Convention)]
	public static extern void luaL_openlibs(lua_State L);
	
}

