using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NewMyLaserGun", menuName = "Time Edge/Weapon/MyLaserGun")]
public class MyLaserGun : Weapon
{
    protected override void Shoot(WeaponInterface wi)
    {
        if (wi.load > 0) 
        {
            var light = Instantiate(ammunition, wi.owner.transform.position + wi.owner.transform.up * 10.3f, wi.owner.transform.rotation);
            light.transform.SetParent(wi.owner.transform);
            light.damage = bulletDamage;
            light.velocity = 0;
            light.duration = bulletDuration;

            wi.load -= bulletVelocity * Time.deltaTime;
        }
    }
}