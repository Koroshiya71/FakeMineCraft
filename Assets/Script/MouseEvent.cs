using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using GameCore;

public class MouseEvent : UnitySingleton<MouseEvent>
{
    //拖拽物体的Rect组件
    Rect rt;
    //Rect的Size
    float[] rtSize = { -1, -1 };
    //起始位置
    Vector2 startPos;
    //是否在拖拽
    private bool isMouseDown = false;

    public bool IsMouseDown
    {
        get
        {
            return isMouseDown;
        }
    }

    //格子下标
    int cutIndex;
    //拖拽的物体
    Transform goTransform;

    int[] putThingNum = { 0, 0 };

    void Update()
    {
        Vector2 mousepos = Input.mousePosition;

        if (!isMouseDown)
            OnMouseEnter(mousepos.x, mousepos.y);

        if (cutIndex != -1)
            OnMouseDown(mousepos.x, mousepos.y);

        OnMouseUp(mousepos.x, mousepos.y);
        MouseMove(mousepos.x, mousepos.y);

        AllCompose.Instance.ComposeStuff(BagData.Instance.BagCuts[40]);
    }

    void OnMouseEnter(float mouseX, float mouseY)
    {
        if (EventSystem.current.IsPointerOverGameObject())
        {
            for (var i = 0; i < BagData.Instance.BagCuts.Count; i++)
            {
                Transform go = BagData.Instance.BagCuts[i].transform;

                if (go.transform.childCount != 0)
                {
                    rt = GameTool.GetTheChildComponent<RectTransform>(go.gameObject, "CutThing(Clone)").rect;

                    rtSize[0] = rt.width / 3f;
                    rtSize[1] = rt.height / 3f;

                    //如果鼠标指着在合适的格子上
                    if (mouseX > go.position.x - rtSize[0] && mouseX < go.position.x + rtSize[0] &&
                        mouseY > go.position.y - rtSize[1] && mouseY < go.position.y + rtSize[1] && BagData.Instance.IdList[i] != -1)
                    {
                        cutIndex = i;

                        GameObject txGo = GameTool.FindTheChild(GameObject.Find("PackUI(Clone)"), "ThingName(Clone)(Clone)").gameObject;
                        txGo.transform.position = Vector2.zero;
                        txGo.transform.position = new Vector2(
                            BagData.Instance.BagCuts[i].transform.position.x, BagData.Instance.BagCuts[i].transform.position.y);

                        Text tx = GameTool.GetTheChildComponent<Text>(txGo, "ThingNameText");
                        tx.text = DataController.Instance.ReadCfg("Name", BagData.Instance.IdList[i], DataController.Instance.dicStuff);

                        txGo.SetActive(true);
                        break;
                    }
                    //否则
                    else
                    {
                        GameObject txGo = GameTool.FindTheChild(GameObject.Find("PackUI(Clone)"), "ThingName(Clone)(Clone)").gameObject;
                        txGo.SetActive(false);
                    }
                }
                else
                    continue;
            }

        }


    }
    void OnMouseDown(float mouseX, float mouseY)
    {
        if (Input.GetMouseButtonDown(0))
        {
            isMouseDown = true;

            if (EventSystem.current.IsPointerOverGameObject())
            {
                if (cutIndex == 40)
                {
                    EventDispatcher.TriggerEvent(E_MessageType.Compose);
                    GameDebuger.Log("合成消息发送");
                }


                Transform go = BagData.Instance.BagCuts[cutIndex].transform;

                if (go.transform.childCount != 0)
                {
                    if (mouseX > go.position.x - rtSize[0] && mouseX < go.position.x + rtSize[0] &&
                        mouseY > go.position.y - rtSize[1] && mouseY < go.position.y + rtSize[1])
                    {
                        startPos = go.position;

                        GameObject goPar = GameObject.Find("PackUI(Clone)");

                        Transform cgo = BagData.Instance.BagCuts[cutIndex].transform;
                        GameObject txGo = GameTool.FindTheChild(goPar, "ThingName(Clone)(Clone)").gameObject;
                        txGo.SetActive(false);

                        goTransform = GameTool.FindTheChild(cgo.gameObject, "CutThing(Clone)");
                        putThingNum[0] = int.Parse(GameTool.GetTheChildComponent<Text>(goTransform.gameObject, "ThingNum").text);
                        GameTool.AddChildToParent(goPar.transform, goTransform);
                        goTransform.name = "TouchThing";
                    }
                    else
                        cutIndex = -1;
                }
            }

        }
    }

