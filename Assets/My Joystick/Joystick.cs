using UnityEngine;
using UnityEngine.EventSystems;

public class Joystick : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    private float MaxDist;
    private Transform MiddleCircle;

    const int size_rect = 300;
    Rect rect;

    [HideInInspector] public float Vertical = 0, Horizontal = 0;

    private bool isTrigerred = false;

    private void Start()
    {
        MiddleCircle = transform.GetChild(0).transform;
        MaxDist = GetComponent<RectTransform>().rect.width / 2;
    }

    public void OnPointerDown(PointerEventData pointerEvent)
    {
        rect = new Rect(pointerEvent.position, new Vector2(size_rect, size_rect));
        isTrigerred = true;
    }

    public void OnPointerUp(PointerEventData pointerEvent) => isTrigerred = false;

    private void AndroeedMode()
    {
        if (isTrigerred)
        {
            foreach (var touch in Input.touches)
                if (touch.position.x >= rect.x && touch.position.x <= rect.x + rect.width && touch.position.y >= rect.y && touch.position.y <= rect.y + rect.height)
                {
                    rect.position = new Vector2(touch.position.x - rect.width / 2, touch.position.y - rect.height / 2);

                    MiddleCircle.position = touch.position;
                    MiddleCircle.localPosition = Vector2.ClampMagnitude(MiddleCircle.localPosition, MaxDist);

                    Vertical = MiddleCircle.localPosition.y / MaxDist;
                    Horizontal = MiddleCircle.localPosition.x / MaxDist;
                }
        }
        else if (transform.position.x != 0 || transform.position.y != 0)
        {
            MiddleCircle.transform.localPosition = new Vector2(0, 0);
            Vertical = 0;
            Horizontal = 0;
        }
    }

    private void DesktopMove()
    {
        if (Input.GetKey(KeyCode.W))
            MiddleCircle.localPosition = new Vector2(MiddleCircle.localPosition.x, MaxDist);
        else if (Input.GetKey(KeyCode.S))
            MiddleCircle.localPosition = new Vector2(MiddleCircle.localPosition.x, -MaxDist);
        else
            MiddleCircle.localPosition = new Vector2(MiddleCircle.localPosition.x, 0);

        if (Input.GetKey(KeyCode.D))
            MiddleCircle.localPosition = new Vector2(MaxDist, MiddleCircle.localPosition.y);
        else if (Input.GetKey(KeyCode.A))
            MiddleCircle.localPosition = new Vector2(-MaxDist, MiddleCircle.localPosition.y);
        else
            MiddleCircle.localPosition = new Vector2(0, MiddleCircle.localPosition.y);

        MiddleCircle.localPosition = Vector2.ClampMagnitude(MiddleCircle.localPosition, MaxDist);

        Vertical = MiddleCircle.localPosition.y / MaxDist;
        Horizontal = MiddleCircle.localPosition.x / MaxDist;
    }

    private void Update()
    {
        if (DefinePC.isPC)
            DesktopMove();
        else
            AndroeedMode();
    }



}
