//using DG.Tweening;
using HarmonyLib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using TMPro;
using GFApi;

namespace GFApi.ItemShop;
[HarmonyPatch(typeof(global::Shop))]
public static class ShopPatcher
{
    public const float ROW_HEIGHT = 0.75f;
    public static int shop_rows;

    [HarmonyPatch("SetShop"), HarmonyPrefix]
    public static void SetShopPrefix(Shop __instance)
    {
        int rows = PatchShopItems(__instance);
        PatchShopGraphics(__instance, rows);
    }

    [HarmonyPatch(typeof(ShopItem), "SetShopItem"), HarmonyPostfix]
    public static void SetShopItemPostfix(ShopItem __instance)
    {
        if (__instance.itemToSell == null)
            return;

        if(HandCursor.handCursor.rarityColors != null && HandCursor.handCursor.rarityColors.Count > 0)
            __instance.itemNameText.color = HandCursor.handCursor.rarityColors[Mathf.Clamp(__instance.itemToSell.rarity, 0, HandCursor.handCursor.rarityColors.Count - 1)];
        __instance.itemNameText.sortingOrder = 100;
        __instance.glimAmountText.sortingOrder = 100;
    }

    private static void PatchShopGraphics(Shop shop, int rows = 1)
    {
        shop_rows = rows;
        Vector2 offset = new(0, rows * ROW_HEIGHT);
        shop.GetComponent<EdgeCollider2D>().offset += offset;
        try { shop.GetComponentsInChildren<BoxCollider2D>().FirstOrDefault((c) => c.transform.position.y > 1.5f).offset += offset; }
        catch { }

        SpriteRenderer spriteRenderer = shop.GetComponent<SpriteRenderer>();
        Sprite originalSprite = spriteRenderer.sprite;

        Sprite replacementSprite = Sprite.Create(
            originalSprite.texture, originalSprite.textureRect, new Vector2(0.5f, 0),
            originalSprite.pixelsPerUnit, 1, SpriteMeshType.FullRect, new Vector4(0, 17, 0, 43));
        spriteRenderer.sprite = replacementSprite;

        spriteRenderer.drawMode = SpriteDrawMode.Tiled;
        Vector2 startSize = new Vector2(
            originalSprite.textureRect.width / originalSprite.pixelsPerUnit,
            originalSprite.textureRect.height / originalSprite.pixelsPerUnit);
        spriteRenderer.size = startSize;

        Vector2 targetSize = startSize + offset;

        //DOTween.To(() => spriteRenderer.size, (v) => spriteRenderer.size = v, targetSize, 2).SetEase(Ease.OutElastic, 3).SetDelay(2.3f);
        spriteRenderer.size = targetSize;

        shop.transform.localScale = Vector3.one;
    }

    private static int PatchShopItems(Shop shop)
    {
    	List<ShopItem> shopItems = shop.shopItemList;
    	List<Item> itemsToSell = new();

    	foreach (var item in MainPlugin.registeredItems)
    	{
            MainPlugin.Logger.LogInfo(MainPlugin.currentLoadedScene);
    		if (item.Value != null && item.Value.Contains(MainPlugin.currentLoadedScene))
    		{
    			itemsToSell.Add(item.Key);
    		}
    	}

    	shop.shopItemList.RemoveAll(item =>
    	{
    		if (item.itemToSell == null) return false;
    		bool shouldRemove = MainPlugin.registeredItems.Any(registered => registered.Key.itemTechnicalName == item.itemToSell.itemTechnicalName);
    		if (shouldRemove)
    			GameObject.Destroy(item.gameObject);
    		return shouldRemove;
    	});

    	Vector2 spawnDistance = shopItems[1].transform.position - shopItems[0].transform.position;
    	int moddedItemsCount = itemsToSell.Count;

    	shopItems.Capacity = shopItems.Count + moddedItemsCount;
    	for (int i = 0; i < moddedItemsCount; i++)
        {
        	ShopItem newShopItem = GameObject.Instantiate(shopItems[0].gameObject).GetComponent<ShopItem>();
        	newShopItem.transform.position = (Vector2)shopItems[0].transform.position + spawnDistance * (i % 3) + Vector2.up * ROW_HEIGHT * ((i + 3) / 3);
        	newShopItem.itemToSell = itemsToSell[i];
        	shopItems.Add(newShopItem);
        }

    	shop.shopItemList = shopItems;
    	return shopItems.Count / 3;
    }

}
