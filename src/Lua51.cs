namespace Lua51;

using System;
using System.Runtime.InteropServices;


public static class Lua
{
	
	private const string DllName = "Lua515.dll";
	
	public const string LUA_LDIR = "!\\lua\\";
	public const string LUA_CDIR = "!\\";
	public const string LUA_PATH_DEFAULT = ".\\?.lua;" + LUA_LDIR + "?.lua;" + LUA_LDIR + "?\\init.lua;" + LUA_CDIR + "?.lua;" + LUA_CDIR + "?\\init.lua";
	public const string LUA_CPATH_DEFAULT = ".\\?.dll;" + LUA_CDIR + "?.dll;" + LUA_CDIR + "loadall.dll";
	
	public const string LUA_DIRSEP = "\\";
	
	public const string LUA_PATHSEP = ";";
	public const string LUA_PATH_MARK = "?";
	public const string LUA_EXECDIR = "!";
	public const string LUA_IGMARK = "-";
	
	public const int LUA_IDSIZE = 60;
	
	public const int LUAI_MAXCALLS = 20000;
	public const int LUAI_MAXCSTACK = 8000;
	public const int LUAI_BITSINT = 32;
	public const int LUAI_MAXCCALLS = 200;
	public const int LUAI_MAXVARS = 200;
	public const int LUAI_MAXUPVALUES = 60;
	public const int LUAL_BUFFERSIZE = 512;
	
	public const int LUA_MAXCAPTURES = 32;
	
	public const string LUA_VERSION = "Lua 5.1";
	public const string LUA_RELEASE = "Lua 5.1.5";
	public const int LUA_VERSION_NUM = 501;
	public const string LUA_COPYRIGHT = "Copyright (C) 1994-2012 Lua.org, PUC-Rio";
	public const string LUA_AUTHORS = "R. Ierusalimschy, L. H. de Figueiredo & W. Celes";
	
	// TODO: \033Lua
	public const string LUA_SIGNATURE = "\x1bLua";
	
	public const int LUA_MULTRET = -1;
	
	public const int LUA_REGISTRYINDEX = -10000;
	public const int LUA_ENVIRONINDEX = -10001;
	public const int LUA_GLOBALSINDEX = -10002;
	
	public static int lua_upvalueindex(int i)
	{
		return LUA_GLOBALSINDEX - i;
	}
	
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
	
	public const int LUA_MINSTACK = 20;
	
	[DllImport(DllName, CallingConvention = CallingConvention.StdCall)]
	public static extern IntPtr lua_newstate(lua_Alloc f, IntPtr ud);
	
	[DllImport(DllName, CallingConvention = CallingConvention.StdCall)]
	public static extern void lua_close(IntPtr L);
	
	[DllImport(DllName, CallingConvention = CallingConvention.StdCall)]
	public static extern IntPtr lua_newthread(IntPtr L);
	
	[DllImport(DllName, CallingConvention = CallingConvention.StdCall)]
	public static extern lua_CFunction lua_atpanic(IntPtr L, lua_CFunction panicf);
	
	[DllImport(DllName, CallingConvention = CallingConvention.StdCall)]
	public static extern int lua_gettop(IntPtr L);
	
	[DllImport(DllName, CallingConvention = CallingConvention.StdCall)]
	public static extern void lua_settop(IntPtr L, int idx);
	
	[DllImport(DllName, CallingConvention = CallingConvention.StdCall)]
	public static extern void lua_pushvalue(IntPtr L, int idx);
	
	[DllImport(DllName, CallingConvention = CallingConvention.StdCall)]
	public static extern void lua_remove(IntPtr L, int idx);
	
	[DllImport(DllName, CallingConvention = CallingConvention.StdCall)]
	public static extern void lua_insert(IntPtr L, int idx);
	
	[DllImport(DllName, CallingConvention = CallingConvention.StdCall)]
	public static extern void lua_replace(IntPtr L, int idx);
	
	[DllImport(DllName, CallingConvention = CallingConvention.StdCall)]
	public static extern int lua_checkstack(IntPtr L, int sz);
	
	[DllImport(DllName, CallingConvention = CallingConvention.StdCall)]
	public static extern void lua_xmove(IntPtr L, IntPtr to, int n);
	
	[DllImport(DllName, CallingConvention = CallingConvention.StdCall)]
	public static extern int lua_isnumber(IntPtr L, int idx);
	
