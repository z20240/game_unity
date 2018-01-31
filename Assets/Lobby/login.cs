using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.Networking;
using Facebook.Unity;
using UnityEngine.UI;


public class login : MonoBehaviour {
    void Awake () {

    }

	// Use this for initialization
	void Start () {

	}

	// Update is called once per frame
	void Update () {

	}

    /*
     * 取得 login 相關的資料

     */
    public IEnumerator GetLocalAccount() {
        if (User.getInstance().GetLocalUserData() != null) {
            // show lobby panel
            var host = "localhost";
            var port = 3000;

            var request = UnityWebRequest.Get(host + port);
            yield return request.Send();

            if (request.isError) {
                Debug.LogError("request user data error");
                yield break;
            }

        } else {
            // show login choose panel
            yield break;
        }
    }
}
