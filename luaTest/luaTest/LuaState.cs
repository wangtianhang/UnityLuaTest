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
        luaState.DoString("print('你好')");
        luaState.DoString("print(2 + 3)");
        luaState.DoString("print(2 == 3)");
    }

    public static LuaState CreateLuaState()
    {
        IntPtr l = LuaDLL.luaL_newstate();

        LuaDLL.luaL_openlibs(l);

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

        LuaDLL.lua_register(m_luaState, "print", new LuaCSFunction(WriteLine));


    }

    static int WriteLine(IntPtr L)
    {
        try
        {
            object arg0 = LuaHelper.ToVarObject(L, 1);
            Console.WriteLine(arg0.ToString());
            return 0;
        }
        catch (System.Exception ex)
        {
            Console.WriteLine(ex.Message);
            return -1;
        }
    }

    public void DoString(string str)
    {
        LuaDLL.luaL_dostring(m_luaState, str);
    }
}

