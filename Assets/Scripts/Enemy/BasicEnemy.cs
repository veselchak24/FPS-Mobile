using System.Collections.Generic;
using UnityEngine;

public class BasicEnemy : MonoBehaviour
{
    [Header("Enemy Objects")]
    [SerializeField] protected GameObject Head;
    [SerializeField] protected GameObject Body;
    [SerializeField] protected GameObject leftArm, rightArm;
    [SerializeField] protected GameObject leftLeg, rightLeg;
    [SerializeField] protected GameObject leftLegJoint, rightLegJoint;

    [Header("Params")]
    [SerializeField] protected float maxTiltAngle = 20;
    [SerializeField] protected float speedRotate = 2;

    [Header("Legs")]
    [SerializeField] protected float rotationSpeed = 1.0f;
    [SerializeField] protected float maxAngleLeg = 30;

    [SerializeField] protected int healph = 100;
    [SerializeField] protected float speed = 2;

    private List<GameObject> textMeshes = new List<GameObject>();

    private static PlayerMove player = null;

    protected void Start()
    {
        if (player == null)
            player = FindObjectOfType<PlayerMove>();
    }

    public virtual void TakeDamage(int damage, Vector3 hitPoint, float distance)
    {
        healph -= Mathf.Clamp(damage,0,healph);

        CreateDamageText(damage, hitPoint, distance);

        if (healph == 0)
            Die();
    }

    private void Unfasten(ref GameObject obj)
    {
        obj.transform.SetParent(null);
        obj.AddComponent<Suicide>().SetLifeTime(Random.Range(2,3),Random.Range(4,6));
        var rb = obj.AddComponent<Rigidbody>();
        rb.velocity = new Vector3(Random.Range(3f, 5f), Random.Range(3f, 5f), Random.Range(3f, 5f));
    }

    protected virtual void Die()
    {
        ScoreManager.Score.bots += 1;

        Unfasten(ref Head);
        Unfasten(ref leftLeg);
        Unfasten(ref rightLeg);
        Unfasten(ref Body);

        Destroy(gameObject);
    }

    void CreateDamageText(int damage, Vector3 hitPoint, float distance)
    {
        GameObject textObject = new GameObject("Damage Text");
        textObject.transform.position = hitPoint;

        textObject.AddComponent<MeshRenderer>();

        TextMesh text = textObject.AddComponent<TextMesh>();
        text.text = damage.ToString();
        text.fontSize = 10 + Mathf.RoundToInt(distance) / 2;
        text.color = Color.Lerp(Color.red, Color.green, healph * 0.01f);
        text.gameObject.AddComponent<Suicide>().SetLifeTime(1.8f,2.2f);

        textMeshes.Add(textObject);
    }

    private void FixedUpdate()
    {
        for (int i = textMeshes.Count - 1; i >= 0; i--)
            if (textMeshes[i] != null)
            {
                textMeshes[i].transform.position += new Vector3(0, 2f * Time.deltaTime, 0);
                textMeshes[i].transform.Rotate(Vector3.up, Vector3.SignedAngle(transform.position, player.transform.position, Vector3.up));
            }
            else
                textMeshes.RemoveAt(i);
    }
}