	[DllImport(DllName, CallingConvention = CallingConvention.StdCall)]
	public static extern int lua_isstring(IntPtr L, int idx);
	
	[DllImport(DllName, CallingConvention = CallingConvention.StdCall)]
	public static extern int lua_iscfunction(IntPtr L, int idx);
	
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
	public static extern int lua_equal(IntPtr L, int idx1, int idx2);
	
	[DllImport(DllName, CallingConvention = CallingConvention.StdCall)]
	public static extern int lua_rawequal(IntPtr L, int idx1, int idx2);
	
	[DllImport(DllName, CallingConvention = CallingConvention.StdCall)]
	public static extern int lua_lessthan(IntPtr L, int idx1, int idx2);
	
	[DllImport(DllName, CallingConvention = CallingConvention.StdCall)]
	public static extern double lua_tonumber(IntPtr L, int idx);
	
	[DllImport(DllName, CallingConvention = CallingConvention.StdCall)]
	public static extern long lua_tointegerx(IntPtr L, int idx);
	
	[DllImport(DllName, CallingConvention = CallingConvention.StdCall)]
	public static extern int lua_toboolean(IntPtr L, int idx);
	
	[DllImport(DllName, CallingConvention = CallingConvention.StdCall, EntryPoint = "lua_tolstring")]
	public static extern IntPtr _lua_tolstring(IntPtr L, int idx, ref ulong len);
	public static string? lua_tolstring(IntPtr L, int idx, ref ulong len)
	{
		return Marshal.PtrToStringAnsi(_lua_tolstring(L, idx, ref len));
	}
	
	[DllImport(DllName, CallingConvention = CallingConvention.StdCall)]
	public static extern ulong lua_objlen(IntPtr L, int idx);
	
	[DllImport(DllName, CallingConvention = CallingConvention.StdCall)]
	public static extern lua_CFunction lua_tocfunction(IntPtr L, int idx);
	
	[DllImport(DllName, CallingConvention = CallingConvention.StdCall)]
	public static extern IntPtr lua_touserdata(IntPtr L, int idx);
	
	[DllImport(DllName, CallingConvention = CallingConvention.StdCall)]
	public static extern IntPtr lua_tothread(IntPtr L, int idx);
	
	[DllImport(DllName, CallingConvention = CallingConvention.StdCall)]
	public static extern IntPtr lua_topointer(IntPtr L, int idx);
	
	[DllImport(DllName, CallingConvention = CallingConvention.StdCall)]
	public static extern void lua_pushnil(IntPtr L);
	
	[DllImport(DllName, CallingConvention = CallingConvention.StdCall)]
	public static extern void lua_pushnumber(IntPtr L, double n);
	
	[DllImport(DllName, CallingConvention = CallingConvention.StdCall)]
	public static extern void lua_pushinteger(IntPtr L, long n);
	
	[DllImport(DllName, CallingConvention = CallingConvention.StdCall)]
	public static extern void lua_pushlstring(IntPtr L, string s, ulong l);
	
	[DllImport(DllName, CallingConvention = CallingConvention.StdCall)]
	public static extern void lua_pushstring(IntPtr L, string s);
	
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
	public static extern void lua_gettable(IntPtr L, int idx);
	
	[DllImport(DllName, CallingConvention = CallingConvention.StdCall)]
	public static extern void lua_getfield(IntPtr L, int idx, string k);
	
	[DllImport(DllName, CallingConvention = CallingConvention.StdCall)]
	public static extern void lua_rawget(IntPtr L, int idx);
	
	[DllImport(DllName, CallingConvention = CallingConvention.StdCall)]
	public static extern void lua_rawgeti(IntPtr L, int idx, int n);
	
	[DllImport(DllName, CallingConvention = CallingConvention.StdCall)]
	public static extern void lua_createtable(IntPtr L, int narr, int nrec);
	
	[DllImport(DllName, CallingConvention = CallingConvention.StdCall)]
	public static extern IntPtr lua_newuserdata(IntPtr L, ulong sz);
	
	[DllImport(DllName, CallingConvention = CallingConvention.StdCall)]
	public static extern int lua_getmetatable(IntPtr L, int objindex);
	
	[DllImport(DllName, CallingConvention = CallingConvention.StdCall)]
	public static extern void lua_getfenv(IntPtr L, int idx);
	
	[DllImport(DllName, CallingConvention = CallingConvention.StdCall)]
	public static extern void lua_settable(IntPtr L, int idx);
	
