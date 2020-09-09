using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelfDestroy : MonoBehaviour
{
    public float lifeDuration;

    private float timeElapsed;
    void Start()
    {
        timeElapsed = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if (GameStatus.IsPauseGame())
            return;
        timeElapsed += Time.deltaTime;
        if (timeElapsed > lifeDuration) {
            Destroy(gameObject);
        }
    }
}
