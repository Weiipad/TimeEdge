using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NewMachineGun", menuName = "Time Edge/Weapon/MachineGun")]
public class MachineGun : Weapon
{
    protected override void Shoot()
    {
        Bullet bullet = Instantiate(ammunition, owner.transform.position + owner.transform.up * 0.6f, owner.transform.rotation);
        bullet.damage = bulletDamage * owner.damageRate;
        bullet.velocity = bulletVelocity;
        bullet.duration = bulletDuration;
    }
}
