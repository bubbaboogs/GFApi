using System;
using System.Collections.Generic;
using System.IO;
using BepInEx;
using BepInEx.Logging;
using GFApi.Creation;
using GFApi.Modification;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;
using UnityEngine.Tilemaps;
using UnityEngine.UI;

namespace GFApi;

[BepInPlugin(MyPluginInfo.PLUGIN_GUID, MyPluginInfo.PLUGIN_NAME, MyPluginInfo.PLUGIN_VERSION)]
[BepInProcess("Gelli Fields")]
public class MainPlugin : BaseUnityPlugin
{
    internal static new ManualLogSource Logger;

    public static UnityEvent OnBiomeLoad = new();
    public static UnityEvent OnGameLoad = new();
    public static UnityEvent OnGameStart = new();
    public static UnityEvent<GameData.biomeList> OnSceneLoad = new();

    public static GameData gameData;
    public static GameObject gameMaster;
    public static HandCursor handCursor;
    public static SoundManager soundManager = null;
    public static Shop shop;

    private static bool hasLoaded = false;
    public static AssetBundle UIBundle;
    public static TMP_FontAsset gameFont;

    public static GameData.biomeList currentScene = GameData.biomeList.MainMenu;
    public static string currentLoadedScene = "";
    public static bool genSounds = true;
    public static string ImagesPath { get; } = Path.Combine(Paths.GameRootPath, "Textures");
    public static string GFApiPath;
    public static Dictionary<string, string> images = new Dictionary<string, string>();
    public static Dictionary<string, TileBase> tiles = new();

    public static Dictionary<Item, string[]> registeredItems = new();
    public static List<Crop> registeredCrops = new();
    private void Awake()
    {
        DontDestroyOnLoad(gameObject);
        hideFlags = HideFlags.HideAndDontSave;
        // Plugin startup logic
        Logger = base.Logger;
        Logger.LogInfo($"Plugin {MyPluginInfo.PLUGIN_GUID} is loaded!");
        SceneManager.activeSceneChanged += gameStart;
        OnSceneLoad.AddListener(MainMenuLoad);
        GFApiPath = Path.GetDirectoryName(Info.Location);
        //UIBundle = AssetBundle.LoadFromFile(Path.Combine(GFApiPath, "UI"));
        HarmonyLib.Harmony harmony = new HarmonyLib.Harmony(MyPluginInfo.PLUGIN_GUID);
        harmony.PatchAll();
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
        gameFont = FindFirstObjectByType<TextMeshPro>().font;
        if(newScene.name != "WonderpondIntro"){
            if(!hasLoaded){
                Logger.LogInfo("Starting game");
                gameData = GameObject.Find("GameData").GetComponent<GameData>();
                hasLoaded = true;
                OnGameLoad.Invoke();
            }
            TileBase[] allLoadedTiles = Resources.FindObjectsOfTypeAll<TileBase>();
            foreach(TileBase tile in allLoadedTiles){
                if (!tiles.ContainsKey(tile.name))
                {
                    tiles.Add(tile.name, tile);
                }
                else
                {
                    continue;
                }
            }
            allLoadedTiles = null;
            currentLoadedScene = newScene.name;
            OnSceneLoad.Invoke(currentScene);
            if(GameObject.Find("GameMaster")){
                handCursor = HandCursor.handCursor;
                gameMaster = GameObject.Find("GameMaster");
                soundManager = gameMaster.GetComponentInChildren<SoundManager>();
                shop = GameObject.Find("Shop").GetComponent<Shop>();
                Items.itemManager = gameMaster.GetComponent<ItemManager>();
                Crops.cropManager = gameMaster.GetComponent<CropManager>();
                GameObject ground = GameObject.FindGameObjectWithTag("Ground");
                Tilemap tilemap = ground.GetComponent<Tilemap>();
                OnBiomeLoad.Invoke();
                Logger.LogInfo("Biome Load Called");
                Logger.LogInfo($"OnBiomeLoad listener count: {OnBiomeLoad.GetPersistentEventCount()}");
                if(genSounds)
                    GameSoundGenerator.GenerateSoundClass(soundManager.gameObject);
                GameSoundsLoader.LoadSounds(soundManager.gameObject);
                tilemap.RefreshAllTiles();
                shop.SetShop();
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

    public void MainMenuLoad(GameData.biomeList scene){
        if (scene == GameData.biomeList.MainMenu){
            GameObject versionText = GameObject.Find("Game-Version-Text");
            versionText.transform.position = new Vector3(versionText.transform.position.x + 1.38f, versionText.transform.position.y, versionText.transform.position.z);
            versionText.GetComponent<TextMeshPro>().text += " (MODDED)";
            GameObject discordText = UI.UICreation.CreateText("Join the Gelli Fields Modding Discord!", 5);
            discordText.transform.SetParent(GameObject.Find("HUD").transform);
            discordText.GetComponent<TextMeshProUGUI>().color = new Color(0, 123, 255);
            discordText.transform.localScale = new Vector3(3, 3);
            discordText.name = "DiscordText";
            discordText.AddComponent<ContentSizeFitter>().horizontalFit = ContentSizeFitter.FitMode.PreferredSize;
            discordText.GetComponent<ContentSizeFitter>().verticalFit = ContentSizeFitter.FitMode.PreferredSize;
            Button discordButton = discordText.AddComponent<Button>();
            DiscordTextHover discordTextHover = discordText.AddComponent<DiscordTextHover>();
            discordButton.onClick.AddListener(() => Application.OpenURL("https://discord.gg/Ygr4zQAMxn"));
            discordButton.targetGraphic = discordText.GetComponent<TextMeshProUGUI>();
            discordText.transform.position = new Vector3(-9, -7, 0);
        }
    }
}
