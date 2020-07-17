using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : GameEntity
{
    private Weapon weapon;
    protected override void Start()
    {
        base.Start();
        weapon = new MachineGun(this);
    }

    protected override void Update()
    {
        base.Update();
        weapon.Update();
    }
    protected override void OnTriggerEnter2D(Collider2D collision)
    {
        base.OnTriggerEnter2D(collision);
    }
}
