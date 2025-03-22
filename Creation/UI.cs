using UnityEngine;
using UnityEngine.UI;

namespace GFApi.UI{
    public static class UI{
        public static GameObject createButton(Sprite sprite, Menu menu, ButtonAction action, ButtonAction selectAction = null){
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
    }
}