using System;
using System.IO;
using System.IO.Compression;
using BepInEx;
using GFApi.Helper;
using UnityEngine;
using System.Collections.Generic;

namespace GFApi.Modification
{
    public static class ResourcePack
    {
        static string packDir = Path.Combine(Paths.GameRootPath, "ResourcePacks");

        public static void LoadResourcePacks()
        {
            foreach (string file in Directory.GetFiles(packDir, "*.zip"))
            {
                LoadPack(file);
            }
        }

        public static void ChangePackDir(string newDir) => packDir = newDir;

        public static void LoadPack(string path)
        {
            using (ZipArchive zip = ZipFile.OpenRead(path))
            {
                // Process the ZIP contents (not implemented yet)
            }
        }

        public static void GetImages()
        {
            if (!Directory.Exists(MainPlugin.ImagesPath))
            {
                MainPlugin.Logger.LogInfo("Image directory does not exist.");
                return;
            }

            try
            {
                foreach (string file in Directory.GetFiles(MainPlugin.ImagesPath, "*.png"))
                {
                    string fileName = Path.GetFileNameWithoutExtension(file);
                    string newFileHash = HelperFunctions.checkMD5(file);

                    if (MainPlugin.images.TryGetValue(fileName, out string existingFile))
                    {
                        string existingFileHash = HelperFunctions.checkMD5(existingFile);
                        if (newFileHash != existingFileHash)
                        {
                            fileName += "-2"; // Handle duplicate names
                        }
                    }

                    MainPlugin.images[fileName] = file; // Update or add
                }
            }
            catch (Exception ex)
            {
                MainPlugin.Logger.LogError(ex.ToString());
            }
        }

        public static void ReplaceImages()
        {
            foreach (Sprite sprite in Resources.FindObjectsOfTypeAll<Sprite>())
            {
                if (!MainPlugin.images.TryGetValue(sprite.name, out string filePath)) continue;

                MainPlugin.Logger.LogInfo($"Replacing sprite: {sprite.name}");

                try
                {
                    byte[] fileData = File.ReadAllBytes(filePath);
                    Texture2D newTex = new Texture2D(2, 2, TextureFormat.ARGB32, false);
                    if (newTex.LoadImage(fileData))
                    {
                        newTex.Apply();
                        Sprite newSprite = Sprite.Create(newTex, sprite.rect, sprite.pivot, sprite.pixelsPerUnit);
                        UpdateSpriteReferences(sprite, newSprite);
                    }
                }
                catch (Exception ex)
                {
                    MainPlugin.Logger.LogError(ex.ToString());
                }
            }

            Resources.UnloadUnusedAssets();
        }

        private static void UpdateSpriteReferences(Sprite oldSprite, Sprite newSprite)
        {
            foreach (SpriteRenderer renderer in GameObject.FindObjectsOfType<SpriteRenderer>())
            {
                if (renderer.sprite == oldSprite) renderer.sprite = newSprite;
            }

            foreach (UnityEngine.UI.Image uiImage in GameObject.FindObjectsOfType<UnityEngine.UI.Image>())
            {
                if (uiImage.sprite == oldSprite) uiImage.sprite = newSprite;
            }
        }
    }
}
