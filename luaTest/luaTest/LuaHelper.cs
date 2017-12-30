using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;


class LuaHelper
{
    public static object ToVarObject(IntPtr L, int stackPos)
    {
        LuaTypes type = LuaDLL.lua_type(L, stackPos);

        switch (type)
        {
            case LuaTypes.LUA_TNUMBER:
                return LuaDLL.lua_tonumber(L, stackPos);
            case LuaTypes.LUA_TSTRING:
                return LuaDLL.lua_tostring(L, stackPos);
            case LuaTypes.LUA_TBOOLEAN:
                return LuaDLL.lua_toboolean(L, stackPos);
            default:
                return null;
        }
    }

    public static string lua_ptrtostring(IntPtr str, int len)
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

