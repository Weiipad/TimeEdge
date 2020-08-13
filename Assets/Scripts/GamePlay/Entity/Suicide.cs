using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Suicide : MonoBehaviour
{
    public float SuicideTime = 0f;
    private float curTime = 0f;

    public void StartCountTime()
    {
        if (SuicideTime <= 0f)
            return;
        StartCoroutine(CountTime());
    }

    private IEnumerator CountTime()
    {
        while(true)
        {
            if (GameStatus.IsPauseGame())
                yield return 0;
            else
            {
                if (curTime >= SuicideTime)
                {
                    Destroy(gameObject);
                    yield break;
                }
                yield return new WaitForSeconds(0.02f);
                curTime += 0.02f;
            }
        }
    }
}
