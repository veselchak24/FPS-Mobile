using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class LoadSettings : MonoBehaviour
{
    public GameObject CanvasUIComponents;

    private string SavePath;

    [SerializeField] private Interact interactUI;

    [SerializeField] private Material night;
    [SerializeField] private Light sunLight;

    [SerializeField] private List<GameObject> nightMods;

    private void LoadUIComponents()
    {
        if (!File.Exists(SavePath + "Control Settings.json")) return;

        Dictionary<string, Transform> Child = new Dictionary<string, Transform>();
        for (int i = 0; i < CanvasUIComponents.transform.childCount; i++)
        {
            Transform current_child = CanvasUIComponents.transform.GetChild(i);
            Child[current_child.name] = current_child;
        }

        using (var reader = new StreamReader(SavePath + "Control Settings.json"))
        {
            while (!reader.EndOfStream)
            {
                string setting = reader.ReadLine();
                string[] VectorStr = setting.Substring(setting.IndexOf('(') + 1, setting.IndexOf(')') - setting.IndexOf('(') - 1).Split(';');
                string name = setting.Substring(0, setting.IndexOf(':'));

                Vector3 vect = new Vector3(float.Parse(VectorStr[0]), float.Parse(VectorStr[1]), float.Parse(VectorStr[2]));

                Child[name].transform.position = vect;
            }
        }
    }

    private void LoadTimeOfDay()
    {
        if (!File.Exists(Application.persistentDataPath + "/Save/Settings/Main Settings.json")) return;

        using (StreamReader reader = new StreamReader(Application.persistentDataPath + "/Save/Settings/Main Settings.json"))
            while (!reader.EndOfStream)
            {
                string setting = reader.ReadLine();

                int index = setting.IndexOf(':') + 1;
                string value = setting.Substring(index, setting.Length - index);

                if (setting.Substring(0, setting.IndexOf(':')) == "TimeOfDay")
                    if (value == "Ночь")
                    {
                        RenderSettings.skybox = night;
                        sunLight.intensity = 0.2f;

                        foreach(var night in nightMods) night.SetActive(true);
                    }
                    else
                        interactUI.gameObject.SetActive(false);
            }
    }

    void Start()
    {
        SavePath = Application.persistentDataPath + "/Save/Settings/";

        LoadUIComponents();
        LoadTimeOfDay();
    }
}