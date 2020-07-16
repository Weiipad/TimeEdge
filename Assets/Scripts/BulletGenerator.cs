using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletGenerator : MonoBehaviour
{
    public GameObject bullet;
    public int bulletNumber;
    public virtual void Shoot()
    {
        Quaternion old = transform.rotation;
        float angle;
        if (bulletNumber % 2 == 0)
        {
            angle = 360 - ((bulletNumber / 2 - 1) * 10 + 5);
        }
        else
        {
            angle =360 - ((bulletNumber / 2) * 10);
        }
        for (int i = 1; i <= bulletNumber; i++)
        {
            transform.Rotate(new Vector3(0, 0, angle));
            var bulletGO = Instantiate(bullet, transform.position, transform.rotation);
            bulletGO.GetComponent<Rigidbody2D>().velocity = bulletGO.GetComponent<Bullet>().data.velocity * Time.deltaTime * transform.up;
            transform.rotation = old;
            angle += 10;
        }
        transform.rotation = old;
    }
}
