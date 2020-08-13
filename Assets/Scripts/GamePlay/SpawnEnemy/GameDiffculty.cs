using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameDiffculty
{
    private GameDiffculty() { }
    public enum Diffculty
    {
        nochoose,
        easy,
        normal,
        hard,
    }

    public static Diffculty diffculty = Diffculty.nochoose;
}
