using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IBulletSpawnMode
{
    void Generate(GameObject parent, BaseBullet bullet);
}
