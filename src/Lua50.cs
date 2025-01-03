using System.Runtime.InteropServices;

using size_t = System.UInt64;
using lua_Number = System.Double;

namespace LuaNET.Lua50;

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

public static class Lua
{
	
	private const string DllName = "lua515";
	private const CallingConvention Convention = CallingConvention.Cdecl;
	
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
		[MarshalAs(UnmanagedType.ByValArray, SizeConst = LUA_IDSIZE)]
		public sbyte[] short_src;
		public int i_ci;
	};
	
	[StructLayout(LayoutKind.Sequential, Pack = 1, CharSet = CharSet.Ansi)]
	public struct luaL_Reg {
		public string name;
		public nint func;
	};
	
	[StructLayout(LayoutKind.Sequential, Pack = 1, CharSet = CharSet.Ansi)]
	public struct luaL_Buffer {
		public nint p;
		public int lvl;
		public lua_State L;
		// This is a sbyte[]
		public nint buffer;
	};
	
	public delegate int lua_CFunction(lua_State L);
	public delegate nint lua_Chunkreader(lua_State L, nuint ud, ref size_t sz);
	public delegate int lua_Chunkwriter(lua_State L, nuint p, size_t sz, nuint ud);
	public delegate void lua_Hook(lua_State L, lua_Debug ar);
	
	public static unsafe luaL_Reg AsLuaLReg(string name, delegate*unmanaged<lua_State, int> func) => new() { name  = name, func = (nint) func };
	
	public const string LUA_VERSION = "Lua 5.0.3";
	public const string LUA_COPYRIGHT = "Copyright (C) 1994-2006 Tecgraf, PUC-Rio";
	public const string LUA_AUTHORS = "R. Ierusalimschy, L. H. de Figueiredo & W. Celes";
	
	public const int LUA_MULTRET = -1;
	
	public const int LUA_REGISTRYINDEX = -10000;
	public const int LUA_GLOBALSINDEX = -10001;
	
	public static int lua_upvalueindex(int i)
	{
		return LUA_GLOBALSINDEX - i;
	}
	
	public const int LUA_ERRRUN = 1;
	public const int LUA_ERRFILE = 2;
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
	
	[DllImport(DllName, CallingConvention = Convention)]
	public static extern lua_State lua_open();
	
	[DllImport(DllName, CallingConvention = Convention)]
	public static extern void lua_close(lua_State L);
	
	[DllImport(DllName, CallingConvention = Convention)]
	public static extern lua_State lua_newthread(lua_State L);
	
	[DllImport(DllName, CallingConvention = Convention, EntryPoint = "lua_atpanic")]
	private static extern nint _lua_atpanic(lua_State L, nint panicf);
	public static lua_CFunction? lua_atpanic(lua_State L, lua_CFunction? panicf)
	{
		nint panic = _lua_atpanic(L, panicf == null ? 0 : Marshal.GetFunctionPointerForDelegate(panicf));
		return panic == 0 ? null : Marshal.GetDelegateForFunctionPointer<lua_CFunction>(panic);
	}
	
	[DllImport(DllName, CallingConvention = Convention)]
	public static extern int lua_gettop(lua_State L);
	
	[DllImport(DllName, CallingConvention = Convention)]
	public static extern void lua_settop(lua_State L, int idx);
	
	[DllImport(DllName, CallingConvention = Convention)]
	public static extern void lua_pushvalue(lua_State L, int idx);
	
	[DllImport(DllName, CallingConvention = Convention)]
	public static extern void lua_remove(lua_State L, int idx);
	
	[DllImport(DllName, CallingConvention = Convention)]
	public static extern void lua_insert(lua_State L, int idx);
	
	[DllImport(DllName, CallingConvention = Convention)]
	public static extern void lua_replace(lua_State L, int idx);
	
	[DllImport(DllName, CallingConvention = Convention)]
	public static extern int lua_checkstack(lua_State L, int sz);
	
	[DllImport(DllName, CallingConvention = Convention)]
	public static extern void lua_xmove(lua_State from, lua_State to, int n);
	
	[DllImport(DllName, CallingConvention = Convention)]
	public static extern int lua_isnumber(lua_State L, int idx);
	
	[DllImport(DllName, CallingConvention = Convention)]
	public static extern int lua_isstring(lua_State L, int idx);
	
	[DllImport(DllName, CallingConvention = Convention)]
	public static extern int lua_iscfunction(lua_State L, int idx);
	
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
	public static extern int lua_equal(lua_State L, int idx1, int idx2);
	
	[DllImport(DllName, CallingConvention = Convention)]
	public static extern int lua_rawequal(lua_State L, int idx1, int idx2);
	
	[DllImport(DllName, CallingConvention = Convention)]
	public static extern int lua_lessthan(lua_State L, int idx1, int idx2);
	
	[DllImport(DllName, CallingConvention = Convention)]
	public static extern lua_Number lua_tonumber(lua_State L, int idx);
	
	[DllImport(DllName, CallingConvention = Convention)]
	public static extern int lua_toboolean(lua_State L, int idx);
	
	[DllImport(DllName, CallingConvention = Convention, EntryPoint = "lua_tostring")]
	private static extern nint _lua_tostring(lua_State L, int idx);
	public static string? lua_tostring(lua_State L, int idx)
	{
		return Marshal.PtrToStringAnsi(_lua_tostring(L, idx));
	}
	
	[DllImport(DllName, CallingConvention = Convention)]
	public static extern size_t lua_strlen(lua_State L, int idx);
	
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
	
	[DllImport(DllName, CallingConvention = Convention)]
	public static extern void lua_pushnil(lua_State L);
	
	[DllImport(DllName, CallingConvention = Convention)]
	public static extern void lua_pushnumber(lua_State L, lua_Number n);
	
	[DllImport(DllName, CallingConvention = Convention)]
	public static extern void lua_pushlstring(lua_State L, string s, size_t l);
	
	[DllImport(DllName, CallingConvention = Convention)]
	public static extern void lua_pushstring(lua_State L, string s);
	
	// TODO:
	// [DllImport(DllName, CallingConvention = Convention)]
	// public static extern nint lua_pushvfstring(lua_State L, string fmt, va_list argp);
	
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
	public static extern void lua_gettable(lua_State L, int idx);
	
	[DllImport(DllName, CallingConvention = Convention)]
	public static extern void lua_rawget(lua_State L, int idx);
	
	[DllImport(DllName, CallingConvention = Convention)]
	public static extern void lua_rawgeti(lua_State L, int idx, int n);
	
	[DllImport(DllName, CallingConvention = Convention)]
	public static extern void lua_newtable(lua_State L);
	
	[DllImport(DllName, CallingConvention = Convention)]
	public static extern nuint lua_newuserdata(lua_State L, size_t sz);
	
	[DllImport(DllName, CallingConvention = Convention)]
	public static extern int lua_getmetatable(lua_State L, int objindex);
	
	[DllImport(DllName, CallingConvention = Convention)]
	public static extern void lua_getfenv(lua_State L, int idx);
	
	[DllImport(DllName, CallingConvention = Convention)]
	public static extern void lua_settable(lua_State L, int idx);
	
	[DllImport(DllName, CallingConvention = Convention)]
	public static extern void lua_rawset(lua_State L, int idx);
	
	[DllImport(DllName, CallingConvention = Convention)]
	public static extern void lua_rawseti(lua_State L, int idx, int n);
	
	[DllImport(DllName, CallingConvention = Convention)]
	public static extern int lua_setmetatable(lua_State L, int objindex);
	
	[DllImport(DllName, CallingConvention = Convention)]
	public static extern int lua_setfenv(lua_State L, int idx);
	
	[DllImport(DllName, CallingConvention = Convention)]
	public static extern void lua_call(lua_State L, int nargs, int nresults);
	
	[DllImport(DllName, CallingConvention = Convention)]
	public static extern int lua_pcall(lua_State L, int nargs, int nresults, int errfunc);
	
	[DllImport(DllName, CallingConvention = Convention, EntryPoint = "lua_cpcall")]
	private static extern int _lua_cpcall(lua_State L, nint func, nuint ud);
	public static int lua_cpcall(lua_State L, lua_CFunction? func, nuint ud)
	{
		return _lua_cpcall(L, func == null ? 0 : Marshal.GetFunctionPointerForDelegate(func), ud);
	}
	
	[DllImport(DllName, CallingConvention = Convention, EntryPoint = "lua_load")]
	private static extern int _lua_load(lua_State L, nint reader, nuint dt, string chunkname);
	public static int lua_load(lua_State L, lua_Chunkreader? reader, nuint dt, string chunkname)
	{
		return _lua_load(L, reader == null ? 0 : Marshal.GetFunctionPointerForDelegate(reader), dt, chunkname);
	}
	
	[DllImport(DllName, CallingConvention = Convention, EntryPoint = "lua_dump")]
	private static extern int _lua_dump(lua_State L, nint writer, nuint data);
	public static int lua_dump(lua_State L, lua_Chunkwriter? writer, nuint data)
	{
		return _lua_dump(L, writer == null ? 0 : Marshal.GetFunctionPointerForDelegate(writer), data);
	}
	
	[DllImport(DllName, CallingConvention = Convention)]
	public static extern int lua_yield(lua_State L, int nresults);
	
	[DllImport(DllName, CallingConvention = Convention)]
	public static extern int lua_resume(lua_State L, int narg);
	
	[DllImport(DllName, CallingConvention = Convention)]
	public static extern int lua_getgcthreshold(lua_State L);
	
	[DllImport(DllName, CallingConvention = Convention)]
	public static extern int lua_getgccount(lua_State L);
	
	[DllImport(DllName, CallingConvention = Convention)]
	public static extern void lua_setgcthreshold(lua_State L, int newthreshold);
	
	[DllImport(DllName, CallingConvention = Convention, EntryPoint = "lua_version")]
	private static extern nint _lua_version();
	public static string? lua_version()
	{
		return Marshal.PtrToStringAnsi(_lua_version());
	}
	
	[DllImport(DllName, CallingConvention = Convention)]
	public static extern int lua_error(lua_State L);
	
	[DllImport(DllName, CallingConvention = Convention)]
	public static extern int lua_next(lua_State L, int idx);
	
	[DllImport(DllName, CallingConvention = Convention)]
	public static extern void lua_concat(lua_State L, int n);
	
	public static nuint lua_boxpointer(lua_State L, nuint u)
	{
		// unsafe in intellisense wouldn't make sense
		unsafe
		{
			return (nuint) (*(void **) lua_newuserdata(L, (ulong) sizeof(void *)) = (void*) u);
		}
	}
	
	public static nuint lua_unboxpointer(lua_State L, int i)
	{
		// unsafe in intellisense wouldn't make sense
		unsafe
		{
			return (nuint) (*(nuint **) lua_touserdata(L, i));
		}
	}
	
	public static void lua_pop(lua_State L, int n)
	{
		lua_settop(L, -n-1);
	}
	
	public static void lua_register(lua_State L, string n, lua_CFunction? f)
	{
		lua_pushstring(L, n);
		lua_pushcfunction(L, f);
		lua_settable(L, LUA_GLOBALSINDEX);
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
	
	public static int lua_isnone(lua_State L, int n)
	{
		return (lua_type(L, n) == LUA_TNONE) ? 1 : 0;
	}
	
	public static int lua_isnoneornil(lua_State L, int n)
	{
		return (lua_type(L, n) <= 0) ? 1 : 0;
	}
	
	public static void lua_pushliteral(lua_State L, string s)
	{
		lua_pushlstring(L, s, (ulong) s.Length);
	}
	
	[DllImport(DllName, CallingConvention = Convention)]
	public static extern int lua_pushupvalues(lua_State L);
	
	public static void lua_getregistry(lua_State L)
	{
		lua_pushvalue(L, LUA_REGISTRYINDEX);
	}
	
	public static void lua_getglobal(lua_State L, string s)
	{
		lua_pushstring(L, s);
		lua_gettable(L, LUA_GLOBALSINDEX);
	}
	
	public const int LUA_NOREF = -2;
	public const int LUA_REFNIL = -1;
	
	public static int lua_ref(lua_State L, bool _lock)
	{
		if (_lock)
			return luaL_ref(L, LUA_REGISTRYINDEX);
		lua_pushstring(L, "unlocked references are obsolete");
		lua_error(L);
		return 0;
	}
	
	public static void lua_unref(lua_State L, int _ref)
	{
		luaL_unref(L, LUA_REGISTRYINDEX, _ref);
	}
	
	public static void lua_getref(lua_State L, int _ref)
	{
		lua_rawgeti(L, LUA_REGISTRYINDEX, _ref);
	}
	
	public const string LUA_NUMBER_SCAN = "%lf";
	public const string LUA_NUMBER_FMT = "%.14g";
	
	public const int LUA_HOOKCALL = 0;
	public const int LUA_HOOKRET = 1;
	public const int LUA_HOOKLINE = 2;
	public const int LUA_HOOKCOUNT = 3;
	public const int LUA_HOOKTAILRET = 4;
	
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
	
	[DllImport(DllName, CallingConvention = Convention, EntryPoint = "lua_sethook")]
	private static extern int _lua_sethook(lua_State L, nint func, int mask, int count);
	public static int lua_sethook(lua_State L, lua_Hook? func, int mask, int count)
	{
		return _lua_sethook(L, func == null ? 0 : Marshal.GetFunctionPointerForDelegate(func), mask, count);
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
	
	public const int LUA_IDSIZE = 60;
	
	[DllImport(DllName, CallingConvention = Convention)]
	public static extern void luaL_openlib(lua_State L, string libname, luaL_Reg l, int nup);
	
	[DllImport(DllName, CallingConvention = Convention)]
	public static extern int luaL_getmetafield(lua_State L, int obj, string e);
	
	[DllImport(DllName, CallingConvention = Convention)]
	public static extern int luaL_callmeta(lua_State L, int obj, string e);
	
	[DllImport(DllName, CallingConvention = Convention)]
	public static extern int luaL_typerror(lua_State L, int narg, string tname);
	
	[DllImport(DllName, CallingConvention = Convention)]
	public static extern int luaL_argerror(lua_State L, int numarg, string extramsg);
	
	[DllImport(DllName, CallingConvention = Convention, EntryPoint = "luaL_checklstring")]
	private static extern nint _luaL_checklstring(lua_State L, int numArg, ref size_t l);
	public static string? luaL_checklstring(lua_State L, int numArg, ref size_t l)
	{
		return Marshal.PtrToStringAnsi(_luaL_checklstring(L, numArg, ref l));
	}
	
	[DllImport(DllName, CallingConvention = Convention, EntryPoint = "luaL_optlstring")]
	private static extern nint _luaL_optlstring(lua_State L, int numArg, string def, ref size_t l);
	public static string? luaL_optlstring(lua_State L, int numArg, string def, ref size_t l)
	{
		return Marshal.PtrToStringAnsi(_luaL_optlstring(L, numArg, def, ref l));
	}
	
	[DllImport(DllName, CallingConvention = Convention)]
	public static extern lua_Number luaL_checknumber(lua_State L, int numArg);
	
	[DllImport(DllName, CallingConvention = Convention)]
	public static extern lua_Number luaL_optnumber(lua_State L, int nArg, lua_Number def);
	
	[DllImport(DllName, CallingConvention = Convention)]
	public static extern void luaL_checkstack(lua_State L, int sz, string msg);
	
	[DllImport(DllName, CallingConvention = Convention)]
	public static extern void luaL_checktype(lua_State L, int narg, int t);
	
	[DllImport(DllName, CallingConvention = Convention)]
	public static extern void luaL_checkany(lua_State L, int narg);
	
	[DllImport(DllName, CallingConvention = Convention)]
	public static extern int luaL_newmetatable(lua_State L, string tname);
	
	[DllImport(DllName, CallingConvention = Convention)]
	public static extern void luaL_getmetatable(lua_State L, string tname);
	
	[DllImport(DllName, CallingConvention = Convention)]
	public static extern nuint luaL_checkudata(lua_State L, int ud, string tname);
	
	[DllImport(DllName, CallingConvention = Convention)]
	public static extern void luaL_where(lua_State L, int lvl);
	
	// TODO: I dont think params works
	[DllImport(DllName, CallingConvention = Convention)]
	public static extern int luaL_error(lua_State L, string fmt, params string[] args);
	
	// TODO: I dont think this marshals right
	[DllImport(DllName, CallingConvention = Convention)]
	public static extern int luaL_findstring(string st, string[] lst);
	
	[DllImport(DllName, CallingConvention = Convention)]
	public static extern int luaL_ref(lua_State L, int t);
	
	[DllImport(DllName, CallingConvention = Convention)]
	public static extern void luaL_unref(lua_State L, int t, int _ref);
	
	[DllImport(DllName, CallingConvention = Convention)]
	public static extern int luaL_getn(lua_State L, int t);
	
	[DllImport(DllName, CallingConvention = Convention)]
	public static extern void luaL_setn(lua_State L, int t, int n);
	
	[DllImport(DllName, CallingConvention = Convention)]
	public static extern int luaL_loadfile(lua_State L, string filename);
	
	[DllImport(DllName, CallingConvention = Convention)]
	public static extern int luaL_loadbuffer(lua_State L, string buff, size_t sz, string name);
	
	public static void luaL_argcheck(lua_State L, bool cond, int numarg, string extramsg)
	{
		if (cond == false)
			luaL_argerror(L, numarg, extramsg);
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
	
	public static int luaL_checkint(lua_State L, int n)
	{
		return (int) luaL_checknumber(L, n);
	}
	
	public static long luaL_checklong(lua_State L, int n)
	{
		return (long) luaL_checknumber(L, n);
	}
	
	public static int luaL_optint(lua_State L, int n, lua_Number d)
	{
		return (int) luaL_optnumber(L, n, d);
	}
	
	public static long luaL_optlong(lua_State L, int n, lua_Number d)
	{
		return (long) luaL_optnumber(L, n, d);
	}
	
	public const int LUAL_BUFFERSIZE = 512;
	
	public static void luaL_putchar(luaL_Buffer B, char c)
	{
		if (B.p >= (B.buffer + LUAL_BUFFERSIZE))
		{
			luaL_prepbuffer(B);
		}
		unsafe
		{
			sbyte* p = (sbyte*) B.p;
			*p = (sbyte) c;
			B.p++;
		}
	}
	
	[DllImport(DllName, CallingConvention = Convention)]
	public static extern void luaL_buffinit(lua_State L, luaL_Buffer B);
	
	[DllImport(DllName, CallingConvention = Convention)]
	public static extern nint luaL_prepbuffer(luaL_Buffer B);
	
	[DllImport(DllName, CallingConvention = Convention)]
	public static extern void luaL_addlstring(luaL_Buffer B, string s, size_t l);
	
	[DllImport(DllName, CallingConvention = Convention)]
	public static extern void luaL_addstring(luaL_Buffer B, string s);
	
	[DllImport(DllName, CallingConvention = Convention)]
	public static extern void luaL_addvalue(luaL_Buffer B);
	
	[DllImport(DllName, CallingConvention = Convention)]
	public static extern void luaL_pushresult(luaL_Buffer B);
	
	[DllImport(DllName, CallingConvention = Convention)]
	public static extern int lua_dofile(lua_State L, string filename);
	
	[DllImport(DllName, CallingConvention = Convention)]
	public static extern int lua_dostring(lua_State L, string str);
	
	[DllImport(DllName, CallingConvention = Convention)]
	public static extern int lua_dobuffer(lua_State L, string buff, size_t sz, string n);
	
	public static string? luaL_check_lstr(lua_State L, int numArg, size_t l) => luaL_checklstring(L, numArg, ref l);
	public static string? luaL_opt_lstr(lua_State L, int numArg, string def, size_t l) => luaL_optlstring(L, numArg, def, ref l);
	public static string? luaL_check_number(lua_State L, int numArg) => luaL_check_number(L, numArg);
	public static string? luaL_opt_number(lua_State L, int numArg) => luaL_opt_number(L, numArg);
	public static void luaL_arg_check(lua_State L, bool cond, int numarg, string extramsg) => luaL_argcheck(L, cond, numarg, extramsg);
	public static string? luaL_check_string(lua_State L, int n) => luaL_checkstring(L, n);
	public static string? luaL_opt_string(lua_State L, int n, string d) => luaL_optstring(L, n, d);
	public static int luaL_check_int(lua_State L, int n) => luaL_checkint(L, n);
	public static long luaL_check_long(lua_State L, int n) => luaL_checklong(L, n);
	public static int luaL_opt_int(lua_State L, int n, lua_Number d) => luaL_optint(L, n, d);
	public static long luaL_opt_long(lua_State L, int n, lua_Number d) => luaL_optlong(L, n, d);
	
	public const string LUA_COLIBNAME = "coroutine";
	public const string LUA_TABLIBNAME = "table";
	public const string IOLIBNAME = "io";
	public const string OSLIBNAME = "os";
	public const string LUA_STRLIBNAME = "string";
	public const string LUA_MATHLIBNAME = "math";
	public const string DBLIBNAME = "debug";
	
	[DllImport(DllName, CallingConvention = Convention)]
	public static extern int luaopen_base(lua_State L);
	
	[DllImport(DllName, CallingConvention = Convention)]
	public static extern int luaopen_table(lua_State L);
	
	[DllImport(DllName, CallingConvention = Convention)]
	public static extern int luaopen_io(lua_State L);
	
	[DllImport(DllName, CallingConvention = Convention)]
	public static extern int luaopen_string(lua_State L);
	
	[DllImport(DllName, CallingConvention = Convention)]
	public static extern int luaopen_math(lua_State L);
	
	[DllImport(DllName, CallingConvention = Convention)]
	public static extern int luaopen_debug(lua_State L);
	
	[DllImport(DllName, CallingConvention = Convention)]
	public static extern int luaopen_loadlib(lua_State L);
	
	public static int lua_baselibopen(lua_State L) => luaopen_base(L);
	public static int lua_tablibopen(lua_State L) => luaopen_table(L);
	public static int lua_iolibopen(lua_State L) => luaopen_io(L);
	public static int lua_strlibopen(lua_State L) => luaopen_string(L);
	public static int lua_mathlibopen(lua_State L) => luaopen_math(L);
	public static int lua_dblibopen(lua_State L) => luaopen_debug(L);
	
}
