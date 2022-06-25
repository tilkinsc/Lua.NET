namespace Lua53;

using System;
using System.Runtime.InteropServices;


public static class Lua
{
	
	private const string DllName = "Lua536.dll";
	
	public static readonly int LUA_BITSINT = 32;
	
	public static readonly int LUA_INT_INT = 1;
	public static readonly int LUA_INT_LONG = 2;
	public static readonly int LUA_INT_LONGLONG = 3;
	
	public static readonly int LUA_FLOAT_FLOAT = 1;
	public static readonly int LUA_FLOAT_DOUBLE = 2;
	public static readonly int LUA_FLOAT_LONGDOUBLE = 3;
	
	public static readonly int LUA_INT_TYPE = LUA_INT_DEFAULT;
	public static readonly int LUA_FLOAT_TYPE = LUA_FLOAT_DEFAULT;
	
	public static readonly int LUA_INT_DEFAULT = LUA_INT_LONGLONG;
	public static readonly int LUA_FLOAT_DEFAULT = LUA_FLOAT_DOUBLE;
	
	public static readonly string LUA_PATH_SEP = ";";
	public static readonly string LUA_PATH_MARK = "?";
	public static readonly string LUA_EXEC_DIR = "!";
	
	public static readonly string LUA_VDIR = LUA_VERSION_MAJOR + "." + LUA_VERSION_MINOR;
	
	public static readonly string LUA_LDIR = "!\\lua\\";
	public static readonly string LUA_CDIR = "!\\";
	public static readonly string LUA_SHRDIR = "!\\..\\share\\lua\\" + LUA_VDIR + "\\";
	
	public static readonly string LUA_PATH_DEFAULT = LUA_LDIR + "?.lua;" + LUA_LDIR + "?\\init.lua;" + LUA_CDIR + "?.lua;" + LUA_CDIR + "?\\init.lua;" + LUA_SHRDIR + "?.lua;" + LUA_SHRDIR + "?\\init.lua;" + ".\\?lua;" + ".\\?\\init.lua";
	public static readonly string LUA_CPATH_DEFAULT = LUA_CDIR + "?.dll;" + LUA_CDIR + "..\\lib\\lua\\" + LUA_VDIR + "\\?.dll;" + LUA_CDIR + "loadall.dll;" + ".\\?.dll";
	
	public static readonly string LUA_DIRSEP = "\\";
	
	public static readonly int LUA_32BITS = 0;
	
	public static readonly int LUAI_MAXSTACK = 1000000;
	
	public static readonly int LUA_IDSIZE = 60;
	
	public static readonly int LUAL_BUFFERSIZE = 0x80 * 8 * 8;
	
	public static ulong lua_strlen(IntPtr L, int i)
	{
		return lua_rawlen(L, i);
	}
	
	public static ulong lua_objlen(IntPtr L, int i)
	{
		return lua_rawlen(L, i);
	}
	
	public static int lua_equal(IntPtr L, int idx1, int idx2)
	{
		return lua_compare(L, idx1, idx2, LUA_OPEQ);
	}
	
	public static int lua_lessthan(IntPtr L, int idx1, int idx2)
	{
		return lua_compare(L, idx1, idx2, LUA_OPLT);
	}
	
	public static readonly long LUA_MAXINTEGER = long.MaxValue;
	public static readonly long LUA_MININTEGER = long.MinValue;
	
	public static readonly ulong LUA_MAXUNSIGNED = ulong.MaxValue;
	
	public static readonly string LUA_VERSION_MAJOR = "5";
	public static readonly string LUA_VERSION_MINOR = "3";
	public static readonly string LUA_VERSION_RELEASE = "6";
	public static readonly int LUA_VERSION_NUM = 503;
	
	public static readonly string LUA_VERSION = "Lua " + LUA_VERSION_MAJOR + "." + LUA_VERSION_MINOR;
	public static readonly string LUA_RELEASE = LUA_VERSION + "." + LUA_VERSION_RELEASE;
	public static readonly string LUA_COPYRIGHT = LUA_RELEASE + "  Copyright (C) 1994-2022 Lua.org, PUC-Rio";
	public static readonly string LUA_AUTHORS = "R. Ierusalimschy, L. H. de Figueiredo, W. Celes";
	
	public static readonly string LUA_SIGNATURE = "\x1bLua";
	
	public static readonly int LUA_MULTRET = -1;
	
	public static readonly int LUA_REGISTRYINDEX = (-LUAI_MAXSTACK - 1000);
	
	public static int lua_upvalueindex(int i)
	{
		return LUA_REGISTRYINDEX - i;
	}
	
	// public static readonly int LUA_VERSION_RELEASE_NUM = LUA_VERSION_NUM * 100 + 4;
	
	public static readonly int LUA_OK = 0;
	public static readonly int LUA_YIELD = 1;
	public static readonly int LUA_ERRRUN = 2;
	public static readonly int LUA_ERRSYNTAX = 3;
	public static readonly int LUA_ERRMEM = 4;
	public static readonly int LUA_ERRGCMM = 5;
	public static readonly int LUA_ERRERR = 6;
	
	public static readonly int LUA_TNONE = -1;
	public static readonly int LUA_TNIL = 0;
	public static readonly int LUA_TBOOLEAN = 1;
	public static readonly int LUA_TLIGHTUSERDATA = 2;
	public static readonly int LUA_TNUMBER = 3;
	public static readonly int LUA_TSTRING = 4;
	public static readonly int LUA_TTABLE = 5;
	public static readonly int LUA_TFUNCTION = 6;
	public static readonly int LUA_TUSERDATA = 7;
	public static readonly int LUA_TTHREAD = 8;
	
	public static readonly int LUA_NUMTAGS = 9;
	
