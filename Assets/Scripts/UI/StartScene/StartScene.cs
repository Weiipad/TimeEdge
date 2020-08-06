using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StartScene : MonoBehaviour
{
    public GameObject Canvas;
    public GameObject aboutWindow;
    public GameObject Star;

    private GameObject[] stars;
    private bool isAboutGameWindowActive = false;

    private void Start()
    {
        aboutWindow.GetComponent<AboutWindow>().OnAboutWindowActive += SetIsAboutGameWindowActiveValueFalse;
        stars = new GameObject[Star.transform.childCount];
        for(int i = 0;i < stars.Length;i ++)
        {
            stars[i] = Star.transform.GetChild(i).gameObject;
        }
    }

    private IEnumerator ShowStars()
    {
        foreach(var i in stars)
        {
            i.SetActive(true);
            yield return new WaitForSeconds(0.25f);
        }
    }

    public void StartGame()
    {
        if (isAboutGameWindowActive)
            return;
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
