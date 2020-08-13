using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using UnityEngine;

public class GameStatus
{
    private GameStatus() { }
    public enum GameStatusType
    {
        none,
        playing,
        pause,
    }

    public static GameStatusType CurrentGameStatus = GameStatusType.none;

    public static bool IsPauseGame()
    {
        return (CurrentGameStatus == GameStatusType.pause);
    }

    public static bool IsPlayGame()
    {
        return (CurrentGameStatus == GameStatusType.playing);
    }
}
