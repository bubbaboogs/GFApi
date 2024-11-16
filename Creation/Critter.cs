using GFApi;
using UnityEngine;

namespace GFApi.Creation{
    public static class Critters{
        public static Insect CreateBug(Item bugItem, int catchAttempts = 1, bool requiresWeather = false){
           Insect critter = new Insect();
           CritterData data = new CritterData();
           GameObject critterObject = new GameObject();
           critter.bugItem = bugItem;
           critter.catchAttempt = catchAttempts;
           data.critter = CritterData.critterType.Bug;
            data.spawnChance = 50f;
            data.critterItem = bugItem;
            Helper.HelperFunctions.CopyComponent(critter, critterObject);
           data.critterObject = critterObject;

            GFApi.MainPlugin.Logger.LogInfo(critter);
            return critter;
        }
    }
}