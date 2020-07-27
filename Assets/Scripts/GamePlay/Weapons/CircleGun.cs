using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CircleGun : Weapon
{
    public CircleGun(GameEntity owner, string dataPath = "ScriptableObjects/CircleGun") : base(owner)
    {
        data = Resources.Load<WeaponData>(dataPath);
    }

    protected override void Shoot()
    {
        float angleRate = 10f;
        float angle = 0f;
        for(int i = 0;i < 360f / angleRate; i ++)
        {
            Bullet bullet = Object.Instantiate(data.ammunition, owner.transform.position, owner.transform.rotation);
            bullet.damage = data.bulletDamage * owner.damageRate;
            bullet.transform.rotation = Quaternion.Euler(new Vector3(0f, 0f, angle));
            bullet.rigidbody.velocity = data.bulletVelocity * bullet.transform.up;
            bullet.weaponData = data;
            angle += angleRate;
        }
    }
}
