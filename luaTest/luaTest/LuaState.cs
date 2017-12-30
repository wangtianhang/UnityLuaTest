using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;


class LuaState
{
    public static void Test()
    {
        LuaState luaState = CreateLuaState();
        luaState.RegisterCSharpFunc();
        luaState.DoString("print('hello world')");
    }

    public static LuaState CreateLuaState()
    {
        IntPtr l = LuaWrap.luaL_newstate();

        LuaWrap.luaL_openlibs(l);

        return new LuaState(l);
    }

    IntPtr m_luaState = IntPtr.Zero;

    public LuaState(IntPtr luaState)
    {
        m_luaState = luaState;
    }

    public void RegisterCSharpFunc()
    {
        //LuaCSFunction funcWrap = WriteLine;

        LuaWrap.lua_register(m_luaState, "print", new LuaCSFunction(WriteLine));


    }

    static int WriteLine(IntPtr L)
    {
        try
        {
            int count = LuaWrap.lua_gettop(L);
            if (count == 1)
            {
                LuaTypes luaType = LuaWrap.lua_type(L, 1);
                string str = "";
                switch (luaType)
                {
                    case LuaTypes.LUA_TSTRING:
                        str = LuaWrap.tolua_tostring(L, 1);
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

    public void DoString(string str)
    {
        LuaWrap.luaL_dostring(m_luaState, str);
    }
}

