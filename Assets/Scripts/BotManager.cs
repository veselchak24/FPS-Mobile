using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BotManager : MonoBehaviour
{
    [Header("Spawn Objects")]
    [SerializeField] private BasicEnemy enemyPrefab;
    [SerializeField] private int countBots;

    private List<BasicEnemy> listEnemy = new List<BasicEnemy>();

    [Header("Position Spawning")]
    [SerializeField] private float minX;
    [SerializeField] private float maxX, minY, maxY, minZ, maxZ;

    private void Start()
    {
        for (int i = 0; i < countBots; i++)
        {
            BasicEnemy current = Instantiate(enemyPrefab, transform);

            current.transform.position = transform.position + new Vector3(Random.Range(minX, maxX), Random.Range(minY, maxY), Random.Range(minZ, maxZ));
            current.transform.Rotate(Vector3.up, Random.Range(-180, 180));

            current.transform.SetParent(transform);
            listEnemy.Add(current);
        }
    }

    private int second = 0;
    private void Update()
    {
        if (Time.time > second)
        {
            for (int i = listEnemy.Count - 1; i >= 0; i--)
                if (listEnemy[i] == null)
                    listEnemy.RemoveAt(i);

            if (listEnemy.Count < 1)
                Start();

            second += 2;
        }
    }
}
