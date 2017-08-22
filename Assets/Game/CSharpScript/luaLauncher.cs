using UnityEngine;
using System.Collections;

using LuaInterface;

public class LuaLauncher : MonoBehaviour
{
    LuaState m_lua;

	// Use this for initialization
	void Start () 
    {
        GameObject.DontDestroyOnLoad(gameObject);

	    // todo 从远程服务器拉取lua框架

        // 启动lua框架
        StartCoroutine(LaunchLua());
	}

    IEnumerator LaunchLua()
    {
        string url = "file://" + Application.streamingAssetsPath + "/FrameLuaScript/UpdateRes.lua";
        WWW www = new WWW(url);
        yield return www;
        string luaText = www.text;

        m_lua = new LuaState();
        m_lua.Start();

        LuaBinder.Bind(m_lua);

        m_lua.DoString(luaText);
        //lua.CheckTop();
        //lua.Dispose();

        LuaFunction luaFunc = m_lua.GetFunction("Main");
        luaFunc.Call();
    }
	
	// Update is called once per frame
	void Update () 
    {
	
	}

    void OnLevelWasLoaded()
    {
        LuaFunction luaFunc = m_lua.GetFunction("OnLevelWasLoaded");
        luaFunc.Call<string>(Application.loadedLevelName);
    }
}
