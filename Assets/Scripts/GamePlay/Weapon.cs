using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Weapon
{
    protected GameEntity owner;
    protected WeaponData data = null;
    protected float load;

    private bool readyToFire = false;

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
        if (load >= data.fullLoad && readyToFire)
        {
            Shoot();
            load = 0;
        }
        readyToFire = false;
    }

    // Must be called before Update.
    public void Fire()
    {
        readyToFire = true;
    }
    
    protected abstract void Shoot();
}