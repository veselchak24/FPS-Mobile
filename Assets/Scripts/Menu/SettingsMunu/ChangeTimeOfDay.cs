using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ChangeTimeOfDay : MonoBehaviour,IPointerDownHandler
{
    [SerializeField] private Sprite Day, Night;

    public void OnPointerDown(PointerEventData eventData)
    {
        Text component = GetComponentInChildren<Text>();
        if (component.text == "День")
        {
            component.text = "Ночь";
            component.color = Color.cyan;
            GetComponent<Image>().sprite = Night;
        }
        else
        {
            component.text = "День";
            component.color = Color.yellow;
            GetComponent<Image>().sprite = Day;
        }
    }
}
