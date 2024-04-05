using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO;

public class SettingsMenu : MonoBehaviour
{
    [SerializeField] private GameObject ControlSettings;
    [SerializeField] private GameObject BackroundMenu;

    [SerializeField] private Slider sensivity;

    [SerializeField] private ChangeTimeOfDay TimeOfDay;

    public void SaveSettings()
    {

        string filePath = Application.persistentDataPath + "/Save/Settings/Main Settings.json";

        if (!Directory.Exists("Save/Settings")) Directory.CreateDirectory(Application.persistentDataPath + "/Save/Settings");

        using (StreamWriter writer = new StreamWriter(filePath))
        {
            writer.WriteLine("Sensivity" + ":" + sensivity.value);
            writer.Write("TimeOfDay" + ":" + TimeOfDay.GetComponentInChildren<Text>().text);
        }
    }

    private void LoadMainSettings()
    {
        if (!File.Exists(Application.persistentDataPath + "/Save/Settings/Main Settings.json")) return;

        using (StreamReader reader = new StreamReader(Application.persistentDataPath + "/Save/Settings/Main Settings.json"))
            while (!reader.EndOfStream)
            {
                string setting = reader.ReadLine();

                int index = setting.IndexOf(':') + 1;
                string value = setting.Substring(index, setting.Length - index);

                switch (setting.Substring(0, setting.IndexOf(':')))
                {
                    case "Sensivity":
                        sensivity.value = float.Parse(value);
                        break;
                    case "TimeOfDay":
                        if (value == "Ночь")
                            TimeOfDay.OnPointerDown(null);
                        break;
                }
            }
    }

    private void Start() => LoadMainSettings();

    public void SetControlSettings()
    {
        ControlSettings.gameObject.SetActive(true);

        this.gameObject.SetActive(false);
        BackroundMenu.SetActive(false);
    }
}
