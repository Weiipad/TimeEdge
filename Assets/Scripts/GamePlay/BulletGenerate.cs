using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletGenerate : MonoBehaviour
{
    public IBulletSpawnMode mode;
    public Bullet bullet;

    public void OnDestory() {
        mode.Generate(gameObject, bullet);
    }
}
