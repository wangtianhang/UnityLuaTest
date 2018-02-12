using UnityEngine;
using System.Collections;
using System.IO;

using LuaInterface;

public class LuaLauncher : LuaClient
{
    //LuaState m_lua;

	// Use this for initialization
	void Start () 
    {
        //GameObject.DontDestroyOnLoad(gameObject);

	    // todo 从远程服务器拉取lua框架
	}

    protected override LuaFileUtils InitLoader()
    {
        LuaResLoader ret = new LuaResLoader();
        
        return ret;
    }

    protected override void Bind()
    {
        base.Bind();

        // 启动lua框架
        StartCoroutine(LaunchLua());
    }

    IEnumerator LaunchLua()
    {
        //m_lua = new LuaState();
        //m_lua.Start();

        //LuaBinder.Bind(m_lua);

        string[] luaFiles = Directory.GetFiles(Application.streamingAssetsPath, "*.lua", SearchOption.AllDirectories);
        foreach(var iter in luaFiles)
        {
            string url = "file://" + iter;
            WWW www = new WWW(url);
            yield return www;

            string fileName = Path.GetFileNameWithoutExtension(iter);
            string luaText = www.text;
            luaState.DoString(luaText, fileName);
        }
        
        //lua.CheckTop();
        //lua.Dispose();

        Debug.Log("LaunchLua Main");

        LuaFunction luaFunc = luaState.GetFunction("GameMain");
        luaFunc.Call();
    }
	
	// Update is called once per frame
	void Update () 
    {
	
	}

    protected override void OnLevelWasLoaded(int level)
    {
        base.OnLevelWasLoaded(level);

        LuaFunction luaFunc = luaState.GetFunction("GameOnLevelWasLoaded");
        Debug.Log("OnLevelWasLoaded " + Application.loadedLevelName);
        luaFunc.Call<string>(Application.loadedLevelName);
    }

    public LuaState GetLuaState()
    {
        return luaState;
    }
}
