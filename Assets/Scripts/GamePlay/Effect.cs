using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Effect
{
    protected EffectData data;
    protected GameEntity entity;
    private float timeCount = 0;

    public bool Deprecated 
    {
        get => data.duration < 0 ? false : timeCount >= data.duration;
    }

    public Effect(GameEntity target)
    {
        entity = target;
    }

    public void Update()
    {
        if (data.duration < 0) return;
        timeCount += Time.deltaTime;
    }

    public abstract void Affect();
}
