using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.Networking;
using Facebook.Unity;
using UnityEngine.UI;
using System;
using LitJson; // 外部載入的套件，如果可以用的話，以後要留著

public class Login : MonoBehaviour {

    // [快速登入]
    public void QuickStartCreate() {
        StartCoroutine(CreateNewAccount());
    }

    // [開始遊戲]
    public void StartGame() {
        StartCoroutine(GetLocalAccount());
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

            StartCoroutine(GetFBUserAccount(aToken.UserId));

            // 進入遊戲

        });
    }

    /*
     * 取得 login 相關的資料
     */
    IEnumerator GetLocalAccount() {
        // 如果沒資料的話，顯示登入畫面
        if (User.getInstance().GetLocalUserData() == null) {
            // show login choose panel
            yield break;
        }

        string url = Util.getInstance().LobbyAddr + "/users/user?user_id=" + User.getInstance().User_id + "&fb_userid=" + User.getInstance().Fb_userid;

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
                Debug.Log("[json] json = " + jsonResult);
                Debug.Log("[json] json[result] = " + json["result"]);
                Debug.Log("[json] json[data][uid] = " + json["data"]["uid"]);
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
