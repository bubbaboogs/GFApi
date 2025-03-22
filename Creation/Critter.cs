using JetBrains.Annotations;
using UnityEngine;

namespace GFApi.Creation{
    public static class Critters{
        public static CritterData CreateCritter(Item critterItem, CritterData.critterType type, GameObject critterObject, float spawnChance = 50, bool requireWeather = false, int weatherRequired = 0, bool requireTime = false, TimeManager.timeOfDay timeOfDayRequired = TimeManager.timeOfDay.Afternoon){
            CritterData critter = new CritterData();
            critter.critterItem = critterItem;
            critter.critter = type;
            critter.critterObject = critterObject;
            critter.spawnChance = spawnChance;
            critter.isRequireWeather = requireWeather;
            critter.weatherRequired = weatherRequired;
            critter.isRequireTime = requireTime;
            critter.timeOfDayRequired = timeOfDayRequired;
            return critter;
        }

        public static GameObject CreateFish(Item fishItem, string catchSoundPath, int starRankAmount, int catchTime, bool isTrainStation = false){
            GameObject fishObject = new GameObject();
            Rigidbody2D rb = fishObject.AddComponent<Rigidbody2D>();
            CritterMovement movement = fishObject.AddComponent<CritterMovement>();
            CritterInfo info = fishObject.AddComponent<CritterInfo>();
            CritterData data = new CritterData();
            data.critter = CritterData.critterType.Fish;
            Fish fish = new Fish();
            movement.catchTimerMax = catchTime;
            fish.fishItem = fishItem;
            fish.catchSound = catchSoundPath;
            fish.isTrainStation = isTrainStation;
            fish.starRankAmount = starRankAmount;

            return fishObject;
        }

        public static void RegisterFish(){

        }
    }
}