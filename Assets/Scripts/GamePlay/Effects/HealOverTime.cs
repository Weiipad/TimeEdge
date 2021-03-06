using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealOverTime : Effect
{
    private float speed;
    public HealOverTime(GameEntity target, float healSpeed = 1f) : base(target) 
    {
        speed = healSpeed;
        data = Resources.Load("ScriptableObjects/EffectData/HealOverTime") as EffectData;
    }

    public override void Affect()
    {
        entity.HP += speed;
    }
}