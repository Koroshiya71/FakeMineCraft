using UnityEngine;
using System.Collections;
using GameCore;
using UnityEngine.UI;
using Uniblocks;

public class GameEvent : UnitySingleton<GameEvent>
{
    private bool isShowInputField = false;

    public bool IsShowInputField
    {
        get
        {
            return isShowInputField;
        }
        set
        {
            isShowInputField = value;
        }
    }

    void Update()
    {
        if (!isShowInputField)
        {
            //扣血键
            if (Input.GetKeyDown(KeyCode.J))
            {
                if (Time.time - GameSceneManager.Instance.LastHurtTime >= 1)
                {
                    Change();
                    GameSceneManager.Instance.LastHurtTime = Time.time;

                }
                GameDebuger.Log("按键J");
            }
            //回血键
            if (Input.GetKeyDown(KeyCode.K))
            {
                Change1();
                GameDebuger.Log("按键K");
            }
            //输入栏键
            if (Input.GetKeyDown(KeyCode.T))
            {
                isShowInputField = !isShowInputField;
                GameObject go = GameTool.FindTheChild(GameObject.Find("GameMainUI(Clone)"), "InputField").gameObject;
                go.SetActive(isShowInputField);

                if (isShowInputField)
                    go.GetComponent<InputField>().ActivateInputField();

                GameDebuger.Log("按键T");
            }
        }
        else
        {
            if(Input.GetKeyDown(KeyCode.Escape) || Input.GetKeyDown(KeyCode.KeypadEnter))
            {
                isShowInputField = !isShowInputField;
                GameObject go = GameTool.FindTheChild(GameObject.Find("GameMainUI(Clone)"), "InputField").gameObject;
                go.SetActive(isShowInputField);
                GameDebuger.Log("按键Esc / Enter");
            }
        }


    }

    private void OnGUI()
    {
        if (Input.anyKeyDown)
        {
            Event e = Event.current;
            //GameDebuger.Log(e);
            if (e.character >= 49 && e.character <= 57)
            {
                BagData.Instance.Selectindex = e.character - 49;
                BagData.Instance.SelectCut.position = new Vector2(BagData.Instance.CutsArr[BagData.Instance.Selectindex].transform.position.x, BagData.Instance.SelectCut.position.y);

                GameDebuger.Log(BagData.Instance.IdList[BagData.Instance.Selectindex]);
                if (BagData.Instance.IdList[BagData.Instance.Selectindex] != -1)
                    HoldBlockManager.HeldBlock = (ushort)BagData.Instance.IdList[BagData.Instance.Selectindex];
                else
                    HoldBlockManager.HeldBlock = 0;
                GameDebuger.Log(HoldBlockManager.HeldBlock);
            }
        }
        
    }


    #region UI
    public void ChangeValue(string str)
    {

    }

    public void EndValue(string str)
    {
        string[] cstr = str.Split(' ');

        foreach (var item in cstr)
        {
            GameDebuger.Log(item);
        }
    }

    private void ChangeSelect(int index)
    {
        GameDebuger.Log(index);
        BagData.Instance.SelectCut.position = BagData.Instance.CutsArr[index].transform.position;
    }
    #endregion

    #region 玩家
    public void HurtHp(int hurt)
    {
        if (Time.time - GameSceneManager.Instance.LastHurtTime >= 1)
        {
            GameSceneManager.Instance.LastHurtTime = Time.time;
        }
        else
        {
            return;
        }

        if (PlayerData.Instance.HP == 0)
        {
            GameDebuger.Log("生命值为0~~~Game Over!!~~");
            return;
        }

        for (var i = 0; i < hurt; i++)
        {
            int hpnow = PlayerData.Instance.HP - 1;
            PlayerData.Instance.EditorHp(hpnow);

            PlayerData.Instance.HpList[hpnow].SetActive(false);
            GameDebuger.Log(hpnow);
            GameDebuger.Log("扣血     -" + hurt.ToString());
        }
    }

    public void CureHp(int cure)
    {
        if (PlayerData.Instance.HP == 20)
        {
            GameDebuger.Log("满  血~~~哦！！耶！！~~");
            return;
        }

        for (var i = 0; i < cure; i++)
        {
            int hpnow = PlayerData.Instance.HP;
            PlayerData.Instance.EditorHp(hpnow + 1);
            PlayerData.Instance.HpList[hpnow].SetActive(true);
            GameDebuger.Log(hpnow);
            GameDebuger.Log("回血     +" + cure.ToString());
        }
    }
    #endregion

    #region 消息测试
    public void Change()
    {
        EventDispatcher.TriggerEvent<int>(E_MessageType.Hurt, 5);
    }
    public void Change1()
    {
        EventDispatcher.TriggerEvent<int>(E_MessageType.Cure, 1);
    }

    #endregion

    #region 消息调用的方法
    public void DestroyStuff()
    {
        GameDebuger.Log("合成");
        
        for(var i =0;i<4;i++)
        {
            if (BagData.Instance.BagCuts[36 + i].transform.childCount != 0)
                AllCompose.Instance.Compos(36 + i, BagData.Instance.IdList[36 + i], BagData.Instance.BagCuts[36 + i], 1, true);
        }
    }
    #endregion
}
