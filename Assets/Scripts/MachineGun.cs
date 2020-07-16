using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MachineGun : Weapon
{
    public MachineGun(Player player) : base(player)
    {
        data = Resources.Load("Resources/MachineGun") as WeaponData;
    }

    public override void Shoot()
    {
        if (load >= data.fullLoad)
        {
            Object.Instantiate(data.ammunition, player.transform.position, player.transform.rotation);
        }
    }
}
