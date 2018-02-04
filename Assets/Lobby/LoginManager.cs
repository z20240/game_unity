using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.Networking;
using Facebook.Unity;
using UnityEngine.UI;
using System;
using LitJson; // 外部載入的套件，如果可以用的話，以後要留著


public class LoginManager : MonoBehaviour {
    UICanvasMain uICanvasMain;
    NetWorkManager nwMng;

    void Start() {
        // 拿到場景中的 gameObject, 從 hirochy 的 root 開始抓取
        GameObject goCvsMain = GameObject.Find("canvas_main");
        if ( goCvsMain == null )
            return;
        uICanvasMain = goCvsMain.GetComponent<UICanvasMain>();

        // delegate 將 onclick 的方式委派過來 ( 用 += )
        uICanvasMain.gameStart += StartGame;
        uICanvasMain.login += FacebookLogin;
        uICanvasMain.guest += QuickStartCreate;

        GameObject goNwMng = GameObject.Find("NetworkManager");
        if ( goNwMng == null )
            return;
        nwMng = goNwMng.GetComponent<NetWorkManager>();
    }

    // [快速登入]
    public void QuickStartCreate() {
        string url = Util.getInstance().LobbyAddr + "/users/autoCreate";
        Debug.Log("url : " + url);
        WWWForm form = new WWWForm();

        nwMng.WebRequest_Post(url, form).Then(json => {
            User.getInstance().Clone(json["data"]);
            User.getInstance().SetLocalUserData();
            Debug.Log("[CreateNewAccount] User instance: " + User.getInstance().ToString());
            Debug.Log("json : " + JsonMapper.ToJson(json));

            Debug.Log("Form upload complete!");
        }).Catch(err => {
            Debug.Log(err);
        });
    }

    // [開始遊戲]
    public void StartGame() {
        // 如果沒資料的話，顯示登入畫面
        if (User.getInstance().GetLocalUserData() == null) {
            // show login choose panel
            // 讓藍色那塊滑進來
            uICanvasMain.pnlLoginAni.SetBool("panelEnter", true);
            Debug.Log("show login choose panel");
            return;
        }

        string url = Util.getInstance().LobbyAddr + "/users/user?user_id=" + User.getInstance().user_id + "&fb_userid=" + User.getInstance().fb_userid;
        nwMng.WebRequest_Get(url).Then(json => {
            User.getInstance().Clone(json["data"]);
            User.getInstance().SetLocalUserData();
            Debug.Log("[StartGame] User instance: " + User.getInstance().ToString());
            Debug.Log("dictiionaryResult : " + json.ToJson());

            Debug.Log("Form upload complete!");

            // 顯示成功登入，轉場

        }).Catch(err => {
            Debug.LogError(err);
        });

    }

    // [FB登入]
    public void FacebookLogin() {
        // 設定權限
        var permissions = new List<string> () { "public_profile", "email", "user_friends" };
        FB.LogInWithReadPermissions (permissions, (result) => {
            print (message: "check login success or not: " + result);

            if (!FB.IsLoggedIn) {
                Debug.Log ("User cancelled login");
                return;
            }

            // if FB.IsLoggedIn 往下做
            // AccessToken class will have session details
            var aToken = Facebook.Unity.AccessToken.CurrentAccessToken;
            // Print current access token's User ID
            Debug.Log ("facebook " + aToken.UserId);
            // Print current access token's granted permissions
            foreach (string perm in aToken.Permissions) {
                Debug.Log (perm);
            }

            string url = Util.getInstance().LobbyAddr + "/users/facebookUser/" + aToken.UserId;
            Debug.Log("url : " + url);


            nwMng.WebRequest_Get(url).Then(json => {
                User.getInstance().Clone(json["data"]);
                User.getInstance().SetLocalUserData();
                Debug.Log("[facebookUserAccount] User instance: " + User.getInstance().ToString());
                Debug.Log("dictiionaryResult : " + json.ToJson());

                Debug.Log("Form upload complete!");

                // 進入遊戲
            }).Catch(err => {
                Debug.LogError(err);
            });

        });
    }

