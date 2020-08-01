using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NewEnemyLaserGun", menuName = "Time Edge/Weapon/EnemyLaserGun")]
public class EnemyLaserGun : Weapon
{
    private float powerLoad = 0f;
    public float powerLoadSpeed = 1f;

    private float offSet;
    private float curDuration = 0f;

    private Bullet bullet = null;
    private Bullet laser = null;

    private bool isShoot = false;
    private bool isPowerLoad = false;
    public override void Update()
    {
        if (load < fullLoad && !isShoot)
            load += baseLoadSpeed * owner.loadSpeedScale * Time.deltaTime;
        if(!isShoot && load >= fullLoad)
        {
            Shoot();
        }

        if(isShoot)
        {
            curDuration += Time.deltaTime;
            if(curDuration >= bulletDuration)
            {
                load = 0f;
                curDuration = 0f;
                isShoot = false;
                if (bullet != null)
                    Destroy(bullet.gameObject);
                bullet = null;
                laser = null;
            }
            else
            {
                if(bullet == null)
                {
                    isShoot = false;
                    load = 0f;
                    curDuration = 0f;
                }
            }
        }
    }

    protected override void Shoot()
    {
        if (laser == null)
        {
            if(bullet != null)
            {
                Destroy(bullet.gameObject);
                bullet = null;
            }
            bullet = Instantiate(ammunition, owner.transform.position, owner.transform.rotation);
            bullet.transform.parent = owner.transform;
            bullet.damage = bulletDamage;
            bullet.duration = Mathf.Infinity;
            

            laser = bullet.gameObject.transform.GetChild(0).GetComponent<Bullet>();
            laser.damage = bulletDamage;
            laser.duration = Mathf.Infinity;
        }

        offSet = powerLoadSpeed * Time.deltaTime * Mathf.Abs(bullet.transform.localScale.x - 1f);

        if (bullet != null && bullet.transform.localScale.x < 1f)
        {
            powerLoad += powerLoadSpeed * Time.deltaTime;
            bullet.transform.localScale = new Vector3(bullet.transform.localScale.x + offSet, bullet.transform.localScale.y + offSet, 0f);
            if (powerLoad >= 1f)
            {
                isPowerLoad = true;
            }
        }
        if (laser != null && isPowerLoad && laser.transform.localScale.y <= 10f)
        {
            laser.transform.localScale = new Vector3(laser.transform.localScale.x, laser.transform.localScale.y + Time.deltaTime * bulletVelocity);
            if (laser.transform.localScale.y >= 10f)
            {
                isShoot = true;
                isPowerLoad = false;
            }
        }
    }
}