	public static readonly int LUA_MINSTACK = 20;
	
	public static readonly int LUA_RIDX_MAINTHREAD = 1;
	public static readonly int LUA_RIDX_GLOBALS = 2;
	public static readonly int LUA_RIDX_LAST = LUA_RIDX_GLOBALS;
	
	[StructLayout(LayoutKind.Sequential, Pack = 1, CharSet = CharSet.Ansi)]
	public struct lua_Debug {
		public int _event;
		public string name;
		public string namewhat;
		public string what;
		public string source;
		public ulong srclen;
		public int currentline;
		public int linedefined;
		public int lastlinedefined;
		public byte nups;
		public byte nparams;
		public sbyte isvararg;
		public sbyte istailcall;
		public ushort ftransfer;
		public ushort ntransfer;
		[MarshalAs(UnmanagedType.ByValArray, SizeConst = 60)]
		public sbyte[] short_src;
		public IntPtr i_ci;
	};
	
	[StructLayout(LayoutKind.Sequential, Pack = 1, CharSet = CharSet.Ansi)]
	public struct luaL_Buffer {
		public IntPtr b;
		public ulong size;
		public ulong n;
		public IntPtr L;
		[MarshalAs(UnmanagedType.ByValArray, SizeConst = 1024)]
		public sbyte[] init;
	};
	
	public delegate int lua_CFunction(IntPtr L);
	public delegate int lua_KFunction(IntPtr L, int status, IntPtr ctx);
	public delegate void lua_WarnFunction(IntPtr ud, string msg, int tocont);
	public delegate int lua_Writer(IntPtr L, IntPtr p, ulong sz, ref ulong ud);
	public delegate IntPtr lua_Reader(IntPtr L, IntPtr ud, ulong sz);
	public delegate IntPtr lua_Alloc(IntPtr ud, IntPtr ptr, ulong osize, ulong nsize);
	public delegate void lua_Hook(IntPtr L, lua_Debug ar);
	
	[DllImport(DllName, CallingConvention = CallingConvention.StdCall)]
	public static extern IntPtr lua_newstate(lua_Alloc f, IntPtr ud);
	
	[DllImport(DllName, CallingConvention = CallingConvention.StdCall)]
	public static extern void lua_close(IntPtr L);
	
	[DllImport(DllName, CallingConvention = CallingConvention.StdCall)]
	public static extern IntPtr lua_newthread(IntPtr L);
	
	[DllImport(DllName, CallingConvention = CallingConvention.StdCall)]
	public static extern lua_CFunction lua_atpanic(IntPtr L, lua_CFunction panicf);
	
	[DllImport(DllName, CallingConvention = CallingConvention.StdCall)]
	public static extern double lua_version(IntPtr L);
	
	[DllImport(DllName, CallingConvention = CallingConvention.StdCall)]
	public static extern int lua_absindex(IntPtr L, int idx);
	
	[DllImport(DllName, CallingConvention = CallingConvention.StdCall)]
	public static extern int lua_gettop(IntPtr L);
	
	[DllImport(DllName, CallingConvention = CallingConvention.StdCall)]
	public static extern void lua_settop(IntPtr L, int idx);
	
	[DllImport(DllName, CallingConvention = CallingConvention.StdCall)]
	public static extern void lua_pushvalue(IntPtr L, int idx);
	
	[DllImport(DllName, CallingConvention = CallingConvention.StdCall)]
	public static extern void lua_rotate(IntPtr L, int idx, int n);
	
	[DllImport(DllName, CallingConvention = CallingConvention.StdCall)]
	public static extern void lua_copy(IntPtr L, int fromidx, int toidx);
	
	[DllImport(DllName, CallingConvention = CallingConvention.StdCall)]
	public static extern int lua_checkstack(IntPtr L, int n);
	
	[DllImport(DllName, CallingConvention = CallingConvention.StdCall)]
	public static extern void lua_xmove(IntPtr L, IntPtr to, int n);
	
	[DllImport(DllName, CallingConvention = CallingConvention.StdCall)]
	public static extern int lua_isnumber(IntPtr L, int idx);
	
	[DllImport(DllName, CallingConvention = CallingConvention.StdCall)]
	public static extern int lua_isstring(IntPtr L, int idx);
	
	[DllImport(DllName, CallingConvention = CallingConvention.StdCall)]
	public static extern int lua_iscfunction(IntPtr L, int idx);
	
	[DllImport(DllName, CallingConvention = CallingConvention.StdCall)]
	public static extern int lua_isinteger(IntPtr L, int idx);
	
	[DllImport(DllName, CallingConvention = CallingConvention.StdCall)]
	public static extern int lua_isuserdata(IntPtr L, int idx);
	
	[DllImport(DllName, CallingConvention = CallingConvention.StdCall)]
	public static extern int lua_type(IntPtr L, int idx);
	
	[DllImport(DllName, CallingConvention = CallingConvention.StdCall, EntryPoint = "lua_typename")]
	public static extern IntPtr _lua_typename(IntPtr L, int tp);
	public static string? lua_typename(IntPtr L, int tp)
	{
		return Marshal.PtrToStringAnsi(_lua_typename(L, tp));
	}
	
	[DllImport(DllName, CallingConvention = CallingConvention.StdCall)]
	public static extern double lua_tonumberx(IntPtr L, int idx, ref int isnum);
	
	[DllImport(DllName, CallingConvention = CallingConvention.StdCall)]
	public static extern long lua_tointegerx(IntPtr L, int idx, ref int isnum);
	
	[DllImport(DllName, CallingConvention = CallingConvention.StdCall)]
	public static extern int lua_toboolean(IntPtr L, int idx);
	
