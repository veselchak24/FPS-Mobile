using UnityEngine;

public class TriggerLight : MonoBehaviour
{
    [SerializeField] private GameObject selfLight;
    [SerializeField] private GameObject nextLight;
    private void OnTriggerEnter(Collider other)
    {
        if (other.name != "Player") return;

        if(selfLight.activeInHierarchy)
        {
            selfLight.SetActive(false);
            nextLight.SetActive(true);
        }
    }
}
