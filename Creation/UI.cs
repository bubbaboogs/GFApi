using UnityEngine;
using UnityEngine.UI;
using System.IO;
using BepInEx;
using TMPro;

namespace GFApi.UI{
    public static class UICreation{
        public static GameObject CreateButton(Sprite sprite, Menu menu, ButtonAction action, ButtonAction selectAction = null, Vector2? size = null){
            if(size == null)
                size = Vector2.one;
            var button = new VanillaButton();
            var buttonObject = new GameObject();
            buttonObject.AddComponent<Image>().sprite = sprite;
            button.buttonImage = buttonObject.GetComponent<Image>();
            button.spriteRenderer = new SpriteRenderer();
            button.menu = menu;
            button.buttonAction = action;
            button.selectAction = selectAction;

            return buttonObject;
        }

        public static GameObject CreatePanel(Sprite sprite, Menu menu, int PPUMultiplier){
            GameObject panel = new GameObject();
            panel.AddComponent<Image>().sprite = sprite;
            panel.GetComponent<Image>().type = Image.Type.Tiled;
            panel.GetComponent<Image>().pixelsPerUnitMultiplier = PPUMultiplier;
            return panel;
        }

        public static GameObject CreateText(string text, int fontSize){
            GameObject textObject = new GameObject();
            TextMeshProUGUI textMesh = textObject.AddComponent<TextMeshProUGUI>();
            textMesh.text = text;
            textMesh.fontSize = fontSize;
            textMesh.font = MainPlugin.gameFont;
            textMesh.characterSpacing = -27;
            textMesh.wordSpacing = 20;
            textMesh.fontSize = fontSize;
            return textObject;
        }
    }
}