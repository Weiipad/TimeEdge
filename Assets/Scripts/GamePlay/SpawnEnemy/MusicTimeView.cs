using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicTimeView : MonoBehaviour
{
    public int Minutes;
    public int Seconds;
    public float Time;
    private AudioSource player;
    // Start is called before the first frame update
    void Start()
    {
        player = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        Time = player.time;
        Minutes = Convert.ToInt32(Time / 60f);
        Seconds = Convert.ToInt32(Time % 60f);
    }
}
