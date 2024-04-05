using UnityEngine;
using UnityEngine.EventSystems;

public class SetUIComponentPosition : MonoBehaviour,IPointerDownHandler,IPointerUpHandler
{
    private Vector2 firstPoint;
    
    private bool isPressed = false;

    public void OnPointerDown(PointerEventData eventData) { isPressed = true; }

    public void OnPointerUp(PointerEventData eventData) { isPressed = false; }

    public void Update()
    {
        if (!isPressed) return;

        foreach (Touch touch in Input.touches)
        {
            if (touch.phase == TouchPhase.Began) //если нажали в правой половине экрана
                firstPoint = touch.position;
            else
        if (touch.phase == TouchPhase.Moved)
            {
                Vector2 Axis = firstPoint - touch.position;
                transform.position = transform.position - new Vector3(Axis.x, Axis.y, 0);

                firstPoint = touch.position;
            }
        }
    }
}
