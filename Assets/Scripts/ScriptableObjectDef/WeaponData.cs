using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

[CreateAssetMenu(fileName = "Bullet", menuName = "Time Edge/WeaponData")]
public class WeaponData : ScriptableObject
{
    public GameObject ammunition;
    public float fullLoad;
    public float baseLoadSpeed;
}