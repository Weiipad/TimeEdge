using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : GameEntity
{

    private Weapon weapon;
    public Weapon GetWeapon { get => weapon; }
    // Start is called before the first frame update
    protected override void Start()
    {
        base.Start();
        weapon = new MachineGun(this);
    }

    // Update is called once per frame
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
