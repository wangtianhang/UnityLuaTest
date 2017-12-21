using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace luaTest
{
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

    class Program
    {
        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        public delegate int LuaCSFunction(IntPtr luaState);

        [DllImport("lua515.dll", EntryPoint = "luaL_newstate", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.Cdecl)]
        public static extern IntPtr luaL_newstate();

        [DllImport("lua515.dll", EntryPoint = "luaL_openlibs", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.Cdecl)]
        public static extern IntPtr luaL_openlibs(IntPtr l);

        [DllImport("lua515.dll", EntryPoint = "lua_close", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.Cdecl)]
        public static extern void lua_close(IntPtr l);

        [DllImport("lua515.dll", EntryPoint = "luaL_loadstring", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.Cdecl)]
        public static extern int luaL_loadstring(IntPtr l, string str);

        [DllImport("lua515.dll", EntryPoint = "luaL_register", CallingConvention = CallingConvention.Cdecl)]
        public static extern void luaL_register(IntPtr L, string name, IntPtr fn);

        [DllImport("lua515.dll", EntryPoint = "lua_gettop", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.Cdecl)]
        public static extern int lua_gettop(IntPtr luaState);

        [DllImport("lua515.dll", EntryPoint = "lua_type", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.Cdecl)]
        public static extern LuaTypes lua_type(IntPtr luaState, int index);

        [DllImport("lua515.dll", EntryPoint = "lua_tolstring", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.Cdecl)]
        public static extern IntPtr lua_tolstring(IntPtr l, int index, out int len);

        [DllImport("lua515.dll", EntryPoint = "lua_pushstring", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.Cdecl)]
        public static extern IntPtr lua_pushstring(IntPtr l, string name);

        [DllImport("lua515.dll", EntryPoint = "lua_pushcclosure", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.Cdecl)]
        public static extern IntPtr lua_pushcclosure(IntPtr l, IntPtr fn, int n);

        [DllImport("lua515.dll", EntryPoint = "lua_setfield", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.Cdecl)]
        public static extern IntPtr lua_setfield(IntPtr l, int idx, string name);

        [DllImport("lua515.dll", EntryPoint = "lua_pcall", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.Cdecl)]
        public static extern IntPtr lua_pcall(IntPtr l, int nargs, int nresults, int errfunc);

        [DllImport("testCharpToC.dll", EntryPoint = "Add", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.Cdecl)]
        public static extern int Add(int a, int b);

        [DllImport("testCharpToC.dll", EntryPoint = "Length", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.Cdecl)]
        public static extern int Length(string str);

        static void Main(string[] args)
        {
            int x = Add(1, 2);
            Console.WriteLine(x.ToString());

            int length = Length("test");
            Console.WriteLine(length.ToString());

            IntPtr l = luaL_newstate();
            //luaL_openlibs(l);

            LuaCSFunction funcWrap = WriteLine;
            IntPtr fn = Marshal.GetFunctionPointerForDelegate(funcWrap);
            lua_pushcclosure(l, fn, 0);
            lua_setfield(l, -10002, "print");
            //luaL_register(l, "print2", fn);
            //lua_pushstring(l, "print");
            //lua_pushcfunction(l, fn);
            //lua_setglobal(l, "print");
            //lua_pushcfunction(l, fn);
            //luaL_register(l, "print2", fn);

            //luaL_loadstring(l, "print('hello world')");
            luaL_loadstring(l, "print('hello world')");
            lua_pcall(l, 0, -1, 0);

            //lua_close(l);
            Console.ReadLine();
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
            catch(System.Exception ex)
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
}
