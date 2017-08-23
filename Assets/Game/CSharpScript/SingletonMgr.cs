using UnityEngine;
using System.Collections;

using LuaInterface;

public class SingletonMgr : MonoBehaviour {

    static LuaLauncher m_luaLauncher = null;

	// Use this for initialization
	void Start () 
    {
        GameObject.DontDestroyOnLoad(gameObject);

        GameObject luaLauncherGo = new GameObject("luaLauncherGo");
        m_luaLauncher = luaLauncherGo.AddComponent<LuaLauncher>();
        m_luaLauncher.transform.parent = transform;
	}
	
	// Update is called once per frame
	void Update () 
    {
	
	}

    public static LuaState GetLuaState()
    {
        return m_luaLauncher.GetLuaState();
    }
}
