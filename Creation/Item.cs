using UnityEngine;

using System.Collections.Generic;

namespace GFApi.Creation{
    public static class Items{
        public static Item CreateItem(string itemName, string technicalName, Sprite itemSprite, int sellAmount = 0, int rarity = 0,  bool isPlantable = false, bool isEdible = false){
            Item item = (Item)ScriptableObject.CreateInstance("Item");
            item.itemName = itemName;
            item.itemTechnicalName = technicalName;
            item.sellAmount = sellAmount;
            item.isPlantable = isPlantable;
            item.rarity = rarity;
            item.isEdible = isEdible;
            item.itemSprite = itemSprite;
            MainPlugin.Logger.LogInfo(item.itemName + "\n" + item.isEdible);
            return item;
       }

       public static void RegisterItems(List<Item> itemList = null){
            if(itemList != null){
                for(int i = 0; i < itemList.Count; i++){
                    itemList[i].itemID = MainPlugin.gameData.itemDatabase.itemDataList.Count + 1;
                    MainPlugin.gameData.itemDatabase.itemDataList.Add(itemList[i]);
                    MainPlugin.Logger.LogInfo(itemList[i].itemName);
                }
            }
       }

       public static void RegisterItem(Item item){
            item.itemID = MainPlugin.gameData.itemDatabase.itemDataList.Count + 1;
            MainPlugin.gameData.itemDatabase.itemDataList.Add(item);
            MainPlugin.Logger.LogInfo(item.itemName);
       }
    }
}