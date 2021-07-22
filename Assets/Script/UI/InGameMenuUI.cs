using UnityEngine;
using System.Collections;
using GameCore;
using UnityEngine.UI;
using Uniblocks;
public class InGameMenuUI : BaseUI {

    private Button btn_ReturnToMain;
    private Button btn_Close;
    private Button btn_Save;


    protected override void InitUiOnAwake()
    {
        base.InitUiOnAwake();
        btn_ReturnToMain = GameTool.GetTheChildComponent<Button>(gameObject, "Btn_ReturnToMain");
        btn_ReturnToMain.onClick.AddListener(ReturnToMain);
        btn_Close = GameTool.GetTheChildComponent<Button>(gameObject, "Btn_Close");
        btn_Close.onClick.AddListener(CloseMenu);
        btn_Save = GameTool.GetTheChildComponent<Button>(gameObject, "Btn_Save");
        btn_Save.onClick.AddListener(SaveWorld);

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
}
