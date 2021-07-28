using UnityEngine;
using System.Collections;
using GameCore;
using UnityEngine.UI;
using System.Collections.Generic;
using Uniblocks;

public class GameMainUI : BaseUI
{
    protected override void InitUiOnAwake()
    {
        base.InitUiOnAwake();

        //造血
        for (var i = 0; i < 10; i++)
        {
            GameObject kAll = Instantiate(Resources.Load("Prefab/Xin") as GameObject);
            GameTool.AddChildToParent(GameTool.FindTheChild(gameObject, "Status"), kAll.transform);
            kAll.transform.localPosition = new Vector2(i * 25.5f, 0);

            PlayerData.Instance.HpList.Add(GameTool.FindTheChild(kAll, "sxinL").gameObject);
            PlayerData.Instance.HpList.Add(GameTool.FindTheChild(kAll, "sxinR").gameObject);
        }
        //造鸡腿
        for (var i = 0; i < 10; i++)
        {
            GameObject kAll = Instantiate(Resources.Load("Prefab/hungry") as GameObject);
            GameTool.AddChildToParent(GameTool.FindTheChild(gameObject, "Status"), kAll.transform);
            kAll.transform.localPosition = new Vector2(468.7f + i * 26.5f, 0);

            PlayerData.Instance.HungryList.Add(GameTool.FindTheChild(kAll, "chikenL").gameObject);
            PlayerData.Instance.HungryList.Add(GameTool.FindTheChild(kAll, "chikenR").gameObject);
        }
        //造下方快捷栏格子
        GameObject gp = GameTool.FindTheChild(gameObject, "ShortCuts").gameObject;
        for (var i = 0; i < 9; i++)
        {
            BagData.Instance.CutsArr.Add(GameTool.GetTheChildComponent<Transform>(gp, "Se" + i.ToString()).gameObject);

        }
        //造选择框
        BagData.Instance.SelectCut = GameTool.FindTheChild(gameObject, "SelectCut");
        GameDebuger.Log(BagData.Instance.SelectCut);
        BagData.Instance.SelectCut.position = new Vector2(BagData.Instance.CutsArr[0].transform.position.x, BagData.Instance.SelectCut.position.y);

    }

    protected override void InitDataOnAwake()
    {
        base.InitDataOnAwake();
        this.uiId = E_UiId.GameMainUI;
        this.uiType.showMode = E_ShowUIMode.HideOther;


    }

    public override void AddMessageListener()
    {
        EventDispatcher.AddListener<int>(E_MessageType.Cure, GameEvent.Instance.CureHp);
        EventDispatcher.AddListener<int>(E_MessageType.Hurt, GameEvent.Instance.HurtHp);
    }

    public override void RemoveMessageListener()
    {
        EventDispatcher.AddListener<int>(E_MessageType.Cure, GameEvent.Instance.CureHp);
        EventDispatcher.AddListener<int>(E_MessageType.Hurt, GameEvent.Instance.HurtHp);
    }

}