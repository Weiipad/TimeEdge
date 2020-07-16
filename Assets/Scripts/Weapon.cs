using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Weapon
{
    protected Player player;

    // TODO: WeaponData -- A ScriptableObject class to describe basic data of a weapon.
    protected WeaponData data;
    protected float load;
    public Weapon(Player player)
    {
        this.player = player;
    }

    public void Update()
    {
        load += data.baseLoadSpeed * player.loadSpeedScale * Time.deltaTime;
    }

    public abstract void Shoot();
}