    void OnMouseUp(float mouseX, float mouseY)
    {
        if (Input.GetMouseButtonUp(0))
        {
            if (BagData.Instance.BagCuts[40].transform.childCount != 0)
                Destroy(GameTool.FindTheChild(BagData.Instance.BagCuts[40], "CutThing(Clone)").gameObject);

            isMouseDown = false;

            if (cutIndex != -1)
            {
                for (var i = 0; i < BagData.Instance.BagCuts.Count; i++)
                {
                    if (i == 40)
                        continue;

                    Transform go = BagData.Instance.BagCuts[i].transform;

                    if (go.childCount != 0)
                        putThingNum[1] = int.Parse(GameTool.GetTheChildComponent<Text>(BagData.Instance.BagCuts[i], "ThingNum").text);

                    if (mouseX > go.position.x - rtSize[0] && mouseX < go.position.x + rtSize[0] &&
                        mouseY > go.position.y - rtSize[1] && mouseY < go.position.y + rtSize[1])
                    {
                        GameTool.AddChildToParent(go.transform, goTransform);

                        int temp;

                        if (i == cutIndex)
                        {
                            goTransform.name = "CutThing(Clone)";
                            break;
                        }
                        else
                        {
                            if (BagData.Instance.IdList[i] == BagData.Instance.IdList[cutIndex] &&
                                (putThingNum[0] + putThingNum[1]) >
                                    int.Parse(DataController.Instance.ReadCfg("Max", BagData.Instance.IdList[i], DataController.Instance.dicStuff)))
                            {
                                BagData.Instance.IdList[cutIndex] = -1;
                                Destroy(GameTool.FindTheChild(BagData.Instance.BagCuts[i], "TouchThing").gameObject);
                                AllCompose.Instance.Compos(i, AllCompose.Instance.ComposeThing, go.gameObject, putThingNum[0]);

                                AllCompose.Instance.UpdateBagCutAndToolbarCut();
                                break;
                            }
                            else
                            {
                                temp = BagData.Instance.IdList[cutIndex];
                                BagData.Instance.IdList[cutIndex] = BagData.Instance.IdList[i];
                                BagData.Instance.IdList[i] = temp;

                                if (go.childCount == 2)
                                {
                                    Transform newGo = GameTool.FindTheChild(go.gameObject, "CutThing(Clone)");
                                    GameTool.AddChildToParent(go, goTransform.transform);
                                    goTransform.position = newGo.position;
                                    GameTool.AddChildToParent(BagData.Instance.BagCuts[cutIndex].transform, newGo);
                                    newGo.position = startPos;
                                }
                                
                                goTransform.name = "CutThing(Clone)";

                                AllCompose.Instance.UpdateBagCutAndToolbarCut();
                                break;
                            }
                        }

                    }
                    else if (goTransform != null)
                    {
                        goTransform.position = startPos;
                        GameTool.AddChildToParent(BagData.Instance.BagCuts[cutIndex].transform, goTransform);
                        goTransform.name = "CutThing(Clone)";
                    }
                }
            }
        }


    }

    void MouseMove(float mouseX, float mouseY)
    {
        if (isMouseDown && cutIndex != -1 && BagData.Instance.IdList[cutIndex] != -1)
        {
            goTransform.position = new Vector2(mouseX, mouseY);

        }
    }
}