using BepInEx;
using BepInEx.Logging;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace GFApi;

[BepInPlugin(MyPluginInfo.PLUGIN_GUID, MyPluginInfo.PLUGIN_NAME, MyPluginInfo.PLUGIN_VERSION)]
[BepInProcess("Gelli Fields")]
public class MainPlugin : BaseUnityPlugin
{
    internal static new ManualLogSource Logger;
    public static GameData gameData;
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
            //gameData.itemDatabase.SetItemIDs();
            gameData.challengeDatabase.SetItemIDs();
        }
    }
}
