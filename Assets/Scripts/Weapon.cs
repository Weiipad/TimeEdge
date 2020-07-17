using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Weapon
{
    protected GameEntity owner;

    // TODO: WeaponData -- A ScriptableObject class to describe basic data of a weapon.
    protected WeaponData data = null;
    protected float load;

    public float Load
    {
        get => load;
    }

    public float FullLoad
    {
        get => data.fullLoad;
    }
    public Weapon(GameEntity owenr)
    {
        this.owner = owenr;
    }

    public void Update()
    {
        if (data == null) return;

        load += data.baseLoadSpeed * owner.loadSpeedScale * Time.deltaTime;
        if (load >= data.fullLoad && Input.GetKey(KeyCode.Mouse0))
        {
            Shoot();
            load = 0;
        }

    }

    public abstract void Shoot();
}