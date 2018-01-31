using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;


public class LocalFile {

    public static void SaveLocalFile(string fileName, object obj) {
        //填寫jplayerState格式的資料..

        //將myPlayer轉換成json格式的字串
        string saveString = JsonUtility.ToJson(obj);

        //將字串saveString存到硬碟中
        StreamWriter file = new StreamWriter(System.IO.Path.Combine(Application.streamingAssetsPath, fileName));
        file.Write(saveString);
        file.Close();
    }

    public static object LoadLocalFile(string fileName) {
        //讀取json檔案並轉存成文字格式
        StreamReader file = new StreamReader(System.IO.Path.Combine(Application.streamingAssetsPath, fileName));
        string loadJson = file.ReadToEnd();
        file.Close();

        //新增一個物件類型為playerState的變數 loadData
        object loadData = new object();

        //使用JsonUtillty的FromJson方法將存文字轉成Json
        loadData = JsonUtility.FromJson<object>(loadJson);
        return loadData;
    }
}
