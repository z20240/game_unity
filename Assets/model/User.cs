using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using LitJson;

public class User {

    private static User instance;
    private int m_uid;
    private int m_ctime;
    private string m_user_id;
    private string m_pass_word;
    private string m_name;
    private int m_job;
    private int m_last_play;
    private int m_level;
    private int m_exp;
    private int m_vip;
    private int m_mugshot_customized;
    private string m_mugshot_url;
    private int m_game_role;
    private string m_fb_userid;

    private User(){ /* constructor */ }
    public static User getInstance() {
        if(instance == null){
            instance = new User();
        }
        return instance;
    }
    public int uid {
        get { return m_uid; }
        set { m_uid = value; }
    }
    public int ctime {
        get { return m_ctime; }
        set { m_ctime = value; }
    }
    public string user_id {
        get { return m_user_id; }
        set { m_user_id = value; }
    }
    public string pass_word {
        get { return m_pass_word; }
        set { m_pass_word = value; }
    }
    public string name {
        get { return m_name; }
        set { m_name = value; }
    }
    public int job {
        get { return m_job; }
        set { m_job = value; }
    }
    public int last_play {
        get { return m_last_play; }
        set { m_last_play = value; }
    }
    public int level {
        get { return m_level; }
        set { m_level = value; }
    }
    public int exp {
        get { return m_exp; }
        set { m_exp = value; }
    }
    public int vip {
        get { return m_vip; }
        set { m_vip = value; }
    }
    public int mugshot_customized {
        get { return m_mugshot_customized; }
        set { m_mugshot_customized = value; }
    }
    public string mugshot_url {
        get { return m_mugshot_url; }
        set { m_mugshot_url = value; }
    }
    public int game_role {
        get { return m_game_role; }
        set { m_game_role = value; }
    }

    public string fb_userid {
        get { return m_fb_userid; }
        set { m_fb_userid = value; }
    }
    public User GetLocalUserData() {
        JsonData jsonData_user = LocalFile.LoadLocalFile("user");

        if (jsonData_user == null) {
            return null;
        }
        Debug.Log("[GetLocalUserData] jsonData_user:" + jsonData_user.ToJson());

        User.getInstance().Clone(jsonData_user);

        Debug.Log("[In User.cs] Instance uid=" + uid);
        Debug.Log("[In User.cs] Instance ctime=" + ctime);
        Debug.Log("[In User.cs] Instance user_id=" + user_id);
        Debug.Log("[In User.cs] Instance pass_word=" + pass_word);
        Debug.Log("[In User.cs] Instance name=" + name);
        Debug.Log("[In User.cs] Instance job=" + job);
        Debug.Log("[In User.cs] Instance last_play=" + last_play);
        Debug.Log("[In User.cs] Instance level=" + level);
        Debug.Log("[In User.cs] Instance exp=" + exp);
        Debug.Log("[In User.cs] Instance vip=" + vip);
        Debug.Log("[In User.cs] Instance mugshot_customized=" + mugshot_customized);
        Debug.Log("[In User.cs] Instance mugshot_url=" + mugshot_url);
        Debug.Log("[In User.cs] Instance game_role=" + game_role);
        Debug.Log("[In User.cs] Instance fb_userid=" + fb_userid);
        Debug.Log("[GetLocalUserData] instance:" + instance.ToString());
        return instance;

    }
    public void SetLocalUserData() {
        LocalFile.SaveLocalFile("user", instance);
    }

    public void Clone(object obj) {
        string json = JsonMapper.ToJson(obj);

        JsonData dic_Obj = JsonMapper.ToObject<JsonData>(json);
        uid = (int)dic_Obj["uid"];
        ctime = (int)dic_Obj["ctime"];
        user_id = (string)dic_Obj["user_id"];
        pass_word = (string)dic_Obj["pass_word"];
        name = (string)dic_Obj["name"];
        job = (int)dic_Obj["job"];
        last_play = (int)dic_Obj["last_play"];
        level = (int)dic_Obj["level"];
        exp = (int)dic_Obj["exp"];
        vip = (int)dic_Obj["vip"];
        mugshot_customized = (int)dic_Obj["mugshot_customized"];
        mugshot_url = dic_Obj["mugshot_url"] != null ? (string)dic_Obj["mugshot_url"] : "";
        game_role = (int)dic_Obj["game_role"];
        fb_userid = dic_Obj["fb_userid"] != null ? (string)dic_Obj["fb_userid"] : "";

        Debug.Log("[In User.cs] uid=" + uid);
        Debug.Log("[In User.cs] ctime=" + ctime);
        Debug.Log("[In User.cs] user_id=" + user_id);
        Debug.Log("[In User.cs] pass_word=" + pass_word);
        Debug.Log("[In User.cs] name=" + name);
        Debug.Log("[In User.cs] job=" + job);
        Debug.Log("[In User.cs] last_play=" + last_play);
        Debug.Log("[In User.cs] level=" + level);
        Debug.Log("[In User.cs] exp=" + exp);
        Debug.Log("[In User.cs] vip=" + vip);
        Debug.Log("[In User.cs] mugshot_customized=" + mugshot_customized);
        Debug.Log("[In User.cs] mugshot_url=" + mugshot_url);
        Debug.Log("[In User.cs] game_role=" + game_role);
        Debug.Log("[In User.cs] fb_userid=" + fb_userid);
    }
}
