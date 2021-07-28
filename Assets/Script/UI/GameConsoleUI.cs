using System;
using System.Collections;
using GameCore;
using Uniblocks;
using UnityEngine.UI;
using Random = UnityEngine.Random;

public class GameConsoleUI : BaseUI
{
    private InputField input_GetBlock;
    private Button btn_Submit;
    private Button btn_Close;
    private Button btn_RandomWeather;
    private Button btn_SetTimeNoon;
    private Button btn_SetTimeNight;
    private Toggle tog_LockTime;

    protected override void InitUiOnAwake()
    {
        base.InitUiOnAwake();
        input_GetBlock = GameTool.GetTheChildComponent<InputField>(gameObject, "Input_GetBlock");
        btn_Submit= GameTool.GetTheChildComponent<Button>(gameObject, "Btn_Submit");
        btn_Submit.onClick.AddListener(GetBlock);
        btn_Close = GameTool.GetTheChildComponent<Button>(gameObject, "Btn_Close");
        btn_Close.onClick.AddListener(ConsoleClose);

        btn_RandomWeather = GameTool.AddTheChildComponent<Button>(gameObject, "Btn_RandomWeather");
        btn_RandomWeather.onClick.AddListener(RandomWeather);

        btn_SetTimeNoon = GameTool.AddTheChildComponent<Button>(gameObject, "Btn_SetTimeNoon");
        btn_SetTimeNoon.onClick.AddListener(SetTimeToNoon);

        btn_SetTimeNight = GameTool.AddTheChildComponent<Button>(gameObject, "Btn_SetTimeNight");
        btn_SetTimeNight.onClick.AddListener(SetTimeToNight);

        tog_LockTime = GameTool.GetTheChildComponent<Toggle>(gameObject, "Tog_LockTime");
        tog_LockTime.isOn = false;
        tog_LockTime.onValueChanged.AddListener(ChangeLockTime);
    }

    private void RandomWeather()
    {
        UniStormWeatherSystem_C.Instance.weatherForecaster = Random.Range(1, 13);

    }

    private void ChangeLockTime(bool isOn)
    {
        if (isOn)
        {
            UniStormWeatherSystem_C.Instance.timeStopped = true;
        }
        else
        {
            UniStormWeatherSystem_C.Instance.timeStopped = false;

        }
    }

    private void SetTimeToNoon()
    {
        UniStormWeatherSystem_C.Instance.Hour = 12;
        UniStormWeatherSystem_C.Instance.startTime = 0.5f;
        UniStormWeatherSystem_C.Instance.minuteCounterCalculator = 0;


    }

    private void SetTimeToNight()
    {
        UniStormWeatherSystem_C.Instance.Hour = 0;
        UniStormWeatherSystem_C.Instance.startTime = 1.0f;

        UniStormWeatherSystem_C.Instance.minuteCounterCalculator = 0;
    }

    protected override void InitDataOnAwake()
    {
        base.InitDataOnAwake();
        this.uiId = E_UiId.GameConsoleUI;
        this.uiType.showMode = E_ShowUIMode.DoNothing;
    }

    private void GetBlock()
    {

        int id = int.Parse(input_GetBlock.text);
        if (id<=20||id==70)
        {
            //HoldBlockManager.HeldBlock = (ushort)id;
            AllCompose.Instance.ControllerGet(id);
        }
    }

    private void ConsoleClose()
    {
        UIManager.Instance.HideSingleUI(E_UiId.GameConsoleUI);
        EventDispatcher.TriggerEvent(E_MessageType.CloseGameUI);
    }
}
