// luaCTest.cpp : 定义控制台应用程序的入口点。
//

#include "stdafx.h"

//#define LUALIB_API extern "C"
extern "C"
{
#include "..\lua515\lua-5.1.5\src\lua.h"
#include "..\lua515\lua-5.1.5\src\lauxlib.h";
}


#include <math.h>

static int l_sin(lua_State *L)
{
	double d = lua_tonumber(L, 1);
	lua_pushnumber(L, sin(d));
	return 1;
}

static int l_print(lua_State *L)
{
	printf("%s", lua_tostring(L, 1));
	return 1;
}

int _tmain(int argc, _TCHAR* argv[])
{
	lua_State * L = luaL_newstate();
	lua_pushcfunction(L, l_print);
	lua_setglobal(L, "print");

	luaL_dostring(L, "print('hello')");
	return 0;
}



