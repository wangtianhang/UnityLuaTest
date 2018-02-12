using UnityEngine;
using System.Collections;

using LuaInterface;

public class LuaBehaviour : MonoBehaviour
{
    public string m_luaName = null;

    public LuaFunction m_start = null;
    public LuaFunction m_update = null;
    public LuaFunction m_click = null;

    void Start()
    {
        if (m_start != null)
        {
            m_start.Call();
        }
    }

    void Update()
    {
        if (m_update != null)
        {
            m_update.Call();
        }
    }

    void OnClick()
    {
        //Debug.Log("charp OnClick");
        //LuaState state = SingletonMgr.GetLuaState();
        //string luaStr = m_luaTable + ".OnClick";
        if (m_click != null)
        {
            m_click.Call();
        }
    }

    void OnDestroy()
    {
        if (m_start != null)
        {
            m_start.Dispose();
        }
        if (m_update != null)
        {
            m_update.Dispose();
        }
        if (m_click != null)
        {
            m_click.Dispose();
        }
    }

    public void AddStart(LuaFunction luafunc)
    {
        m_start = luafunc;
    }

    public void AddUpdate(LuaFunction luafunc)
    {
        m_update = luafunc;
    }

    public void AddClick(LuaFunction luafunc)
    {
        m_click = luafunc;
    }
}