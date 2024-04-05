using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Suicide : MonoBehaviour
{
    private float timeCreate;

    [SerializeField] private float minLT = 1, maxLT = 2;

    private float LifeTime;

    public void SetLifeTime(float minLifeTime, float maxLifeTime)
    {
        minLT = minLifeTime;
        maxLT = maxLifeTime;
    }

    private void Start()
    {
        timeCreate = Time.time;
        LifeTime = Random.Range(minLT, maxLT);
    }

    // Update is called once per frame
    void Update()
    {
        if (Time.time - timeCreate >= LifeTime)
            Destroy(gameObject);
    }
}
