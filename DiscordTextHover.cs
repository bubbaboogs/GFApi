using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;


public class DiscordTextHover : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public void OnPointerEnter(PointerEventData eventData)
    {
        gameObject.GetComponent<TextMeshProUGUI>().color = new Color(0, 0, 238);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        gameObject.GetComponent<TextMeshProUGUI>().color = new Color(0, 123, 255);
    }
}