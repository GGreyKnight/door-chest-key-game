using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public CameraController cameraController;
    public GameOverWindow gameOverPanel;
    public GameObject hudContainer;

    public TextMeshProUGUI timeCounter, countdownText, bestTime;
    TimeSpan timePlaying;
    private float startTime, elapsedTime, bestTimeFloat;
    public int countdownTime;

    public bool gamePlaying { get; private set; }

    public static GameManager Instance { get; private set; }

    private void Awake()
    {
        if(Instance == null) { Instance = this; }
        else { Destroy(gameObject); }

        cameraController = FindObjectOfType<CameraController>();
        
        UnlockCursor();
    }

    void Start()
    {
        GetHighscore();
        Debug.Log(bestTimeFloat);

        gameOverPanel = FindObjectOfType<GameOverWindow>(true);
        
        gamePlaying = false;
        StartCoroutine(CountdownToStart());
    }

    private void BeginGame()
    {
        gamePlaying = true;
        startTime = Time.time;
    }

    public void EndGame()
    {
        if ((float)timePlaying.TotalMilliseconds < bestTimeFloat)
        {
            UpdateHighscore();
        }

        gamePlaying = false;

        ShowGameOverScreen();
    }

    private void ShowGameOverScreen()
    {
        GetHighscore();
        hudContainer.SetActive(false);
        string timePlayingString = "Time: " + timePlaying.ToString("mm':'ss'.'ff");
        gameOverPanel.transform.Find("FinalTime").GetComponent<TextMeshProUGUI>().text = timePlayingString;
        gameOverPanel.gameObject.SetActive(true);
    }

    void Update()
    {
        if(gamePlaying)
        {
            elapsedTime = Time.time - startTime;
            timePlaying = TimeSpan.FromSeconds(elapsedTime);

            string timePlayingString = "Time: " + timePlaying.ToString("mm':'ss'.'ff");
            timeCounter.text = timePlayingString;
        }
    }

    private void UnlockCursor()
    {
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }

    IEnumerator CountdownToStart()
    {
        while(countdownTime > 0)
        {
            countdownText.text = countdownTime.ToString();
            yield return new WaitForSeconds(1f);
            countdownTime--;
        }

        BeginGame();
        countdownText.text = "GO!";

        yield return new WaitForSeconds(1f);
        countdownText.gameObject.SetActive(false);
    }

    public void OnButtonTryAgain()
    {
        SceneManager.LoadScene(1);
    }

    public void OnButtonQuit()
    {
        Application.Quit();
    }

    private void GetHighscore()
    {
        bestTime.text = PlayerPrefs.GetString("BestTime", "BEST TIME: 00:00.00");
        bestTimeFloat = PlayerPrefs.GetFloat("BestTimeFloat", 1000000000000f);
    }

    private void UpdateHighscore()
    {
        PlayerPrefs.SetString("BestTime", "BEST TIME: " + timePlaying.ToString("mm':'ss'.'ff"));
        PlayerPrefs.SetFloat("BestTimeFloat", (float)timePlaying.TotalMilliseconds);
    }
}
