using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnTime : MonoBehaviour
{
    private static float time;
    public static float Time { get { return time; } }

    public Level[] levels;
    private bool isEndCountTIme;
    private Coroutine preCountTime;
    public void StartCountTIme(bool isClearPreTimeCount)
    {
        if (isClearPreTimeCount)
            time = 0f;
        isEndCountTIme = false;
        preCountTime = StartCoroutine(CountTime());
    }

    public void EndCountTime()
    {
        isEndCountTIme = true;
        if (preCountTime != null)
            StopCoroutine(preCountTime);
    }

    private void Update()
    {
        if (GameStatus.CurrentGameStatus != GameStatus.GameStatusType.playing)
            return;
        for (int i = 0;i < levels.Length;i ++)
        {
            if (Mathf.Abs(levels[i].StartTime - time) <= 0.01f)
                levels[i].StartLevel();
            if (Mathf.Abs(levels[i].StartTime + levels[i].Duration - time) <= 0.01f)
                levels[i].EndLevel();
        }
    }

    private IEnumerator CountTime()
    {
        while (!isEndCountTIme)
        {
            if (!GameStatus.IsPauseGame())
            {
                yield return new WaitForSeconds(0.02f);
                time += 0.02f;
            }
            else
                yield return 0;
        }
    }

}
