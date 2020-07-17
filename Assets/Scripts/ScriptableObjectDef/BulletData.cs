using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NewBulletData", menuName = "Time Edge/BulletData")]
public class BulletData : ScriptableObject
{
    public float velocity;
    public float damage;
    public float duration;
}