	[DllImport(DllName, CallingConvention = CallingConvention.StdCall, EntryPoint = "lua_tolstring")]
	public static extern IntPtr _lua_tolstring(IntPtr L, int idx, ref ulong len);
	public static string? lua_tolstring(IntPtr L, int idx, ref ulong len)
	{
		return Marshal.PtrToStringAnsi(_lua_tolstring(L, idx, ref len));
	}
	
	[DllImport(DllName, CallingConvention = CallingConvention.StdCall)]
	public static extern ulong lua_rawlen(IntPtr L, int idx);
	
	[DllImport(DllName, CallingConvention = CallingConvention.StdCall)]
	public static extern lua_CFunction lua_tocfunction(IntPtr L, int idx);
	
	[DllImport(DllName, CallingConvention = CallingConvention.StdCall)]
	public static extern IntPtr lua_touserdata(IntPtr L, int idx);
	
	[DllImport(DllName, CallingConvention = CallingConvention.StdCall)]
	public static extern IntPtr lua_tothread(IntPtr L, int idx);
	
	[DllImport(DllName, CallingConvention = CallingConvention.StdCall)]
	public static extern IntPtr lua_topointer(IntPtr L, int idx);
	
	public static readonly int LUA_OPADD = 0;
	public static readonly int LUA_OPSUB = 1;
	public static readonly int LUA_OPMUL = 2;
	public static readonly int LUA_OPMOD = 3;
	public static readonly int LUA_OPPOW = 4;
	public static readonly int LUA_OPDIV = 5;
	public static readonly int LUA_OPIDIV = 6;
	public static readonly int LUA_OPBAND = 7;
	public static readonly int LUA_OPBOR = 8;
	public static readonly int LUA_OPBXOR = 9;
	public static readonly int LUA_OPSHL = 10;
	public static readonly int LUA_OPSHR = 11;
	public static readonly int LUA_OPUNM = 12;
	public static readonly int LUA_OPBNOT = 13;
	
	[DllImport(DllName, CallingConvention = CallingConvention.StdCall)]
	public static extern void lua_arith(IntPtr L, int op);
	
	public static readonly int LUA_OPEQ = 0;
	public static readonly int LUA_OPLT = 1;
	public static readonly int LUA_OPLE = 2;
	
	[DllImport(DllName, CallingConvention = CallingConvention.StdCall)]
	public static extern int lua_rawequal(IntPtr L, int idx1, int idx2);
	
	[DllImport(DllName, CallingConvention = CallingConvention.StdCall)]
	public static extern int lua_compare(IntPtr L, int idx1, int idx2, int op);
	
	[DllImport(DllName, CallingConvention = CallingConvention.StdCall)]
	public static extern void lua_pushnil(IntPtr L);
	
	[DllImport(DllName, CallingConvention = CallingConvention.StdCall)]
	public static extern void lua_pushnumber(IntPtr L, double n);
	
	[DllImport(DllName, CallingConvention = CallingConvention.StdCall)]
	public static extern void lua_pushinteger(IntPtr L, long n);
	
	[DllImport(DllName, CallingConvention = CallingConvention.StdCall, EntryPoint = "lua_pushlstring")]
	public static extern IntPtr _lua_pushlstring(IntPtr L, string s, ulong len);
	public static string? lua_pushlstring(IntPtr L, string s, ulong len)
	{
		return Marshal.PtrToStringAnsi(_lua_pushlstring(L, s, len));
	}
	
	[DllImport(DllName, CallingConvention = CallingConvention.StdCall, EntryPoint = "lua_pushstring")]
	public static extern IntPtr _lua_pushstring(IntPtr L, string s);
	public static string? lua_pushstring(IntPtr L, string s)
	{
		return Marshal.PtrToStringAnsi(_lua_pushstring(L, s));
	}
	
	// TODO:
	// [DllImport(DllName, CallingConvention = CallingConvention.StdCall)]
	// public static extern IntPtr lua_pushvfstring(IntPtr L, string fmt, va_list argp);
	
	// TODO:
	// [DllImport(DllName, CallingConvention = CallingConvention.StdCall, EntryPoint = "lua_pushfstring")]
	// public static extern IntPtr _lua_pushfstring(IntPtr L, string fmt, params string[] args);
	// public static string? lua_pushfstring(IntPtr L, string fmt, params string[] args)
	// {
	// 	return Marshal.PtrToStringAnsi(_lua_pushfstring(L, fmt, args));
	// }
	
	[DllImport(DllName, CallingConvention = CallingConvention.StdCall)]
	public static extern void lua_pushcclosure(IntPtr L, lua_CFunction fn, int n);
	
	[DllImport(DllName, CallingConvention = CallingConvention.StdCall)]
	public static extern void lua_pushboolean(IntPtr L, int b);
	
	[DllImport(DllName, CallingConvention = CallingConvention.StdCall)]
	public static extern void lua_pushlightuserdata(IntPtr L, IntPtr p);
	
	[DllImport(DllName, CallingConvention = CallingConvention.StdCall)]
	public static extern int lua_pushthread(IntPtr L);
	
	[DllImport(DllName, CallingConvention = CallingConvention.StdCall)]
	public static extern int lua_getglobal(IntPtr L, string name);
	
	[DllImport(DllName, CallingConvention = CallingConvention.StdCall)]
	public static extern int lua_gettable(IntPtr L, int idx);
	
	[DllImport(DllName, CallingConvention = CallingConvention.StdCall)]
	public static extern int lua_getfield(IntPtr L, int idx, string k);
	
	[DllImport(DllName, CallingConvention = CallingConvention.StdCall)]
	public static extern int lua_geti(IntPtr L, int idx, long n);
	
	[DllImport(DllName, CallingConvention = CallingConvention.StdCall)]
	public static extern int lua_rawget(IntPtr L, int idx);
	
