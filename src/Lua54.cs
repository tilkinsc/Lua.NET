namespace Lua54;

using System;
using System.Diagnostics;
using System.Runtime.InteropServices;

using voidp = System.UIntPtr;
using charp = System.IntPtr;
using size_t = System.UInt64;
using lua_State = System.UIntPtr;
using lua_Number = System.Double;
using lua_Integer = System.Int64;
using lua_Unsigned = System.UInt64;


public static class Lua
{
	
	private const string DllName = "Lua544.dll";
	private const CallingConvention Convention = CallingConvention.Cdecl;
	
	[StructLayout(LayoutKind.Sequential, Pack = 1, CharSet = CharSet.Ansi)]
	public struct lua_Debug {
		public int _event;
		public string name;
		public string namewhat;
		public string what;
		public string source;
		public size_t srclen;
		public int currentline;
		public int linedefined;
		public int lastlinedefined;
		public byte nups;
		public byte nparams;
		public sbyte isvararg;
		public sbyte istailcall;
		public ushort ftransfer;
		public ushort ntransfer;
		[MarshalAs(UnmanagedType.ByValArray, SizeConst = LUA_IDSIZE)]
		public sbyte[] short_src;
		public IntPtr i_ci;
	};
	
	[StructLayout(LayoutKind.Sequential, Pack = 1, CharSet = CharSet.Ansi)]
	public struct luaL_Reg {
		public string name;
		public lua_CFunction func;
	};
	
	[StructLayout(LayoutKind.Sequential, Pack = 1, CharSet = CharSet.Ansi)]
	public struct luaL_Buffer {
		public charp b;
		public size_t size;
		public size_t n;
		public lua_State L;
		[MarshalAs(UnmanagedType.ByValArray, SizeConst = LUAL_BUFFERSIZE)]
		public sbyte[] init;
	};
	
	public delegate int lua_CFunction(lua_State L);
	public delegate int lua_KFunction(lua_State L, int status, IntPtr ctx);
	public delegate charp lua_Reader(lua_State L, voidp ud, ref size_t sz);
	public delegate int lua_Writer(lua_State L, voidp p, size_t sz, voidp ud);
	public delegate voidp lua_Alloc(lua_State ud, voidp ptr, size_t osize, size_t nsize);
	public delegate void lua_WarnFunction(voidp ud, string msg, int tocont);
	public delegate void lua_Hook(lua_State L, lua_Debug ar);
	
	public const int LUAI_IS32INT = 1;
	
	public const int LUA_INT_INT = 1;
	public const int LUA_INT_LONG = 2;
	public const int LUA_INT_LONGLONG = 3;
	
	public const int LUA_FLOAT_FLOAT = 1;
	public const int LUA_FLOAT_DOUBLE = 2;
	public const int LUA_FLOAT_LONGDOUBLE = 3;
	
	public const int LUA_INT_DEFAULT = LUA_INT_LONGLONG;
	public const int LUA_FLOAT_DEFAULT = LUA_FLOAT_DOUBLE;
	
	public const int LUA_32BITS = 0;
	
	public const int LUA_INT_TYPE = LUA_INT_DEFAULT;
	public const int LUA_FLOAT_TYPE = LUA_FLOAT_DEFAULT;
	
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
	
	public const long LUA_MAXINTEGER = lua_Integer.MaxValue;
	public const long LUA_MININTEGER = lua_Integer.MinValue;
	
	public const ulong LUA_MAXUNSIGNED = lua_Unsigned.MaxValue;
	
	public const int LUAI_MAXSTACK = 1000000;
	
	public const int LUA_EXTRASPACE = 8;
	
	public const int LUA_IDSIZE = 60;
	
	public const int LUAL_BUFFERSIZE = 1024;
	
	public const string LUA_VERSION_MAJOR = "5";
	public const string LUA_VERSION_MINOR = "4";
	public const string LUA_VERSION_RELEASE = "4";
	
