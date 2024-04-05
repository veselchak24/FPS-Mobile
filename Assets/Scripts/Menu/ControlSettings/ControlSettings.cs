using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class ControlSettings : MonoBehaviour
{
    [SerializeField] private SettingsMenu SettingsMenu;
    [SerializeField] private GameObject BackgroundMenu;

    public void Back()
    {
        SettingsMenu.gameObject.SetActive(true);
        BackgroundMenu.gameObject.SetActive(true);

        this.gameObject.SetActive(false);
    }

    private string MyDictionaryToJson(Dictionary<string, Vector3> dict)
    {
        string str = "";
        foreach (var key in dict.Keys)
            str += key + ": (" + dict[key].x + ';' + dict[key].y + ';' + dict[key].z + ")" + '\n';
        return str.Substring(0, str.Length - 1);
    }

    private void LoadUIComponents()
    {
        if (!File.Exists(Application.persistentDataPath + "/Save/Settings/Control Settings.json")) return;

        Dictionary<string, Transform> Child = new Dictionary<string, Transform>();
        for (int i = 0; i < transform.childCount; i++)
        {
            Transform current_child = transform.GetChild(i);
            Child[current_child.name] = current_child;
        }

        using (var reader = new StreamReader(Application.persistentDataPath + "/Save/Settings/Control Settings.json"))
        {
            while (!reader.EndOfStream)
            {
                string setting = reader.ReadLine();
                for (int i = 0; i < Child.Count; i++)
                {
                    string[] VectorStr = setting.Substring(setting.IndexOf('(') + 1, setting.IndexOf(')') - setting.IndexOf('(') - 1).Split(';');
                    string name = setting.Substring(0, setting.IndexOf(':'));

                    Vector3 vect = new Vector3(float.Parse(VectorStr[0]), float.Parse(VectorStr[1]), float.Parse(VectorStr[2]));

                    Child[name].transform.position = vect;
                    break;
                }
            }
        }
    }

    private void Start() => LoadUIComponents();
 
    public void SaveComponents()
    {
        Dictionary<string, Vector3> ChildTransform = new Dictionary<string, Vector3>();

        for (int i = 0; i < transform.childCount; i++)
        {
            Transform Child = transform.GetChild(i);
            if (Child.tag == "Player")
                ChildTransform[Child.name] = Child.transform.position;
        }

        string filePath = Application.persistentDataPath + "/Save/Settings/Control Settings.json";
        var json = MyDictionaryToJson(ChildTransform);

        if (!Directory.Exists("Save/Settings")) Directory.CreateDirectory(Application.persistentDataPath + "/Save/Settings");

        using (StreamWriter writer = new StreamWriter(filePath))
            writer.Write(json);
    }

    public void SetDefaultSettings()
    {
        try
        {
            File.Delete(Application.persistentDataPath + "/Save/Settings/Control Settings.json");
        }
        finally
        {
            UnityEngine.SceneManagement.SceneManager.LoadScene(0);
        }
    }

}
