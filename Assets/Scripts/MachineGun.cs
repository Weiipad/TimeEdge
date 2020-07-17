using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MachineGun : Weapon
{
    public MachineGun(GameEntity owner) : base(owner)
    {
        data = Resources.Load("ScriptableObjects/MachineGun") as WeaponData;
    }

    public override void Shoot()
    {
        Bullet bullet = Object.Instantiate(data.ammunition, owner.transform.position, owner.transform.rotation);
        bullet.isFromPlayer = owner.CompareTag("Player");
        bullet.rigidbody.velocity = bullet.data.velocity * bullet.transform.up;
    }
}
