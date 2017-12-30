using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


class LuaInterpreter
{
    public static void Test()
    {
        LuaInterpreter interpreter = new LuaInterpreter();
        interpreter.Run();
    }

    LuaState m_state = null;
    public LuaInterpreter()
    {
        m_state = LuaState.CreateLuaState();
        m_state.RegisterCSharpFunc();
    }

    public void Run()
    {
        while(true)
        {
            string inStr = Console.ReadLine();
            m_state.DoString(inStr);
        }
    }
}

