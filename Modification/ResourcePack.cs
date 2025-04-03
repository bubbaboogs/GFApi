using System;
using System.IO;
using System.IO.Compression;
using BepInEx;
using GFApi.Helper;
using UnityEngine;

namespace GFApi.Modification{
    public static class ResourcePack{
        static string packDir = Path.Combine(Paths.GameRootPath, "ResourcePacks");
        public static void LoadResourcePacks(){
            foreach(string file in Directory.GetFiles(packDir, "*.zip")){
                LoadPack(file);
            }
        }

        public static void ChangePackDir(string newDir){
            packDir = newDir;
        }

        public static void LoadPack(string path){
            ZipArchive zip = ZipFile.OpenRead(path);
            /*foreach (var file in zip.Entries){

            }*/
        }


        
        public static void GetImages()
        {
            if (!Directory.Exists(MainPlugin.ImagesPath))
            {
                MainPlugin.Logger.LogInfo("dir doesn't exist");
                return;
            }
            try
            {
                foreach (string file in Directory.GetFiles(MainPlugin.ImagesPath))
                {
                    if (Path.GetExtension(file).Equals(".png", System.StringComparison.OrdinalIgnoreCase))
                    {
                        //Logger.LogInfo(file);
                        if(MainPlugin.images.ContainsKey(Path.GetFileNameWithoutExtension(file))){
                            MainPlugin.Logger.LogInfo("New File Hash: " + HelperFunctions.checkMD5(Path.GetFileName(file)));
                            MainPlugin.Logger.LogInfo("Old File Hash: " + HelperFunctions.checkMD5(MainPlugin.images[Path.GetFileNameWithoutExtension(file)]));
                            if(HelperFunctions.checkMD5(Path.GetFileName(file)) != HelperFunctions.checkMD5(MainPlugin.images[Path.GetFileNameWithoutExtension(file)]))
                                MainPlugin.images.Add(Path.GetFileNameWithoutExtension(file) + "-2", file);
                        }
                        else
                            MainPlugin.images.Add(Path.GetFileNameWithoutExtension(file), file);
                    }
                 }
            }
            catch (Exception ex)
            {
                MainPlugin.Logger.Log(BepInEx.Logging.LogLevel.Error, ex.ToString());
            }
        }

        public static void ReplaceImages()
        {
            UnityEngine.Object[] textures = Resources.FindObjectsOfTypeAll(typeof(Sprite));
            foreach (UnityEngine.Object obj in textures)
            {
                Sprite sprite = obj as Sprite;
                if (sprite == null) continue;

                MainPlugin.Logger.LogInfo($"Checking sprite: {sprite.name}");

                if (!MainPlugin.images.ContainsKey(sprite.name))
                {
                    MainPlugin.Logger.LogInfo($"No replacement found for: {sprite.name}");
                    continue;
                }

                MainPlugin.Logger.Log(BepInEx.Logging.LogLevel.Debug, "Replacing Image: " + sprite.name);

                try
                {
                    byte[] fileData = File.ReadAllBytes(MainPlugin.images[sprite.name]);
                    Texture2D newTex = new Texture2D(2, 2, TextureFormat.ARGB32, false);
                    if (newTex.LoadImage(fileData))
                    {
                        newTex.Apply();
                        RenderTexture tmp = RenderTexture.GetTemporary(
                            newTex.width, newTex.height, 0,
                            RenderTextureFormat.Default,
                            RenderTextureReadWrite.Linear);
                        Graphics.Blit(newTex, tmp);
                        RenderTexture previous = RenderTexture.active;
                        RenderTexture.active = tmp;
                        Texture2D convertedTexture = new Texture2D(newTex.width, newTex.height, TextureFormat.ARGB32, false);
                        convertedTexture.ReadPixels(new Rect(0, 0, tmp.width, tmp.height), 0, 0);
                        convertedTexture.Apply();
                        RenderTexture.active = previous;
                        RenderTexture.ReleaseTemporary(tmp);
                        Rect spriteRect = new Rect(0, 0, convertedTexture.width, convertedTexture.height);
                        Sprite newSprite = Sprite.Create(convertedTexture, spriteRect, sprite.pivot, sprite.pixelsPerUnit);

                        MainPlugin.Logger.LogInfo($"Successfully replaced {sprite.name}");
                        UpdateSpriteReferences(sprite, newSprite);
                    }
                }
                catch (Exception ex)
                {
                    MainPlugin.Logger.Log(BepInEx.Logging.LogLevel.Error, ex.ToString());
                }
            }
            Resources.UnloadUnusedAssets();
        }

        public static void UpdateSpriteReferences(Sprite oldSprite, Sprite newSprite)
        {
            foreach (SpriteRenderer renderer in GameObject.FindObjectsOfType<SpriteRenderer>())
            {
                if (renderer.sprite == oldSprite)
                {
                    MainPlugin.Logger.LogInfo($"Updating SpriteRenderer: {renderer.gameObject.name}");
                    renderer.sprite = newSprite;
                }
            }
            foreach (UnityEngine.UI.Image uiImage in GameObject.FindObjectsOfType<UnityEngine.UI.Image>())
            {
                if (uiImage.sprite == oldSprite)
                {
                    MainPlugin.Logger.LogInfo($"Updating UI Image: {uiImage.gameObject.name}");
                    uiImage.sprite = newSprite;
                }
            }
        }

    }
}