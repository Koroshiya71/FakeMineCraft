using UnityEngine;
using System.Collections;
using GameCore;
using UnityEngine.UI;
public class MainUI : BaseUI {

    private Button btn_EnterGameScene;
    protected override void InitDataOnAwake()
    {
        base.InitDataOnAwake();
        this.uiType.showMode = E_ShowUIMode.HideOther;
        this.uiId = E_UiId.MainUI;
    }

    protected override void InitUiOnAwake()
    {
        base.InitUiOnAwake();
        btn_EnterGameScene = GameTool.GetTheChildComponent<Button>(this.gameObject, "Btn_EnterGameScene");
        btn_EnterGameScene.onClick.AddListener(ToGameScene);

    }

    private void ToGameScene()
    {
        SceneController.Instance.LoadSceneAsync("GameScene", delegate
        {
            UIManager.Instance.ShowUI(E_UiId.GameMainUI);
            UIManager.Instance.ShowUI(E_UiId.WeatherStatusUI);

            UIManager.Instance.ShowUI(E_UiId.PackUI);
            UIManager.Instance.HideSingleUI(E_UiId.PackUI);
            GameSceneManager.Instance.StartGame();
        });
    }
}