	public const int LUA_VERSION_NUM = 504;
	public const int LUA_VERSION_RELEASE_NUM = LUA_VERSION_NUM * 100 + 4;
	
	public const string LUA_VERSION = "Lua " + LUA_VERSION_MAJOR + "." + LUA_VERSION_MINOR;
	public const string LUA_RELEASE = LUA_VERSION + "." + LUA_VERSION_RELEASE;
	public const string LUA_COPYRIGHT = LUA_RELEASE + "  Copyright (C) 1994-2022 Lua.org, PUC-Rio";
	public const string LUA_AUTHORS = "R. Ierusalimschy, L. H. de Figueiredo, W. Celes";
	
	public const string LUA_SIGNATURE = "\x1bLua";
	
	public const int LUA_MULTRET = -1;
	
	public const int LUA_REGISTRYINDEX = (-LUAI_MAXSTACK - 1000);
	
	public static int lua_upvalueindex(int i)
	{
		return LUA_REGISTRYINDEX - i;
	}
	
	public const int LUA_OK = 0;
	public const int LUA_YIELD = 1;
	public const int LUA_ERRRUN = 2;
	public const int LUA_ERRSYNTAX = 3;
	public const int LUA_ERRMEM = 4;
	public const int LUA_ERRERR = 5;
	
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
	
	public const int LUA_NUMTYPES = 9;
	
	public const int LUA_MINSTACK = 20;
	
	public const int LUA_RIDX_MAINTHREAD = 1;
	public const int LUA_RIDX_GLOBALS = 2;
	public const int LUA_RIDX_LAST = LUA_RIDX_GLOBALS;
	
	[DllImport(DllName, CallingConvention = Convention)]
	public static extern lua_State lua_newstate(lua_Alloc f, voidp ud);
	
	[DllImport(DllName, CallingConvention = Convention)]
	public static extern void lua_close(lua_State L);
	
	[DllImport(DllName, CallingConvention = Convention)]
	public static extern lua_State lua_newthread(lua_State L);
	
	[DllImport(DllName, CallingConvention = Convention)]
	public static extern int lua_resetthread(lua_State L);
	
	[DllImport(DllName, CallingConvention = Convention)]
	public static extern lua_CFunction lua_atpanic(lua_State L, lua_CFunction panicf);
	
	[DllImport(DllName, CallingConvention = Convention)]
	public static extern double lua_version(lua_State L);
	
	[DllImport(DllName, CallingConvention = Convention)]
	public static extern int lua_absindex(lua_State L, int idx);
	
	[DllImport(DllName, CallingConvention = Convention)]
	public static extern int lua_gettop(lua_State L);
	
	[DllImport(DllName, CallingConvention = Convention)]
	public static extern void lua_settop(lua_State L, int idx);
	
	[DllImport(DllName, CallingConvention = Convention)]
	public static extern void lua_pushvalue(lua_State L, int idx);
	
	[DllImport(DllName, CallingConvention = Convention)]
	public static extern void lua_rotate(lua_State L, int idx, int n);
	
	[DllImport(DllName, CallingConvention = Convention)]
	public static extern void lua_copy(lua_State L, int fromidx, int toidx);
	
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
	public static extern IntPtr _lua_typename(lua_State L, int tp);
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
	public static extern IntPtr _lua_tolstring(lua_State L, int idx, ref size_t len);
	public static string? lua_tolstring(lua_State L, int idx, ref size_t len)
	{
		return Marshal.PtrToStringAnsi(_lua_tolstring(L, idx, ref len));
	}
	
	[DllImport(DllName, CallingConvention = Convention)]
	public static extern lua_Unsigned lua_rawlen(lua_State L, int idx);
	
	[DllImport(DllName, CallingConvention = Convention)]
	public static extern lua_CFunction lua_tocfunction(lua_State L, int idx);
	
	[DllImport(DllName, CallingConvention = Convention)]
	public static extern voidp lua_touserdata(lua_State L, int idx);
	
