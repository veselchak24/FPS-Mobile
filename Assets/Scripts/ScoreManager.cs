using UnityEngine;

public class ScoreManager : MonoBehaviour
{
    public static class Score { public static int target = 0, bots = 0; public static int light = 0; };

    private void Start()
    {
        Score.target = 0;
        Score.bots = 0;
        Score.light = 0;
    }

    private void OnGUI()
    {
        GUIStyle fontStyle = new GUIStyle();
        fontStyle.fontSize = 32;
        GUI.Label(new Rect(0, 50, 200, 200), "<color=Cyan>><size=35>" + "Target:" + Score.target + "</size></color>");
        GUI.Label(new Rect(0, 100, 210, 200), "<color=Red>><size=35>" + "Bots:" + Score.bots + "</size></color>");

        if(Score.light != 0)
            GUI.Label(new Rect(0, 150, 210, 200), "<color=Yellow>><size=35>" + "Light:" + Score.light + "</size></color>");
    }
}
