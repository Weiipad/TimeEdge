using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DiffcultyChoose : MonoBehaviour
{
    public SpawnTime SpawnTime;

    private void Start()
    {
        HideCursor(false);
        GameStatus.CurrentGameStatus = GameStatus.GameStatusType.none;
    }

    public void ChooseEasy()
    {
        ChangeDifficulty(1);
    }

    public void ChooseNormal()
    {
        ChangeDifficulty(2);
    }

    public void ChooseHard()
    {
        ChangeDifficulty(3);
    }

    private void HideCursor(bool value)
    {
        Cursor.visible = !value;
        if (value)
            Cursor.lockState = CursorLockMode.Locked;
        else
            Cursor.lockState = CursorLockMode.None;
    }

    private void ChangeDifficulty(int enumIndex)
    {
        HideCursor(true);
        GameDiffculty.diffculty = (GameDiffculty.Diffculty)enumIndex;
        GameStatus.CurrentGameStatus = GameStatus.GameStatusType.playing;
        if(SpawnTime != null)
            SpawnTime.StartCountTIme(true);
        gameObject.SetActive(false);
    }
}
