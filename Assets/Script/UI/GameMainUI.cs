using UnityEngine;
using System.Collections;
using GameCore;
using UnityEngine.UI;
using System.Collections.Generic;

public class GameMainUI : BaseUI
{
    private List<GameObject> hpLife = new List<GameObject>();

    //选择框
    private Transform selectCut;
    //格子下标
    private int selectindex;
    

    private bool isCanHurt = true;

    public bool IsCanHurt
    {
        get
        {
            return isCanHurt;
        }
    }

    protected override void InitUiOnAwake()
    {
        base.InitUiOnAwake();
    }

    protected override void InitDataOnAwake()
    {
        base.InitDataOnAwake();
        this.uiId = E_UiId.GameMainUI;
        this.uiType.showMode = E_ShowUIMode.HideOther;

        //造血
        for (var i = 0; i < 10; i++)
        {
            GameObject kAll = Instantiate(Resources.Load("Prefab/Xin") as GameObject);
            GameTool.AddChildToParent(GameTool.FindTheChild(gameObject, "AllHp"), kAll.transform);
            kAll.transform.localPosition = new Vector2(i * 25.5f, 0);

            hpLife.Add(GameTool.FindTheChild(kAll, "sxinL").gameObject);
            hpLife.Add(GameTool.FindTheChild(kAll, "sxinR").gameObject);
        }

        for (var i = 0; i < 20; i++)
        {
            GameDebuger.Log(hpLife[i]);
        }

        //造下方快捷栏格子
        GameObject gp = GameTool.FindTheChild(gameObject, "ShortCuts").gameObject;
        for (var i=0;i<9; i++)
        {
            AllCompose.Instance.CutsArr.Add(GameTool.GetTheChildComponent<Transform>(gp, "Se" + i.ToString()).gameObject);

        }
        //造选择框
        selectCut = GameTool.FindTheChild(gameObject, "SelectCut");
        GameDebuger.Log(selectCut);
        selectCut.position = new Vector2(AllCompose.Instance.CutsArr[0].transform.position.x, selectCut.position.y);

    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.J))
        {
            Change();
            GameDebuger.Log("按键J");
        }
        if (Input.GetKeyDown(KeyCode.K))
        {
            Change1();
            GameDebuger.Log("按键K");
        }


        if (GameSceneManager.Instance.CameraAnim.GetCurrentAnimatorStateInfo(0).normalizedTime >= 1.0f && GameSceneManager.Instance.CameraAnim != null)
            isCanHurt = true;
    }

    private void OnGUI()
    {
        if (Input.anyKeyDown)
        {
            Event e = Event.current;
            //GameDebuger.Log(e);
            if (e.character >= 49 && e.character <= 57)
            {
                selectindex = e.character - 49;
                selectCut.position = new Vector2(AllCompose.Instance.CutsArr[selectindex].transform.position.x, selectCut.position.y);
                GameDebuger.Log(selectindex);
            }

            
        }
    }

    public override void AddMessageListener()
    {
        EventDispatcher.AddListener<int>(E_MessageType.Cure, CureHp);
        EventDispatcher.AddListener<int>(E_MessageType.Hurt, HurtHp);
    }

    public override void RemoveMessageListener()
    {
        EventDispatcher.AddListener<int>(E_MessageType.Cure, CureHp);
        EventDispatcher.AddListener<int>(E_MessageType.Hurt, HurtHp);
    }

    private void HurtHp(int hurt)
    {
        if (PlayerData.Instance.HP == 0)
        {
            GameDebuger.Log("生命值为0~~~Game Over!!~~");
            return;
        }


        if (isCanHurt)
        {
            isCanHurt = false;
            for (var i = 0; i < hurt; i++)
            {
                int hpnow = PlayerData.Instance.HP - 1;
                PlayerData.Instance.EditorHp(hpnow);
                GameSceneManager.Instance.CameraAnim.Play("CameraHurt");
                hpLife[hpnow].SetActive(false);
                GameDebuger.Log(hpnow);
                GameDebuger.Log("扣血     -" + hurt.ToString());
            }
        }
    }

    private void CureHp(int cure)
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
            hpLife[hpnow].SetActive(true);
            GameDebuger.Log(hpnow);
            GameDebuger.Log("回血     +" + cure.ToString());
        }
    }

    #region 消息测试
    private void Change()
    {
        EventDispatcher.TriggerEvent<int>(E_MessageType.Hurt, 5);
    }
    private void Change1()
    {
        EventDispatcher.TriggerEvent<int>(E_MessageType.Cure, 1);
    }
    #endregion

    private void ChangeSelect(int index)
    {
        GameDebuger.Log(index);
        selectCut.position = AllCompose.Instance.CutsArr[index].transform.position;
    }

}
