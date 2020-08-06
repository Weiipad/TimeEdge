using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NewMachineGun", menuName = "Time Edge/Weapon/MachineGun")]
public class MachineGun : Weapon
{
    protected override void Shoot(WeaponInterface wi)
    {
        if (wi.load >= wi.fullLoad)
        {
            GenBullet(wi);
            wi.load = 0;
        }
    }
    private void GenBullet(WeaponInterface wi)
    {
        Bullet bullet = Instantiate(ammunition, wi.owner.transform.position + wi.owner.transform.up * 0.6f, wi.owner.transform.rotation);
        bullet.damage = bulletDamage * wi.owner.damageRate;
        bullet.velocity = bulletVelocity;
        bullet.duration = bulletDuration;
    }
}