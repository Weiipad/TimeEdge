using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeView : MonoBehaviour
{
    public float time;

    // Update is called once per frame
    void Update()
    {
        time = SpawnTime.Time;
    }
}
