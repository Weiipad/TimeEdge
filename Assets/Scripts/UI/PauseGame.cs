using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Animations;
using UnityEngine.SceneManagement;

public class PauseGame : MonoBehaviour
{
    private int pressButtonType;

    public GameObject PauseWindow;
    public Animation PauseWindowAnimation;

    private bool isPressButton = false;

    // Start is called before the first frame update
    void Start()
    {
        if (PauseWindow == null)
        {
            PauseWindow = GameObject.Find("PauseGameWindow");
            PauseWindowAnimation = PauseWindow.GetComponent<Animation>();
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (PauseWindow == null)
            return;
        if(Input.GetKeyDown(KeyCode.Escape) && GameStatus.CurrentGameStatus != GameStatus.GameStatusType.pause)
        {
            PauseWindow.SetActive(true);
            GameStatus.CurrentGameStatus = GameStatus.GameStatusType.pause;
            if(!PauseWindowAnimation.isPlaying)
            {
                PauseWindowAnimation["PauseWindowShowUp"].time = 0;
                PauseWindowAnimation["PauseWindowShowUp"].speed = 1f;
                PauseWindowAnimation.Play("PauseWindowShowUp");
            }
        }
        else
        {
            if(isPressButton)
            {
                if (!PauseWindowAnimation.isPlaying)
                {
                    PauseWindow.SetActive(false);
                    switch (pressButtonType)
                    {
                        case 0:
                            break;
                        case 1:
                            GameStatus.CurrentGameStatus = GameStatus.GameStatusType.none;
                            SceneManager.LoadScene(0);
                            break;
                        case 2:
                            Application.Quit();
                            break;
                    }
                    GameStatus.CurrentGameStatus = GameStatus.GameStatusType.playing;
                    isPressButton = false;
                }
            }
        }
    }

    public void PressPauseWinowReturnGameButton()
    {
        PressButton();
        pressButtonType = 0;
    }

    public void PressPauseWinowReturnStartPageButton()
    {
        PressButton();
        pressButtonType = 1;
    }

    public void PressPauseWindowExitButton()
    {
        PressButton();
        pressButtonType = 2;
    }

    private void PressButton()
    {
        isPressButton = true;

        PauseWindowAnimation["PauseWindowShowUp"].time = PauseWindowAnimation["PauseWindowShowUp"].clip.length;
        PauseWindowAnimation["PauseWindowShowUp"].speed = -1f;
        PauseWindowAnimation.Play("PauseWindowShowUp");
    }
}