	[DllImport(DllName, CallingConvention = Convention)]
	public static extern lua_State lua_tothread(lua_State L, int idx);
	
	[DllImport(DllName, CallingConvention = Convention)]
	public static extern voidp lua_topointer(lua_State L, int idx);
	
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
	
	[DllImport(DllName, CallingConvention = Convention)]
	public static extern void lua_arith(lua_State L, int op);
	
	public const int LUA_OPEQ = 0;
	public const int LUA_OPLT = 1;
	public const int LUA_OPLE = 2;
	
	[DllImport(DllName, CallingConvention = Convention)]
	public static extern int lua_rawequal(lua_State L, int idx1, int idx2);
	
	[DllImport(DllName, CallingConvention = Convention)]
	public static extern int lua_compare(lua_State L, int idx1, int idx2, int op);
	
	[DllImport(DllName, CallingConvention = Convention)]
	public static extern void lua_pushnil(lua_State L);
	
	[DllImport(DllName, CallingConvention = Convention)]
	public static extern void lua_pushnumber(lua_State L, lua_Number n);
	
	[DllImport(DllName, CallingConvention = Convention)]
	public static extern void lua_pushinteger(lua_State L, lua_Integer n);
	
	[DllImport(DllName, CallingConvention = Convention, EntryPoint = "lua_pushlstring")]
	public static extern IntPtr _lua_pushlstring(lua_State L, string s, size_t len);
	public static string? lua_pushlstring(lua_State L, string s, size_t len)
	{
		return Marshal.PtrToStringAnsi(_lua_pushlstring(L, s, len));
	}
	
	[DllImport(DllName, CallingConvention = Convention, EntryPoint = "lua_pushstring")]
	public static extern IntPtr _lua_pushstring(lua_State L, string s);
	public static string? lua_pushstring(lua_State L, string s)
	{
		return Marshal.PtrToStringAnsi(_lua_pushstring(L, s));
	}
	
	// TODO:
	// [DllImport(DllName, CallingConvention = Convention)]
	// public static extern IntPtr lua_pushvfstring(lua_State L, string fmt, va_list argp);
	
	// TODO:
	// [DllImport(DllName, CallingConvention = Convention, EntryPoint = "lua_pushfstring")]
	// public static extern IntPtr _lua_pushfstring(lua_State L, string fmt, params string[] args);
	// public static string? lua_pushfstring(lua_State L, string fmt, params string[] args)
	// {
	// 	return Marshal.PtrToStringAnsi(_lua_pushfstring(L, fmt, args));
	// }
	
	[DllImport(DllName, CallingConvention = Convention)]
	public static extern void lua_pushcclosure(lua_State L, lua_CFunction fn, int n);
	
	[DllImport(DllName, CallingConvention = Convention)]
	public static extern void lua_pushboolean(lua_State L, int b);
	
	[DllImport(DllName, CallingConvention = Convention)]
	public static extern void lua_pushlightuserdata(lua_State L, voidp p);
	
	[DllImport(DllName, CallingConvention = Convention)]
	public static extern int lua_pushthread(lua_State L);
	
	[DllImport(DllName, CallingConvention = Convention)]
	public static extern int lua_getglobal(lua_State L, string name);
	
	[DllImport(DllName, CallingConvention = Convention)]
	public static extern int lua_gettable(lua_State L, int idx);
	
	[DllImport(DllName, CallingConvention = Convention)]
	public static extern int lua_getfield(lua_State L, int idx, string k);
	
	[DllImport(DllName, CallingConvention = Convention)]
	public static extern int lua_geti(lua_State L, int idx, lua_Integer n);
	
	[DllImport(DllName, CallingConvention = Convention)]
	public static extern int lua_rawget(lua_State L, int idx);
	
	[DllImport(DllName, CallingConvention = Convention)]
	public static extern int lua_rawgeti(lua_State L, int idx, lua_Integer n);
	
