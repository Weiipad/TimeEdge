using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NewMyLaserGun", menuName = "Time Edge/Weapon/MyLaserGun")]
public class MyLaserGun : Weapon
{
    Bullet light = null;

    public override void Update()
    {
        load += baseLoadSpeed * owner.loadSpeedScale * Time.deltaTime;
        if (readyToFire)
        {
            Shoot();
            load -= bulletVelocity * Time.deltaTime;
        }
        readyToFire = false;
        if (light != null) light.gameObject.SetActive(false);
    }

    protected override void Shoot()
    {
        if (light == null)
        {
            light = Instantiate(ammunition, owner.transform.position + owner.transform.up * 10, owner.transform.rotation);
            light.transform.SetParent(owner.transform);
            light.damage = bulletDamage;
            light.velocity = 0;
        }
        light.gameObject.SetActive(true);
    }
}
