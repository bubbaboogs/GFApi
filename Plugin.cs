﻿using System.Collections.Generic;
using System.IO;
using BepInEx;
using BepInEx.Logging;
using GFApi.Creation;
using GFApi.Modification;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;
using UnityEngine.Tilemaps;

namespace GFApi;

[BepInPlugin(MyPluginInfo.PLUGIN_GUID, MyPluginInfo.PLUGIN_NAME, MyPluginInfo.PLUGIN_VERSION)]
[BepInProcess("Gelli Fields")]
public class MainPlugin : BaseUnityPlugin
{
    internal static new ManualLogSource Logger;
    public static UnityEvent OnBiomeLoad = new();
    public static UnityEvent OnGameLoad = new();
    public static UnityEvent OnGameStart = new();
    public static GameData gameData;
    public static GameObject gameMaster;
    public static HandCursor handCursor;
    public static SoundManager soundManager = null;
    public static GameData.biomeList currentScene = GameData.biomeList.MainMenu;
    public static bool genSounds = true;
    public static string ImagesPath { get; } = Path.Combine(Paths.GameRootPath, "Textures");
    public static Dictionary<string, string> images = new Dictionary<string, string>();
    public static Dictionary<string, TileBase> tiles = new();
    private void Awake()
    {
        DontDestroyOnLoad(gameObject);
        // Plugin startup logic
        Logger = base.Logger;
        Logger.LogInfo($"Plugin {MyPluginInfo.PLUGIN_GUID} is loaded!");
        SceneManager.activeSceneChanged += gameStart;
    }

    public void gameStart(Scene previousScene, Scene newScene){
        OnGameStart.Invoke();
        switch (newScene.name){
            case "WonderpondIntro":
                currentScene = GameData.biomeList.WonderpondIntro;
                break;
            case "MainMenu":
                currentScene = GameData.biomeList.MainMenu;
                break;
            case "TrainStation":
                currentScene = GameData.biomeList.TrainStation;
                break;
            case "FriendlyFields":
                currentScene = GameData.biomeList.FriendlyFields;
                break;
            case "RockyRetreat":
                currentScene = GameData.biomeList.RockyRetreat;
                break;
            case "LoadingScene":
                currentScene = GameData.biomeList.LoadingScene;
                break;
            case "Credits":
                currentScene = GameData.biomeList.Credits;
                break;
        }
        ResourcePack.GetImages();
        ResourcePack.ReplaceImages();
        Logger.LogInfo("Resource packs loaded");
        if(newScene.name != "WonderpondIntro"){
            Logger.LogInfo("Starting game");
            gameData = GameObject.Find("GameData").GetComponent<GameData>();
            OnGameLoad.Invoke();
            TileBase[] allLoadedTiles = Resources.FindObjectsOfTypeAll<TileBase>();
            foreach(TileBase tile in allLoadedTiles){
                if(!tiles.ContainsKey(tile.name)){
                    tiles.Add(tile.name, tile);
                }
                else{
                    continue;
                }
            }
            allLoadedTiles = null;
            if(GameObject.Find("GameMaster")){
                handCursor = GameObject.Find("Hand-Cursor").GetComponent<HandCursor>();
                gameMaster = GameObject.Find("GameMaster");
                soundManager = gameMaster.GetComponentInChildren<SoundManager>();
                GameObject ground = GameObject.FindGameObjectWithTag("Ground");
                Tilemap tilemap = ground.GetComponent<Tilemap>();
                OnBiomeLoad.Invoke();
                Logger.LogInfo("Biome Load Called");
                Logger.LogInfo($"OnBiomeLoad listener count: {OnBiomeLoad.GetPersistentEventCount()}");
                if(genSounds)
                    GameSoundGenerator.GenerateSoundClass(soundManager.gameObject);
                GameSoundsLoader.LoadSounds(soundManager.gameObject);
                tilemap.RefreshAllTiles();
            }
        }
    }

    /*private void Update(){
        if(Input.GetKeyDown(KeyCode.O)){
            TileGenerator.GenerateTileClass(tiles);
            Logger.LogInfo("Generated Tiles");
        }
    }*/

    public void LoadItemFiles(){
        foreach(var file in Directory.GetFiles(Paths.PluginPath, "*.item.json", SearchOption.AllDirectories)){
            print(file);
            using (StreamReader r = new StreamReader(file))
            {
                string json = r.ReadToEnd();
                //Item item = JsonConvert.DeserializeObject<Item>(json);
                print(json);
                Item item = Items.CreateItemFromFile(json, file);
                Items.RegisterItem(item);
            }
        }
    }
}
