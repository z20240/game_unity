using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class UICanvasMain : MonoBehaviour {
    public Button btn_gameStart;
    public Button btn_login;
    public Button btn_quickLogin;

    // 定義一個要委派的struct，該傳入什麼參數或設定，在這邊就要先定義好。
    public delegate void OnclickDelegate();

    // 以下這三個是上面委派結構實體化出來要被委派的事件。
    public OnclickDelegate gameStart;
    public OnclickDelegate login;
    public OnclickDelegate guest;

    public Animator pnlLoginAni;

    /* UI Controller Logic */


	// Use this for initialization
	void Start () {

	}

	// Update is called once per frame
	void Update () {

	}

    public void btn_gameStart_Onclick() {
        if (gameStart != null) {
            gameStart();
        }
    }
    public void btn_login_Onclick() {
        if (login != null) {
            login();
        }
    }
    public void btn_quickLogin_Onclick() {
        if ( guest != null) {
            guest();
        }
    }
}
