using UnityEngine;
using UnityEngine.UI;

public class PauseMenu : MonoBehaviour
{
    GameObject pauseMenuObj;

    private void Start()
    {
        pauseMenuObj = GameObject.Find("Canvas/PauseMenu");
        GameObject menuButtonObj = GameObject.Find("Canvas/PauseMenu/ReturnToMenu");
        GameObject playButtonObj = GameObject.Find("Canvas/PauseMenu/ReturnToMenu");
        GameObject settingsButtonObj = GameObject.Find("Canvas/PauseMenu/ReturnToMenu");
        Button menuButton = menuButtonObj.GetComponent<Button>();
        Button playButton = playButtonObj.GetComponent<Button>();
        Button settingsButton = settingsButtonObj.GetComponent<Button>();
        menuButton.onClick.AddListener(MenuClick);
        playButton.onClick.AddListener(TogglePauseMenu);
        settingsButton.onClick.AddListener(SettingsClick);
    }

    public void TogglePauseMenu()
    {
        pauseMenuObj.SetActive(false);
    }

    private void MenuClick()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene("Game");
    }

    private void SettingsClick()
    {
        pauseMenuObj.SetActive(!pauseMenuObj.activeSelf);
    }
}
