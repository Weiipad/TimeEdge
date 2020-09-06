using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NewEnemyLaserGun", menuName = "Time Edge/Weapon/EnemyLaserGun")]
public class EnemyLaserGun : Weapon
{
    private float powerLoad = 0f;
    public float powerLoadSpeed = 1f;

    private float curDuration = 0f;
    private bool isShoot = false;
    private bool isPowerLoad = false;

    protected override void TryShoot(WeaponInterface wi)
    {
        if (wi.load < fullLoad && !isShoot)
            wi.load += baseLoadSpeed * wi.owner.loadSpeedScale * Time.deltaTime;
        Bullet bullet = null;
        Bullet laser = null;
        if(wi.owner.transform.childCount > 0)
        {
            bullet = wi.owner.transform.GetChild(0).GetComponent<Bullet>();
            if(wi.owner.transform.GetChild(0).transform.childCount > 0)
            {
                laser = wi.owner.transform.GetChild(0).transform.GetChild(0).GetComponent<Bullet>();
            }
        }
        float offSet;
        if (isShoot)
        {
            curDuration += Time.deltaTime;
            if (curDuration >= bulletDuration)
            {
                wi.load = 0f;
                curDuration = 0f;
                isShoot = false;
                if (bullet != null)
                    Destroy(bullet.gameObject);
                bullet = null;
                laser = null;
            }
        }

        if (wi.load >= wi.fullLoad)
        {
            if (laser == null)
            {
                if (bullet != null)
                {
                    Destroy(bullet.gameObject);
                    bullet = null;
                }
                bullet = Instantiate(ammunition);
                bullet.transform.parent = wi.owner.transform;
                bullet.transform.localPosition = Vector3.zero;
                Vector3 parentRotation = wi.owner.transform.rotation.eulerAngles;
                bullet.transform.rotation = Quaternion.Euler(new Vector3(parentRotation.x, parentRotation.y, parentRotation.z + 180.0f));
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
}
