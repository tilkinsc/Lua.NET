using System.Reflection;
using System.Runtime.InteropServices;

namespace LuaNET;

public static class Imports
{
	
	internal static nint ImportResolver(string libName, Assembly assembly, DllImportSearchPath? searchPath)
	{
		nint libHandle = (nint) 0;
		if (libName != "lua51" && libName != "lua515" && libName != "lua524" && libName != "lua536" && libName != "lua546")
			return libHandle;
		
		if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
		{
			if (Environment.Is64BitProcess)
			{
				NativeLibrary.TryLoad($"./native/win-x64/{libName}.dll", out libHandle);
			}
			else
			{
				NativeLibrary.TryLoad($"./native/win-x86/{libName}.dll", out libHandle);
			}
		}
		else if (RuntimeInformation.IsOSPlatform(OSPlatform.Linux))
		{
			NativeLibrary.TryLoad($"./native/linux-x64/{libName}.so", out libHandle);
		}
		else if (RuntimeInformation.IsOSPlatform(OSPlatform.OSX))
		{
			NativeLibrary.TryLoad($"./native/macos-x64/{libName}.dylib", out libHandle);
		}
		else if (RuntimeInformation.IsOSPlatform(OSPlatform.FreeBSD))
		{
			NativeLibrary.TryLoad($"./native/freebsd-x64/{libName}.so", out libHandle);
		}
		return libHandle;
	}
	
}
