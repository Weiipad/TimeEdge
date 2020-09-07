using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class RadialSpawn : BulletGenerate
{
    public int bulletCount;

    private void OnDestroy()
    {
        float deltaAngle = 360.0f / bulletCount;
        float currentAngle = 0;
        for (int i = 0; i < bulletCount; i++)
        {
            Instantiate(bullet, transform.position, Quaternion.Euler(0, 0, currentAngle));
            currentAngle += deltaAngle;
        }
    }
}
