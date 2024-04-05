using UnityEngine.UI;
using UnityEngine;

public class SwitchShootingMode : MonoBehaviour
{
    public Sprite[] ShootModesSprite;

    private int it = 0;
    public void Switch()
    {
        GetComponent<Image>().sprite = ShootModesSprite[++it];
        if (it == ShootModesSprite.Length - 1) it = -1;
    }
}