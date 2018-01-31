using System.Collections;
using System.Collections.Generic;
using UnityEngine.Networking;
using Facebook.Unity;
using UnityEngine;
using UnityEngine.UI;

public class fb_tool : MonoBehaviour {

    public Text FriendsText;
    /// Awake is called when the script instance is being loaded.
    void Awake () {
        if (!FB.IsInitialized) {
            // Initialize the Facebook SDK
            FB.Init (InitCallback, OnHideUnity);
        } else {
            // Already initialized, signal an app activation App Event
            FB.ActivateApp ();
        }
    }

    private void InitCallback () {
        if (FB.IsInitialized) {
            // Signal an app activation App Event
            FB.ActivateApp ();
            // Continue with Facebook SDK
            // ...
        } else {
            Debug.Log ("Failed to Initialize the Facebook SDK");
        }
    }

    private void OnHideUnity (bool isGameShown) {
        if (!isGameShown) {
            // Pause the game - we will need to hide
            Time.timeScale = 0;
        } else {
            // Resume the game - we're getting focus again
            Time.timeScale = 1;
        }
    }

    #region Login / Logout
    // #region 可讓您指定在使用 Visual Studio 程式碼編輯器的大綱功能時，可以展開或摺疊的程式碼區塊。
    // 在程式碼較常的檔案中，如果可以摺疊或隱藏一個或多個區域會更加方便，因為如此一來，您可以專注在目前工作的檔案內容上。

    public void FacebookLogin () {
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

            } else {
                Debug.Log ("User cancelled login");
            }
        });
    }

    public void FacebookLogout () {
        FB.LogOut ();
    }

    #endregion (Login / Logout)

    public void FacebookShare () {
        string uri = "http://z20240-les-lee.com"; // 這個遊戲的 link
        string title = "卡牌對戰遊戲";
        string context = "好玩的遊戲測試"; // 應該要讓使用者可以填寫
        string photoUrl = "";
        FB.ShareLink (new System.Uri (uri), title, context, new System.Uri (photoUrl));
    }

    #region Inviting code
    public void FacebookGameRequest () {
        FB.AppRequest (message: "測試 request，不知道是幹嘛的");
    }

    #endregion

    public void getFriendsPlayingThisGame () {
        string query = "me/friends";
        FB.API (query, HttpMethod.GET, result => {
            print (result);
            // result 會是一個 json string 可以使用 Facebook.MiniJSON.Json.Deserialize() 轉換成 json Dictionary
            var dictionary = (Dictionary<string, object>) Facebook.MiniJSON.Json.Deserialize (result.RawResult); // result.RawResult 才是 result 的 string type
            print ("====>dictionary<=====");
            print (dictionary);
            var friendsList = (List<object>) dictionary["data"];
            print (">=====friendsList======<");
            print (friendsList);
            foreach (var friendObj in friendsList) {
                FriendsText.text += ((Dictionary<string, object>) friendObj) ["name"];
            }
        });
    }
}