	[DllImport(DllName, CallingConvention = Convention)]
	public static extern int lua_rawgetp(lua_State L, int idx, voidp p);
	
	[DllImport(DllName, CallingConvention = Convention)]
	public static extern void lua_createtable(lua_State L, int narr, int nrec);
	
	[DllImport(DllName, CallingConvention = Convention)]
	public static extern voidp lua_newuserdatauv(lua_State L, size_t sz, int nuvalue);
	
	[DllImport(DllName, CallingConvention = Convention)]
	public static extern int lua_getmetatable(lua_State L, int objindex);
	
	[DllImport(DllName, CallingConvention = Convention)]
	public static extern int lua_getiuservalue(lua_State L, int idx, int n);
	
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
	public static extern void lua_rawsetp(lua_State L, int idx, voidp p);
	
	[DllImport(DllName, CallingConvention = Convention)]
	public static extern int lua_setmetatable(lua_State L, int objindex);
	
	[DllImport(DllName, CallingConvention = Convention)]
	public static extern int lua_setiuservalue(lua_State L, int idx, int n);
	
	[DllImport(DllName, CallingConvention = Convention)]
	public static extern void lua_callk(lua_State L, int nargs, int nresults, IntPtr ctx, lua_KFunction? k);
	
	public static void lua_call(lua_State L, int n, int r)
	{
		lua_callk(L, n, r, IntPtr.Zero, null);
	}
	
	[DllImport(DllName, CallingConvention = Convention)]
	public static extern int lua_pcallk(lua_State L, int nargs, int nresults, int errfunc, IntPtr ctx, lua_KFunction? k);
	
	public static int lua_pcall(lua_State L, int n, int r, int f)
	{
		return lua_pcallk(L, n, r, f, IntPtr.Zero, null);
	}
	
	[DllImport(DllName, CallingConvention = Convention)]
	public static extern int lua_load(lua_State L, lua_Reader reader, voidp dt, string chunkname, string? mode);
	
	[DllImport(DllName, CallingConvention = Convention)]
	public static extern int lua_dump(lua_State L, lua_Writer writer, voidp data, int strip);
	
	[DllImport(DllName, CallingConvention = Convention)]
	public static extern int lua_yieldk(lua_State L, int nresults, IntPtr ctx, lua_KFunction? k);
	
	[DllImport(DllName, CallingConvention = Convention)]
	public static extern int lua_resume(lua_State L, lua_State from, int narg, ref int nres);
	
	[DllImport(DllName, CallingConvention = Convention)]
	public static extern int lua_status(lua_State L);
	
	[DllImport(DllName, CallingConvention = Convention)]
	public static extern int lua_isyieldable(lua_State L);
	
	public static int lua_yield(lua_State L, int n)
	{
		return lua_yieldk(L, n, IntPtr.Zero, null);
	}
	
	[DllImport(DllName, CallingConvention = Convention)]
	public static extern void lua_setwarnf(lua_State L, lua_WarnFunction f, voidp ud);
	
	[DllImport(DllName, CallingConvention = Convention)]
	public static extern void lua_warning(lua_State L, string msg, int tocont);
	
	public const int LUA_GCSTOP = 0;
	public const int LUA_GCRESTART = 1;
	public const int LUA_GCCOLLECT = 2;
	public const int LUA_GCCOUNT = 3;
	public const int LUA_GCCOUNTB = 4;
	public const int LUA_GCSTEP = 5;
	public const int LUA_GCSETPAUSE = 6;
	public const int LUA_GCSETSTEPMUL = 7;
	public const int LUA_GCISRUNNING = 9;
	public const int LUA_GCGEN = 10;
	public const int LUA_GCINC = 11;
	
	// TODO: I dont think params works
	[DllImport(DllName, CallingConvention = Convention)]
	public static extern int lua_gc(lua_State L, int what, params int[] args);
	
