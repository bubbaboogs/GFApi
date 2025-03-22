using System.Collections.Generic;
using System.IO;
using BepInEx;
using BepInEx.Logging;
using GFApi.Audio;
using GFApi.Creation;
using GFApi.Helper;
using Newtonsoft.Json;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace GFApi;

[BepInPlugin(MyPluginInfo.PLUGIN_GUID, MyPluginInfo.PLUGIN_NAME, MyPluginInfo.PLUGIN_VERSION)]
[BepInProcess("Gelli Fields")]
public class MainPlugin : BaseUnityPlugin
{
    internal static new ManualLogSource Logger;
    public static GameData gameData;
    public static bool genSounds = true;
    public static SoundManager soundManager = null;
    private void Awake()
    {
        DontDestroyOnLoad(gameObject);
        // Plugin startup logic
        Logger = base.Logger;
        Logger.LogInfo($"Plugin {MyPluginInfo.PLUGIN_GUID} is loaded!");
        SceneManager.activeSceneChanged += gameStart;
    }

    public void gameStart(Scene previousScene, Scene newScene){

        if(newScene.name != "WonderpondIntro"){
            Logger.LogInfo("Starting game");
            gameData = GameObject.Find("GameData").GetComponent<GameData>();
            if(GameObject.Find("GameMaster")){
                GameObject gameMaster = GameObject.Find("GameMaster");
                soundManager = gameMaster.GetComponentInChildren<SoundManager>();
                if(genSounds)
                    GameSoundGenerator.GenerateSoundClass(soundManager.gameObject);
                GameSoundsLoader.LoadSounds(soundManager.gameObject);
            }
        }
    }

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
