using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MachineGun : Weapon
{
    public MachineGun(Player player) : base(player)
    {
        data = Resources.Load("ScriptableObjects/MachineGun") as WeaponData;
    }

    public override void Shoot()
    {
        Bullet bullet = Object.Instantiate(data.ammunition, player.transform.position, player.transform.rotation);
        bullet.weaponData = data;
        bullet.rigidbody.velocity = data.bulletVelocity * bullet.transform.up;
    }
}
