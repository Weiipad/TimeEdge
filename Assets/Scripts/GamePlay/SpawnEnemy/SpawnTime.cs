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

    private IEnumerator CountTime()
    {
        while (!isEndCountTIme)
        {
            if (GameStatus.CurrentGameStatus == GameStatus.GameStatusType.pause)
                continue;
            time += 0.02f;
            yield return new WaitForSeconds(0.02f);
        }
    }
}
