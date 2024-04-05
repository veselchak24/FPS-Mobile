using UnityEngine;

public class Ak47 : Weapon
{
    public GameObject FirstPersonCamera;

    [SerializeField] private float maxDistance = 100;

    [SerializeField] private float maxRecoil, powerRecoil;

    [SerializeField] private ParticleSystem particle;

    private AudioSource sound;

    private float delta = 0; // ограницитель отдачи

    private enum RecoilDirection { None, backward, forward };
    private RecoilDirection RecoilDir = RecoilDirection.None;

    /// <summary>
    /// Return result shoot/Out in raycastHit
    /// </summary>
    /// <param name="from">where the raycast is coming from.</param>
    /// <param name="direction">direction raycast</param>
    /// <param name="raycast">out information as a RaycastHit</param>
    /// <returns></returns>
    public override bool Shoot(Vector3 from, Vector3 direction, out RaycastHit raycastHit)
    {
        raycastHit = Shoot(from, direction);

        if (raycastHit.distance < 0)
            return false;

        sound.Play();
        particle.Play();

        if (RecoilDir == RecoilDirection.None || delta >= (maxRecoil / 5) - maxRecoil) RecoilDir = RecoilDirection.backward;

        return true;
    }

    private void Recoil()
    {
        if (RecoilDir == RecoilDirection.None) return;

        if (RecoilDir == RecoilDirection.backward)
        {
            transform.position += transform.TransformDirection(Vector3.back * powerRecoil);
            delta -= powerRecoil;
        }
        else
        {
            transform.position += transform.TransformDirection(Vector3.forward * powerRecoil);
            delta += powerRecoil;
        }


        if (delta <= -maxRecoil) RecoilDir = RecoilDirection.forward;
        else if (delta == 0) RecoilDir = RecoilDirection.None;
    }

    private void Start()
    {
        maxRecoil /= 100;
        powerRecoil /= 100;
        powerImpact *= 100;

        sound = GetComponent<AudioSource>();
    }

    private void Update() => Recoil();
}
