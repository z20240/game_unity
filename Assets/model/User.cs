using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class User {

    private static User instance;
    private int uid;
    private int ctime;
    private int user_id;
    private string pass_word;
    private string name;
    private int job;
    private int last_play;
    private int level;
    private int exp;
    private int vip;
    private string mugshot_customized;
    private string mugshot_url;
    private int game_role;

    private User(){ /* constructor */ }
    public static User getInstance() {
        if(instance == null){
            instance = new User();
        }
        return instance;
    }
    public int Uid {
        get{ return uid; }
    }
    public int Ctime {
        get { return ctime; }
        set { ctime = value; }
    }
    public int User_id {
        get { return user_id; }
        set { user_id = value; }
    }
    public string Pass_word {
        get { return pass_word; }
        set { pass_word = value; }
    }
    public string Name {
        get { return name; }
        set { name = value; }
    }
    public int Job {
        get { return job; }
        set { job = value; }
    }
    public int Last_play {
        get { return last_play; }
        set { last_play = value; }
    }
    public int Level {
        get { return level; }
        set { level = value; }
    }
    public int Exp {
        get { return exp; }
        set { exp = value; }
    }
    public int Vip {
        get { return vip; }
        set { vip = value; }
    }
    public string Mugshot_customized {
        get { return mugshot_customized; }
        set { mugshot_customized = value; }
    }
    public string Mugshot_url {
        get { return mugshot_url; }
        set { mugshot_url = value; }
    }
    public int Game_role {
        get { return game_role; }
        set { game_role = value; }
    }

    public User GetLocalUserData() {
        object obj_user_data = LocalFile.LoadLocalFile("user");
        if (obj_user_data == null) {
            return null;
        } else {
            instance = (User)obj_user_data;
            return instance;
        }
    }
    public void SetLocalUserData() {
        LocalFile.SaveLocalFile("user", instance);
    }
}
