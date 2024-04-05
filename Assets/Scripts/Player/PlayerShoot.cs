using UnityEngine;

public class PlayerShoot : MonoBehaviour
{
    [SerializeField] private Camera FirstPersonCamera;

    public Weapon weapon;

    [SerializeField] private UnityEngine.UI.Image ImageShotMode;

    public bool IsShoot { get; set; } = false; // for autofire

    private const float _deltaShoot = 0.4f;
    private float deltaShoot = 0;

    void Shoot()
    {
        RaycastHit hit;

        if (weapon.Shoot(FirstPersonCamera.transform.position, FirstPersonCamera.transform.forward, out hit))
        {
            if (!hit.rigidbody)
                return;
            switch (hit.transform.tag)
            {
                case "target":
                    ScoreManager.Score.target++;
                    hit.transform.tag = "Untagged";
                    hit.rigidbody.AddForce(-hit.normal * weapon.powerImpact);
                    break;

                case "Enemy":
                    BasicEnemy current = hit.transform.GetComponent<BasicEnemy>();
                    hit.rigidbody.AddForce(-hit.normal * weapon.powerImpact * 2);
                    current.TakeDamage(weapon.damage, hit.point, hit.distance);
                    break;

                case "Lampada":
                    ScoreManager.Score.light++;
                        hit.rigidbody.useGravity = true;
                    hit.transform.tag = "Untagged";
                    hit.rigidbody.AddForce(-hit.normal * weapon.powerImpact);
                    break;

                default:
                    print(hit.transform.name);
                    hit.rigidbody.AddForce(-hit.normal * weapon.powerImpact);
                    break;
            }
        }
    }

    private void Update()
    {
        if (DefinePC.isPC)
        {
            if (Input.GetMouseButton(0))
                if (Time.time - deltaShoot > _deltaShoot)
                {
                    Shoot();
                    deltaShoot = Time.time;
                }
        }
        else
        if (IsShoot)
            switch (ImageShotMode.sprite.name)
            {
                case "Single Shot":
                    Shoot();

                    IsShoot = false;

                    break;
                case "Multi Shot":
                    if (Time.time - deltaShoot > _deltaShoot)
                    {
                        Shoot();

                        deltaShoot = Time.time;
                    }
                    break;
            }
    }
}
