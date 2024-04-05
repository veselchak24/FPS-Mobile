using System.Collections.Generic;
using UnityEngine;

public class CreateLvl : MonoBehaviour
{
    public GameObject Stends;
    public GameObject ForwardWall, BackwardWall, LeftWall, RightWall;

    public int countStand;

    private List<GameObject> ArrStends = new List<GameObject>();
    private int self_score = 0;

    void Start()
    {
        for (int i = 0; i < countStand; i++)
        {
            int index_children = Random.Range(0, Stends.transform.childCount);
            var Child = Instantiate(Stends.transform.GetChild(index_children));

            Child.transform.position = new Vector3(
               /* X */ (float)Random.Range(LeftWall.transform.position.x + 1.5f, RightWall.transform.position.x - 2),
               /* Y */ -1,
               /* Z */ (float)Random.Range(BackwardWall.transform.position.z + 1, ForwardWall.transform.position.z - 1));

            Child.transform.gameObject.SetActive(true);
            ArrStends.Add(Child.gameObject);
        }
    }
    private void Update()
    {
        if (ScoreManager.Score.target - self_score == ArrStends.Count)
        {
            self_score = ScoreManager.Score.target;
            foreach (GameObject Stend in ArrStends)
                Destroy(Stend);
            ArrStends.Clear();
            Start();
        }
    }
}