using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeView : View
{
    public float time;

    // Update is called once per frame
    protected override void Update()
    {
        time = SpawnTime.Time;
        base.Update();
    }

    public override string GetContain()
    {
        return "Spawn:" + time.ToString();
    }
}
