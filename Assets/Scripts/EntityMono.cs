using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EntityMono : MonoBehaviour
{
    public int maxHP;
    public int currentHP;

    public int maxShield;
    public int currentShield;
    private Animation anim;
    private void Start()
    {
        currentHP = maxHP;
        anim = GetComponent<Animation>();
    }

    /// <summary>
    /// 伤害
    /// </summary>
    /// <param name="collision">碰撞的碰撞体</param>
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if ((gameObject.CompareTag("Enemy") && collision.CompareTag("Bullet0")) || (gameObject.CompareTag("Player") && collision.CompareTag("Bullet1")))
        {
            // 受伤
            Hurt(collision.GetComponent<GunBullet>());
            Destroy(collision.gameObject);
        }
        else if(gameObject.CompareTag("Player") && collision.CompareTag("ShieldBuff"))
        {
            currentShield += 10;
            Destroy(collision.gameObject);
        }
    }

    private void Hurt(GunBullet bullet)
    {
        var damage = bullet.data.power;
        if(currentShield > 0)
        {
            if (currentShield > damage)
            {
                currentShield -= damage;
                damage = 0;
            }
            else
            {
                damage -= currentShield;
                currentShield = 0;
            }
        }

        if(damage != 0)
        {
            currentHP -= damage;
        }
        
        anim.Play();
        if (currentHP <= 0)
        {
            Destroy(gameObject);
        }
    }
}