	[DllImport(DllName, CallingConvention = CallingConvention.StdCall)]
	public static extern void lua_setfield(IntPtr L, int idx, string k);
	
	[DllImport(DllName, CallingConvention = CallingConvention.StdCall)]
	public static extern void lua_rawset(IntPtr L, int idx);
	
	[DllImport(DllName, CallingConvention = CallingConvention.StdCall)]
	public static extern void lua_rawseti(IntPtr L, int idx, int n);
	
	[DllImport(DllName, CallingConvention = CallingConvention.StdCall)]
	public static extern int lua_setmetatable(IntPtr L, int objindex);
	
	[DllImport(DllName, CallingConvention = CallingConvention.StdCall)]
	public static extern int lua_setfenv(IntPtr L, int idx);
	
	[DllImport(DllName, CallingConvention = CallingConvention.StdCall)]
	public static extern void lua_call(IntPtr L, int nargs, int nresults);
	
	[DllImport(DllName, CallingConvention = CallingConvention.StdCall)]
	public static extern int lua_pcall(IntPtr L, int nargs, int nresults, int errfunc);
	
	[DllImport(DllName, CallingConvention = CallingConvention.StdCall)]
	public static extern int lua_cpcall(IntPtr L, lua_CFunction func, IntPtr ud);
	
	[DllImport(DllName, CallingConvention = CallingConvention.StdCall)]
	public static extern int lua_load(IntPtr L, lua_Reader reader, IntPtr dt, string chunkname);
	
	[DllImport(DllName, CallingConvention = CallingConvention.StdCall)]
	public static extern int lua_dump(IntPtr L, lua_Writer writer, IntPtr data);
	
	[DllImport(DllName, CallingConvention = CallingConvention.StdCall)]
	public static extern int lua_yield(IntPtr L, int nresults);
	
	[DllImport(DllName, CallingConvention = CallingConvention.StdCall)]
	public static extern int lua_resume(IntPtr L, int narg);
	
	[DllImport(DllName, CallingConvention = CallingConvention.StdCall)]
	public static extern int lua_status(IntPtr L);
	
	public const int LUA_GCSTOP = 0;
	public const int LUA_GCRESTART = 1;
	public const int LUA_GCCOLLECT = 2;
	public const int LUA_GCCOUNT = 3;
	public const int LUA_GCCOUNTB = 4;
	public const int LUA_GCSTEP = 5;
	public const int LUA_GCSETPAUSE = 6;
	public const int LUA_GCSETSTEPMUL = 7;
	
	[DllImport(DllName, CallingConvention = CallingConvention.StdCall)]
	public static extern int lua_gc(IntPtr L, int what, int data);
	
	[DllImport(DllName, CallingConvention = CallingConvention.StdCall)]
	public static extern int lua_error(IntPtr L);
	
	[DllImport(DllName, CallingConvention = CallingConvention.StdCall)]
	public static extern int lua_next(IntPtr L, int idx);
	
	[DllImport(DllName, CallingConvention = CallingConvention.StdCall)]
	public static extern void lua_concat(IntPtr L, int n);
	
	[DllImport(DllName, CallingConvention = CallingConvention.StdCall)]
	public static extern lua_Alloc lua_getallocf(IntPtr L, ref IntPtr ud);
	
