using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

[CreateAssetMenu(fileName = "NewWeaponData", menuName = "Time Edge/WeaponData")]
public class WeaponData : ScriptableObject
{
    public Bullet ammunition;
    public float fullLoad;
    public float baseLoadSpeed;

    // Bullet data
    public float bulletVelocity;
    public float bulletDamage;
    public float bulletDuration;
}