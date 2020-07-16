using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Bullet", menuName = "Time Edge/BulletData")]
public class BulletData : ScriptableObject
{
    public Sprite sprite;
    public float velocity;
    public float duration;
    public int power;
}
