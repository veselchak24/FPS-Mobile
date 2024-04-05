using UnityEngine;

public class BotShooter : BasicEnemy
{
    [Header("Player Trigger")]
    [SerializeField] private float AngleView;
    [SerializeField] private float DistanceView;

    private PlayerStats player;

    [Header("Weapon")]
    [SerializeField] private Weapon weapon;
    [SerializeField] private float FireDelayPerSeconds;
    private float DeltaShoot = 0;
    [SerializeField] private int BaseAmmo = 10;
    private int ammo;

    void ColorSet()
    {
        Head.GetComponent<Renderer>().material.color = Random.ColorHSV();
        Body.GetComponent<Renderer>().material.color = Random.ColorHSV();
        leftLeg.GetComponent<Renderer>().material.color = Random.ColorHSV();
        rightLeg.GetComponent<Renderer>().material.color = Random.ColorHSV();
    }

    void Start()
    {
        base.Start();

        maxAngleLeg /= 100;
        player = FindObjectOfType<PlayerStats>();
        ammo = BaseAmmo;

        ColorSet();
    }

    void MoveLegs()
    {
        float deltaTime = Time.deltaTime;

        if (leftLeg.transform.localRotation.x >= maxAngleLeg) // Если левая нога впереди в максимуме //переделать под локальный угол
            rotationSpeed = Mathf.Abs(rotationSpeed);
        else
            if (leftLeg.transform.localRotation.x <= -maxAngleLeg)
            rotationSpeed = -Mathf.Abs(rotationSpeed);

        leftLeg.transform.RotateAround(leftLegJoint.transform.position, transform.right, -deltaTime * rotationSpeed);
        rightLeg.transform.RotateAround(rightLegJoint.transform.position, transform.right, deltaTime * rotationSpeed);

        transform.position += speed * Time.deltaTime * transform.forward;

        //        transform.eulerAngles = new Vector3(Mathf.Clamp(transform.eulerAngles.x, -maxTiltAngle, maxTiltAngle),
        //            transform.eulerAngles.y,
        //            0
        //);
    }

    void Shoot()
    {
        if (FireDelayPerSeconds + Random.Range(-FireDelayPerSeconds / 2, FireDelayPerSeconds / 2) > Time.time - DeltaShoot) return;
        if (--ammo == 0)
        {
            ammo = BaseAmmo;
            DeltaShoot = Time.time + FireDelayPerSeconds * 5;
        }
        else
            DeltaShoot = Time.time;

        if (weapon.Shoot(Head.transform.position, Vector3.Cross(Head.transform.position, player.transform.position), out RaycastHit hit))
        {
            player.TakeDamage(weapon.damage, Vector3.Cross(Head.transform.position, player.transform.position), weapon.powerImpact);
        }
    }

    bool CheckView()
    {
        Ray ray = new Ray(transform.position, transform.forward);

        if (Physics.SphereCast(ray, 1f, out RaycastHit hit, 2f))
        {
            if (hit.transform.name == "Floor" || hit.transform.name == "Player" || hit.transform.name == "Bullet")
                return false;

            float RotateDir = speedRotate;

            if (Vector3.Dot(ray.direction, hit.normal) < 0) RotateDir *= -1;

            transform.Rotate(transform.up, RotateDir);

            return true;
        }
        else
            return false;

    }

    void CheckAngleToPlayer()
    {
        float angleBeetween = -Vector3.SignedAngle(player.transform.position - Body.transform.position, Body.transform.forward, Vector3.up);

        float distance = Vector3.Distance(transform.position, player.transform.position);

        if (Mathf.Abs(angleBeetween) < AngleView && distance < DistanceView)
        {
            Physics.Linecast(Head.transform.position, player.transform.position, out RaycastHit hit);
            if (hit.transform.name != "Player")
                return;

            if (Mathf.Abs(angleBeetween) > 7)
                transform.Rotate(Vector3.up, Mathf.Clamp(angleBeetween, -speedRotate, speedRotate));
            else
                Shoot();

            Debug.DrawLine(Head.transform.position, player.transform.position, Color.green);
        }
        else
        {
            Debug.DrawLine(Head.transform.position, player.transform.position, Color.red);
        }

    }

    void Update()
    {
        CheckAngleToPlayer();
    }
}
