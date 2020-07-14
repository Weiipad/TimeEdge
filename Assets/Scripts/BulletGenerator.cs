using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletGenerator : MonoBehaviour
{
    public GameObject bullet;
    public void Shoot()
    {
        Instantiate(bullet, transform.position, transform.rotation);
    }
}
