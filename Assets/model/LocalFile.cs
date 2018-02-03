using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using LitJson;


public class LocalFile {

    public static void SaveLocalFile(string fileName, object obj) {
        //填寫jplayerState格式的資料..

        //將myPlayer轉換成json格式的字串
        string saveString = JsonMapper.ToJson(obj);

        //將字串saveString存到硬碟中
        Debug.Log("path : " + Application.persistentDataPath);
        StreamWriter file = new StreamWriter(System.IO.Path.Combine(Application.persistentDataPath, fileName));
        file.Write(saveString);
        file.Close();
    }

    public static JsonData LoadLocalFile(string fileName) {
        if (!File.Exists(System.IO.Path.Combine(Application.persistentDataPath, fileName))) {
            return null;
        }
        Debug.Log("path : " + Application.persistentDataPath);

        //讀取json檔案並轉存成文字格式
        StreamReader file = new StreamReader(System.IO.Path.Combine(Application.persistentDataPath, fileName));
        string loadJson = file.ReadToEnd();
        file.Close();

        //新增一個物件類型為playerState的變數 loadData
        JsonData loadData = JsonMapper.ToObject(loadJson);

        return loadData;
    }
}
