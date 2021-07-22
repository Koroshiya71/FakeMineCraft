using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

//消息类型
public enum E_MessageType
{
    //SellGoods,
    //GoodsBeClick,
    //TestMsg,
    EnterGameScene,
    Cure,
    Hurt,
    Select,
    Add,
    OpenGameUI,
    CloseGameUI
}
//工具类型
public enum E_ToolType
{
    Hand
}
//方块类型
public enum E_BlockType
{
    Null,
    Dirt,
    Grass,
    CobbleStone,
    MossyCobbleStone,
    StoneTiles,
    Wood,
    Door,
    TallGrass,
    Leaves
}
//天空盒类型
public enum E_SkyBoxType
{
    Day,
    Dusk,
    Night,
    Dawn
}
//物品的分类
//public enum E_GoodsType
//{
//    Default,//全部
//    Equipment,//装备
//    Potions,//药水
//    Rune,//符文
//    Material//材料
//}
//窗体的显示方式
public enum E_ShowUIMode
{
    //界面显示出来的时候,不需要去隐藏其他窗体(InforUI)
    DoNothing,
    //界面显示出来的时候,需要去隐藏其他窗体(但是不隐藏保持在最前方的窗体)
    HideOther,
    //界面显示出来的时候,需要去隐藏所有的窗体
    HideAll
}
//窗体的层级类型(父节点的类型)
public enum E_UIRootType
{
    KeepAbove,//保持在前方的窗体(DoNothing)
    Normal//普通窗体(1、HideOther 2、HideAll)
}
//窗体显示出来时是否播放音效
public enum E_UIPlayAudio
{
    Play,
    NoPlay
}
//窗体的ID
public enum E_UiId
{
    NullUI,
    MainUI,
    LoadingUI,
    GameMainUI,
    PackUI,
    InGameMenuUI
}
public class GameDefine
{
    
    public static Dictionary<E_UiId, string> dicPath = new Dictionary<E_UiId, string>()
    {
        { E_UiId.MainUI,"UIPrefab/"+"MainUI"},
        { E_UiId.LoadingUI,"UIPrefab/"+"LoadingUI"},
        { E_UiId.GameMainUI,"UIPrefab/"+"GameMainUI"},
        { E_UiId.PackUI,"UIPrefab/"+"PackUI"},
        { E_UiId.InGameMenuUI,"UIPrefab/"+"InGameMenuUI"},

    };

    //天空盒子路径
    public static Dictionary<E_SkyBoxType, string> skyBoxPath = new Dictionary<E_SkyBoxType, string>()
    {
        {E_SkyBoxType.Day,"Skyboxes/"+"Sky/"+"day"},
        {E_SkyBoxType.Dusk,"Skyboxes/"+"Skybox11/"+"Skybox11"},
        {E_SkyBoxType.Night,"Skyboxes/"+"MoonBox/"+"MoonShine Skybox"},
        {E_SkyBoxType.Dawn,"Skyboxes/"+"Skybox09/"+"Skybox9"}


    };

    //不同方块耐久度字典
    public static Dictionary<E_BlockType, float> blockDurabilityDic = new Dictionary<E_BlockType, float>()
    {
        {E_BlockType.Dirt,30},
        {E_BlockType.Grass,20},

    };
    public static Type GetUIScriptType(E_UiId uiId)
    {
        Type scriptType = null;
        switch (uiId)
        {
            case E_UiId.NullUI:
                GameDebuger.Log("自动添加脚本的时候,传入的窗体id为NullUI");
                break;

            case E_UiId.MainUI:
                scriptType = typeof(MainUI);
                break;

            case E_UiId.GameMainUI:
                scriptType = typeof(GameMainUI);
                break;
            case E_UiId.PackUI:
                scriptType = typeof(PackUI);
                break;

            case E_UiId.LoadingUI:
                scriptType = typeof(LoadingUI);
                break;
            case E_UiId.InGameMenuUI:
                scriptType = typeof(InGameMenuUI);
                break;

            default:
                break;
        }
        return scriptType;
    }
}
