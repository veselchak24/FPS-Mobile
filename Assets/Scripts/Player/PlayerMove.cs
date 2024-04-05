using UnityEngine;
public class PlayerMove : MonoBehaviour
{
    public Camera Cam;

    [SerializeField] private Joystick joystick;

    public float speed;

    private float gravity = Physics.gravity.y;

    public float jumpSpeed;
    private float jSpeed;
    private float sensitivity;

    [SerializeField] private UnityEngine.UI.Slider sensitivitySlider;

    private CharacterController controller;

    void Start()
    {
        controller = GetComponent<CharacterController>();
        controller.minMoveDistance = 0f;

        sensitivity = sensitivitySlider.value;
    }
    void Move()
    {
        // transform.Rotate(0, Input.GetAxis("Mouse X") * mouseSpeed, 0);

        float deltaX = joystick.Horizontal * speed;
        float deltaZ = joystick.Vertical * speed;

        Vector3 movement = new Vector3(deltaX, 0, deltaZ);

        movement = Vector3.ClampMagnitude(movement, speed); // обрезает вектор,для того,чтобы по диагонали скорость не увеличивалась
        movement *= Time.deltaTime; // чтобы человек с мощной видеокартой не бежал быстрее

        // Учёт направления объекта
        movement = transform.TransformDirection(movement);

        //Jump
        if (controller.isGrounded && jSpeed != jumpSpeed)
            jSpeed = 0;

        jSpeed += gravity * Time.deltaTime * 3f;

        movement.y = jSpeed * Time.deltaTime;

        controller.Move(movement);
    }

    Vector2 firstPoint;
    private float rotationX = 0;
    public float minAngle, maxAngle;

    private Rect viewRect = new Rect(-1000, -1000, 200, 200);
    private bool isViewMove = false;
    private bool isViewTouch = false;

    bool Between(Vector2 vector, Vector2 first, Vector2 second) => vector.x >= first.x && vector.x <= second.x && vector.y >= first.y && vector.y <= second.y;
    void View_Direction()
    {
        foreach (Touch touch in Input.touches)
        {
            if (Between(touch.position, viewRect.position, viewRect.position + viewRect.size) || !isViewTouch && touch.position.x >= Screen.width / 2)
            {
                viewRect.position = touch.position - viewRect.size / 2;
                if (!isViewMove)
                {
                    isViewMove = true;
                    firstPoint = touch.position;
                }
            }
            else
                continue;

            if (!isViewTouch)
                isViewTouch = true;

            Vector2 Axis = firstPoint - touch.position;
            rotationX += Axis.y * sensitivity / 25;
            rotationX = Mathf.Clamp(rotationX, minAngle, maxAngle);

            transform.Rotate(0, -Axis.x * sensitivity / 25, 0);
            Cam.transform.localEulerAngles = new Vector3(rotationX, 0, 0);
            firstPoint = touch.position;
        }
        if (!isViewTouch)
        {
            viewRect.x = -1000;
            isViewMove = false;
        }
        else
            isViewTouch = false;
    }

    public void Jump() { if (controller.isGrounded) jSpeed = jumpSpeed; }

    void PCCameraMove()
    {
        rotationX -= Input.GetAxis("Mouse Y") * sensitivity;
        rotationX = Mathf.Clamp(rotationX, -45f, 45f);

        Cam.transform.localEulerAngles = new Vector3(rotationX, 0, 0);
        transform.Rotate(0, Input.GetAxis("Mouse X") * sensitivity, 0);
    }

    void Update()
    {
        Move();

        if (DefinePC.isPC)
        {
            if (Input.GetKeyDown(KeyCode.Space)) Jump();
            PCCameraMove();
        }
        else
            View_Direction();
    }
    private void OnGUI()
    {
        GUIStyle style = new GUIStyle();
        style.normal.textColor = Color.white;
        style.fontSize = 32;
        style.fontStyle = FontStyle.Bold;

        GUILayout.Label("FPS: " + (int)(1.0f / Time.deltaTime), style);
    }

}
