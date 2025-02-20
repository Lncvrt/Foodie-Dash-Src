using UnityEngine;
using UnityEngine.UI;

public class UpdateRequired : MonoBehaviour
{
    void Awake()
    {
        GameObject buttonObj = GameObject.Find("Canvas/Button");
        Button btn = buttonObj.GetComponent<Button>();
        btn.onClick.AddListener(TaskOnClick);
    }

    void TaskOnClick()
    {
#if UNITY_STANDALONE_WIN
        string osSpecificPath = "/windows";
#elif UNITY_STANDALONE_OSX
        string osSpecificPath = "/macos";
#elif UNITY_STANDALONE_LINUX
        string osSpecificPath = "/linux";
#elif UNITY_ANDROID
        string osSpecificPath = "/android";
#else
        string osSpecificPath = "";
#endif

        string finalUrl = "https://foodiedash.xytriza.com/download" + osSpecificPath;

        Application.OpenURL(finalUrl);
    }
}