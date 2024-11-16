﻿using BepInEx;
using BepInEx.Logging;
using UnityEngine;
using System.IO;
using UnityEngine.SceneManagement;
using GFApi.Creation;
using GFApi.Helper;

namespace GFApi;

[BepInPlugin(MyPluginInfo.PLUGIN_GUID, MyPluginInfo.PLUGIN_NAME, MyPluginInfo.PLUGIN_VERSION)]
[BepInProcess("Gelli Fields")]
public class MainPlugin : BaseUnityPlugin
{
    internal static new ManualLogSource Logger;
    public static GameData gameData;
    private void Awake()
    {
        DontDestroyOnLoad(this.gameObject);
        // Plugin startup logic
        Logger = base.Logger;
        Logger.LogInfo($"Plugin {MyPluginInfo.PLUGIN_GUID} is loaded!");
        SceneManager.activeSceneChanged += gameStart;
    }

    public void gameStart(Scene previousScene, Scene newScene){

        if(newScene.name != "WonderpondIntro"){
            Logger.LogInfo("Starting game");
            gameData = GameObject.Find("GameData").GetComponent<GameData>();
            gameData.itemDatabase.SetItemIDs();
        }
    }
}