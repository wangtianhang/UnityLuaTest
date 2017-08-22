using UnityEngine;
using System.Collections;

using LuaInterface;

public class luaLauncher : MonoBehaviour {

	// Use this for initialization
	void Start () 
    {
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

        LuaState lua = new LuaState();
        lua.Start();
        lua.DoString(luaText);
        //lua.CheckTop();
        //lua.Dispose();
    }
	
	// Update is called once per frame
	void Update () 
    {
	
	}
}
