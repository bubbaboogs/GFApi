using UnityEngine;
using System.IO;
using HarmonyLib;
using System.Collections.Generic;
using System;
using UnityEngine.UI;
using System.Security.Cryptography;
using System.Text;

namespace GFApi.Helper{
    public static class HelperFunctions{
        public static Component CopyComponent(Component original, GameObject destination)
        {
        System.Type type = original.GetType();
        Component copy = destination.AddComponent(type);
        // Copied fields can be restricted with BindingFlags
        System.Reflection.FieldInfo[] fields = type.GetFields();
        foreach (System.Reflection.FieldInfo field in fields)
        {
           field.SetValue(copy, field.GetValue(original));
        }
        return copy;
        }

        public static Texture2D LoadTexFromFile(string filePath){
            Texture2D tex = null;
            byte[] fileData;

            if(File.Exists(filePath)){
                fileData = File.ReadAllBytes(filePath);
                tex = new Texture2D(2, 2, TextureFormat.BGRA32, false);
                tex.LoadImage(fileData);
            }

            return tex;
        }

        public static Sprite LoadSpriteFromFile(string filePath){
            Texture2D tex = null;
            byte[] fileData;

            if(File.Exists(filePath)){
                fileData = File.ReadAllBytes(filePath);
                tex = new Texture2D(2, 2, TextureFormat.BGRA32, false);
                tex.LoadImage(fileData);
                tex.filterMode = FilterMode.Point;
            }

            Rect rect = new Rect(0, 0, tex.width, tex.height);
            Sprite sprite = Sprite.Create(tex, rect, new Vector2(0.5f, 0.5f), 16);

            return sprite;
        }

        public static Sprite LoadSpriteFromFile(string filePath, int pixelsPerUnit){
            Texture2D tex = null;
            byte[] fileData;

            if(File.Exists(filePath)){
                fileData = File.ReadAllBytes(filePath);
                tex = new Texture2D(2, 2, TextureFormat.BGRA32, false);
                tex.LoadImage(fileData);
                tex.filterMode = FilterMode.Point;
            }

            Rect rect = new Rect(0, 0, tex.width, tex.height);
            Sprite sprite = Sprite.Create(tex, rect, new Vector2(0.5f, 0.5f), pixelsPerUnit);

            return sprite;
        }

        public static string checkMD5(string filename)
        {
            using (var md5 = MD5.Create())
            {
                using (var stream = File.OpenRead(filename))
                {
                    return Encoding.Default.GetString(md5.ComputeHash(stream));
                }
            }
        }
    }
}