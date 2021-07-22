using UnityEngine;
using System.Collections;
using GameCore;
using System.Collections.Generic;

public class PackUI : BaseUI
{
    private List<Transform> cpCuts = new List<Transform>();

    protected override void InitDataOnAwake()
    {
        base.InitDataOnAwake();
    }

    protected override void InitUiOnAwake()
    {
        base.InitUiOnAwake();
        this.uiId = E_UiId.PackUI;
        this.uiType.showMode = E_ShowUIMode.DoNothing;

        int cpRows = 0, cpCols = 0;
        for (var i = 0; i < 5; i++)
        {
            GameObject go = Instantiate(Resources.Load("Prefab/Cut") as GameObject);
            GameTool.AddChildToParent(GameTool.FindTheChild(gameObject, "Compose"), go.transform);

            if (i < 4)
            {
                if (cpRows == 2)
                {
                    cpCols++;
                    cpRows = 0;
                }

                go.transform.localPosition = new Vector2(84 + 82 * cpRows, 258 - 82 * cpCols);
                cpRows++;
            }
            else
                go.transform.localPosition = new Vector2(337, 218);

            cpCuts.Add(go.transform);
        }

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

            AllCompose.Instance.BagCuts.Add(go);
        }
    }
	

}
