# Lua.NET
![Logo](https://raw.githubusercontent.com/tilkinsc/Lua.NET/main/Lua.NET.Logo.png)

C# .NET Core 6.0 Lua bindings and helper functions.

https://github.com/tilkinsc/Lua.NET  
Copyright © Cody Tilkins 2022 MIT License  

Supports Lua5.4 Lua5.3 Lua5.2 Lua5.1 and LuaJIT  

Hardcoded to only use doubles and 64-bit integers, so the Lua library will have to be built accordingly.
This CAN be changed with manual edits, but it wasn't fun writing this library.
This code was made with with the default includes on a 64-bit windows 10 machine using Lua's makefile and LuaJIT.
All DLL's are named differently, make sure the name of the Lua dll matches that of the .cs file; will make it easier in the future and provide the DLLs for you.
```
Lua5.4 - lua544.dll
Lua5.3 - lua536.dll
Lua5.2 - lua524.dll
Lua5.1 - lua515.dll
LuaJIT - lua51.dll
```

Custom DLLs are supported as long as they don't change any call arguments or return values.

To build Lua, get the Lua source from [Lua.org](https://www.lua.org/download.html) or [LuaJIT](https://luajit.org/download.html).
```bat
make -j24
```
Then rename the dll to the above convention.

# Design Considerations / Usage

Your delegates you pass to functions such as `lua_pushcfunction(...)` should be static.
If you do not use static, then the lifetime of your functions should exceed the lifetime of the Lua the final Lua context you create during the course of the program.
Do not use lambdas.
C# is liable to GC your delegates otherwise.

There are functions prefixed with an underscore.
These functions denote raw DllImported functions.
The reason these exist is because some functions needed a shim function for it to work properly/sanely, i.e. marshaling.
You can write your own functions against those.
For example, if you want a function like lua_pcall but not have to specify an error handler offset you can invoke _lua_pcall(...) in a util class (all functions are static).
This library does not use unsafe, however, going unsafe should work perfectly.
If you are just here to use the library, you can get by without having to worry about the underscore prefixed functions.

# Examples

Example Usage Lua5.4.4:
```C#
using Lua54;
using static Lua54.Lua;

namespace LuaNET;

class Project
{
	
	public static int lfunc(lua_State L)
	{
		lua_getglobal(L, "print");
		lua_pushstring(L, "lol");
		lua_pcallk(L, 1, 0, 0, IntPtr.Zero, null);
		return 0;
	}
	
	public static void Main(String[] args)
	{
		
		lua_State L = luaL_newstate();
		if (L.Handle == UIntPtr.Zero)
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
namespace LuaNET;

using static LuaJIT.Lua;

class Project
{
	
	public static int lfunc(lua_State L)
	{
		lua_getglobal(L, "print");
		lua_pushstring(L, "lol");
		lua_pcall(L, 1, 0, 0);
		return 0;
	}
	
	public static void Main(String[] args)
	{
		
		lua_State L = luaL_newstate();
		if (L.Handle == UIntPtr.Zero)
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
