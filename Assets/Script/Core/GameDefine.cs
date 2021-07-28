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
    CloseGameUI,
    ChangeWeatherAndTime,
    Compose
}
//工具类型

public enum E_ToolType
{
    Hand,
    Sword,
    Axe
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
    Rock,
    Door,
    TallGrass,
    Leaves,
    BlackMetal,
    BlueCloth,
    Glass,
    BlackWall,
    Wood,
    TransParentBlack,
    Gold
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
    InGameMenuUI,
    GameConsoleUI,
    WeatherStatusUI
}
public class GameDefine
{
    //掉落物材质路径
    public static Dictionary<ushort, string> texDicPath = new Dictionary<ushort, string>()
    {
        {1,"Textures/"+"dirt" },
        {2,"Textures/"+"Grass" },
        {3,"Textures/"+"Cobble"},
        {4,"Textures/"+"Mossy" },
        {5,"Textures/"+ "StoneTiles"},
        {6,"Textures/"+"Rock" },
        {7,"Textures/"+"Wood" },
        {8,"Textures/"+"TallGrass" },
        {9,"Textures/"+"Leaves" },
        {10,"Textures/"+"BlackMetal" },
        {11,"Textures/"+"BlueCloth" },
        {12,"Textures/"+"Glass" },
        {13,"Textures/"+"BlackWall" },
        {14,"Textures/"+"Wood" },
        {15,"Textures/"+"TransBlack" },
        {16,"Textures/"+"Gold" },
        {19,"Textures/"+"Candy" },
        {20,"Textures/"+"MilkTea" },

    };


    public static Dictionary<E_UiId, string> dicPath = new Dictionary<E_UiId, string>()
    {
        { E_UiId.MainUI,"UIPrefab/"+"MainUI"},
        { E_UiId.LoadingUI,"UIPrefab/"+"LoadingUI"},
        { E_UiId.GameMainUI,"UIPrefab/"+"GameMainUI"},
        { E_UiId.PackUI,"UIPrefab/"+"PackUI"},
        { E_UiId.InGameMenuUI,"UIPrefab/"+"InGameMenuUI"},
        { E_UiId.GameConsoleUI,"UIPrefab/"+"GameConsoleUI"},
        { E_UiId.WeatherStatusUI,"UIPrefab/"+"WeatherStatusUI"},


    };

    //天空盒子路径
    public static Dictionary<E_SkyBoxType, string> skyBoxPath = new Dictionary<E_SkyBoxType, string>()
    {
        {E_SkyBoxType.Day,"Skyboxes/"+"Sky/"+"day"},
        {E_SkyBoxType.Dusk,"Skyboxes/"+"Skybox11/"+"Skybox11"},
        {E_SkyBoxType.Night,"Skyboxes/"+"MoonBox/"+"MoonShine Skybox"},
        {E_SkyBoxType.Dawn,"Skyboxes/"+"Skybox09/"+"Skybox9"}


    };

    //方块耐久度字典
    public static Dictionary<E_BlockType, float> blockDurabilityDic = new Dictionary<E_BlockType, float>()
    {
        {E_BlockType.Dirt,20},
        {E_BlockType.Grass,30},
        {E_BlockType.CobbleStone,80},
        {E_BlockType.MossyCobbleStone,85},
        {E_BlockType.StoneTiles,75},
        {E_BlockType.Rock,90},
        {E_BlockType.Door,80},
        {E_BlockType.TallGrass,35},
        {E_BlockType.Leaves,10},
        {E_BlockType.BlackMetal,160},
        {E_BlockType.BlueCloth,40},
        {E_BlockType.Glass,35},
        {E_BlockType.BlackWall,100},
        {E_BlockType.Wood,70},
        {E_BlockType.TransParentBlack,5},
        {E_BlockType.Gold,100}

    };

    //不同工具的攻击力字典
    public static Dictionary<E_ToolType, float> toolDamageDic = new Dictionary<E_ToolType, float>()
    {
        {E_ToolType.Hand,10},
        {E_ToolType.Sword,40},
        {E_ToolType.Axe,25}
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
            case E_UiId.GameConsoleUI:
                scriptType = typeof(GameConsoleUI);
                break;
            case E_UiId.WeatherStatusUI:
                scriptType = typeof(WeatherStatusUI);
                break;
            default:
                break;
        }
        return scriptType;
    }
}
