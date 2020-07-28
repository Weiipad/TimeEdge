using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IncreaseDefense : Effect
{
    private float rate;
    public IncreaseDefense(GameEntity target, float rate = 1.5f) : base(target)
    {
        this.rate = rate;
        data = Resources.Load("ScriptableObjects/EffectData/IncreaseDefense") as EffectData;
    }
    public override void OnAdd()
    {
        entity.defenseRate *= rate;
    }

    public override void OnRemove()
    {
        entity.defenseRate /= rate;
    }
}
