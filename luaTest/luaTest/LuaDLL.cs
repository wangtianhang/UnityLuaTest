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

class LuaDLL
{
    const string LUADLL = "lua515.dll";
    const int LUA_GLOBALSINDEX = -10002;

    [DllImport(LUADLL, CallingConvention = CallingConvention.Cdecl)]
    public static extern IntPtr luaL_newstate();

    [DllImport(LUADLL, CallingConvention = CallingConvention.Cdecl)]
    public static extern IntPtr luaL_openlibs(IntPtr l);

    [DllImport(LUADLL, CallingConvention = CallingConvention.Cdecl)]
    public static extern void lua_close(IntPtr l);

    [DllImport(LUADLL, CallingConvention = CallingConvention.Cdecl)]
    public static extern int luaL_loadstring(IntPtr l, string str);

    //[DllImport(m_dll, EntryPoint = "luaL_register", CallingConvention = CallingConvention.Cdecl)]
    //public static extern void luaL_register(IntPtr L, string name, IntPtr fn);

    [DllImport(LUADLL, CallingConvention = CallingConvention.Cdecl)]
    public static extern int lua_gettop(IntPtr luaState);

    [DllImport(LUADLL, CallingConvention = CallingConvention.Cdecl)]
    public static extern LuaTypes lua_type(IntPtr luaState, int index);

    [DllImport(LUADLL, CallingConvention = CallingConvention.Cdecl)]
    public static extern IntPtr lua_tolstring(IntPtr l, int index, out int len);

    [DllImport(LUADLL, CallingConvention = CallingConvention.Cdecl)]
    public static extern IntPtr lua_pushstring(IntPtr l, string name);

    [DllImport(LUADLL, CallingConvention = CallingConvention.Cdecl)]
    public static extern IntPtr lua_pushcclosure(IntPtr l, IntPtr fn, int n);

    [DllImport(LUADLL, CallingConvention = CallingConvention.Cdecl)]
    public static extern IntPtr lua_setfield(IntPtr l, int idx, string name);

    [DllImport(LUADLL, CallingConvention = CallingConvention.Cdecl)]
    public static extern IntPtr lua_pcall(IntPtr l, int nargs, int nresults, int errfunc);

    [DllImport(LUADLL, CallingConvention = CallingConvention.Cdecl)]
    public static extern bool lua_toboolean(IntPtr luaState, int index);

    [DllImport(LUADLL, CallingConvention = CallingConvention.Cdecl)]
    public static extern double lua_tonumber(IntPtr luaState, int idx);

    public static void luaL_dostring(IntPtr l, string str)
    {
        luaL_loadstring(l, str);
        lua_pcall(l, 0, -1, 0);
    }

    public static void lua_register(IntPtr l, string name, LuaCSFunction func)
    {
        lua_pushcfunction(l, func);
        lua_setglobal(l, name);
    }

    public static void lua_pushcfunction(IntPtr l, LuaCSFunction func)
    {
        IntPtr fn = Marshal.GetFunctionPointerForDelegate(func);
        lua_pushcclosure(l, fn, 0);
    }

    public static void lua_setglobal(IntPtr l, string name)
    {
        lua_setfield(l, LUA_GLOBALSINDEX, name);
    }

     public static string lua_tostring(IntPtr l, int index)
     {
         int len = 0;
         IntPtr str = lua_tolstring(l, index, out len);
 
         if (str != IntPtr.Zero)
         {
             return LuaHelper.lua_ptrtostring(str, len);
         }
 
         return null;
     }


}

