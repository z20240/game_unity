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

                StartCoroutine(GetFBUserAccount(aToken.UserId));

                // 進入遊戲
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
            string url = Util.getInstance().LobbyAddr + "/users/user?user_id=" + User.getInstance().User_id + "&fb_userid=" + User.getInstance().Fb_userid;

            var request = UnityWebRequest.Get(url);
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

    IEnumerator GetFBUserAccount(string fb_userid) {
        string url = Util.getInstance().LobbyAddr + "/users/facebookUser/" + fb_userid;

        Debug.Log("url : " + url);

        using (UnityWebRequest www = UnityWebRequest.Get(url)) {
            yield return www.Send();

            if (www.isNetworkError || www.isHttpError) {
                Debug.Log(www.error);
                yield break;
            } else {
                if (www.isDone) {
                    string jsonResult = System.Text.Encoding.UTF8.GetString(www.downloadHandler.data);
                    object objResult = JsonUtility.FromJson<object>(jsonResult);
                    var dictiionaryResult = (Dictionary<string, object>)objResult;

                    if (!(bool)dictiionaryResult["result"]) {
                        Debug.LogError("== result Error! ==");
                        yield break;
                    }

                    User.getInstance().Clone(dictiionaryResult["data"]);
                    User.getInstance().SetLocalUserData();
                    Debug.Log(jsonResult);
                    Debug.Log("[facebookUserAccount] User instance: " + User.getInstance().ToString());
                    Debug.Log("dictiionaryResult : " + dictiionaryResult.ToString());
                }
                Debug.Log("Form upload complete!");
            }
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
            } else {
                if (www.isDone) {
                    string jsonResult = System.Text.Encoding.UTF8.GetString(www.downloadHandler.data);
                    object objResult = JsonUtility.FromJson<object>(jsonResult);
                    var dictiionaryResult = (Dictionary<string, object>)objResult;

                    if (!(bool)dictiionaryResult["result"]) {
                        Debug.LogError("== result Error! ==");
                        yield break;
                    }

                    User.getInstance().Clone(dictiionaryResult["data"]);
                    User.getInstance().SetLocalUserData();
                    Debug.Log(jsonResult);
                    Debug.Log("[CreateNewAccount] User instance: " + User.getInstance().ToString());
                    Debug.Log("dictiionaryResult : " + dictiionaryResult.ToString());
                }
                Debug.Log("Form upload complete!");
            }
        }
    }
}
