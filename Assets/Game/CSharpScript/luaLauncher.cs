using UnityEngine;
using System.Collections;
using System.IO;

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
        m_lua = new LuaState();
        m_lua.Start();

        LuaBinder.Bind(m_lua);

        string[] luaFiles = Directory.GetFiles(Application.streamingAssetsPath, "*.lua", SearchOption.AllDirectories);
        foreach(var iter in luaFiles)
        {
            string url = "file://" + iter;
            WWW www = new WWW(url);
            yield return www;

            string fileName = Path.GetFileNameWithoutExtension(iter);
            string luaText = www.text;
            m_lua.DoString(luaText, fileName);
        }
        
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