	[DllImport(DllName, CallingConvention = CallingConvention.StdCall)]
	public static extern int lua_rawgeti(IntPtr L, int idx, long n);
	
	[DllImport(DllName, CallingConvention = CallingConvention.StdCall)]
	public static extern int lua_rawgetp(IntPtr L, int idx, IntPtr p);
	
	[DllImport(DllName, CallingConvention = CallingConvention.StdCall)]
	public static extern void lua_createtable(IntPtr L, int narr, int nrec);
	
	[DllImport(DllName, CallingConvention = CallingConvention.StdCall)]
	public static extern IntPtr lua_newuserdata(IntPtr L, ulong sz);
	
	[DllImport(DllName, CallingConvention = CallingConvention.StdCall)]
	public static extern int lua_getmetatable(IntPtr L, int objindex);
	
	[DllImport(DllName, CallingConvention = CallingConvention.StdCall)]
	public static extern int lua_getuservalue(IntPtr L, int idx);
	
	[DllImport(DllName, CallingConvention = CallingConvention.StdCall)]
	public static extern void lua_setglobal(IntPtr L, string? name);
	
	[DllImport(DllName, CallingConvention = CallingConvention.StdCall)]
	public static extern void lua_settable(IntPtr L, int idx);
	
	[DllImport(DllName, CallingConvention = CallingConvention.StdCall)]
	public static extern void lua_setfield(IntPtr L, int idx, string k);
	
	[DllImport(DllName, CallingConvention = CallingConvention.StdCall)]
	public static extern void lua_seti(IntPtr L, int idx, long n);
	
	[DllImport(DllName, CallingConvention = CallingConvention.StdCall)]
	public static extern void lua_rawset(IntPtr L, int idx);
	
	[DllImport(DllName, CallingConvention = CallingConvention.StdCall)]
	public static extern void lua_rawseti(IntPtr L, int idx, long n);
	
	[DllImport(DllName, CallingConvention = CallingConvention.StdCall)]
	public static extern void lua_rawsetp(IntPtr L, int idx, IntPtr p);
	
	[DllImport(DllName, CallingConvention = CallingConvention.StdCall)]
	public static extern int lua_setmetatable(IntPtr L, int objindex);
	
	[DllImport(DllName, CallingConvention = CallingConvention.StdCall)]
	public static extern int lua_setuservalue(IntPtr L, int idx);
	
	[DllImport(DllName, CallingConvention = CallingConvention.StdCall)]
	public static extern void lua_callk(IntPtr L, int nargs, int nresults, IntPtr ctx, lua_KFunction? k);
	
	public static void lua_call(IntPtr L, int n, int r)
	{
		lua_callk(L, n, r, IntPtr.Zero, null);
	}
	
	[DllImport(DllName, CallingConvention = CallingConvention.StdCall)]
	public static extern int lua_pcallk(IntPtr L, int nargs, int nresults, int errfunc, IntPtr ctx, lua_KFunction? k);
	
	public static int lua_pcall(IntPtr L, int n, int r, int f)
	{
		return lua_pcallk(L, n, r, f, IntPtr.Zero, null);
	}
	
	[DllImport(DllName, CallingConvention = CallingConvention.StdCall)]
	public static extern int lua_load(IntPtr L, lua_Reader reader, IntPtr dt, string chunkname, string? mode);
	
	[DllImport(DllName, CallingConvention = CallingConvention.StdCall)]
	public static extern int lua_dump(IntPtr L, lua_Writer writer, IntPtr data, int strip);
	
	[DllImport(DllName, CallingConvention = CallingConvention.StdCall)]
	public static extern int lua_yieldk(IntPtr L, int nresults, IntPtr ctx, lua_KFunction? k);
	
	[DllImport(DllName, CallingConvention = CallingConvention.StdCall)]
	public static extern int lua_resume(IntPtr L, IntPtr from, int narg);
	
	[DllImport(DllName, CallingConvention = CallingConvention.StdCall)]
	public static extern int lua_status(IntPtr L);
	
	[DllImport(DllName, CallingConvention = CallingConvention.StdCall)]
	public static extern int lua_isyieldable(IntPtr L);
	
	public static int lua_yield(IntPtr L, int n)
	{
		return lua_yieldk(L, n, IntPtr.Zero, null);
	}
	
	public static readonly int LUA_GCSTOP = 0;
	public static readonly int LUA_GCRESTART = 1;
	public static readonly int LUA_GCCOLLECT = 2;
	public static readonly int LUA_GCCOUNT = 3;
	public static readonly int LUA_GCCOUNTB = 4;
	public static readonly int LUA_GCSTEP = 5;
	public static readonly int LUA_GCSETPAUSE = 6;
	public static readonly int LUA_GCSETSTEPMUL = 7;
	public static readonly int LUA_GCISRUNNING = 9;
	
	[DllImport(DllName, CallingConvention = CallingConvention.StdCall)]
	public static extern int lua_gc(IntPtr L, int what, int args);
	
	[DllImport(DllName, CallingConvention = CallingConvention.StdCall)]
	public static extern int lua_error(IntPtr L);
	
	[DllImport(DllName, CallingConvention = CallingConvention.StdCall)]
	public static extern int lua_next(IntPtr L, int idx);
	
	[DllImport(DllName, CallingConvention = CallingConvention.StdCall)]
	public static extern void lua_concat(IntPtr L, int n);
	
	[DllImport(DllName, CallingConvention = CallingConvention.StdCall)]
	public static extern void lua_len(IntPtr L, int idx);
	
	[DllImport(DllName, CallingConvention = CallingConvention.StdCall)]
	public static extern ulong lua_stringtonumber(IntPtr L, string s);
	
