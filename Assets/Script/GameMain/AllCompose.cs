using UnityEngine;
using System.Collections;
using GameCore;
using UnityEngine.UI;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using Uniblocks;

public class AllCompose : UnitySingleton<AllCompose>
{
    private bool isAddStuff = false;
    private bool stuffIsFull = false;

    private int composedThing = -1;

    bool isCompose = false;

    public int ComposeThing
    {
        get
        {
            return composedThing;
        }
    }

    private int[] composeId;

    public int[] ComposeId
    {   
        get
        {
            return composeId;
        }
        set
        {
            composeId = value;
        }
    }

    public void GetStuff(int id, List<GameObject> golist)
    {
        for (var i = 0; i < golist.Count; i++)
        {
            //已经有物体
            //相同id就叠加，改text
            Compos(i, id, golist[i]);
            GameDebuger.Log(id);
            
            if (isAddStuff == true)
            {
                BagData.Instance.IdList[i] = id;

                GameDebuger.Log("AAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAA");
                UpdateBagCutAndToolbarCut();
                break;
            }
        }
    }
    //物品叠加                                                      bl为true则反向叠加，即减少num个
    public void Compos(int index, int id, GameObject go, int num = 1, bool bl = false)
    {
        isAddStuff = false;
        Text tx = GameTool.GetTheChildComponent<Text>(go, "ThingNum");

        if (id==-1)
        {
            return;
        }

        int maxNum = int.Parse(DataController.Instance.ReadCfg("Max", id, DataController.Instance.dicStuff));

        if (bl == false)
        {
            if (BagData.Instance.IdList[index] == -1)
            {
                AddStuff(id, go);
                GameDebuger.Log("物品生成！！！");
                isAddStuff = true;
            }
            else
                if (BagData.Instance.IdList[index] == id)
                {
                    if (int.Parse(tx.text) < maxNum)
                    {
                        int thisNum = int.Parse(tx.text) + num;
                        tx.text = (thisNum).ToString();

                        GameDebuger.Log("格子" + index + "的数量：     " + tx.text);

                        if (thisNum > 1)
                            tx.gameObject.SetActive(true);

                        GameDebuger.Log("相同物品已叠加");
                        isAddStuff = true;
                    }
                }
        }
        else
        {
            if (BagData.Instance.IdList[index] == id)
            {
                if (int.Parse(tx.text) > 0)
                {
                    int thisNum = int.Parse(tx.text) - num;
                    tx.text = (thisNum).ToString();

                    GameDebuger.Log("格子" + index + "的数量：     " + tx.text);

                    if (thisNum >= 1)
                        tx.gameObject.SetActive(true);
                    else
                    {
                        UpdateBagCutAndToolbarCut();
                        Destroy(GameTool.FindTheChild(go, "CutThing(Clone)").gameObject);
                        BagData.Instance.IdList[index] = -1;
                        HoldBlockManager.HeldBlock = 0;
                    }

                    GameDebuger.Log("相同物品已减少");
                    isAddStuff = true;
                }
            }
        }

    }
    //添加物品
    public void AddStuff(int id, GameObject listgo)
    {
        GameObject go = Instantiate(ResourcesManager.Instance.LoadResources<GameObject>("Prefab/CutThing"));
        GameTool.AddChildToParent(listgo.transform, go.transform);
        go.transform.localPosition = Vector2.zero;
        string path = "Images/" + DataController.Instance.ReadCfg("Path", id, DataController.Instance.dicStuff);
        go.GetComponent<Image>().sprite = ResourcesManager.Instance.LoadResources<Sprite>(path);
        Text tx = GameTool.GetTheChildComponent<Text>(listgo, "ThingNum");
        tx.text = "1";
    }
    //更新背包0-8号格子，与下方快捷栏同步
    public void UpdateBagCutAndToolbarCut()
    {
        for (var i = 0; i < BagData.Instance.CutsArr.Count; i++)
        {
            //0-8id不为-1，即为有东西，则创建
            if (BagData.Instance.IdList[i] != -1)
            {
                //若没有子物体，则创建并添加
                if (BagData.Instance.CutsArr[i].transform.childCount == 0)
                {
                    if (BagData.Instance.IdList[i] != -1)
                        AddStuff(BagData.Instance.IdList[i], BagData.Instance.CutsArr[i]);

                }
                //若有，则更新当前子物体的图片与id，数量
                else
                {
                    //如果数量大于0，则更新
                    if (int.Parse(GameTool.GetTheChildComponent<Text>(BagData.Instance.BagCuts[i], "ThingNum").text) > 0)
                    {
                        Image img = GameTool.GetTheChildComponent<Image>(BagData.Instance.CutsArr[i], "CutThing(Clone)");
                        Image parImg = GameTool.GetTheChildComponent<Image>(BagData.Instance.BagCuts[i], "CutThing(Clone)");
                        img.GetComponent<Image>().sprite = parImg.sprite;
                        Text tx = GameTool.GetTheChildComponent<Text>(BagData.Instance.CutsArr[i], "ThingNum");
                        Text parTx = GameTool.GetTheChildComponent<Text>(BagData.Instance.BagCuts[i], "ThingNum");
                        tx.text = parTx.text;

                        if (int.Parse(tx.text) > 1)
                            tx.gameObject.SetActive(true);
                    }
                    //若小于0，即用完，则销毁
                    else
                    {
                        Destroy(GameTool.FindTheChild(BagData.Instance.CutsArr[i], "CutThing(Clone)").gameObject);
                        BagData.Instance.IdList[i] = -1;
                    }
                }
            }
            //如果没东西，图片改为空
            else
            {
                if (BagData.Instance.CutsArr[i].transform.childCount != 0)
                {
                    Image img = GameTool.GetTheChildComponent<Image>(BagData.Instance.CutsArr[i], "CutThing(Clone)");
                    string path = "Images/null";
                    img.sprite = ResourcesManager.Instance.LoadResources<Sprite>(path);
                    Text tx = GameTool.GetTheChildComponent<Text>(BagData.Instance.CutsArr[i], "ThingNum");
                    tx.text = "0";
                    tx.gameObject.SetActive(false);
                }
            }
        }
    }

