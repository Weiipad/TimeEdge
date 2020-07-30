using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Weapon : ScriptableObject
{
    public Bullet ammunition;
    public float fullLoad;
    public float baseLoadSpeed;

    public float bulletVelocity;
    public float bulletDamage;
    public float bulletDuration;

    protected GameEntity owner;
    protected float load;
    protected bool readyToFire = false;

    public float Load
    {
        get => load;
    }

    public void Equip(GameEntity owner)
    {
        this.owner = owner;
    }

    public virtual void Update()
    {
        load += baseLoadSpeed * owner.loadSpeedScale * Time.deltaTime;
        if (load >= fullLoad && readyToFire)
        {
            Shoot();
            load = 0;
        }
    }

    // Must be called before Update.
    public void Fire()
    {
        readyToFire = true;
    }

    public virtual void LateUpdate()
    {
        readyToFire = false;
    }

    protected abstract void Shoot();
}