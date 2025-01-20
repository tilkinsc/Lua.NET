# Lua.NET
[![.NET](https://github.com/tilkinsc/Lua.NET/actions/workflows/dotnet.yml/badge.svg)](https://github.com/tilkinsc/Lua.NET/actions/workflows/dotnet.yml)  
![Logo](https://raw.githubusercontent.com/tilkinsc/Lua.NET/main/Lua.NET.Logo.png)  

C# .NET Core 8.0
Lua.NET contains full bindings to Lua5.0.3, Lua5.1.5, Lua5.2.4, Lua5.3.6, Lua.5.4.7 and LuaJIT

https://github.com/tilkinsc/Lua.NET  
Copyright © Cody Tilkins 2024 MIT License  

Supports Lua5.4 Lua5.3 Lua5.2 Lua5.1 Lua5.0 and LuaJIT  

Hardcoded to only use doubles and 64-bit integers.  

# Design Considerations / Usage

The delegates you pass to functions such as `lua_pushcfunction(...)` should be static.
Otherwise, the lifetime of your functions should exceed the lifetime of the lua_State.
Do not use non-static lambdas, as C# is liable to GC them.
You may get away with non-static lambdas if you track their references.
When you track lambda references they should be cleaned up with the destruction of the lua_State.
A system to support lambdas may be added in the future.

Acceptable lua_CFunction Pointers:
```C#
[UnmanagedCallersOnly]
public static int lfunc(lua_State L)
{
	...
	return 0;
}
...
lua_pushcfunction(L, lfunc);
```
```C#
lua_pushcfunction(L, static (L) => {
	...
	return 0;
});
```

# Shared Libraries

No shared library has to be built for this library, as its included for you.
Custom DLLs are supported when building from source; they must retain ABI compatibility with Lua.
If using custom shared libraries, you will need to replace the respective shared library Nuget gives you locally in your project in `bin/{configuration}/{target}/runtimes/{platform-rid}/native`. The name must be the same as the one you are replacing. The ideal way to handle this is by rolling your own nuget package clone of this repository. The reason for this is a shortcoming of runtime dlls not being copied over in project references (as opposed to package reference from nuget). All of this can be avoided if you change the DllName inside the respective source, however, that solution adds complexity to your project. Nuget packages are easy.

# Examples

Example Usage Lua5.4.6:
```C#
// test1.csproj
// <PropertyGroup>
//     ...
//     <AllowUnsafeBlocks>true</AllowUnsafeBlocks>
// </PropertyGroup>
using LuaNET.Lua54;
using static LuaNET.Lua54.Lua;

namespace TestLua;

public class Test1
{
	
	[UnmanagedCallersOnly]
	public static int lfunc(lua_State L)
	{
		lua_getglobal(L, "print");
		lua_pushstring(L, "lol");
		lua_pcallk(L, 1, 0, 0, IntPtr.Zero, null);
		return 0;
	}
	
	public static void Main(string[] args)
	{
		lua_State L = luaL_newstate();
		if (L == 0)
		{
			Console.WriteLine("Unable to create context!");
		}
		luaL_openlibs(L);
		lua_pushcfunction(L, lfunc);
		Console.WriteLine(lua_pcallk(L, 0, 0, 0, null, null));
		lua_close(L);
		
		Console.WriteLine("Success");
	}
	
}
```

Example Usage LuaJIT:
```C#
// test2.csproj
// <PropertyGroup>
//     ...
//     <AllowUnsafeBlocks>true</AllowUnsafeBlocks>
// </PropertyGroup>
using System.Runtime.InteropServices;
using LuaNET.LuaJIT;
using static LuaNET.LuaJIT.Lua;

namespace TestLua;

public class Test2
{
	
	[UnmanagedCallersOnly]
	public static int lfunc(lua_State L)
	{
		lua_getglobal(L, "print");
		lua_pushstring(L, "lol");
		lua_pcall(L, 1, 0, 0);
		return 0;
	}
	
	public static void Main(string[] args)
	{
		lua_State L = luaL_newstate();
		if (L == 0)
		{
			Console.WriteLine("Unable to create context!");
		}
		luaL_openlibs(L);
		lua_pushcfunction(L, lfunc);
		Console.WriteLine(lua_pcall(L, 0, 0, 0));
		lua_close(L);
		
		Console.WriteLine("Success");
	}
	
}
```

Example Usage NativeAOT DLL Library:
```C#
// test3.csproj
// <PropertyGroup>
//     ...
//     <AllowUnsafeBlocks>true</AllowUnsafeBlocks>
//     <PublishAot>true</PublishAot>
// </PropertyGroup>
//
// dotnet publish -r win-x64 -c Release
// This will emit test3.dll inside of bin/.../native and bin/.../publish
// I use the publish one

using System.Runtime.InteropServices;
using LuaNET.LuaJIT;
using static LuaNET.LuaJIT.Lua;

namespace TestLua;

public unsafe class Test3
{
	
	[UnmanagedCallersOnly]
	public static int l_GetHello(lua_State L)
	{
		lua_pushstring(L, "Hello World!");
		return 1;
	}
	
	public static luaL_Reg[] test2Lib = new luaL_Reg[]
	{
		AsLuaLReg("GetHello", &l_GetHello),
		AsLuaLReg(null, null)
	};
	
	[UnmanagedCallersOnly(EntryPoint = "luaopen_test2")]
	public static int luaopen_test2(lua_State L)
	{
		luaL_newlib(L, test2Lib);
		lua_setglobal(L, "test2");
		return 1;
	}
	
}
```
```C#
// test4.csproj
// <PropertyGroup>
//     ...
//     <AllowUnsafeBlocks>true</AllowUnsafeBlocks>
// </PropertyGroup>
using LuaNET.LuaJIT;
using static LuaNET.LuaJIT.Lua;

namespace test4;

public class Test4
{
	
	public static void Main(string[] args)
	{
		lua_State L = luaL_newstate();
		if (L == 0)
		{
			Console.WriteLine("Unable to create context!");
		}
		luaL_openlibs(L);
		lua_getglobal(L, "require");
		lua_pushstring(L, "test2");
		int result = lua_pcall(L, 1, 0, 0);
		if (result != LUA_OK)
		{
			string? err = luaL_checkstring(L, 1);
			if (err != null)
			{
				Console.WriteLine($"1 Result: {err}");
			}
		}
		lua_getglobal(L, "test2");
		lua_getglobal(L, "print");
		lua_getfield(L, 1, "GetHello");
		result = lua_pcall(L, 0, 1, 0);
		if (result != LUA_OK)
		{
			string? err = luaL_checkstring(L, 1);
			if (err != null)
			{
				Console.WriteLine($"2 Result: {err}");
			}
		}
		result = lua_pcall(L, 1, 0, 0);
		if (result != LUA_OK)
		{
			string? err = luaL_checkstring(L, 1);
			if (err != null)
			{
				Console.WriteLine($"3 Result: {err}");
			}
		}
		lua_pop(L, 1);
		
		lua_close(L);
		
		Console.WriteLine("Success!");
	}
	
}
```