	[DllImport(DllName, CallingConvention = CallingConvention.StdCall)]
	public static extern lua_Alloc lua_getallocf(IntPtr L, ref IntPtr ud);
	
	[DllImport(DllName, CallingConvention = CallingConvention.StdCall)]
	public static extern void lua_setallocf(IntPtr L, lua_Alloc f, IntPtr ud);
	
	public static IntPtr lua_getextraspace(IntPtr L)
	{
		return L - 8;
	}
	
	public static double lua_tonumber(IntPtr L, int i)
	{
		int temp = 0; // NOP
		return lua_tonumberx(L, i, ref temp);
	}
	
	public static long lua_tointeger(IntPtr L, int i)
	{
		int temp = 0; // NOP
		return lua_tointegerx(L, i, ref temp);
	}
	
	public static void lua_pop(IntPtr L, int n)
	{
		lua_settop(L, -(n)-1);
	}
	
	public static void lua_newtable(IntPtr L)
	{
		lua_createtable(L, 0, 0);
	}
	
	public static void lua_register(IntPtr L, string n, lua_CFunction f)
	{
		lua_pushcclosure(L, f, 0);
		lua_setglobal(L, n);
	}
	
	public static void lua_pushcfunction(IntPtr L, lua_CFunction f)
	{
		lua_pushcclosure(L, f, 0);
	}
	
	public static int lua_isfunction(IntPtr L, int n)
	{
		return (lua_type(L, n) == LUA_TFUNCTION) ? 1 : 0;
	}
	
	public static int lua_istable(IntPtr L, int n)
	{
		return (lua_type(L, n) == LUA_TTABLE) ? 1 : 0;
	}
	
	public static int lua_islightuserdata(IntPtr L, int n)
	{
		return (lua_type(L, n) == LUA_TLIGHTUSERDATA) ? 1 : 0;
	}
	
	public static int lua_isnil(IntPtr L, int n)
	{
		return (lua_type(L, n) == LUA_TNIL) ? 1 : 0;
	}
	
	public static int lua_isboolean(IntPtr L, int n)
	{
		return (lua_type(L, n) == LUA_TBOOLEAN) ? 1 : 0;
	}
	
	public static int lua_isthread(IntPtr L, int n)
	{
		return (lua_type(L, n) == LUA_TTHREAD) ? 1 : 0;
	}
	
	public static int lua_isnone(IntPtr L, int n)
	{
		return (lua_type(L, n) == LUA_TNONE) ? 1 : 0;
	}
	
	public static int lua_isnoneornil(IntPtr L, int n)
	{
		return (lua_type(L, n) <= 0) ? 1 : 0;
	}
	
	public static string? lua_pushliteral(IntPtr L, string s)
	{
		return lua_pushstring(L, s);
	}
	
	public static void lua_pushglobaltable(IntPtr L)
	{
		lua_rawgeti(L, LUA_REGISTRYINDEX, LUA_RIDX_GLOBALS);
	}
	
	public static string? lua_tostring(IntPtr L, int i)
	{
		ulong temp = 0; // NOP
		return lua_tolstring(L, i, ref temp);
	}
	
	public static void lua_insert(IntPtr L, int idx)
	{
		lua_rotate(L, idx, 1);
	}
	
	public static void lua_remove(IntPtr L, int idx)
	{
		lua_rotate(L, idx, -1);
		lua_pop(L, 1);
	}
	
	public static void lua_replace(IntPtr L, int idx)
	{
		lua_copy(L, -1, idx);
		lua_pop(L, 1);
	}
	
	public static void lua_pushunsigned(IntPtr L, long n)
	{
		lua_pushinteger(L, n);
	}
	
	public static ulong lua_tounsignedx(IntPtr L, int i, ref int isnum)
	{
		return (ulong) lua_tointegerx(L, i, ref isnum);
	}
	
	public static ulong lua_tounsigned(IntPtr L, int i)
	{
		int temp = 0; // NOP
		return lua_tounsignedx(L, i, ref temp);
	}
	
	public static readonly int LUA_HOOKCALL = 0;
	public static readonly int LUA_HOOKRET = 1;
	public static readonly int LUA_HOOKLINE = 2;
	public static readonly int LUA_HOOKCOUNT = 3;
	public static readonly int LUA_HOOKTAILCALL = 4;
	
	public static readonly int LUA_MASKCALL = (1 << LUA_HOOKCALL);
	public static readonly int LUA_MASKRET = (1 << LUA_HOOKRET);
	public static readonly int LUA_MASKLINE = (1 << LUA_HOOKLINE);
	public static readonly int LUA_MASKCOUNT = (1 << LUA_HOOKCOUNT);
	
	[DllImport(DllName, CallingConvention = CallingConvention.StdCall)]
	public static extern int lua_getstack(IntPtr L, int level, lua_Debug ar);
	
	[DllImport(DllName, CallingConvention = CallingConvention.StdCall)]
	public static extern int lua_getinfo(IntPtr L, string what, lua_Debug ar);
	
	[DllImport(DllName, CallingConvention = CallingConvention.StdCall, EntryPoint = "lua_getlocal")]
	public static extern IntPtr _lua_getlocal(IntPtr L, lua_Debug ar, int n);
	public static string? lua_getlocal(IntPtr L, lua_Debug ar, int n)
	{
		return Marshal.PtrToStringAnsi(_lua_getlocal(L, ar, n));
	}
	
	[DllImport(DllName, CallingConvention = CallingConvention.StdCall, EntryPoint = "lua_setlocal")]
	public static extern IntPtr _lua_setlocal(IntPtr L, lua_Debug ar, int n);
	public static string? lua_setlocal(IntPtr L, lua_Debug ar, int n)
	{
		return Marshal.PtrToStringAnsi(_lua_setlocal(L, ar, n));
	}
	
