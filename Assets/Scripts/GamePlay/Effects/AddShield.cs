using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AddShield : Effect
{
    private float quantity;
    public AddShield(GameEntity target, float quantity = 10f) : base(target)
    {
        this.quantity = quantity;
        data = Resources.Load("ScriptableObjects/AddShield") as EffectData;
    }

    public override void OnAdd()
    {
        entity.maxShield += quantity;
        entity.currentShield += quantity;
    }

    public override void OnRemove()
    {
        entity.maxShield -= quantity;
    }
}
