using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class MainMenuController : MonoBehaviour
{
    public Button startButton;
    public Button quitButton;

    public TextMeshProUGUI messageText;

    private void Start()
    {
        messageText.text = PlayerPrefs.GetString("BestTime", "BEST TIME: 00:00.00");
    }

    public void OnButtonPlayGame()
    {
        SceneManager.LoadScene(1);
    }

    public void OnButtonQuit()
    {
        Application.Quit();
    }
}
