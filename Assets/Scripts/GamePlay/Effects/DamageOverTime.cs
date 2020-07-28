using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageOverTime : Effect
{
    private float speed;
    public DamageOverTime(GameEntity target, float healSpeed = 1f) : base(target)
    {
        speed = healSpeed;
        data = Resources.Load("ScriptableObjects/EffectData/DamageOverTime") as EffectData;
    }

    public override void Affect()
    {
        entity.HP -= speed;
    }
}
