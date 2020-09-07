using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RadialSpawn : IBulletSpawnMode
{
    private readonly int bulletCount;

    public RadialSpawn(int bulletCount)
    {
        this.bulletCount = bulletCount;
    }

    public void Generate(GameObject parent, Bullet bullet)
    {
        float deltaAngle = 360.0f / bulletCount;
        float currentAngle = 0;
        for (int i = 0; i < bulletCount; i++)
        {
            Object.Instantiate(bullet, parent.transform.position, Quaternion.Euler(0, 0, currentAngle));
            currentAngle += deltaAngle;
        }
    }
}
