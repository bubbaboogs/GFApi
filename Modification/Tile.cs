using UnityEngine;
using UnityEngine.Tilemaps;

namespace GFApi.Modification{
    public static class Tiles{
        public static void ModifyTileSprite(Sprite sprite, TileBase tile){
                var spriteField = typeof(Tile).GetField("m_Sprite", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance);
                spriteField.SetValue(tile, sprite);
        }
    }
}