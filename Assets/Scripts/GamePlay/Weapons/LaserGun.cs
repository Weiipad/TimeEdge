using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NewLaserGun", menuName = "Time Edge/Weapon/LaserGun")]
public class LaserGun : Weapon
{
    private float powerLoad = 0f;
    public float powerLoadSpeed = 1f;

    private float offSet;
    private bool isPowerLoad = false;

    private Bullet bullet = null;
    private Bullet laser = null;

    protected override void Shoot()
    {
        if (laser == null)
        {
            if(bullet != null)
            {
                Destroy(bullet.gameObject);
                bullet = null;
            }
            bullet = Object.Instantiate(ammunition, owner.transform.position, owner.transform.rotation);
            bullet.transform.parent = owner.transform;
            bullet.damage = bulletDamage;
            bullet.duration = bulletDuration;
            offSet = powerLoadSpeed * Time.deltaTime * Mathf.Abs(bullet.transform.localScale.x - 0.6f);

            laser = bullet.gameObject.transform.GetChild(0).GetComponent<Bullet>();
            laser.damage = bulletDamage;
            laser.duration = bulletDuration;
        }

        if (bullet != null && bullet.transform.localScale.x <0.6f)
        {
            powerLoad += powerLoadSpeed * Time.deltaTime;
            bullet.transform.localScale = new Vector3(bullet.transform.localScale.x + offSet, bullet.transform.localScale.y + offSet, 0f);
            if (powerLoad >= 1f)
            {
                isPowerLoad = true;
            }
        }
        if (laser != null && isPowerLoad && laser.transform.localScale.y <= 200f)
        {
            laser.transform.localScale = new Vector3(laser.transform.localScale.x, laser.transform.localScale.y + Time.deltaTime * bulletVelocity);
            laser.transform.localPosition = new Vector3(laser.transform.localPosition.x, laser.transform.localPosition.y + Time.deltaTime * bulletVelocity * 0.05f);
        }
    }
}
