using UnityEngine;

public class MainMenu : MonoBehaviour
{
    [SerializeField] private GameObject InGame;

    [SerializeField] private GameObject Menu;

    [SerializeField] private SettingsMenu SettingsMenu;

    public void PLAY()
    {
        InGame.SetActive(true);

        Menu.SetActive(false);
    }

    public void SETTINGS()
    {
        SettingsMenu.gameObject.SetActive(true);

        this.gameObject.SetActive(false);
    }

    public void EXIT()
    {
        Application.Quit();
    }
}
