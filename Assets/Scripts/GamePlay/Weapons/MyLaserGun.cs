using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NewMyLaserGun", menuName = "Time Edge/Weapon/MyLaserGun")]
public class MyLaserGun : Weapon
{
    Bullet light = null;
    public override void Update()
    {
        if (load <= fullLoad) load += baseLoadSpeed * owner.loadSpeedScale * Time.deltaTime;
        else load = fullLoad;
        if (readyToFire)
        {
            if (load > -0.1) load -= bulletVelocity * Time.deltaTime;
            if (load >= 0) Shoot();
        }
        
    }

    protected override void Shoot()
    {
        if (light == null) 
        {
            light = Instantiate(ammunition, owner.transform.position + owner.transform.up * 10.3f, owner.transform.rotation);
            light.transform.SetParent(owner.transform);
            light.damage = bulletDamage;
            light.velocity = 0;
            light.duration = bulletDuration;
        }
    }
}
