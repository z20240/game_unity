using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class model_user {
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
    private int game_rol;
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
    public int Game_rol {
        get { return game_rol; }
        set { game_rol = value; }
    }
}