	[DllImport(DllName, CallingConvention = Convention)]
	public static extern int lua_error(lua_State L);
	
	[DllImport(DllName, CallingConvention = Convention)]
	public static extern int lua_next(lua_State L, int idx);
	
	[DllImport(DllName, CallingConvention = Convention)]
	public static extern void lua_concat(lua_State L, int n);
	
	[DllImport(DllName, CallingConvention = Convention)]
	public static extern void lua_len(lua_State L, int idx);
	
	[DllImport(DllName, CallingConvention = Convention)]
	public static extern size_t lua_stringtonumber(lua_State L, string s);
	
	[DllImport(DllName, CallingConvention = Convention)]
	public static extern lua_Alloc lua_getallocf(lua_State L, ref voidp ud);
	
	[DllImport(DllName, CallingConvention = Convention)]
	public static extern void lua_setallocf(lua_State L, lua_Alloc f, voidp ud);
	
	[DllImport(DllName, CallingConvention = Convention)]
	public static extern void lua_toclose(lua_State L, int idx);
	
	[DllImport(DllName, CallingConvention = Convention)]
	public static extern void lua_closeslot(lua_State L, int idx);
	
	public static voidp lua_getextraspace(lua_State L)
	{
		return L - 8;
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
		lua_settop(L, -(n)-1);
	}
	
	public static void lua_newtable(lua_State L)
	{
		lua_createtable(L, 0, 0);
	}
	
	public static void lua_register(lua_State L, string n, lua_CFunction f)
	{
		lua_pushcclosure(L, f, 0);
		lua_setglobal(L, n);
	}
	
	public static void lua_pushcfunction(lua_State L, lua_CFunction f)
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
		size_t temp = 0; // NOP
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
	
	public static voidp lua_newuserdata(lua_State L, size_t s)
	{
		return lua_newuserdatauv(L, s, 1);
	}
	
	public static int lua_getuservalue(lua_State L, int idx)
	{
		return lua_getiuservalue(L, idx, 1);
	}
	
	public static int lua_setuservalue(lua_State L, int idx)
	{
		return lua_setiuservalue(L, idx, 1);
	}
	
	public const int LUA_NUMTAGS = LUA_NUMTYPES;
	
	public const int LUA_HOOKCALL = 0;
	public const int LUA_HOOKRET = 1;
	public const int LUA_HOOKLINE = 2;
	public const int LUA_HOOKCOUNT = 3;
	public const int LUA_HOOKTAILCALL = 4;
	
	public const int LUA_MASKCALL = (1 << LUA_HOOKCALL);
	public const int LUA_MASKRET = (1 << LUA_HOOKRET);
	public const int LUA_MASKLINE = (1 << LUA_HOOKLINE);
	public const int LUA_MASKCOUNT = (1 << LUA_HOOKCOUNT);
	
	[DllImport(DllName, CallingConvention = Convention)]
	public static extern int lua_getstack(lua_State L, int level, lua_Debug ar);
	
	[DllImport(DllName, CallingConvention = Convention)]
	public static extern int lua_getinfo(lua_State L, string what, lua_Debug ar);
	
	[DllImport(DllName, CallingConvention = Convention, EntryPoint = "lua_getlocal")]
	public static extern IntPtr _lua_getlocal(lua_State L, lua_Debug ar, int n);
	public static string? lua_getlocal(lua_State L, lua_Debug ar, int n)
	{
		return Marshal.PtrToStringAnsi(_lua_getlocal(L, ar, n));
	}
	
	[DllImport(DllName, CallingConvention = Convention, EntryPoint = "lua_setlocal")]
	public static extern IntPtr _lua_setlocal(lua_State L, lua_Debug ar, int n);
	public static string? lua_setlocal(lua_State L, lua_Debug ar, int n)
	{
		return Marshal.PtrToStringAnsi(_lua_setlocal(L, ar, n));
	}
	
