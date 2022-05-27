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



