using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NewMyLaserGun", menuName = "Time Edge/Weapon/MyLaserGun")]
public class MyLaserGun : Weapon
{
    Bullet light = null;
    protected override void TryShoot(WeaponInterface wi)
    {
        if (wi.load > wi.fullLoad / 4f || (wi.Continuous && wi.load > 0)) 
        {
            if (light == null)
            {
                light = Instantiate(ammunition, wi.owner.transform.position + wi.owner.transform.up * 10.3f, wi.owner.transform.rotation);
                light.transform.SetParent(wi.owner.transform);
                light.damage = bulletDamage;
                light.velocity = 0;
                light.duration = bulletDuration;
            }

            wi.load -= bulletVelocity * Time.deltaTime;
        }
    }
}