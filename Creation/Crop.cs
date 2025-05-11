using UnityEngine;
using Newtonsoft.Json.Linq;

using System.Collections.Generic;
using GFApi.Helper;
using System.IO;
using BepInEx;
using System;

namespace GFApi.Creation{
    public static class Crops{

        public static CropManager cropManager = null;
        public static Crop CreateCrop(string cropName, List<Sprite> cropSprites, Item seedItem = null, Item cropItem = null, int growClicksNeeded = 8, int grownStage = 3, int growTimerMax = 2, int harvestXP = 3, int growthOffset = 3, int seedDropChance = 2){
            Crop crop = (Crop)ScriptableObject.CreateInstance("Crop");
            crop.cropName = cropName;
            crop.seedItem = seedItem;
            crop.CropItem = cropItem;
            crop.growClicksNeeded = growClicksNeeded;
            crop.grownStage = grownStage;
            crop.growTimerMax = growTimerMax;
            crop.harvestXP = harvestXP;
            crop.cropSprites = cropSprites;
            crop.name = cropName;
            crop.growTimerOffset = growthOffset;
            crop.seedDropChance = seedDropChance;
            return crop;
       }

       public static void RegisterCrops(List<Crop> cropList){
            if(cropList != null){
                for(int i = 0; i < cropList.Count; i++){
                    cropList[i].cropID = MainPlugin.gameData.cropDatabase.cropDataList.Count + 1;
                    MainPlugin.gameData.cropDatabase.cropDataList.Add(cropList[i]);
                    MainPlugin.registeredCrops.Add(cropList[i]);
                    MainPlugin.Logger.LogInfo(cropList[i].cropName);
                }
            }
       }

       public static void RegisterCrop(Crop crop){
            crop.cropID = MainPlugin.gameData.cropDatabase.cropDataList.Count + 1;
            MainPlugin.gameData.cropDatabase.cropDataList.Add(crop);
            MainPlugin.registeredCrops.Add(crop);
            MainPlugin.Logger.LogInfo(crop.cropName);
       }

       public static void SpawnCropByName(Crop cropToSpawn, int cropX, int cropStage, bool playerPlanted){
            cropManager.SpawnNewCrop(cropToSpawn, cropX, cropStage, playerPlanted);
       }
    }
}