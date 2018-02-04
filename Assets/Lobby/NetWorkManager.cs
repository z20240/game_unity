using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.Networking;
using LitJson; // 外部載入的套件，如果可以用的話，以後要留著
using RSG;

public class NetWorkManager : MonoBehaviour {

    public IPromise<JsonData> WebRequest_Get(string url) {
        var promise = new Promise<JsonData>();
        Debug.Log("[WebRequest_Get] url" + url);
        StartCoroutine(_WebRequest_Get(promise, url));
        Debug.Log("[WebRequest_Get] url" + url);
        return promise;
    }

    public IPromise<JsonData> WebRequest_Post(string url, WWWForm form) {
        var promise = new Promise<JsonData>();
        Debug.Log("[WebRequest_Post] url" + url);
        StartCoroutine(_WebRequest_Post(promise, url, form));
        return promise;
    }

    IEnumerator _WebRequest_Get(Promise<JsonData> promise, string url) {
        Debug.Log(">>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>");
        using (UnityWebRequest www = UnityWebRequest.Get(url)) {
            Debug.Log("[_WebRequest_Get] url  " + url);
            yield return www.Send();

            if (www.isNetworkError || www.isHttpError) {
                Debug.Log(www.error);
                promise.Reject(new RequestExceptoin(www.error));
                yield break;
            }

            if (www.isDone) {
                string jsonResult = System.Text.Encoding.UTF8.GetString(www.downloadHandler.data);

                JsonData json = JsonMapper.ToObject<JsonData>(jsonResult);
                if (!(bool)json["result"]) {
                    Debug.LogError("== result Error! ==");
                    promise.Reject(new RequestExceptoin(www.error));
                    yield break;
                }

                promise.Resolve(json);
            }
        }
        Debug.Log("<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<");
    }

    IEnumerator _WebRequest_Post(Promise<JsonData> promise, string url, WWWForm form) {
        using (UnityWebRequest www = UnityWebRequest.Post(url, form)) {
            yield return www.Send();

            if (www.isNetworkError || www.isHttpError) {
                Debug.Log(www.error);
                promise.Reject(new RequestExceptoin(www.error));
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
                    promise.Reject(new RequestExceptoin(www.error));
                    yield break;
                }

                promise.Resolve(json);
            }
        }
    }
}
