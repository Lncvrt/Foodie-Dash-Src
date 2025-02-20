using System.Linq;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class Menu : MonoBehaviour
{
    public RuntimePlatform[] desktopPlatforms = new[] {
        RuntimePlatform.WindowsPlayer,
        RuntimePlatform.LinuxPlayer,
        RuntimePlatform.OSXPlayer
    };

    void Awake()
    {
        GameObject exitObj = GameObject.Find("Canvas/ExitButton");
        GameObject playObj = GameObject.Find("Canvas/PlayButton");
        GameObject settingsObj = GameObject.Find("Canvas/SettingsButton");
        GameObject garageObj = GameObject.Find("Canvas/GarageButton");
        Button exitBtn = exitObj.GetComponent<Button>();
        Button playBtn = playObj.GetComponent<Button>();
        Button settingsBtn = settingsObj.GetComponent<Button>();
        Button garageBtn = garageObj.GetComponent<Button>();

        if (desktopPlatforms.Contains(Application.platform))
        {
            exitBtn.onClick.AddListener(ExitClick);
        } else
        {
            Destroy(exitObj);
        }

        playBtn.onClick.AddListener(PlayClick);
        settingsBtn.onClick.AddListener(SettingsClick);
        garageBtn.onClick.AddListener(GarageClick);
    }

    void PlayClick()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene("Game");
    }

    void SettingsClick()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene("Settings");
    }

    void GarageClick()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene("Garage");
    }

    void ExitClick()
    {
        Application.Quit();
    }
}