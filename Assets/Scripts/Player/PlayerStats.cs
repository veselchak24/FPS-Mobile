using UnityEngine;
using UnityEngine.UI;
public class PlayerStats : MonoBehaviour
{
    [SerializeField] private Text healphText;
    [SerializeField] private Text armorText;

    [SerializeField] private int healph = 100;
    [SerializeField] private int armor = 100;
    
    private CharacterController controller;

    private void Start()
    {
        controller = GetComponent<CharacterController>();
        healphText.text = healph.ToString();
        armorText.text = armor.ToString();
    }
    public void TakeDamage(int damage, Vector3 normal, int powerImpact)
    {
        armor -= Mathf.Clamp(Mathf.RoundToInt(damage * 0.65f), 0, armor);
        healph -= Mathf.Clamp(Mathf.RoundToInt(damage * 0.35f), 0, healph);
        controller.Move(transform.TransformVector(Vector3.back / 3));
        
        healphText.text = healphText.text.Substring(0, healphText.text.IndexOf(':') + 1) + healph.ToString();
        healphText.color = Color.Lerp(Color.green, Color.red, healph > 1 ? 1 / healph : 1);
        healphText.transform.GetChild(0).GetComponent<Image>().color = healphText.color;

        armorText.text = armorText.text.Substring(0, armorText.text.IndexOf(':') + 1) + armor.ToString();
        armorText.color = Color.Lerp(Color.blue, Color.red, armor > 1 ? 1 / armor : 1);
        armorText.transform.GetChild(0).GetComponent<Image>().color = armorText.color;

        if (healph == 0)
        {
            GetComponent<PlayerMove>().enabled = false;
            healphText.text = "LOSE!";
            healphText.color = Color.red;
            healphText.fontSize = 50;
            healphText.transform.position = new Vector2(Screen.width/2,Screen.height/2);
        }
    }
}
