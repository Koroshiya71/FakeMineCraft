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
            CheckUI();
        }
        lastClickPos = new Vector3(GameTool.GetFloat("LastPos.X"), GameTool.GetFloat("LastPos.Y"),
            GameTool.GetFloat("LastPos.Z"));
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
        isInGame = true;

        ccamera = GameObject.Find("Main Camera");
        cameraAnim = ccamera.GetComponent<Animator>();
        player = GameObject.Find("Player").transform;   
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
                EventDispatcher.TriggerEvent(E_MessageType.CloseGameUI);
                hasShowMenu = false;

            }
        }
        if (Input.GetKeyDown(KeyCode.G) && gTime >= .2f)
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
