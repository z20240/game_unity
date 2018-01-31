using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.Networking;
using Facebook.Unity;
using UnityEngine.UI;


public class login : MonoBehaviour {

    public void QuickStartCreate() {
        StartCoroutine(CreateNewAccount());
    }

    public void FacebookLogin() {
        // 設定權限
        var permissions = new List<string> () { "public_profile", "email", "user_friends" };
        FB.LogInWithReadPermissions (permissions, (result) => {
            print (message: "check login success or not: " + result);
            if (FB.IsLoggedIn) {
                // AccessToken class will have session details
                var aToken = Facebook.Unity.AccessToken.CurrentAccessToken;
                // Print current access token's User ID
                Debug.Log ("facebook " + aToken.UserId);
                // Print current access token's granted permissions
                foreach (string perm in aToken.Permissions) {
                    Debug.Log (perm);
                }

                // 發送此用戶的機碼
                string uniDeviceID = SystemInfo.deviceUniqueIdentifier;
                Debug.Log ("uniDeviceID " + uniDeviceID);

                User.getInstance().SetLocalUserData();

            } else {
                Debug.Log ("User cancelled login");
            }
        });
    }

    /*
     * 取得 login 相關的資料
     */
    IEnumerator GetLocalAccount() {
        // 如果有資料的話
        if (User.getInstance().GetLocalUserData() != null) {
            // show lobby panel
            var host = "localhost";
            var port = 3000;

            var request = UnityWebRequest.Get(host + port + "/users");
            yield return request.Send();

            if (request.isNetworkError) {
                Debug.LogError("request user data error");
                yield break;
            }

        } else {
            // show login choose panel
            yield break;
        }
    }

    // 快速登入
    IEnumerator CreateNewAccount() {
        string user_id = Util.getInstance().RandomAccount;
        var host = "localhost";
        var port = 3000;
        string url = host + ":" + port + "/users/autoCreate";

        Debug.Log("url : " + url);

        WWWForm form = new WWWForm();
        form.AddField("user_id", user_id);

        using (UnityWebRequest www = UnityWebRequest.Post(url, form)) {
            yield return www.Send();

            if (www.isNetworkError || www.isHttpError) {
                Debug.Log(www.error);
            } else {
                if (www.isDone) {
                    string jsonResult = System.Text.Encoding.UTF8.GetString(www.downloadHandler.data);
                    Debug.Log(jsonResult);
                }
                Debug.Log("Form upload complete!");
            }
        }
    }
}
