using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletGenerate : MonoBehaviour
{
    public BulletSpawnMode mode;
    public Bullet bullet;

    public void OnDestory() {
        mode.Ganerate(gameObject, bullet.gameObject);
    }
}