	[DllImport(DllName, CallingConvention = Convention, EntryPoint = "lua_getupvalue")]
	public static extern IntPtr _lua_getupvalue(lua_State L, int funcindex, int n);
	public static string? lua_getupvalue(lua_State L, int funcindex, int n)
	{
		return Marshal.PtrToStringAnsi(_lua_getupvalue(L, funcindex, n));
	}
	
	[DllImport(DllName, CallingConvention = Convention, EntryPoint = "lua_setupvalue")]
	public static extern IntPtr _lua_setupvalue(lua_State L, int funcindex, int n);
	public static string? lua_setupvalue(lua_State L, int funcindex, int n)
	{
		return Marshal.PtrToStringAnsi(_lua_setupvalue(L, funcindex, n));
	}
	
	[DllImport(DllName, CallingConvention = Convention)]
	public static extern voidp lua_upvalueid(lua_State L, int fidx, int n);
	
	[DllImport(DllName, CallingConvention = Convention)]
	public static extern void lua_upvaluejoin(lua_State L, int fidx1, int n1, int fidx2, int n2);
	
	[DllImport(DllName, CallingConvention = Convention)]
	public static extern void lua_sethook(lua_State L, lua_Hook func, int mask, int count);
	
	[DllImport(DllName, CallingConvention = Convention)]
	public static extern lua_Hook lua_gethook(lua_State L);
	
	[DllImport(DllName, CallingConvention = Convention)]
	public static extern int lua_gethookmask(lua_State L);
	
	[DllImport(DllName, CallingConvention = Convention)]
	public static extern int lua_gethookcount(lua_State L);
	
	[DllImport(DllName, CallingConvention = Convention)]
	public static extern int lua_setcstacklimit(lua_State L, uint limit);
	
	public const string LUA_GNAME = "_G";
	
	public const int LUA_ERRFILE = LUA_ERRERR + 1;
	
	public const string LUA_LOADED_TABLE = "_LOADED";
	public const string LUA_RELOAD_TABLE = "_PRELOAD";
	
	public const int LUAL_NUMSIZES = 8*16 + 8;
	
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
	public static extern IntPtr _luaL_tolstring(lua_State L, int idx, ref size_t len);
	public static string? luaL_tolstring(lua_State L, int idx, ref size_t len)
	{
		return Marshal.PtrToStringAnsi(_luaL_tolstring(L, idx, ref len));
	}
	
	[DllImport(DllName, CallingConvention = Convention)]
	public static extern int luaL_argerror(lua_State L, int arg, string extramsg);
	
	[DllImport(DllName, CallingConvention = Convention)]
	public static extern int luaL_typeerror(lua_State L, int arg, string tname);
	
	[DllImport(DllName, CallingConvention = Convention, EntryPoint = "luaL_checklstring")]
	public static extern IntPtr _luaL_checklstring(lua_State L, int arg, ref size_t l);
	public static string? luaL_checklstring(lua_State L, int arg, ref size_t l)
	{
		return Marshal.PtrToStringAnsi(_luaL_checklstring(L, arg, ref l));
	}
	
	[DllImport(DllName, CallingConvention = Convention, EntryPoint = "luaL_optlstring")]
	public static extern IntPtr _luaL_optlstring(lua_State L, int arg, string def, ref size_t l);
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
	public static extern voidp luaL_testudata(lua_State L, int ud, string tname);
	
	[DllImport(DllName, CallingConvention = Convention)]
	public static extern voidp luaL_checkudata(lua_State L, int ud, string tname);
	
	[DllImport(DllName, CallingConvention = Convention)]
	public static extern void luaL_where(lua_State L, int lvl);
	
	// TODO: I dont think params works
	[DllImport(DllName, CallingConvention = Convention)]
	public static extern int luaL_error(lua_State L, string fmt, params string[] args);
	
	// TODO: I dont think string[][] works
	[DllImport(DllName, CallingConvention = Convention)]
	public static extern int luaL_checkoption(lua_State L, int arg, string def, string[][] lst);
	
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
	
