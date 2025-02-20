using UnityEngine;
using UnityEngine.UI;

public class Settings : MonoBehaviour
{
    public float screenWidth = 0f;
    void Awake()
    {
        screenWidth = Camera.main.orthographicSize * 2 * Camera.main.aspect;
        GameObject setting1toggleobj = GameObject.Find("Toggle1");
        GameObject setting2toggleobj = GameObject.Find("Toggle2");
        GameObject setting3toggleobj = GameObject.Find("Toggle3");
        GameObject backbuttonobj = GameObject.Find("BackButton");

        UnityEngine.UI.Toggle setting1toggle = setting1toggleobj.GetComponent<UnityEngine.UI.Toggle>();
        UnityEngine.UI.Toggle setting2toggle = setting2toggleobj.GetComponent<UnityEngine.UI.Toggle>();
        UnityEngine.UI.Toggle setting3toggle = setting3toggleobj.GetComponent<UnityEngine.UI.Toggle>();
        Button backbutton = backbuttonobj.GetComponent<Button>();

        if (!Application.isMobilePlatform)
        {
            setting1toggle.isOn = PlayerPrefs.GetInt("Setting1") == 1;
            setting2toggle.isOn = PlayerPrefs.GetInt("Setting2") == 1;
            setting3toggle.isOn = PlayerPrefs.GetInt("Setting3") == 1;
        }
        else
        {
            setting1toggle.isOn = false;
            setting2toggle.isOn = true;
            setting3toggle.isOn = PlayerPrefs.GetInt("Setting3") == 1;
            setting1toggle.interactable = false;
            setting2toggle.interactable = false;
            PlayerPrefs.SetInt("Setting1", 0);
            PlayerPrefs.SetInt("Setting2", 1);
            PlayerPrefs.Save();
        }
        backbutton.onClick.AddListener(BackButton);

        setting1toggle.onValueChanged.AddListener(delegate
        {
            Setting1ToggleValueChanged(setting1toggle);
        });
        setting2toggle.onValueChanged.AddListener(delegate
        {
            Setting2ToggleValueChanged(setting2toggle);
        });
        setting3toggle.onValueChanged.AddListener(delegate
        {
            Setting3ToggleValueChanged(setting3toggle);
        });
    }

    public void Setting1ToggleValueChanged(Toggle change)
    {
        if (Application.platform != RuntimePlatform.Android && Application.platform != RuntimePlatform.IPhonePlayer)
        {
            Screen.fullScreen = change.isOn;
            PlayerPrefs.SetInt("Setting1", change.isOn ? 1 : 0);
        }
    }

    public void Setting2ToggleValueChanged(Toggle change)
    {
        if (!Application.isMobilePlatform)
        {
            PlayerPrefs.SetInt("Setting2", change.isOn ? 1 : 0);
        }
    }

    public void Setting3ToggleValueChanged(Toggle change)
    {
        PlayerPrefs.SetInt("Setting3", change.isOn ? 1 : 0);
    }

    public void BackButton()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene("Menu");
    }
}
