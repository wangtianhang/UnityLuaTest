using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using System.IO;
using LuaInterface;

public class GUtil
{
    public static string TestLuaCallByString(string param)
    {
        Debug.Log(param);
        return param;
    }

    public static byte[] GetFileData(string relativePath)
    {
        string path = Application.streamingAssetsPath + "/" + relativePath;
        byte[] bytes = File.ReadAllBytes(path);
        return bytes;
    }

    public static void HandleFileData(byte[] data)
    {
        int test = 0;
    }

    public static LuaTable TestLuaCallByLuaDicTable(LuaTable param)
    {
        LuaDictTable dicTable = param.ToDictTable();
        foreach (var iter in dicTable)
        {
            Debug.Log("key " + iter.Key + " value " + iter.Value);
        }

        int reference = SingletonMgr.GetLuaState().ToLuaRef();
        LuaTable ret = new LuaTable(reference, SingletonMgr.GetLuaState());
        //ret();
        //Dictionary<string, string> ret = new Dictionary<string, string>();
        //ret.Add("csharpKey", "csharpValue");
        ret["csharpKey"] = "csharpValue";
        return ret;
    }

    public static LuaTable TestLuaCallByLuaArrayTable(LuaTable param)
    {
        LuaArrayTable arrayTable = param.ToArrayTable();
        foreach(var iter in arrayTable)
        {
            Debug.Log("array " + iter);
        }

        int reference = SingletonMgr.GetLuaState().ToLuaRef();
        LuaTable ret = new LuaTable(reference, SingletonMgr.GetLuaState());
        ret[1] = "array1";
        ret[2] = "array2";
        return ret;
    }
}

