using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Analytics;
using UnityEngine.SceneManagement;

public class StartScene : MonoBehaviour
{
    public GameObject Canvas;
    public GameObject aboutWindow;
    private bool isAboutGameWindowActive = false;

    private void Start()
    {
        aboutWindow.GetComponent<AboutWindow>().OnAboutWindowActive += SetIsAboutGameWindowActiveValueFalse;
        aboutWindow.SetActive(false);
    }

    

    public void StartGame()
    {
        if (isAboutGameWindowActive)
            return;
        GameStatus.CurrentGameStatus = GameStatus.GameStatusType.playing;
        SceneManager.LoadScene(1);
    }

    public void AboutGame()
    {
        aboutWindow.SetActive(true);
        isAboutGameWindowActive = true;
    }

    private void SetIsAboutGameWindowActiveValueFalse()
    {
        isAboutGameWindowActive = false;
    }


    public void ExitGame()
    {
        if (isAboutGameWindowActive)
            return;
        Application.Quit();
    }
}
