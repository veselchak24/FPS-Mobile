using UnityEngine;

public class MovingBot : BasicEnemy
{
    [Header("Flag")]
    [SerializeField] private bool _isMove;

    void ColorSet()
    {
        Head.GetComponent<Renderer>().material.color = Random.ColorHSV();
        Body.GetComponent<Renderer>().material.color = Random.ColorHSV();
        leftLeg.GetComponent<Renderer>().material.color = Random.ColorHSV();
        rightLeg.GetComponent<Renderer>().material.color = Random.ColorHSV();
    }

    private void Start()
    {
        base.Start();

        maxAngleLeg /= 100;
        ColorSet();
    }

    void Move()
    {
        CheckView();
        //Движение ног
        if (leftLeg.transform.localRotation.x >= maxAngleLeg) // Если левая нога впереди в максимуме //переделать под локальный угол
            rotationSpeed = Mathf.Abs(rotationSpeed);
        else
            if (leftLeg.transform.localRotation.x <= -maxAngleLeg)
            rotationSpeed = -Mathf.Abs(rotationSpeed);

        leftLeg.transform.RotateAround(leftLegJoint.transform.position, transform.right, -Time.deltaTime * rotationSpeed);
        rightLeg.transform.RotateAround(rightLegJoint.transform.position, transform.right, Time.deltaTime * rotationSpeed);

        //Перемещение
        transform.position += speed * Time.deltaTime * transform.forward;
    }
    
    bool CheckView()
    {
        Ray ray = new Ray(transform.position, transform.forward);

        if (Physics.SphereCast(ray, 1f, out RaycastHit hit, 2f))
        {
            if (hit.transform.name == "Floor" || hit.transform.name == "Player")
                return false;

            float RotateDir = speedRotate;

            if (Vector3.Dot(ray.direction, hit.normal) < 0) RotateDir *= -1;

            transform.Rotate(transform.up, RotateDir);

            return true;
        }
        else
            return false;

    }

    void Unfasten(ref GameObject obj)
    {
        obj.transform.SetParent(null);
        obj.AddComponent<Suicide>();
        //var rb = obj.AddComponent<Rigidbody>();
        //rb.velocity = new Vector3(Random.Range(3f, 5f), Random.Range(3f, 5f), Random.Range(3f, 5f));
    }

    private void Die()
    {
        Unfasten(ref Head);
        Unfasten(ref leftLeg);
        Unfasten(ref rightLeg);
        Unfasten(ref Body);

        Destroy(gameObject);
    }

    void Update()
    {
        if (_isMove)
         Move();
    }
}
