using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NewCircleGun", menuName = "Time Edge/Weapon/CircleGun")]
public class CircleGun : Weapon
{
    private float startAngle = 0f;
    public float angleRate = 10f;
    public float angleOffset = 3f;
    protected override void TryShoot(WeaponInterface wi)
    {
        if (wi.load >= wi.fullLoad)
        {
            GenBullet(wi);
            wi.load = 0;
        }
    }

    private void GenBullet(WeaponInterface wi)
    {
        
        float angle = 0f + startAngle;
        for (int i = 0; i < 360f / angleRate; i++)
        {
            Bullet bullet = Object.Instantiate(ammunition, wi.owner.transform.position, wi.owner.transform.rotation);
            bullet.damage = bulletDamage * wi.owner.damageRate;
            bullet.transform.rotation = Quaternion.Euler(new Vector3(0f, 0f, angle));
            bullet.velocity = bulletVelocity;
            bullet.duration = bulletDuration;
            angle += angleRate;
        }
        startAngle += angleOffset;
    }
}
