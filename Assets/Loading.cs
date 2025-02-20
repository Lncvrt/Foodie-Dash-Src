using System.Net.Http;
using UnityEngine;
using System;
using System.Linq;

public class Loading : MonoBehaviour
{
    private HttpClient httpClient = new HttpClient();
    private string clientVersion = "1.1.1";
    private bool shouldLoadUpdateScene = false;
    private bool versionChecked = false;
    private RuntimePlatform[] desktopPlatforms = new[] {
        RuntimePlatform.WindowsPlayer,
        RuntimePlatform.LinuxPlayer,
        RuntimePlatform.OSXPlayer,
        RuntimePlatform.WindowsEditor,
        RuntimePlatform.LinuxEditor,
        RuntimePlatform.OSXEditor
    };

    void Awake()
    {
        if (!PlayerPrefs.HasKey("Setting1"))
        {
            if (!Application.isMobilePlatform)
            {
                PlayerPrefs.SetInt("Setting1", 1);
            }
            else
            {
                PlayerPrefs.SetInt("Setting1", 1);
            }
        }
        if (!PlayerPrefs.HasKey("Setting2"))
        {
            if (!Application.isMobilePlatform)
            {
                PlayerPrefs.SetInt("Setting2", 1);
            }
            else
            {
                PlayerPrefs.SetInt("Setting2", 0);
            }
        }
        if (!PlayerPrefs.HasKey("Setting3"))
        {
            PlayerPrefs.SetInt("Setting3", 0);
        }
    }

    void Start()
    {
        try
        {
            if (Application.platform == RuntimePlatform.WebGLPlayer)
            {
                versionChecked = true;
                shouldLoadUpdateScene = false;
            }
            else
            {
                httpClient.GetAsync("https://foodiedash.xytriza.com/api/getLatestVersion.php").ContinueWith(responseTask =>
                {
                    if (responseTask.IsCompletedSuccessfully)
                    {
                        versionChecked = true;
                        var response = responseTask.Result;
                        if (response.IsSuccessStatusCode)
                        {
                            string version = response.Content.ReadAsStringAsync().Result;
                            if (version != clientVersion)
                            {
                                shouldLoadUpdateScene = true;
                            }
                        }
                    }
                    else
                    {
                        GameObject loadingText = GameObject.Find("LoadingText");
                        loadingText.GetComponent<TextMesh>().text = "Error loading. Please contact support";
                        loadingText.GetComponent<TextMesh>().fontSize = 50;
                    }
                });
            }
        }
        catch (Exception e)
        {
            Debug.LogError(e);
            GameObject loadingText = GameObject.Find("LoadingText");
            loadingText.GetComponent<TextMesh>().text = "Error loading. Please contact support";
            loadingText.GetComponent<TextMesh>().fontSize = 50;
        }

        if (Application.platform == RuntimePlatform.Android)
        {
            Screen.orientation = ScreenOrientation.LandscapeLeft;
        }
        if (desktopPlatforms.Contains(Application.platform))
        {
            Screen.fullScreen = PlayerPrefs.GetInt("Setting1") == 1;
        }
    }

    void Update()
    {
        if (versionChecked)
        {
            if (shouldLoadUpdateScene)
            {
                UnityEngine.SceneManagement.SceneManager.LoadScene("UpdateRequired");
                shouldLoadUpdateScene = false;
            }
            else
            {
                UnityEngine.SceneManagement.SceneManager.LoadScene("Menu");
            }
        }
    }

    private static bool IsNewerVersion(string currentVersion, string latestVersion)
    {
        Version currentVer, latestVer;
        if (Version.TryParse(currentVersion, out currentVer) && Version.TryParse(latestVersion, out latestVer))
        {
            return latestVer > currentVer;
        }
        return false;
    }
}
