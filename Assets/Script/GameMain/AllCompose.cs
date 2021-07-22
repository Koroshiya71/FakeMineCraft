using UnityEngine;
using System.Collections;
using GameCore;
using System.Collections.Generic;
using UnityEngine.UI;

public class AllCompose : UnitySingleton<AllCompose>
{
    //存放快捷栏格子表
    private List<GameObject> cutsArr = new List<GameObject>();
    //存放背包格子表
    private List<GameObject> bagCuts = new List<GameObject>();

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

    private List<int> idList = new List<int>();

    private bool isNotAddStuff = false;
    private bool stuffIsFull = false;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.I))
        {
            GetStuff(4, cutsArr, bagCuts);
            GameDebuger.Log("快捷栏多了个  " + 4 + "号  方块");
        }
    }

    public void GetStuff(int id, List<GameObject> golist, List<GameObject> baglist)
    {
        if (idList.Count == 0)
        {
            AddStuff(id, golist[0]);
            AddStuff(id, baglist[0]);
        }
        else
        {
            for (var i = 0; i < baglist.Count; i++)
            {
                //已经有物体
                //相同id就叠加，改text
                if (i < golist.Count - 1)
                    Compos(i, id, golist);

                Compos(i, id, baglist);

                if (isNotAddStuff == true)
                    break;

                if (stuffIsFull)
                {
                    if (i < golist.Count - 1)
                        AddStuff(id, golist[i + 1], i);

                    AddStuff(id, baglist[i + 1], i);
                    break;
                }
            }
        }
    }
    //物品叠加
    private void Compos(int index, int id, List<GameObject> go)
    {
        isNotAddStuff = false;
        stuffIsFull = false;

        int maxNum = int.Parse(DataController.Instance.ReadCfg("Max", id, DataController.Instance.dicCompose));
        Text tx = GameTool.GetTheChildComponent<Text>(go[index], "Text");

        if (idList[index] == id && int.Parse(tx.text) < maxNum)
        {
            
            int thisNum = int.Parse(tx.text) + 1;
            GameDebuger.Log(thisNum);

            tx.text = (thisNum).ToString();
            if (thisNum > 1)
                tx.gameObject.SetActive(true);

            GameDebuger.Log("相同物品已叠加");
            isNotAddStuff = true;

        }
        else if (index + 1 < go.Count)
        {
            if (go[index + 1].transform.childCount == 0)
                stuffIsFull = true;
        }
    }
    //添加物品
    private void AddStuff(int id, GameObject listgo, int index = 0)
    {
        GameObject go = Instantiate(ResourcesManager.Instance.LoadResources<GameObject>("Prefab/CutThing"));
        GameTool.AddChildToParent(listgo.transform, go.transform);
        go.transform.localPosition = Vector2.zero;
        string path = "Images/" + DataController.Instance.ReadCfg("Path", id, DataController.Instance.dicCompose);
        go.GetComponent<Image>().sprite = ResourcesManager.Instance.LoadResources<Sprite>(path);
        Text tx = GameTool.GetTheChildComponent<Text>(listgo.gameObject, "Text");
        tx.text = "1";
        idList.Add(id);
        GameDebuger.Log("OK");

    }

}
