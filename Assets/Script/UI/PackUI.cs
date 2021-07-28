using UnityEngine;
using System.Collections;
using GameCore;
using System.Collections.Generic;

public class PackUI : BaseUI
{
    protected override void InitDataOnAwake()
    {
        base.InitDataOnAwake();

        BagData.Instance.InitBagData();
    }

    protected override void InitUiOnAwake()
    {
        base.InitUiOnAwake();
        this.uiId = E_UiId.PackUI;
        this.uiType.showMode = E_ShowUIMode.DoNothing;

        //背包内36个格子
        int index = 0, cols = 0;
        for (var i = 0; i < 36; i++)
        {
            GameObject go = Instantiate(Resources.Load("Prefab/Cut") as GameObject);
            GameTool.AddChildToParent(GameTool.FindTheChild(gameObject, "Cuts"), go.transform);

            if (i < 9)
            {
                go.transform.localPosition = new Vector2(go.transform.localPosition.x + 82 * i, 0);
            }
            else
            {
                if (index == 9)
                {
                    index = 0;
                    cols++;
                }

                go.transform.localPosition = new Vector2(go.transform.localPosition.x + 82 * (i - 9 * (cols + 1)), 262 - 82 * cols);
                index++;
            }

            BagData.Instance.BagCuts.Add(go);
        }
        //背包上面的2x2合成格子
        int cpRows = 0, cpCols = 0;
        for (var i = 0; i < 4; i++)
        {
            GameObject go = Instantiate(Resources.Load("Prefab/Cut") as GameObject);
            GameTool.AddChildToParent(GameTool.FindTheChild(gameObject, "Cuts"), go.transform);

            if (cpRows == 2)
            {
                cpCols++;
                cpRows = 0;
            }

            go.transform.localPosition = new Vector2(413.2f + 82 * cpRows, 567.3f - 82 * cpCols);
            cpRows++;

            BagData.Instance.BagCuts.Add(go);
        }

        GameObject compossCut = Instantiate(Resources.Load("Prefab/Cut") as GameObject);
        GameTool.AddChildToParent(GameTool.FindTheChild(gameObject, "Cuts"), compossCut.transform);
        compossCut.transform.localPosition = new Vector2(665.3f, 526);
        compossCut.name = "CompossCut";
        BagData.Instance.BagCuts.Add(compossCut);

        BagData.Instance.BagCutNum = BagData.Instance.BagCuts.Count;

        if (!GameTool.FindTheChild(GameObject.Find("PackUI(Clone)"), "ThingName(Clone)(Clone)"))
        {
            GameObject go = Instantiate(ResourcesManager.Instance.LoadAsset("Prefab/ThingName"));
            GameTool.AddChildToParent(GameObject.Find("PackUI(Clone)").transform, go.transform);
        }

    }

    public override void AddMessageListener()
    {
        EventDispatcher.AddListener(E_MessageType.Compose, GameEvent.Instance.DestroyStuff);
    }

    public override void RemoveMessageListener()
    {
        EventDispatcher.AddListener(E_MessageType.Compose, GameEvent.Instance.DestroyStuff);
    }


}
