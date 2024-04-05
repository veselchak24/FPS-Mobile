using UnityEngine;

public class RestartScene : MonoBehaviour
{
    public void restart() { UnityEngine.SceneManagement.SceneManager.LoadScene(0); }
}
