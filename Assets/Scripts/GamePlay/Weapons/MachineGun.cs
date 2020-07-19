using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MachineGun : Weapon
{
    public MachineGun(GameEntity owner, string dataPath = "ScriptableObjects/MachineGun") : base(owner)
    {
        data = Resources.Load(dataPath) as WeaponData;
    }

    protected override void Shoot()
    {
        Bullet bullet = Object.Instantiate(data.ammunition, owner.transform.position, owner.transform.rotation);
        bullet.damage = data.bulletDamage * owner.damageRate;
        bullet.rigidbody.velocity = data.bulletVelocity * bullet.transform.up;
        bullet.weaponData = data;
    }
}
