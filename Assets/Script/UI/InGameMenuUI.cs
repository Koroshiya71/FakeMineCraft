using UnityEngine;
using System.Collections;
using GameCore;
using UnityEngine.UI;
using Uniblocks;
public class InGameMenuUI : BaseUI {

    private Button btn_Close;
    private Button btn_Save;
    private Button btn_Console;

    protected override void InitUiOnAwake()
    {
        base.InitUiOnAwake();
        

        btn_Close = GameTool.GetTheChildComponent<Button>(gameObject, "Btn_Close");
        btn_Close.onClick.AddListener(CloseMenu);

        btn_Save = GameTool.GetTheChildComponent<Button>(gameObject, "Btn_Save");
        btn_Save.onClick.AddListener(SaveWorld);

        btn_Console = GameTool.GetTheChildComponent<Button>(gameObject, "Btn_Console");
        btn_Console.onClick.AddListener(ToConsole);

    }
    protected override void InitDataOnAwake()
    {
        base.InitDataOnAwake();
        this.uiId = E_UiId.InGameMenuUI;
        this.uiType.showMode = E_ShowUIMode.DoNothing;
    }

    private void ReturnToMain()
    {
        SceneController.Instance.LoadSceneAsync("MainScene", delegate
         {
             UIManager.Instance.ShowUI(E_UiId.MainUI);
         });
    }

    private void CloseMenu()
    {
        UIManager.Instance.HideSingleUI(E_UiId.InGameMenuUI);
        EventDispatcher.TriggerEvent(E_MessageType.CloseGameUI);
    }

    private void SaveWorld()
    {
        Engine.SaveWorldInstant();
    }

    private void ToConsole()
    {
        UIManager.Instance.ShowUI(E_UiId.GameConsoleUI);
        UIManager.Instance.HideSingleUI(E_UiId.InGameMenuUI);
    }
}
