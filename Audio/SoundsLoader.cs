using System;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using BepInEx;

internal static class GameSoundGenerator
{
    public static void GenerateSoundClass(GameObject gameObjectWithSounds)
    {
        var audioComponent = gameObjectWithSounds.GetComponent<SoundManager>();
        if (audioComponent == null)
        {
            Debug.LogError("[GameSoundGenerator] Could not find GameAudioManager on the provided GameObject!");
            return;
        }

        Dictionary<string, AudioClip> soundDict = audioComponent.audioClipDictionary;

        string filePath = Path.Combine(Paths.BepInExRootPath, "GeneratedGameSounds.cs");

        using (StreamWriter writer = new StreamWriter(filePath))
        {
            writer.WriteLine("using UnityEngine;");
            writer.WriteLine("using GFApi;");
            writer.WriteLine();
            writer.WriteLine("namespace GFApi.Audio{");
            writer.WriteLine("public static class GameSounds");
            writer.WriteLine("{");

            foreach (var sound in soundDict)
            {
                string sanitizedFieldName = SanitizeFieldName(sound.Key);
                writer.WriteLine($"\tpublic static AudioClip {sanitizedFieldName} => MainPlugin.soundManager.audioClipDictionary[\"{sound.Key}\"];");
            }

            writer.WriteLine("}");
            writer.WriteLine("}");
        }

        Debug.Log($"[GameSoundGenerator] Generated GameSounds class at {filePath}");
    }
    private static string SanitizeFieldName(string key)
    {
        return key
            .Replace("-", "_")
            .Replace(" ", "_")
            .ToUpper();
    }
}

public static class GameSoundsLoader
{
    private static Dictionary<string, string> reverseLookupDict = new Dictionary<string, string>();

    public static void LoadSounds(GameObject gameObjectWithSounds)
    {
        var audioComponent = gameObjectWithSounds.GetComponent<SoundManager>();
        if (audioComponent == null)
        {
            Debug.LogError("[GameSoundsLoader] Could not find GameAudioManager on the provided GameObject!");
            return;
        }

        Dictionary<string, AudioClip> soundDict = audioComponent.audioClipDictionary;

        foreach (var sound in soundDict)
        {
            string sanitizedKey = SanitizeFieldName(sound.Key);
            reverseLookupDict[sanitizedKey] = sound.Key;
        }

        foreach (var field in typeof(GFApi.Audio.GameSounds).GetFields())
        {
            string sanitizedFieldName = field.Name;

            if (reverseLookupDict.TryGetValue(sanitizedFieldName, out var originalKey))
            {
                if (soundDict.TryGetValue(originalKey, out var clip))
                {
                    field.SetValue(null, clip);
                }
            }
        }

        Debug.Log($"[GameSoundsLoader] Loaded {soundDict.Count} sounds into GameSounds.");
    }

    private static string SanitizeFieldName(string key)
    {
        return key
            .Replace("-", "_")
            .Replace(" ", "_")
            .ToUpper();
    }
}
