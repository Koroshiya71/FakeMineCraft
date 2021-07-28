using System;
using UnityEngine;
using System.Collections;
using GameCore;
using UnityEngine.UI;
public class GameSceneManager : UnitySingleton<GameSceneManager>
{
    public Transform player;
    public Vector3 lastClickPos;
    private GameObject ccamera;
    private Animator cameraAnim;
    private E_ToolType activeToolType;
    public Vector3 currentPos;
    private GameObject swordObj;
    private GameObject axeObj;
    private GameObject torchObj;

    private Animator swordAnimator;
    private Animator axeAnimator;

    public bool hasTorch;
    public bool hasSword;
    public bool hasAxe;

    private bool useTorch;
    public E_ToolType ActiveToolType
    {
        get { return activeToolType; }
    }

    public float LastHurtTime
    {
        get;
        set;
    }

    private bool isInGame;

    //是否开背包
    private bool isOpenBag = false;
    //允许背包按键再次按下的缓冲时间
    private float gTime;

    public bool IsInGame
    {
        get { return isInGame; }
    }

    private bool hasShowMenu=false;

    public bool IsOpenBag
    {
        get { return isOpenBag; }
    }

    public bool HasShowMenu
    {
        get { return hasShowMenu; }
    }

    public Animator CameraAnim
    {
        get
        {
            return cameraAnim;
        }
    }

	private void Update()
    {
        if (isInGame)
        {
            currentPos = GameObject.Find("selected block graphics").transform.position;

            CheckUI();
        }
        lastClickPos = new Vector3(GameTool.GetFloat("LastPos.X"), GameTool.GetFloat("LastPos.Y"),
            GameTool.GetFloat("LastPos.Z"));

        if (!GameEvent.Instance.IsShowInputField)
        {
            if (Input.GetKeyDown(KeyCode.E))
            {
                if (activeToolType == E_ToolType.Hand)
                {
                    if (hasSword)
                    {
                        activeToolType = E_ToolType.Sword;
                        swordObj.SetActive(true);
                    }
                    else if (hasAxe)
                    {
                        activeToolType = E_ToolType.Axe;
                        swordObj.SetActive(false);
                        axeObj.SetActive(true);
                    }
                }
                else if (activeToolType == E_ToolType.Sword)
                {
                    if (hasAxe)
                    {
                        activeToolType = E_ToolType.Axe;
                        swordObj.SetActive(false);
                        axeObj.SetActive(true);
                    }
                    else
                    {
                        activeToolType = E_ToolType.Hand;
                        axeObj.SetActive(false);
                        swordObj.SetActive(false);

                    }
                }
                else
                {

                    activeToolType = E_ToolType.Hand;
                    axeObj.SetActive(false);
                }
            }

            if (Input.GetKeyDown(KeyCode.F))
            {
                if (!hasTorch)
                {
                    return;
                }
                if (!useTorch)
                {
                    useTorch = true;
                    torchObj.SetActive(true);
                }
                else
                {
                    useTorch = false;
                    torchObj.SetActive(false);

                }
            }
        }
    }

    private void OnGUI()
    {
        if (Input.anyKeyDown)
        {
            Event e = Event.current;

            if (e.character == 73)
                ;

            if (e.character == 79)
                ;

            if (e.character == 80)
                ;
        }
    }

    public void StartGame()
    {
        currentPos = GameObject.Find("selected block graphics").transform.position;
        isInGame = true;
        PlayerData.Instance.InitHunImg();
        //PlayerData.Instance.EditorEnt(20);
        activeToolType = E_ToolType.Hand;
        ccamera = GameObject.Find("Camera");
        cameraAnim = ccamera.GetComponent<Animator>();
        player = GameObject.Find("Player").transform;
        swordObj=GameObject.Find("Sword");

        swordAnimator = swordObj.GetComponent<Animator>();
        swordObj.SetActive(false);

        axeObj = GameObject.Find("Axe");
        axeAnimator = axeObj.GetComponent<Animator>();
        axeObj.SetActive(false);

        torchObj=GameObject.Find("Torch");
        torchObj.SetActive(false);

        EventDispatcher.TriggerEvent(E_MessageType.EnterGameScene);

        EventDispatcher.AddListener(E_MessageType.CloseGameUI, delegate
        {
            hasShowMenu = false;
        });
        LoadPlayerPos();
    }
    private void CheckUI()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (isOpenBag)
            {
                return;
            }
            if (!hasShowMenu)
            {
                UIManager.Instance.ShowUI(E_UiId.InGameMenuUI);
                EventDispatcher.TriggerEvent(E_MessageType.OpenGameUI);
                hasShowMenu = true;
            }
            else
            {
                UIManager.Instance.HideSingleUI(E_UiId.InGameMenuUI);
                UIManager.Instance.HideSingleUI(E_UiId.GameConsoleUI);

                EventDispatcher.TriggerEvent(E_MessageType.CloseGameUI);
                hasShowMenu = false;

            }
        }
        if (Input.GetKeyDown(KeyCode.G) && gTime >= .2f && !GameEvent.Instance.IsShowInputField)
        {
            gTime = 0;
            ShowPackUI();
        }
        else
        {
            gTime += Time.deltaTime;

        }
    }

    public void LoadPlayerPos()
    {
        if (GameTool.HasKey("PlayerPos.X"))
        {
            Vector3 loadedPos=new Vector3(
                GameTool.GetFloat("PlayerPos.X"),
                GameTool.GetFloat("PlayerPos.Y"),
                GameTool.GetFloat("PlayerPos.Z"));

            player.position = loadedPos;
        }

    }

    public void PlayAttackAnim()
    {
        switch (activeToolType)
        {
            case E_ToolType.Sword:
                swordAnimator.Play("SwordAttack");
                break;
            case E_ToolType.Axe:
                axeAnimator.Play("AxeAttack");
                break;
        }
    }
    private void ShowPackUI()
    {
        if (hasShowMenu)
        {
            return;
        }
        isOpenBag = !isOpenBag;
        GameDebuger.Log("按键G！！~~" + "         " + isOpenBag);

        if (isOpenBag)
        {
            UIManager.Instance.ShowUI(E_UiId.PackUI, false);
            EventDispatcher.TriggerEvent(E_MessageType.OpenGameUI);
        }
        else
        {
            UIManager.Instance.HideSingleUI(E_UiId.PackUI);
            EventDispatcher.TriggerEvent(E_MessageType.CloseGameUI);
        }
    }
}
