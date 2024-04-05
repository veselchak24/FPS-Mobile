using UnityEngine;
using UnityEngine.EventSystems;

public class Interact : MonoBehaviour,IPointerDownHandler
{
    bool interact = false;

    public bool isInteract()
    {
        if (interact)
        {
            interact = false;
            return true;
        }
        else
            return false;
    }

    public void OnPointerDown(PointerEventData eventData) => interact = true;
}
