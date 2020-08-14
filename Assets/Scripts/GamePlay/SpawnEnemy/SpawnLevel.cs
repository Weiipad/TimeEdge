using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnLevel : MonoBehaviour
{
    public int StartLevel = 0;
    public List<Level> levels;

    private int curLevel;
    public void StartSpawnLevel()
    {
        curLevel = StartLevel;
    }

    private void Update()
    {
        if (GameStatus.CurrentGameStatus != GameStatus.GameStatusType.playing)
            return;
        if (curLevel >= levels.Count)
        {
            GameStatus.CurrentGameStatus = GameStatus.GameStatusType.end;
            return;
        }
        if (!levels[curLevel].IsThisLevelStart)
            levels[curLevel].StartLevel();
        else if (levels[curLevel].IsThisLevelEnd)
        {
            levels[curLevel].EndLevel();
            curLevel++;
        }
    }
}