	[DllImport(DllName, CallingConvention = Convention)]
	public static extern void luaL_addgsub(luaL_Buffer b, string s, string p, string r);
	
	[DllImport(DllName, CallingConvention = Convention, EntryPoint = "luaL_gsub")]
	public static extern IntPtr _luaL_gsub(lua_State L, string s, string p, string r);
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
	
	[DllImport(DllName, CallingConvention = Convention)]
	public static extern void luaL_requiref(lua_State L, string modname, lua_CFunction openf, int glb);
	
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
	
	public static void luaL_argexpected(lua_State L, bool cond, int arg, string tname)
	{
		if (cond == false)
			luaL_typeerror(L, arg, tname);
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
	
	public static void luaL_pushfail(lua_State L)
	{
		lua_pushnil(L);
	}
	
	public static size_t luaL_bufferlen(luaL_Buffer bf)
	{
		return bf.n;
	}
	
	public static charp luaL_buffaddr(luaL_Buffer bf)
	{
		return bf.b;
	}
	
	public static void luaL_addchar(luaL_Buffer B, byte c)
	{
		if (B.n >= B.size)
			luaL_prepbuffsize(B, 1);
		Marshal.WriteByte(new IntPtr(B.b.ToInt64() + (long) B.n), c);
		B.n += 1;
	}
	
	public static void luaL_addsize(luaL_Buffer B, long s)
	{
		B.n = (size_t) ((long) B.n + s);
	}
	
	public static void luaL_buffsub(luaL_Buffer B, long s)
	{
		B.n = (size_t) ((long) B.n - s);
	}
	
	[DllImport(DllName, CallingConvention = Convention)]
	public static extern int luaL_buffinit(lua_State L, luaL_Buffer B);
	
	[DllImport(DllName, CallingConvention = Convention)]
	public static extern charp luaL_prepbuffsize(luaL_Buffer B, size_t sz);
	
	[DllImport(DllName, CallingConvention = Convention)]
	public static extern void luaL_addlstring(IntPtr B, string s, size_t l);
	
	[DllImport(DllName, CallingConvention = Convention)]
	public static extern void luaL_addstring(IntPtr B, string s);
	
	[DllImport(DllName, CallingConvention = Convention)]
	public static extern void luaL_addvalue(IntPtr B, string s);
	
	[DllImport(DllName, CallingConvention = Convention)]
	public static extern void luaL_pushresult(IntPtr B);
	
	[DllImport(DllName, CallingConvention = Convention)]
	public static extern void luaL_pushresultsize(IntPtr B, size_t sz);
	
	[DllImport(DllName, CallingConvention = Convention)]
	public static extern charp luaL_buffinitsize(lua_State L, luaL_Buffer B, size_t sz);
	
	public static charp luaL_prepbuffer(luaL_Buffer B)
	{
		return luaL_prepbuffsize(B, LUAL_BUFFERSIZE);
	}
	
	public const string LUA_FILEHANDLED = "FILE*";
	
	public const string LUA_VERSUFFIX = "_" + LUA_VERSION_MAJOR + "_" + LUA_VERSION_MINOR;
	
	public const string LUA_COLIBNAME = "coroutine";
	public const string LUA_TABLIBNAME = "table";
	public const string IOLIBNAME = "io";
	public const string OSLIBNAME = "os";
	public const string LUA_STRLIBNAME = "string";
	public const string LUA_UTF8LIBNAME = "utf8";
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
	public static extern int luaopen_math(lua_State L);
	
	[DllImport(DllName, CallingConvention = Convention)]
	public static extern int luaopen_debug(lua_State L);
	
	[DllImport(DllName, CallingConvention = Convention)]
	public static extern int luaopen_package(lua_State L);
	
	[DllImport(DllName, CallingConvention = Convention)]
	public static extern void luaL_openlibs(lua_State L);
	
}

