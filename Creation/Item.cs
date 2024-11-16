
using UnityEngine;
using System;
using System.Collections;
using Unity;
using GFApi;

namespace GFApi.Creation{
    public static class Items{
        public static Item CreateItem(string itemName, string technicalName, Sprite itemSprite, int sellAmount = 0,  bool isPlantable = false, bool isEdible = false){
            Item item = (Item)ScriptableObject.CreateInstance("Item");
            item.itemName = itemName;
            item.itemTechnicalName = technicalName;
            item.sellAmount = sellAmount;
            item.isPlantable = isPlantable;
           item.isEdible = isEdible;
           item.itemSprite = itemSprite;
           MainPlugin.gameData.itemDatabase.itemDataList.Add(item);

           return item;
       }
}
}