	[DllImport(DllName, CallingConvention = CallingConvention.StdCall, EntryPoint = "lua_getupvalue")]
	public static extern IntPtr _lua_getupvalue(IntPtr L, int funcindex, int n);
	public static string? lua_getupvalue(IntPtr L, int funcindex, int n)
	{
		return Marshal.PtrToStringAnsi(_lua_getupvalue(L, funcindex, n));
	}
	
	[DllImport(DllName, CallingConvention = CallingConvention.StdCall, EntryPoint = "lua_setupvalue")]
	public static extern IntPtr _lua_setupvalue(IntPtr L, int funcindex, int n);
	public static string? lua_setupvalue(IntPtr L, int funcindex, int n)
	{
		return Marshal.PtrToStringAnsi(_lua_setupvalue(L, funcindex, n));
	}
	
	[DllImport(DllName, CallingConvention = CallingConvention.StdCall)]
	public static extern IntPtr lua_upvalueid(IntPtr L, int fidx, int n);
	
	[DllImport(DllName, CallingConvention = CallingConvention.StdCall)]
	public static extern void lua_upvaluejoin(IntPtr L, int fidx1, int n1, int fidx2, int n2);
	
	[DllImport(DllName, CallingConvention = CallingConvention.StdCall)]
	public static extern void lua_sethook(IntPtr L, lua_Hook func, int mask, int count);
	
	[DllImport(DllName, CallingConvention = CallingConvention.StdCall)]
	public static extern lua_Hook lua_gethook(IntPtr L);
	
	[DllImport(DllName, CallingConvention = CallingConvention.StdCall)]
	public static extern int lua_gethookmask(IntPtr L);
	
	[DllImport(DllName, CallingConvention = CallingConvention.StdCall)]
	public static extern int lua_gethookcount(IntPtr L);
	
	public static readonly int LUA_ERRFILE = LUA_ERRERR + 1;
	
	public static readonly string LUA_LOADED_TABLE = "_LOADED";
	public static readonly string LUA_RELOAD_TABLE = "_PRELOAD";
	
	[StructLayout(LayoutKind.Sequential, Pack = 1, CharSet = CharSet.Ansi)]
	public struct luaL_Reg {
		string name;
		lua_CFunction func;
	};
	
	public static readonly int LUAL_NUMSIZES = 8*16 + 8;
	
	[DllImport(DllName, CallingConvention = CallingConvention.StdCall)]
	public static extern void luaL_checkversion_(IntPtr L, double ver, ulong sz);
	
	public static void luaL_checkversion(IntPtr L)
	{
		luaL_checkversion_(L, LUA_VERSION_NUM, (ulong) LUAL_NUMSIZES);
	}
	
	[DllImport(DllName, CallingConvention = CallingConvention.StdCall)]
	public static extern int luaL_getmetafield(IntPtr L, int obj, string e);
	
	[DllImport(DllName, CallingConvention = CallingConvention.StdCall)]
	public static extern int luaL_callmeta(IntPtr L, int obj, string e);
	
	[DllImport(DllName, CallingConvention = CallingConvention.StdCall, EntryPoint = "luaL_tolstring")]
	public static extern IntPtr _luaL_tolstring(IntPtr L, int idx, ref ulong len);
	public static string? luaL_tolstring(IntPtr L, int idx, ref ulong len)
	{
		return Marshal.PtrToStringAnsi(_luaL_tolstring(L, idx, ref len));
	}
	
	[DllImport(DllName, CallingConvention = CallingConvention.StdCall)]
	public static extern int luaL_argerror(IntPtr L, int arg, string extramsg);
	
	[DllImport(DllName, CallingConvention = CallingConvention.StdCall, EntryPoint = "luaL_checklstring")]
	public static extern IntPtr _luaL_checklstring(IntPtr L, int arg, ref ulong l);
	public static string? luaL_checklstring(IntPtr L, int arg, ref ulong l)
	{
		return Marshal.PtrToStringAnsi(_luaL_checklstring(L, arg, ref l));
	}
	
	[DllImport(DllName, CallingConvention = CallingConvention.StdCall, EntryPoint = "luaL_optlstring")]
	public static extern IntPtr _luaL_optlstring(IntPtr L, int arg, string def, ref ulong l);
	public static string? luaL_optlstring(IntPtr L, int arg, string def, ref ulong l)
	{
		return Marshal.PtrToStringAnsi(_luaL_optlstring(L, arg, def, ref l));
	}
	
	[DllImport(DllName, CallingConvention = CallingConvention.StdCall)]
	public static extern double luaL_checknumber(IntPtr L, int arg);
	
	[DllImport(DllName, CallingConvention = CallingConvention.StdCall)]
	public static extern double luaL_optnumber(IntPtr L, int arg, double def);
	
	[DllImport(DllName, CallingConvention = CallingConvention.StdCall)]
	public static extern long luaL_checkinteger(IntPtr L, int arg);
	
	[DllImport(DllName, CallingConvention = CallingConvention.StdCall)]
	public static extern long luaL_optinteger(IntPtr L, int arg, long def);
	
	[DllImport(DllName, CallingConvention = CallingConvention.StdCall)]
	public static extern void luaL_checkstack(IntPtr L, int sz, string msg);
	
	[DllImport(DllName, CallingConvention = CallingConvention.StdCall)]
	public static extern void luaL_checktype(IntPtr L, int arg, int t);
	
	[DllImport(DllName, CallingConvention = CallingConvention.StdCall)]
	public static extern void luaL_checkany(IntPtr L, int arg);
	
	[DllImport(DllName, CallingConvention = CallingConvention.StdCall)]
	public static extern int luaL_newmetatable(IntPtr L, string tname);
	
