using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

public enum LuaTypes
{
    LUA_TNONE = -1,
    LUA_TNIL = 0,
    LUA_TBOOLEAN = 1,
    LUA_TLIGHTUSERDATA = 2,
    LUA_TNUMBER = 3,
    LUA_TSTRING = 4,
    LUA_TTABLE = 5,
    LUA_TFUNCTION = 6,
    LUA_TUSERDATA = 7,
    LUA_TTHREAD = 8,

}

[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
public delegate int LuaCSFunction(IntPtr luaState);

class LuaWrap
{
    const string m_dll = "lua515.dll";

    [DllImport(m_dll, EntryPoint = "luaL_newstate", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.Cdecl)]
    public static extern IntPtr luaL_newstate();

    [DllImport(m_dll, EntryPoint = "luaL_openlibs", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.Cdecl)]
    public static extern IntPtr luaL_openlibs(IntPtr l);

    [DllImport(m_dll, EntryPoint = "lua_close", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.Cdecl)]
    public static extern void lua_close(IntPtr l);

    [DllImport(m_dll, EntryPoint = "luaL_loadstring", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.Cdecl)]
    public static extern int luaL_loadstring(IntPtr l, string str);

    [DllImport(m_dll, EntryPoint = "luaL_register", CallingConvention = CallingConvention.Cdecl)]
    public static extern void luaL_register(IntPtr L, string name, IntPtr fn);

    [DllImport(m_dll, EntryPoint = "lua_gettop", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.Cdecl)]
    public static extern int lua_gettop(IntPtr luaState);

    [DllImport(m_dll, EntryPoint = "lua_type", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.Cdecl)]
    public static extern LuaTypes lua_type(IntPtr luaState, int index);

    [DllImport(m_dll, EntryPoint = "lua_tolstring", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.Cdecl)]
    public static extern IntPtr lua_tolstring(IntPtr l, int index, out int len);

    [DllImport(m_dll, EntryPoint = "lua_pushstring", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.Cdecl)]
    public static extern IntPtr lua_pushstring(IntPtr l, string name);

    [DllImport(m_dll, EntryPoint = "lua_pushcclosure", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.Cdecl)]
    public static extern IntPtr lua_pushcclosure(IntPtr l, IntPtr fn, int n);

    [DllImport(m_dll, EntryPoint = "lua_setfield", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.Cdecl)]
    public static extern IntPtr lua_setfield(IntPtr l, int idx, string name);

    [DllImport(m_dll, EntryPoint = "lua_pcall", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.Cdecl)]
    public static extern IntPtr lua_pcall(IntPtr l, int nargs, int nresults, int errfunc);

    public static LuaState CreateLuaState()
    {
        IntPtr l = luaL_newstate();

        LuaCSFunction funcWrap = WriteLine;
        IntPtr fn = Marshal.GetFunctionPointerForDelegate(funcWrap);
        lua_pushcclosure(l, fn, 0);
        lua_setfield(l, -10002, "print");

        return new LuaState(l);
    }

    public static void luaL_dostring(IntPtr l, string str)
    {
        LuaWrap.luaL_loadstring(l, str);
        LuaWrap.lua_pcall(l, 0, -1, 0);
    }

    static int WriteLine(IntPtr L)
    {
        try
        {
            int count = lua_gettop(L);
            if (count == 1)
            {
                LuaTypes luaType = lua_type(L, 1);
                string str = "";
                switch (luaType)
                {
                    case LuaTypes.LUA_TSTRING:
                        str = tolua_tostring(L, 1);
                        break;
                }
                Console.WriteLine(str);
                return 0;
            }
            else
            {
                Console.WriteLine("WriteLine param error");
                return 0;
            }
        }
        catch (System.Exception ex)
        {
            Console.WriteLine(ex.Message);
            return 0;
        }
    }

    public static string tolua_tostring(IntPtr l, int index)
    {
        int len = 0;
        IntPtr str = lua_tolstring(l, index, out len);

        if (str != IntPtr.Zero)
        {
            return tolua_ptrtostring(str, len);
        }

        return null;
    }

    public static string tolua_ptrtostring(IntPtr str, int len)
    {
        string ss = Marshal.PtrToStringAnsi(str, len);

        if (ss == null)
        {
            byte[] buffer = new byte[len];
            Marshal.Copy(str, buffer, 0, len);
            return Encoding.UTF8.GetString(buffer);
        }

        return ss;
    }
}

