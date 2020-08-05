using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class WeaponPos : MonoBehaviour
{
    public Weapon weapon;
    private void Start()
    {
        
        if (weapon != null)
            weapon.Equip(GetComponent<GameEntity>());
    }
}
