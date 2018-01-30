using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;


public class login : MonoBehaviour {

	// Use this for initialization
	void Start () {

	}

	// Update is called once per frame
	void Update () {

	}

    public void save() {
        //填寫jplayerState格式的資料..
        model_user myPlayer = new model_user();
        myPlayer.Name = "testplayer1";
        myPlayer.Level = 1;

        //將myPlayer轉換成json格式的字串
        string saveString = JsonUtility.ToJson(myPlayer);

        //將字串saveString存到硬碟中
        StreamWriter file = new StreamWriter(System.IO.Path.Combine(Application.streamingAssetsPath, "myPlayer"));
        file.Write(saveString);
        file.Close();
    }

    public void load() {
        //讀取json檔案並轉存成文字格式
        StreamReader file = new StreamReader(System.IO.Path.Combine(Application.streamingAssetsPath, "myPlayer"));
        string loadJson = file.ReadToEnd();
        file.Close();

        //新增一個物件類型為playerState的變數 loadData
        model_user loadData = new model_user();

        //使用JsonUtillty的FromJson方法將存文字轉成Json
        loadData = JsonUtility.FromJson<model_user>(loadJson);
    }
}