    public void ComposeStuff(GameObject go)
    {
        composeId = new int[] { 
            BagData.Instance.IdList[36], BagData.Instance.IdList[37],
            BagData.Instance.IdList[38], BagData.Instance.IdList[39]};

        int need = 0, notNeed = 0;

        foreach (var item in composeId)
        {
            if (item == -1)
                notNeed++;
            else
                need++;
        }

        if (notNeed == 4)
        {
            if (go.transform.childCount != 0)
                Destroy(GameTool.FindTheChild(go, "CutThing(Clone)").gameObject);

            isCompose = false;
            return;
        }

        //合成木板
        if ((composeId[0] == 14 || composeId[1] == 14 || composeId[2] == 14 || composeId[3] == 14) && need == 1)
        {
            composedThing = 17;
            isCompose = true;
        } 
        //合成火把
        else if ((composeId[0] == 14 && composeId[2] == 17) || (composeId[1] == 14 && composeId[3] == 17) && need == 2)
        {
            composedThing = 23;
            isCompose = true;
            GameSceneManager.Instance.hasTorch = true;
        }
        //合成木棍
        else if ((composeId[0] == 17 && composeId[2] == 17) || (composeId[1] == 17 && composeId[3] == 17) && need == 2)
        {
            composedThing = 18;
            isCompose = true;
        }      
        //合成玻璃剑
        else if ((composeId[0] == 12 && composeId[2] == 10) || (composeId[1] == 12 && composeId[3] == 10) && need == 2)
        {
            composedThing = 21;
            isCompose = true;
            GameSceneManager.Instance.hasSword = true;

        }
        //合成斧头
        else if ((composeId[0] == 10 && composeId[1] == 10 && composeId[2] == 10) && need == 3)
        {
            composedThing = 22;
            isCompose = true;
            GameSceneManager.Instance.hasAxe = true;
        }

        if (isCompose)
        {
            if (go.transform.childCount == 0)
            {
                AddStuff(composedThing, go);
                BagData.Instance.IdList[40] = composedThing;
            }

            isCompose = false;
        }
        else
        {
            if (go.transform.childCount != 0)
                Destroy(GameTool.FindTheChild(go, "CutThing(Clone)").gameObject);
        }
    }

    public void ControllerGet(int id)
    {
        GameDebuger.Log(id);
        GetStuff(id, BagData.Instance.BagCuts);
    }

}
