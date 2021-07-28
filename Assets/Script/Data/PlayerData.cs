using UnityEngine;
using System.Collections;
using GameCore;
using System.Collections.Generic;
using UnityEngine.UI;

public class PlayerData : Singleton<PlayerData>
{
    //角色生命值
    private int hp = 0;
    //角色饥饿值
    private float hungry = 0;

    private List<GameObject> hpList = new List<GameObject>();

    private List<GameObject> hungryList = new List<GameObject>();

    
    public List<GameObject> HpList
    {
        get
        {
            return hpList;
        }
    }
    public List<GameObject> HungryList
    {
        get
        {
            return hungryList;
        }
    }

    public int HP
    {
        get
        {
            return hp;
        }
    }

    public float Ent
    {
        get
        {
            return hungry;
        }
    }

    public void InitPlayerData()
    {
        if(!GameTool.HasKey("PlayerHp"))
        {
            GameTool.SetInt("PlayerHp", 20);
        }
        hp = GameTool.GetInt("PlayerHp");

        if (!GameTool.HasKey("PlayerEnt"))
        {
            GameTool.SetFloat("PlayerEnt", 20);
        }
        hungry = GameTool.GetFloat("PlayerEnt");
    }

    public void EditorHp(int newhp)
    {
        if (newhp>=20)
        {
            newhp = 20;
        }
        hp = newhp;
        GameTool.SetInt("PlayerHp", hp);
    }

    public void EditorEnt(float newent)
    {
        if (newent>=20)
        {
            newent = 20;
        }
        hungry = newent;
        GameTool.SetFloat("PlayerEnt", hungry);
        for (int i = 0; i < 20; i++)
        {
            if (i<hungry)
            {
                hungryList[i].SetActive(true);
            }
            else
            {
                hungryList[i].SetActive(false);
            }
        }
    }

    public void InitHunImg()
    {
        if (hungry<=0)
        {
            EditorEnt(20);
        }
        EditorEnt(hungry);
    }
    public void UpdateHungryThroughTime()
    {
        switch (UniStormWeatherSystem_C.Instance.weatherString)
        {
            case "Foggy":
            case "Light Rain":
            case "Light Snow":
            case "Mostly Cloudy":
            case "Falling Fall Leaves":
                EditorEnt(hungry-1);
                break;
            case "Clear":
            case "Mostly Clear":
                EditorEnt(hungry - 2);
                break;
            case "Heavy Rain & Thunder Storm":
            case "Heavy Snow":
                EditorEnt(hungry-3);
                break;
            case "Heavy Rain (No Thunder)":
                EditorEnt(hungry-4);
                break;
        }
    }
}