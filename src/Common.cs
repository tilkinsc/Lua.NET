using System.Reflection;
using System.Runtime.InteropServices;

namespace LuaNET;

public static class Imports
{
	
	private static nint _libHandle_luajit = (nint) 0;
	private static nint _libHandle_lua51 = (nint) 0;
	private static nint _libHandle_lua52 = (nint) 0;
	private static nint _libHandle_lua53 = (nint) 0;
	private static nint _libHandle_lua54 = (nint) 0;
	
	private static nint Resolve(string libName, out nint handle)
	{
		handle = (nint) 0;
		if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
		{
			if (Environment.Is64BitProcess)
			{
				NativeLibrary.TryLoad($"./native/win-x64/{libName}.dll", out handle);
			}
			else
			{
				NativeLibrary.TryLoad($"./native/win-x86/{libName}.dll", out handle);
			}
		}
		else if (RuntimeInformation.IsOSPlatform(OSPlatform.Linux))
		{
			NativeLibrary.TryLoad($"./native/linux-x64/{libName}.so", out handle);
		}
		else if (RuntimeInformation.IsOSPlatform(OSPlatform.OSX))
		{
			NativeLibrary.TryLoad($"./native/macos-x64/{libName}.dylib", out handle);
		}
		else if (RuntimeInformation.IsOSPlatform(OSPlatform.FreeBSD))
		{
			NativeLibrary.TryLoad($"./native/freebsd-x64/{libName}.so", out handle);
		}
		return handle;
	}
	
	internal static nint ImportResolver(string libName, Assembly assembly, DllImportSearchPath? searchPath)
	{
		switch (libName)
		{
			case "lua51":
				if (_libHandle_luajit != 0)
					return _libHandle_luajit;
				return Resolve(libName, out _libHandle_luajit);
			case "lua546":
				if (_libHandle_lua54 != 0)
					return _libHandle_lua54;
				return Resolve(libName, out _libHandle_lua54);
			case "lua515":
				if (_libHandle_lua51 != 0)
					return _libHandle_lua51;
				return Resolve(libName, out _libHandle_lua51);
			case "lua536":
				if (_libHandle_lua53 != 0)
					return _libHandle_lua53;
				return Resolve(libName, out _libHandle_lua53);
			case "lua524":
				if (_libHandle_lua52 != 0)
					return _libHandle_lua52;
				return Resolve(libName, out _libHandle_lua52);
		}
		
		return (nint) 0;
	}
	
}