    IEnumerator GetLocalAccount() {
        string url = Util.getInstance().LobbyAddr + "/users/user?user_id=" + User.getInstance().user_id + "&fb_userid=" + User.getInstance().fb_userid;

        using (UnityWebRequest www = UnityWebRequest.Get(url)) {
            yield return www.Send();

            if (www.isNetworkError || www.isHttpError) {
                Debug.Log(www.error);
                yield break;
            }

            if (www.isDone) {
                string jsonResult = System.Text.Encoding.UTF8.GetString(www.downloadHandler.data);

                JsonData json = JsonMapper.ToObject<JsonData>(jsonResult);
                if (!(bool)json["result"]) {
                    Debug.LogError("== result Error! ==");
                    yield break;
                }

                User.getInstance().Clone(json["data"]);
                User.getInstance().SetLocalUserData();
                Debug.Log(jsonResult);
                Debug.Log("[facebookUserAccount] User instance: " + User.getInstance().ToString());
                Debug.Log("dictiionaryResult : " + json.ToJson());
            }
            Debug.Log("Form upload complete!");

            // 顯示成功登入，轉場
        }
    }
    IEnumerator GetFBUserAccount(string fb_userid) {
        string url = Util.getInstance().LobbyAddr + "/users/facebookUser/" + fb_userid;

        Debug.Log("url : " + url);

        using (UnityWebRequest www = UnityWebRequest.Get(url)) {
            yield return www.Send();

            if (www.isNetworkError || www.isHttpError) {
                Debug.Log(www.error);
                yield break;
            }

            if (www.isDone) {
                string jsonResult = System.Text.Encoding.UTF8.GetString(www.downloadHandler.data);

                JsonData json = JsonMapper.ToObject<JsonData>(jsonResult);
                if (!(bool)json["result"]) {
                    Debug.LogError("== result Error! ==");
                    yield break;
                }

                User.getInstance().Clone(json["data"]);
                User.getInstance().SetLocalUserData();
                Debug.Log(jsonResult);
                Debug.Log("[facebookUserAccount] User instance: " + User.getInstance().ToString());
                Debug.Log("dictiionaryResult : " + json.ToJson());
            }
            Debug.Log("Form upload complete!");
        }
    }

    // 無帳號 快速登入
    IEnumerator CreateNewAccount() {
        string url = Util.getInstance().LobbyAddr + "/users/autoCreate";

        Debug.Log("url : " + url);

        WWWForm form = new WWWForm();

        using (UnityWebRequest www = UnityWebRequest.Post(url, form)) {
            yield return www.Send();

            if (www.isNetworkError || www.isHttpError) {
                Debug.Log(www.error);
                yield break;
            }

            if (www.isDone) {
                string jsonResult = System.Text.Encoding.UTF8.GetString(www.downloadHandler.data);
                JsonData json = JsonMapper.ToObject(jsonResult);
                // Debug.Log("[json] json = " + jsonResult);
                // Debug.Log("[json] json[result] = " + json["result"]);
                // Debug.Log("[json] json[data][uid] = " + json["data"]["uid"]);
                string jsonString = JsonMapper.ToJson(json);
                Debug.Log("[json] json = " + jsonString); // 這行有問題，要怎麼把 json Object 印出來

                if (!(bool)json["result"]) {
                    Debug.LogError("== result Error! ==");
                    yield break;
                }

                User.getInstance().Clone(json["data"]);
                User.getInstance().SetLocalUserData();
                Debug.Log(jsonResult);
                Debug.Log("[CreateNewAccount] User instance: " + User.getInstance().ToString());
                Debug.Log("json : " + JsonMapper.ToJson(json));
            }
            Debug.Log("Form upload complete!");
        }
    }
}
