using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.Networking;
using LitJson; // 外部載入的套件，如果可以用的話，以後要留著
using RSG;

public class RequestExceptoin : System.Exception {
    public RequestExceptoin(string mes) {
        Debug.LogError(mes);
    }
}