using System.Collections;
using System.Collections.Generic;
using UnityEngine;

class Util {

    private static Util instance;
    private Util(){ /* constructor */ }
    public static Util getInstance() {
        if(instance == null){ instance = new Util(); }
        return instance;
    }
    public string RandomAccount {
        get {
            var chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789";
            var stringChars = new char[30];

            for (int i = 0; i < stringChars.Length; i++)
            {
                stringChars[i] = chars[Random.Range(0, chars.Length)];
            }

            return new System.String(stringChars);
        }
    }
}
