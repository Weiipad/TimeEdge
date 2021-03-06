using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CursorControl : MonoBehaviour
{
    public PauseGame pauseGameGO;
    private void Awake()
    {
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
        if (pauseGameGO != null)
        {
            pauseGameGO.OnPressReturnGameButton += OnPressReturnGameButton;
            pauseGameGO.OnPressReturnStartPageButton += OnPressReturnStartPageButton;
        }
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Cursor.visible = true;
            Cursor.lockState = CursorLockMode.None;
        }
    }

    private void OnPressReturnGameButton()
    {
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
    }

    private void OnPressReturnStartPageButton()
    {
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
    }
}