	[DllImport(DllName, CallingConvention = CallingConvention.StdCall)]
	public static extern void luaL_setmetatable(IntPtr L, string tname);
	
	[DllImport(DllName, CallingConvention = CallingConvention.StdCall)]
	public static extern IntPtr luaL_testudata(IntPtr L, int ud, string tname);
	
	[DllImport(DllName, CallingConvention = CallingConvention.StdCall)]
	public static extern IntPtr luaL_checkudata(IntPtr L, int ud, string tname);
	
	[DllImport(DllName, CallingConvention = CallingConvention.StdCall)]
	public static extern void luaL_where(IntPtr L, int lvl);
	
	[DllImport(DllName, CallingConvention = CallingConvention.StdCall)]
	public static extern int luaL_error(IntPtr L, string fmt, params string[] args);
	
	// TODO: I dont think string[][] works
	[DllImport(DllName, CallingConvention = CallingConvention.StdCall)]
	public static extern int luaL_checkoption(IntPtr L, int arg, string def, string[][] lst);
	
	[DllImport(DllName, CallingConvention = CallingConvention.StdCall)]
	public static extern int luaL_fileresult(IntPtr L, int stat, string fname);
	
	[DllImport(DllName, CallingConvention = CallingConvention.StdCall)]
	public static extern int luaL_execresult(IntPtr L, int stat);
	
	public static readonly int LUA_NOREF = -2;
	public static readonly int LUA_REFNIL = -1;
	
	[DllImport(DllName, CallingConvention = CallingConvention.StdCall)]
	public static extern int luaL_ref(IntPtr L, int t);
	
	[DllImport(DllName, CallingConvention = CallingConvention.StdCall)]
	public static extern void luaL_unref(IntPtr L, int t, int _ref);
	
	[DllImport(DllName, CallingConvention = CallingConvention.StdCall)]
	public static extern int luaL_loadfilex(IntPtr L, string filename, string? mode);
	
	public static int luaL_loadfile(IntPtr L, string f)
	{
		return luaL_loadfilex(L, f, null);
	}
	
	[DllImport(DllName, CallingConvention = CallingConvention.StdCall)]
	public static extern int luaL_loadbufferx(IntPtr L, string buff, ulong sz, string name, string? mode);
	
	[DllImport(DllName, CallingConvention = CallingConvention.StdCall)]
	public static extern int luaL_loadstring(IntPtr L, string s);
	
	[DllImport(DllName, CallingConvention = CallingConvention.StdCall)]
	public static extern IntPtr luaL_newstate();
	
	[DllImport(DllName, CallingConvention = CallingConvention.StdCall)]
	public static extern long luaL_len(IntPtr L, int idx);
	
	[DllImport(DllName, CallingConvention = CallingConvention.StdCall, EntryPoint = "luaL_gsub")]
	public static extern IntPtr _luaL_gsub(IntPtr L, string s, string p, string r);
	public static string? luaL_gsub(IntPtr L, string s, string p, string r)
	{
		return Marshal.PtrToStringAnsi(_luaL_gsub(L, s, p, r));
	}
	
	[DllImport(DllName, CallingConvention = CallingConvention.StdCall)]
	public static extern void luaL_setfuncs(IntPtr L, luaL_Reg[] l, int nup);
	
	[DllImport(DllName, CallingConvention = CallingConvention.StdCall)]
	public static extern int luaL_getsubtable(IntPtr L, int idx, string fname);
	
	[DllImport(DllName, CallingConvention = CallingConvention.StdCall)]
	public static extern void luaL_traceback(IntPtr L, IntPtr L1, string msg, int level);
	
	[DllImport(DllName, CallingConvention = CallingConvention.StdCall)]
	public static extern void luaL_requiref(IntPtr L, string modname, lua_CFunction openf, int glb);
	
	public static void luaL_newlibtable(IntPtr L, luaL_Reg[] l)
	{
		lua_createtable(L, 0, l.Length - 1);
	}
	
	public static void luaL_newlib(IntPtr L, luaL_Reg[] l)
	{
		luaL_checkversion(L);
		luaL_newlibtable(L, l);
		luaL_setfuncs(L, l, 0);
	}
	
	public static void luaL_argcheck(IntPtr L, bool cond, int arg, string extramsg)
	{
		if (cond == false)
			luaL_argerror(L, arg, extramsg);
	}
	
	public static string? luaL_checkstring(IntPtr L, int n)
	{
		ulong temp = 0; // NOP
		return luaL_checklstring(L, n, ref temp);
	}
	
	public static string? luaL_optstring(IntPtr L, int n, string d)
	{
		ulong temp = 0; // NOP
		return luaL_optlstring(L, n, d, ref temp);
	}
	
	public static string? luaL_typename(IntPtr L, int i)
	{
		return lua_typename(L, lua_type(L, i));
	}
	
	public static int luaL_dofile(IntPtr L, string fn)
	{
		int status = luaL_loadfile(L, fn);
		if (status > 0)
			return status;
		return lua_pcall(L, 0, LUA_MULTRET, 0);
	}
	
	public static int luaL_dostring(IntPtr L, string s)
	{
		int status = luaL_loadstring(L, s);
		if (status > 0)
			return status;
		return lua_pcall(L, 0, LUA_MULTRET, 0);
	}
	
	public static int luaL_getmetatable(IntPtr L, string n)
	{
		return lua_getfield(L, LUA_REGISTRYINDEX, n);
	}
	
	public delegate T luaL_Function<T>(IntPtr L, int n);
	
	public static T luaL_opt<T>(IntPtr L, luaL_Function<T> f, int n, T d)
	{
		return lua_isnoneornil(L, n) > 0 ? d : f(L, n);
	}
	
