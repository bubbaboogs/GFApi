using System;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using BepInEx;
using UnityEngine.Tilemaps;

internal static class TileGenerator
{
    public static void GenerateTileClass(Dictionary<string, TileBase> tileBases)
    {

        string filePath = Path.Combine(Paths.BepInExRootPath, "Tiles.cs");
        
        Dictionary<string, string> altNames = new Dictionary<string, string>{
            {"FRIENDLY_FIELDS_TILESET_0", "FRIENDLY_FIELDS_GRASS"},
            {"FRIENDLY_FIELDS_TILESET_1", "FRIENDLY_FIELDS_DIRT_BOTTOM"},
            {"FRIENDLY_FIELDS_TILESET_2", "FRIENDLY_FIELDS_GRASS_EDGE"},
            {"FRIENDLY_FIELDS_TILESET_3", "FRIENDLY_FIELDS_DIRT_RIGHT_EDGE"},
            {"FRIENDLY_FIELDS_TILESET_4", "FRIENDLY_FIELDS_DIRT_GRASS_CORNER"},
            {"FRIENDLY_FIELDS_TILESET_7", "FRIENDLY_FIELDS_DIRT_LEFT_EDGE"},
            {"FRIENDLY_FIELDS_TILESET_8", "FRIENDLY_FIELDS_GRASS_LEFT_EDGE"},
            {"FRIENDLY_FIELDS_TILESET_9", "FRIENDLY_FIELDS_STONE_1"},
            {"FRIENDLY_FIELDS_TILESET_10", "FRIENDLY_FIELDS_STONE_2"},
            {"FRIENDLY_FIELDS_TILESET_11", "FRIENDLY_FIELDS_STONE_GRASS"},
            {"FRIENDLY_FIELDS_TILESET_12", "FRIENDLY_FIELDS_GRASS_STONE"},
            {"FRIENDLY_FIELDS_TILESET_13", "FRIENDLY_FIELDS_FILLER"},
            {"FRIENDLY_FIELDS_TILESET_14", "FRIENDLY_FIELDS_DIRT_MIDDLE"},
            {"FRIENDLY_FIELDS_TILESET_21", "FRIENDLY_FIELDS_FILLER_LEFT"},
            {"FRIENDLY_FIELDS_TILESET_23", "FRIENDLY_FIELDS_FILLER_T"},
            {"FRIENDLY_FIELDS_TILESET_24", "FRIENDLY_FIELDS_AIR"},
            {"ROCKY_RETREAT_TILESET_12", "ROCKY_RETREAT_DIRT_BOTTOM"},
            {"ROCKY_RETREAT_TILESET_13", "ROCKY_RETREAT_DIRT_MIDDLE"},
            {"ROCKY_RETREAT_TILESET_18", "ROCKY_RETREAT_DIRT_BOTTOM_LEFT_CORNER"},
            {"ROCKY_RETREAT_TILESET_20", "ROCKY_RETREAT_DIRT_BOTTOM_RIGHT_CORNER"},
            {"FRIENDLY_FIELDS_TILESET_15", "FRIENDLY_FIELDS_FENCE_1"},
            {"FRIENDLY_FIELDS_TILESET_16", "FRIENDLY_FIELDS_FENCE_2"},
            {"FRIENDLY_FIELDS_TILESET_17", "FRIENDLY_FIELDS_MUDDY_GRASS"},
            {"FRIENDLY_FIELDS_TILESET_18", "FRIENDLY_FIELDS_MUD"},
            {"FRIENDLY_FIELDS_TILESET_19", "FRIENDLY_FIELDS_MUDDY_GRASS_BOTTOM"},
            {"FRIENDLY_FIELDS_TILESET_20", "FRIENDLY_FIELDS_FILLER_MIDDLE"},
            {"FRIENDLY_FIELDS_TILESET_22", "FRIENDLY_FIELDS_FILLER_HORIZONTAL"},
            {"ROCKY_RETREAT_TILESET_8", "ROCKY_RETREAT_DIRT_LEFT"},
            {"ROCKY_RETREAT_TILESET_0", "ROCKY_RETREAT_STONE"},
            {"ROCKY_RETREAT_TILESET_1", "ROCKY_RETREAT_GRASS_STONE_BIT"},
            {"ROCKY_RETREAT_TILESET_10", "ROCKY_RETREAT_STONE_TOP_LEFT"},
            {"ROCKY_RETREAT_TILESET_11", "ROCKY_RETREAT_FILLER"},
            {"ROCKY_RETREAT_TILESET_14", "ROCKY_RETREAT_STONE_BOTTOM_LEFT"},
            {"ROCKY_RETREAT_TILESET_15", "ROCKY_RETREAT_STONE_BOTTOM_RIGHT"},
            {"ROCKY_RETREAT_TILESET_16", "ROCKY_RETREAT_STONE_CEILING_BIT"},
            {"ROCKY_RETREAT_TILESET_17", "ROCKY_RETREAT_STONE_CEILING"},
            {"ROCKY_RETREAT_TILESET_19", "ROCKY_RETREAT_DIRT_RIGHT"},
            {"ROCKY_RETREAT_TILESET_2", "ROCKY_RETREAT_GRASS"},
            {"ROCKY_RETREAT_TILESET_21", "ROCKY_RETREAT_DIRT_TOP"},
            {"ROCKY_RETREAT_TILESET_22", "ROCKY_RETREAT_DIRT_STONE_BOTTOM_RIGHT"},
            {"ROCKY_RETREAT_TILESET_23", "ROCKY_RETREAT_DIRT_STONE_BOTTOM_LEFT"},
            {"ROCKY_RETREAT_TILESET_24", "ROCKY_RETREAT_DIRT_TOP_LEFT_CORNER"},
            {"ROCKY_RETREAT_TILESET_25", "ROCKY_RETREAT_DIRT_TOP_RIGHT_CORNER"},
            {"ROCKY_RETREAT_TILESET_3", "ROCKY_RETREAT_STONE_BITS"},
            {"ROCKY_RETREAT_TILESET_4", "ROCKY_RETREAT_DIRT_STONE_TOP_RIGHT"},
            {"ROCKY_RETREAT_TILESET_5", "ROCKY_RETREAT_DIRT_STONE_TOP_LEFT"},
            {"ROCKY_RETREAT_TILESET_6", "ROCKY_RETREAT_STONE_RIGHT"},
            {"ROCKY_RETREAT_TILESET_7", "ROCKY_RETREAT_STONE_LEFT"},
            {"ROCKY_RETREAT_TILESET_9", "ROCKY_RETREAT_STONE_TOP_RIGHT"},
            {"TRAIN_STATION_TILESET_0", "TRAIN_STATION_GRASS"},
            {"TRAIN_STATION_TILESET_10", "TRAIN_STATION_TILE"},
            {"TRAIN_STATION_TILESET_11", "TRAIN_STATION_GRASS_TILE_LEFT"},
            {"TRAIN_STATION_TILESET_12", "TRAIN_STATION_FILLER"},
            {"TRAIN_STATION_TILESET_16", "TRAIN_STATION_FILLER_MIDDLE"},
            {"TRAIN_STATION_TILESET_17", "TRAIN_STATION_FILLER_LEFT"},
            {"TRAIN_STATION_TILESET_18", "TRAIN_STATION_FILLER_HORIZONTAL"},
            {"TRAIN_STATION_TILESET_19", "TRAIN_STATION_FILLER_T"},
            {"TRAIN_STATION_TILESET_9", "TRAIN_STATION_GRASS_TILE_RIGHT"}
        };

        using (StreamWriter writer = new StreamWriter(filePath))
        {
            writer.WriteLine("using UnityEngine.Tilemaps;");
            writer.WriteLine("using GFApi;");
            writer.WriteLine();
            writer.WriteLine("namespace GFApi.Tiles{");
            writer.WriteLine("public static class GameTiles");
            writer.WriteLine("{");

            foreach (var tile in tileBases)
            {
                string sanitizedFieldName = SanitizeFieldName(tile.Key);
                if(altNames.ContainsKey(sanitizedFieldName))
                    writer.WriteLine($"\tpublic static TileBase {altNames[sanitizedFieldName]} => MainPlugin.tiles[\"{tile.Key}\"];");
                else
                    writer.WriteLine($"\tpublic static TileBase {sanitizedFieldName} => MainPlugin.tiles[\"{tile.Key}\"];");
            }

            writer.WriteLine("}");
            writer.WriteLine("}");
        }

        Debug.Log($"[TileGenerator] Generated GameTiles class at {filePath}");
    }
    private static string SanitizeFieldName(string key)
    {
        return key
            .Replace("-", "_")
            .Replace(" ", "_")
            .ToUpper();
    }
}

public static class TilesLoader
{
    private static Dictionary<string, string> reverseLookupDict = new Dictionary<string, string>();

    public static void LoadTiles(Dictionary<string, TileBase> tiles)
    {

        foreach (var tile in tiles)
        {
            string sanitizedKey = SanitizeFieldName(tile.Key);
            reverseLookupDict[sanitizedKey] = tile.Key;
        }

        foreach (var field in typeof(GFApi.Audio.GameSounds).GetFields())
        {
            string sanitizedFieldName = field.Name;

            if (reverseLookupDict.TryGetValue(sanitizedFieldName, out var originalKey))
            {
                if (tiles.TryGetValue(originalKey, out var clip))
                {
                    field.SetValue(null, clip);
                }
            }
        }

        Debug.Log($"[TileLoader] Loaded {tiles.Count} sounds into Tiles.");
    }

    private static string SanitizeFieldName(string key)
    {
        return key
            .Replace("-", "_")
            .Replace(" ", "_")
            .ToUpper();
    }
}
