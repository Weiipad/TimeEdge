using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Entity : MonoBehaviour
{
    public int maxHP;
    public int currentHP;

    private void Start()
    {
        currentHP = maxHP;
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
    }

    private void Hurt(GunBullet bullet)
    {
        currentHP -= bullet.data.power;
        if (currentHP <= 0)
        {
            Destroy(gameObject);
        }
    }
}