	public static int luaL_loadbuffer(IntPtr L, string s, ulong sz, string n)
	{
		return luaL_loadbufferx(L, s, sz, n, null);
	}
	
	public static void luaL_addchar(luaL_Buffer B, sbyte c)
	{
		if (B.n >= B.size)
			luaL_prepbuffsize(B, 1);
		B.init[B.n++] = c;
	}
	
	public static void luaL_addsize(luaL_Buffer B, long s)
	{
		B.n = (ulong) ((long) B.n + s);
	}
	
	[DllImport(DllName, CallingConvention = CallingConvention.StdCall)]
	public static extern void luaL_buffinit(IntPtr L, luaL_Buffer B);
	
	[DllImport(DllName, CallingConvention = CallingConvention.StdCall, EntryPoint = "luaL_prepbuffsize")]
	public static extern IntPtr _luaL_prepbuffsize(luaL_Buffer B, ulong sz);
	public static string? luaL_prepbuffsize(luaL_Buffer b, ulong sz)
	{
		return Marshal.PtrToStringAnsi(_luaL_prepbuffsize(b, sz));
	}
	
	[DllImport(DllName, CallingConvention = CallingConvention.StdCall)]
	public static extern void luaL_addlstring(IntPtr B, string s, ulong l);
	
	[DllImport(DllName, CallingConvention = CallingConvention.StdCall)]
	public static extern void luaL_addstring(IntPtr B, string s);
	
	[DllImport(DllName, CallingConvention = CallingConvention.StdCall)]
	public static extern void luaL_addvalue(IntPtr B);
	
	[DllImport(DllName, CallingConvention = CallingConvention.StdCall)]
	public static extern void luaL_pushresult(IntPtr B);
	
	[DllImport(DllName, CallingConvention = CallingConvention.StdCall)]
	public static extern void luaL_pushresultsize(IntPtr B, ulong sz);
	
	[DllImport(DllName, CallingConvention = CallingConvention.StdCall)]
	public static extern IntPtr luaL_buffinitsize(IntPtr L, IntPtr B, ulong sz);
	
	public static string? luaL_prepbuffer(luaL_Buffer B)
	{
		return luaL_prepbuffsize(B, (ulong) LUAL_BUFFERSIZE);
	}
	
	[DllImport(DllName, CallingConvention = CallingConvention.StdCall)]
	public static extern void luaL_pushmodule(IntPtr L, string modname, int sizehint);
	
	[DllImport(DllName, CallingConvention = CallingConvention.StdCall)]
	public static extern void luaL_openlib(IntPtr L, string libname, luaL_Reg l, int nup);
	
	public static void luaL_register(IntPtr L, string n, luaL_Reg l)
	{
		luaL_openlib(L, n, l, 0);
	}
	
	public static void lua_writestring(string s)
	{
		Console.Write(s);
	}
	
	public static void lua_writeline()
	{
		lua_writestring("\n");
	}
	
	public static void lua_writestringerror(string s, params object?[] args)
	{
		Console.Error.WriteLine(s, args);
	}
	
	public static ulong luaL_checkunsigned(IntPtr L, int a)
	{
		return (ulong) luaL_checkinteger(L, a);
	}
	
	public static ulong luaL_optunsigned(IntPtr L, int a, long d)
	{
		return (ulong) luaL_optinteger(L, a, d);
	}
	
	public static int luaL_checkint(IntPtr L, int n)
	{
		return (int) luaL_checkinteger(L, n);
	}
	
	public static int luaL_optint(IntPtr L, int n, long d)
	{
		return (int) luaL_optinteger(L, n, d);
	}
	
	public static int luaL_checklong(IntPtr L, int n)
	{
		return (int) luaL_checkinteger(L, n);
	}
	
	public static int luaL_optlong(IntPtr L, int n, long d)
	{
		return (int) luaL_optinteger(L, n, d);
	}
	
	public static readonly string LUA_VERSUFFIX = "_" + LUA_VERSION_MAJOR + "_" + LUA_VERSION_MINOR;
	
	[DllImport(DllName, CallingConvention = CallingConvention.StdCall)]
	public static extern int luaopen_base(IntPtr L);
	
	[DllImport(DllName, CallingConvention = CallingConvention.StdCall)]
	public static extern int luaopen_coroutine(IntPtr L);
	
	[DllImport(DllName, CallingConvention = CallingConvention.StdCall)]
	public static extern int luaopen_table(IntPtr L);
	
	[DllImport(DllName, CallingConvention = CallingConvention.StdCall)]
	public static extern int luaopen_io(IntPtr L);
	
	[DllImport(DllName, CallingConvention = CallingConvention.StdCall)]
	public static extern int luaopen_os(IntPtr L);
	
	[DllImport(DllName, CallingConvention = CallingConvention.StdCall)]
	public static extern int luaopen_string(IntPtr L);
	
	[DllImport(DllName, CallingConvention = CallingConvention.StdCall)]
	public static extern int luaopen_utf8(IntPtr L);
	
	[DllImport(DllName, CallingConvention = CallingConvention.StdCall)]
	public static extern int luaopen_bit32(IntPtr L);
	
	[DllImport(DllName, CallingConvention = CallingConvention.StdCall)]
	public static extern int luaopen_math(IntPtr L);
	
	[DllImport(DllName, CallingConvention = CallingConvention.StdCall)]
	public static extern int luaopen_debug(IntPtr L);
	
	[DllImport(DllName, CallingConvention = CallingConvention.StdCall)]
	public static extern int luaopen_package(IntPtr L);
	
	[DllImport(DllName, CallingConvention = CallingConvention.StdCall)]
	public static extern void luaL_openlibs(IntPtr L);
	
}

