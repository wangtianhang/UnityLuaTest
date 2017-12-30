using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


class LuaState
{
    IntPtr m_luaState = IntPtr.Zero;

    public LuaState(IntPtr luaState)
    {
        m_luaState = luaState;
    }

    public void DoString(string str)
    {
        LuaWrap.luaL_dostring(m_luaState, str);
    }
}

