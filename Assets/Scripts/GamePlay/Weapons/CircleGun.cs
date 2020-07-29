using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NewCircleGun", menuName = "Time Edge/Weapon/CircleGun")]
public class CircleGun : Weapon
{
    protected override void Shoot()
    {
        float angleRate = 10f;
        float angle = 0f;
        for(int i = 0; i < 360f / angleRate; i ++)
        {
            Bullet bullet = Object.Instantiate(ammunition, owner.transform.position, owner.transform.rotation);
            bullet.damage = bulletDamage * owner.damageRate;
            bullet.transform.rotation = Quaternion.Euler(new Vector3(0f, 0f, angle));
            bullet.rigidbody.velocity = bulletVelocity * bullet.transform.up;
            bullet.duration = bulletDuration;
            angle += angleRate;
        }
    }
}
