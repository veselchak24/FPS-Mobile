using UnityEngine;

public abstract class Weapon : MonoBehaviour
{
    [SerializeField] protected int baseDamage;
    [SerializeField] protected int damageRange;
    public int damage { get { return Random.Range(baseDamage - damageRange, baseDamage + damageRange); } }

    public int powerImpact;

    public abstract bool Shoot(Vector3 from, Vector3 direction, out RaycastHit raycastHit);

    /// <summary>
    /// Return result shoot in raycastHit.If raycast miss,then return hit,where distance < 0
    /// </summary>
    /// <param name="from">where the raycast is coming from.</param>
    /// <param name="direction">direction raycast</param>
    /// <param name="maxDistance">maximum distance of RaycastHit(have a const attribute)</param>
    /// <returns></returns>
    protected RaycastHit Shoot(Vector3 from, Vector3 direction)
    {
        RaycastHit hit;

        if (!Physics.Raycast(from, direction, out hit))
            hit.distance = -1;

        return hit;
    }
}
