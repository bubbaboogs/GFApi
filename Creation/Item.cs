using UnityEngine;
using Newtonsoft.Json.Linq;

using System.Collections.Generic;
using GFApi.Helper;
using System.IO;
using BepInEx;
using System;

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

       public static GameObject CreateShopItem(int itemID, Item itemToSell){
            GameObject shopItem = new GameObject();
            ShopItem itemComponent = shopItem.AddComponent<ShopItem>();
            itemComponent.shopItemID = itemID;
            itemComponent.itemToSell = itemToSell;
            return shopItem;
       }

       public static Item CreateItemFromFile(string json, string directory){
            JObject itemJson = JObject.Parse(json);
            Item item = (Item)ScriptableObject.CreateInstance("Item");
            item.itemName = (string)itemJson["itemName"];
            item.itemTechnicalName = (string)itemJson["itemTechnicalName"];
            try{
                item.sellAmount = (int)itemJson["sellAmount"];
                item.isPlantable = (bool)itemJson["isPlantable"];
                item.rarity = (int)itemJson["itemRarity"];
                item.isEdible = (bool)itemJson["isEdible"];
            }
            catch(Exception ex){
                MainPlugin.Logger.LogWarning(ex);
            }
            string spritePath = (string)itemJson["spritePath"];
            Sprite itemSprite = HelperFunctions.LoadSpriteFromFile(Path.Combine(directory, spritePath));
            item.itemSprite = itemSprite;
            MainPlugin.Logger.LogInfo(item.itemName + "\n" + item.isEdible);
            return item;
       }
    }
}