using UnityEngine;
using System.Collections;
using GameCore;
using UnityEngine.UI;
public class WeatherStatusUI : BaseUI
{
    
    private Text text_WeatherStatus;
    protected override void InitUiOnAwake()
    {
        base.InitUiOnAwake();
        text_WeatherStatus = GameTool.GetTheChildComponent<Text>(gameObject, "Text_WeatherStatus");
        EventDispatcher.AddListener(E_MessageType.EnterGameScene,InitText);
    }

    private void InitText()
    {
        text_WeatherStatus.text += "\n\n当前天气：" + CheckWeatherName(UniStormWeatherSystem_C.Instance.weatherString);
        EventDispatcher.AddListener(E_MessageType.ChangeWeatherAndTime, delegate
        {
            text_WeatherStatus.text = "当前时间：" + UniStormWeatherSystem_C.Instance.hourCounter + ":" + UniStormWeatherSystem_C.Instance.minuteCounter;
            text_WeatherStatus.text += "\n\n当前天气：" + CheckWeatherName(UniStormWeatherSystem_C.Instance.weatherString);

        });
    }
    protected override void InitDataOnAwake()
    {
        base.InitDataOnAwake();
        this.uiId = E_UiId.WeatherStatusUI;
        this.uiType.showMode = E_ShowUIMode.DoNothing;
    }

    private string CheckWeatherName(string weatherString)
    {
        string newString = "";
        switch (weatherString)
        {
            case "Foggy":
                newString = "大雾";
                break;
            case "Light Rain":
                newString = "小雨";
                break;
            case "Light Snow":
                newString = "小雪";
                break;
            case "Heavy Rain & Thunder Storm":
                newString = "雷暴";
                break;
            case "Heavy Snow":
                newString = "大雪";
                break;
            case "Partly Cloudy":
                newString = "云";
                break;
            case "Mostly Cloudy":
                newString = "多云";
                break;
            case "Mostly Clear":
                newString = "晴天";
                break;
            case "Clear":
                newString = "晴天";
                break;
            case "Lighning Bugs":
                newString = "萤火";
                break;
            case "Heavy Rain (No Thunder)":
                newString = "暴雨";
                break;
            case "Falling Fall Leaves":
                newString = "落叶";
                break;
        }

        return newString;
    }
}
