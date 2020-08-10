using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyLaserBullet : Bullet
{

    private void OnTriggerStay2D(Collider2D collision)
    {
        if ((collision.CompareTag("Enemy") && gameObject.CompareTag("Bullet0")) || (collision.CompareTag("Player") && gameObject.CompareTag("Bullet1")))
        {
            OnCollide(collision.GetComponent<GameEntity>());
        }
    }

    private new void OnTriggerEnter2D(Collider2D collision)
    {
    }

    protected override void OnCollide(GameEntity e)
    {
        e.Hurt(this);
    }
}
