using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicTimeView : View
{
    public float Time;
    private AudioSource player;
    // Start is called before the first frame update
    void Start()
    {
        player = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    protected override void Update()
    {
        Time = player.time;
        base.Update();
    }

    public override string GetContain()
    {
        return "Music:" + Time.ToString();
    }
}
