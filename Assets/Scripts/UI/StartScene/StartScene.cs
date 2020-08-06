using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StartScene : MonoBehaviour
{
    public GameObject Canvas;

    public GameObject aboutWindowPrefabs;
    private GameObject aboutWindowInstance;

    private bool isAboutGameWindowActive = false;

    public void StartGame()
    {
        if (isAboutGameWindowActive)
            return;
        SceneManager.LoadScene(1);
    }

    public void AboutGame()
    {
        if (aboutWindowPrefabs == null)
            aboutWindowPrefabs = Resources.Load<GameObject>("Prefabs/StartScene/AboutWindow");
        if (aboutWindowInstance == null)
            aboutWindowInstance = GameObject.Instantiate(aboutWindowPrefabs, Canvas.transform);
        else
            aboutWindowInstance.SetActive(true);
        isAboutGameWindowActive = true;
    }

    public void ExitGame()
    {
        if (isAboutGameWindowActive)
            return;
        Application.Quit();
    }
}
