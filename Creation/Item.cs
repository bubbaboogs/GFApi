using UnityEngine;
using Newtonsoft.Json.Linq;

using System.Collections.Generic;
using GFApi.Helper;
using System.IO;
using BepInEx;
using System;

namespace GFApi.Creation{
    public static class Items{

        public static ItemManager itemManager = null;
        public static Item CreateItem(string itemName, string technicalName, Sprite itemSprite, int sellAmount = 0, int rarity = 0,  bool isPlantable = false, bool isEdible = false, int XPForEating = 0, Crop cropToPlant = null){
            Item item = (Item)ScriptableObject.CreateInstance("Item");
            item.itemName = itemName;
            item.itemTechnicalName = technicalName;
            item.sellAmount = sellAmount;
            item.isPlantable = isPlantable;
            item.rarity = rarity;
            item.isEdible = isEdible;
            item.itemSprite = itemSprite;
            item.name = itemName;
            item.XPForEating = XPForEating;
            item.cropToPlant = cropToPlant;
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

       public static void RegisterItemShop(Item item, string[] biomes){
            MainPlugin.registeredItems.Add(item, biomes);
       }

       public static GameObject CreateShopItem(Item itemToSell){
            GameObject shopItem = new GameObject();
            ShopItem itemComponent = shopItem.AddComponent<ShopItem>();
            itemComponent.shopItemID = itemToSell.itemID;
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

       public static GameObject SpawnItemByName(Vector2 spawnPos, string name, bool jump = false){
            return itemManager.SpawnItem(spawnPos, name, jump);
       }

       public static GameObject SpawnItem(Vector2 spawnPos, Item item, bool jump = false){
            GameObject gameObject = UnityEngine.Object.Instantiate(itemManager.itemObject, spawnPos, Quaternion.identity);
            gameObject.GetComponent<SpriteRenderer>().sprite = item.itemSprite;
            gameObject.GetComponent<WorldItem>().item = item;
            itemManager.droppedItemList.Add(gameObject);
            if (jump)
            {
                gameObject.GetComponent<Rigidbody2D>().AddForce(new Vector2(UnityEngine.Random.Range(-0.1f, 0.2f), UnityEngine.Random.Range(2.5f, 2.9f)), ForceMode2D.Impulse);
            }

            return gameObject;
       }

    }
}