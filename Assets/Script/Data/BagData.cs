using UnityEngine;
using System.Collections;
using GameCore;
using System.Collections.Generic;
using UnityEngine.UI;

public class BagData : Singleton<BagData>
{
    //存放快捷栏格子表
    private List<GameObject> cutsArr = new List<GameObject>();
    //存放背包格子表
    private List<GameObject> bagCuts = new List<GameObject>();
    //存储背包各格子的物品id
    private List<int> idList = new List<int>();
    //字典存储物品id和物品对应数量
    private Dictionary<int, int> PackData =new Dictionary<int,int>();
    //选择框
    private Transform selectCut;
    //格子下标
    private int selectindex;


    private int bagCutNum = 0;

    public List<GameObject> CutsArr
    {
        get
        {
            return cutsArr;
        }
    }
    public List<GameObject> BagCuts
    {
        get
        {
            return bagCuts;
        }
    }
    public List<int> IdList
    {
        get
        {
            return idList;
        }
    }

    public int BagCutNum
    {
        set
        {
            bagCutNum = value;
        }
    }
    public Transform SelectCut
    {
        get
        {
            return selectCut;
        }
        set
        {
            selectCut = value;
        }
    }
    public int Selectindex
    {
        get
        {
            return selectindex;
        }
        set
        {
            selectindex = value;
        }
    }

    //初始化背包数据
    public void InitBagData()
    {
        if(!GameTool.HasKey("Bag"))
        {
            string str = null;
            int index = 1;

            str += "-1:-1|";
            for (var i = 0; i < 7; i++)
            {
                str += ((i + 1) + ":0|");
                index++;
            }
            str += (index + ":0");
            GameTool.SetString("Bag", str);
        }

        ToDictionary();

        for (var i = 0; i < bagCutNum; i++)
        {
            idList.Add(PackData[-1]);
            //GameDebuger.Log(idList[i]);
        }

    }
    //读取本地数据，传至字典
    public void ToDictionary()
    {
        string str = GameTool.GetString("Bag");

        string[] cstr;
        cstr = GameTool.SplitString(str, '|');

        for(var i = 0;i<cstr.Length;i++)
        {
            string[] astr = GameTool.SplitString(cstr[i], ':');
            PackData.Add(int.Parse(astr[0]), int.Parse(astr[1]));
        }
    }
    //根据id找数量
    public int ReadCountById(int id)
    {
        if (PackData.ContainsKey(id))
            return PackData[id];
        else
            return -1;
    }
    //修改物品的数量，并保存至字典
    public void EditorThingCount(int id, Text num)
    {
        PackData[id] = int.Parse(num.text);
        SavaData();
    }
    //保存
    private void SavaData()
    {
        string strData = null;
        int index = 0;

        foreach(KeyValuePair<int, int> item in PackData)
        {
            index++;
            strData += item.Key + ":" + item.Value;
            if (index != PackData.Count)
                strData += ";";

            AllCompose.Instance.UpdateBagCutAndToolbarCut();
        }
        GameTool.SetString("PackData", strData);
    }
}
