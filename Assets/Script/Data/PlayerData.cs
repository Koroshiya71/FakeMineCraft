using UnityEngine;
using System.Collections;
using GameCore;

public class PlayerData : Singleton<PlayerData>
{
    //角色生命值
    private int hp = 0;
    //角色饥饿值
    private int ent = 0;

    public int HP
    {
        get
        {
            return hp;
        }
    }

    public int Ent
    {
        get
        {
            return ent;
        }
    }

    public void InitPlayerData()
    {
        if(!GameTool.HasKey("PlayerHp"))
        {
            GameTool.SetInt("PlayerHp", 20);
        }
        hp = GameTool.GetInt("PlayerHp");

        if(!GameTool.HasKey("PlayerEnt"))
        {
            GameTool.SetInt("PlayerEnt", 20);
        }
        ent = GameTool.GetInt("PlayerEnt");
    }

    public void EditorHp(int newhp)
    {
        hp = newhp;
        GameTool.SetInt("PlayerHp", hp);
    }

    public void EditorEnt(int newent)
    {
        ent = newent;
        GameTool.SetInt("PlayerEnt", ent);
    }
}
