using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IncreaseDamage : Effect
{
    private float rate;
    public IncreaseDamage(GameEntity target, float rate = 0.5f) : base(target)
    {
        this.rate = rate;
        data = Resources.Load("ScriptableObjects/IncreaseDamage") as EffectData;
    }
    public override void OnAdd()
    {
        entity.damageRate *= rate;
    }

    public override void OnRemove()
    {
        entity.damageRate /= rate;
    }
}
