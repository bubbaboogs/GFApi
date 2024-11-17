using UnityEngine;

namespace GFApi.Creation{
    public static class Critters{
        public static CritterData CreateBug(Item bugItem, int catchAttempts = 1, bool requiresWeather = false){
            CritterData data = new CritterData();
            GameObject critterObject = new GameObject();
            data.critter = CritterData.critterType.Bug;
            data.spawnChance = 50f;
            data.critterItem = bugItem;
            data.critterObject = critterObject;
            return data;
        }
    }
}