using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameEntity : MonoBehaviour
{
    public float maxHP;
    public float currentHP;
    public float loadSpeedScale;

    public float HP 
    {
        get => currentHP;
        set 
        {
            if (value >= maxHP) currentHP = maxHP;
            else if (value >= 0) currentHP = value;
            else currentHP = 0;
        }
    }

    public float maxShield;
    public float currentShield;

    private List<Effect> effects = new List<Effect>();

    // Performance data
    private Animation anim;

    protected virtual void Start()
    {
        currentHP = maxHP;
        anim = GetComponent<Animation>();

        //if (CompareTag("Enemy")) effects.Add(new HealOverTime(this, 5.0f));
    }

    protected virtual void Update()
    {
        foreach(var effect in effects)
        {
            effect.Update();
        }
    }

    protected virtual void FixedUpdate()
    {
        foreach(var effect in effects)
        {
            if (effect.Deprecated) continue;
            effect.Affect();
        }
        
        effects.RemoveAll(e => e.Deprecated);
    }

    protected virtual void OnTriggerEnter2D(Collider2D collision)
    {
        Bullet bullet = collision.GetComponent<Bullet>();
        if ((gameObject.CompareTag("Enemy") && bullet.isFromPlayer) || (gameObject.CompareTag("Player") && !bullet.isFromPlayer))
        {
            // 受伤
            Hurt(collision.GetComponent<Bullet>());
            Destroy(collision.gameObject);
        }
        else if(gameObject.CompareTag("Player") && collision.CompareTag("ShieldBuff"))
        {
            currentShield += 10;
            Destroy(collision.gameObject);
        }
    }

    public void AddEffect(Effect effect)
    {
        effects.Add(effect);
    }

    protected void Hurt(Bullet bullet)
    {
        var damage = bullet.weaponData.bulletDamage;
        if(currentShield > 0)
        {
            if (currentShield > damage)
            {
                currentShield -= damage;
                damage /= 2;
            }
            else
            {
                damage -= currentShield / 2;
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