	[DllImport(DllName, CallingConvention = CallingConvention.StdCall)]
	public static extern void lua_setallocf(IntPtr L, lua_Alloc f, IntPtr ud);
	
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
		lua_pushcfunction(L, f);
		lua_setglobal(L, n);
	}
	
	public static void lua_pushcfunction(IntPtr L, lua_CFunction f)
	{
		lua_pushcclosure(L, f, 0);
	}
	
	public static ulong lua_strlen(IntPtr L, int i)
	{
		return lua_objlen(L, i);
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
	
	public static void lua_pushliteral(IntPtr L, string s)
	{
		lua_pushlstring(L, s, (ulong) s.Length);
	}
	
	public static void lua_setglobal(IntPtr L, string s)
	{
		lua_setfield(L, LUA_GLOBALSINDEX, s);
	}
	
	public static void lua_getglobal(IntPtr L, string s)
	{
		lua_getfield(L, LUA_GLOBALSINDEX, s);
	}
	
	public static string? lua_tostring(IntPtr L, int i)
	{
		ulong temp = 0; // NOP
		return lua_tolstring(L, i, ref temp);
	}
	
	public static IntPtr lua_open()
	{
		return luaL_newstate();
	}
	
	public static void lua_getregistry(IntPtr L)
	{
		lua_pushvalue(L, LUA_REGISTRYINDEX);
	}
	
	public static int lua_getgccount(IntPtr L)
	{
		return lua_gc(L, LUA_GCCOUNT, 0);
	}
	
	[DllImport(DllName, CallingConvention = CallingConvention.StdCall)]
	public static extern void lua_setlevel(IntPtr from, IntPtr to);
	
	public const int LUA_HOOKCALL = 0;
	public const int LUA_HOOKRET = 1;
	public const int LUA_HOOKLINE = 2;
	public const int LUA_HOOKCOUNT = 3;
	public const int LUA_HOOKTAILRET = 4;
	
	public const int LUA_MASKCALL = (1 << LUA_HOOKCALL);
	public const int LUA_MASKRET = (1 << LUA_HOOKRET);
	public const int LUA_MASKLINE = (1 << LUA_HOOKLINE);
	public const int LUA_MASKCOUNT = (1 << LUA_HOOKCOUNT);
	
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
	public static extern int lua_sethook(IntPtr L, lua_Hook func, int mask, int count);
	
	[DllImport(DllName, CallingConvention = CallingConvention.StdCall)]
	public static extern lua_Hook lua_gethook(IntPtr L);
	
	[DllImport(DllName, CallingConvention = CallingConvention.StdCall)]
	public static extern int lua_gethookmask(IntPtr L);
	
	[DllImport(DllName, CallingConvention = CallingConvention.StdCall)]
	public static extern int lua_gethookcount(IntPtr L);
	
	[StructLayout(LayoutKind.Sequential, Pack = 1, CharSet = CharSet.Ansi)]
	public struct lua_Debug {
		public int _event;
		public string name;
		public string namewhat;
		public string what;
		public string source;
		public int currentline;
		public int nups;
		public int linedefined;
		public int lastlinedefined;
		[MarshalAs(UnmanagedType.ByValArray, SizeConst = 60)]
		public sbyte[] short_src;
		public IntPtr i_ci;
	};
	
	[StructLayout(LayoutKind.Sequential, Pack = 1, CharSet = CharSet.Ansi)]
	public struct luaL_Buffer {
		public IntPtr p;
		public int lvl;
		public IntPtr L;
		[MarshalAs(UnmanagedType.ByValArray, SizeConst = 512)]
		public sbyte[] init;
	};
	
	public delegate int lua_CFunction(IntPtr L);
	public delegate int lua_KFunction(IntPtr L, int status, IntPtr ctx);
	public delegate void lua_WarnFunction(IntPtr ud, string msg, int tocont);
	public delegate int lua_Writer(IntPtr L, IntPtr p, ulong sz, ref ulong ud);
	public delegate IntPtr lua_Reader(IntPtr L, IntPtr ud, ulong sz);
	public delegate IntPtr lua_Alloc(IntPtr ud, IntPtr ptr, ulong osize, ulong nsize);
	public delegate void lua_Hook(IntPtr L, lua_Debug ar);
	
	public static ulong luaL_getn(IntPtr L, int i)
	{
		return lua_objlen(L, i);
	}
	
	public const int LUA_ERRFILE = LUA_ERRERR + 1;
	
	[StructLayout(LayoutKind.Sequential, Pack = 1, CharSet = CharSet.Ansi)]
	public struct luaL_Reg {
		string name;
		lua_CFunction func;
	};
	
	[DllImport(DllName, CallingConvention = CallingConvention.StdCall)]
	public static extern void luaI_openlib(IntPtr L, string libname, luaL_Reg l, int nup);
	
	[DllImport(DllName, CallingConvention = CallingConvention.StdCall)]
	public static extern void luaL_register(IntPtr L, string libname, luaL_Reg l);
	
	[DllImport(DllName, CallingConvention = CallingConvention.StdCall)]
	public static extern int luaL_getmetafield(IntPtr L, int obj, string e);
	
	[DllImport(DllName, CallingConvention = CallingConvention.StdCall)]
	public static extern int luaL_callmeta(IntPtr L, int obj, string e);
	
	[DllImport(DllName, CallingConvention = CallingConvention.StdCall)]
	public static extern int luaL_typerror(IntPtr L, int narg, string tname);
	
	[DllImport(DllName, CallingConvention = CallingConvention.StdCall)]
	public static extern int luaL_argerror(IntPtr L, int numarg, string extramsg);
	
	[DllImport(DllName, CallingConvention = CallingConvention.StdCall, EntryPoint = "luaL_checklstring")]
	public static extern IntPtr _luaL_checklstring(IntPtr L, int numArg, ref ulong l);
	public static string? luaL_checklstring(IntPtr L, int numArg, ref ulong l)
	{
		return Marshal.PtrToStringAnsi(_luaL_checklstring(L, numArg, ref l));
	}
	
	[DllImport(DllName, CallingConvention = CallingConvention.StdCall, EntryPoint = "luaL_optlstring")]
	public static extern IntPtr _luaL_optlstring(IntPtr L, int numArg, string def, ref ulong l);
	public static string? luaL_optlstring(IntPtr L, int numArg, string def, ref ulong l)
	{
		return Marshal.PtrToStringAnsi(_luaL_optlstring(L, numArg, def, ref l));
	}
	
	[DllImport(DllName, CallingConvention = CallingConvention.StdCall)]
	public static extern double luaL_checknumber(IntPtr L, int numArg);
	
	[DllImport(DllName, CallingConvention = CallingConvention.StdCall)]
	public static extern double luaL_optnumber(IntPtr L, int nArg, double def);
	
	[DllImport(DllName, CallingConvention = CallingConvention.StdCall)]
	public static extern long luaL_checkinteger(IntPtr L, int numArg);
	
	[DllImport(DllName, CallingConvention = CallingConvention.StdCall)]
	public static extern long luaL_optinteger(IntPtr L, int nArg, long def);
	
	[DllImport(DllName, CallingConvention = CallingConvention.StdCall)]
	public static extern void luaL_checkstack(IntPtr L, int sz, string msg);
	
	[DllImport(DllName, CallingConvention = CallingConvention.StdCall)]
	public static extern void luaL_checktype(IntPtr L, int narg, int t);
	
	[DllImport(DllName, CallingConvention = CallingConvention.StdCall)]
	public static extern void luaL_checkany(IntPtr L, int narg);
	
	[DllImport(DllName, CallingConvention = CallingConvention.StdCall)]
	public static extern int luaL_newmetatable(IntPtr L, string tname);
	
	[DllImport(DllName, CallingConvention = CallingConvention.StdCall)]
	public static extern IntPtr luaL_checkudata(IntPtr L, int ud, string tname);
	
	[DllImport(DllName, CallingConvention = CallingConvention.StdCall)]
	public static extern void luaL_where(IntPtr L, int lvl);
	
	[DllImport(DllName, CallingConvention = CallingConvention.StdCall)]
	public static extern int luaL_error(IntPtr L, string fmt, params string[] args);
	
	// TODO: I dont think string[][] works
	[DllImport(DllName, CallingConvention = CallingConvention.StdCall)]
	public static extern int luaL_checkoption(IntPtr L, int narg, string def, string[][] lst);
	
	public const int LUA_NOREF = -2;
	public const int LUA_REFNIL = -1;
	
	[DllImport(DllName, CallingConvention = CallingConvention.StdCall)]
	public static extern int luaL_ref(IntPtr L, int t);
	
	[DllImport(DllName, CallingConvention = CallingConvention.StdCall)]
	public static extern void luaL_unref(IntPtr L, int t, int _ref);
	
	[DllImport(DllName, CallingConvention = CallingConvention.StdCall)]
	public static extern int luaL_loadfile(IntPtr L, string filename);
	
	[DllImport(DllName, CallingConvention = CallingConvention.StdCall)]
	public static extern int luaL_loadbuffer(IntPtr L, string buff, ulong sz, string name);
	
	[DllImport(DllName, CallingConvention = CallingConvention.StdCall)]
	public static extern int luaL_loadstring(IntPtr L, string s);
	
	[DllImport(DllName, CallingConvention = CallingConvention.StdCall)]
	public static extern IntPtr luaL_newstate();
	
	[DllImport(DllName, CallingConvention = CallingConvention.StdCall, EntryPoint = "luaL_gsub")]
	public static extern IntPtr _luaL_gsub(IntPtr L, string s, string p, string r);
	public static string? luaL_gsub(IntPtr L, string s, string p, string r)
	{
		return Marshal.PtrToStringAnsi(_luaL_gsub(L, s, p, r));
	}
	
	[DllImport(DllName, CallingConvention= CallingConvention.StdCall, EntryPoint = "luaL_findtable")]
	public static extern IntPtr _luaL_findtable(IntPtr L, int idx, string fname, int szhint);
	public static string? luaL_findtable(IntPtr L, int idx, string fname, int szhint)
	{
		return Marshal.PtrToStringAnsi(_luaL_findtable(L, idx, fname, szhint));
	}
	
	public static void luaL_argcheck(IntPtr L, bool cond, int numarg, string extramsg)
	{
		if (cond == false)
			luaL_argerror(L, numarg, extramsg);
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
	
	public static void luaL_getmetatable(IntPtr L, string n)
	{
		lua_getfield(L, LUA_REGISTRYINDEX, n);
	}
	
	public delegate T luaL_Function<T>(IntPtr L, int n);
	
	public static T luaL_opt<T>(IntPtr L, luaL_Function<T> f, int n, T d)
	{
		return lua_isnoneornil(L, n) > 0 ? d : f(L, n);
	}
	
	public static void luaL_addchar(luaL_Buffer B, sbyte c)
	{
		if (B.init.Length >= LUAL_BUFFERSIZE)
			luaL_prepbuffer(B);
		Marshal.WriteByte(B.p, (byte) c);
		B.p += 1;
	}
	
	public static void luaL_putchar(luaL_Buffer B, sbyte c)
	{
		luaL_addchar(B, c);
	}
	
	public static void luaL_addsize(luaL_Buffer B, int n)
	{
		B.p += n;
	}
	
	[DllImport(DllName, CallingConvention = CallingConvention.StdCall)]
	public static extern void luaL_buffinit(IntPtr L, luaL_Buffer B);
	
	[DllImport(DllName, CallingConvention = CallingConvention.StdCall, EntryPoint = "luaL_prepbuffer")]
	public static extern IntPtr _luaL_prepbuffer(luaL_Buffer B);
	public static string? luaL_prepbuffer(luaL_Buffer B)
	{
		return Marshal.PtrToStringAnsi(_luaL_prepbuffer(B));
	}
	
	[DllImport(DllName, CallingConvention = CallingConvention.StdCall)]
	public static extern void luaL_addlstring(IntPtr B, string s, ulong l);
	
	[DllImport(DllName, CallingConvention = CallingConvention.StdCall)]
	public static extern void luaL_addstring(IntPtr B, string s);
	
	[DllImport(DllName, CallingConvention = CallingConvention.StdCall)]
	public static extern void luaL_addvalue(IntPtr B);
	
	[DllImport(DllName, CallingConvention = CallingConvention.StdCall)]
	public static extern void luaL_pushresult(IntPtr B);
	
	public static int lua_ref(IntPtr L, bool _lock)
	{
		if (_lock)
			return luaL_ref(L, LUA_REGISTRYINDEX);
		lua_pushstring(L, "unlocked references are obsolete");
		lua_error(L);
		return 0;
	}
	
	public static void lua_unref(IntPtr L, int _ref)
	{
		luaL_unref(L, LUA_REGISTRYINDEX, _ref);
	}
	
	public static void lua_getref(IntPtr L, int _ref)
	{
		lua_rawgeti(L, LUA_REGISTRYINDEX, _ref);
	}
	
	[DllImport(DllName, CallingConvention = CallingConvention.StdCall)]
	public static extern int luaopen_base(IntPtr L);
	
	[DllImport(DllName, CallingConvention = CallingConvention.StdCall)]
	public static extern int luaopen_table(IntPtr L);
	
	[DllImport(DllName, CallingConvention = CallingConvention.StdCall)]
	public static extern int luaopen_io(IntPtr L);
	
	[DllImport(DllName, CallingConvention = CallingConvention.StdCall)]
	public static extern int luaopen_os(IntPtr L);
	
	[DllImport(DllName, CallingConvention = CallingConvention.StdCall)]
	public static extern int luaopen_string(IntPtr L);
	
	[DllImport(DllName, CallingConvention = CallingConvention.StdCall)]
	public static extern int luaopen_math(IntPtr L);
	
	[DllImport(DllName, CallingConvention = CallingConvention.StdCall)]
	public static extern int luaopen_debug(IntPtr L);
	
	[DllImport(DllName, CallingConvention = CallingConvention.StdCall)]
	public static extern int luaopen_package(IntPtr L);
	
	[DllImport(DllName, CallingConvention = CallingConvention.StdCall)]
	public static extern void luaL_openlibs(IntPtr L);
	
}

