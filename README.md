# Lua.NET
![Logo](https://raw.githubusercontent.com/tilkinsc/Lua.NET/main/Lua.NET.Logo.png)

C# .NET Core 6.0 Lua bindings and helper functions.

https://github.com/tilkinsc/Lua.NET  
Copyright © Cody Tilkins 2022 MIT License  

```
dotnet add package Lua.NET --version 2.0.0
```

Supports Lua5.4 Lua5.3 Lua5.2 Lua5.1 and LuaJIT  

Hardcoded to only use doubles and 64-bit integers, so the Lua library will have to be built accordingly. This CAN be changed with manual edits, but it wasn't fun writing this library. This code was made with with the default includes on a 64-bit windows 10 machine using Lua's makefile and LuaJIT. All DLL's are named differently, make sure the name of the Lua dll matches that of the .cs file; will make it easier in the future.
```
Lua5.4 - lua544.dll
Lua5.3 - lua536.dll
Lua5.2 - lua524.dll
Lua5.1 - lua515.dll
LuaJIT - lua51.dll
```

Example Usage Lua5.4.4:
```C#
namespace LuaNET;

using static Lua54.Lua;

class Project
{
	
	public static int lfunc(IntPtr L)
	{
		lua_getglobal(L, "print");
		lua_pushstring(L, "lol");
		lua_pcallk(L, 1, 0, 0, IntPtr.Zero, null);
		return 0;
	}
	
	public static void Main(String[] args)
	{
		
		IntPtr L = luaL_newstate();
		if (L == IntPtr.Zero)
		{
			Console.WriteLine("Unable to create context!");
		}
		luaL_openlibs(L);
		lua_pushcfunction(L, lfunc);
		Console.WriteLine(lua_pcallk(L, 0, 0, 0, IntPtr.Zero, null));
		lua_close(L);
		
		Console.WriteLine("Success");
		
	}
	
}
```

Example Usage LuaJIT:
```C#
namespace LuaNET;

using static LuaJIT.Lua;

class Project
{
	
	public static int lfunc(IntPtr L)
	{
		lua_getglobal(L, "print");
		lua_pushstring(L, "lol");
		lua_pcall(L, 1, 0, 0);
		return 0;
	}
	
	public static void Main(String[] args)
	{
		
		IntPtr L = luaL_newstate();
		if (L == IntPtr.Zero)
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
