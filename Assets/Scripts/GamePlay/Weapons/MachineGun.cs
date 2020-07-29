using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NewMachineGun", menuName = "Time Edge/Weapon/MachineGun")]
public class MachineGun : Weapon
{
    protected override void Shoot()
    {
        Bullet bullet = Object.Instantiate(ammunition, owner.transform.position, owner.transform.rotation);
        bullet.damage = bulletDamage * owner.damageRate;
        bullet.rigidbody.velocity = bulletVelocity * bullet.transform.up;
        bullet.duration = bulletDuration;
    }
}
