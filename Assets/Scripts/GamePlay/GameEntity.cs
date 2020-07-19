using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameEntity : MonoBehaviour
{
    public float maxHP;
    public float currentHP;
    public float loadSpeedScale;
    public float defenseRate = 1f;
    public float damageRate = 1f;

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

    protected void Start()
    {
        currentHP = maxHP;
        anim = GetComponent<Animation>();

        //if (CompareTag("Enemy")) effects.Add(new HealOverTime(this, 5.0f));
    }

    protected void Update()
    {
        if (currentHP <= 0)
        {
            Destroy(gameObject);
        }
        foreach (var effect in effects)
        {
            effect.Update();
        }
    }

    protected void FixedUpdate()
    {
        foreach(var effect in effects)
        {
            if (effect.Deprecated) continue;
            effect.Affect();
        }
        
        effects.RemoveAll(e =>
        {
            if (e.Deprecated) 
            {
                e.OnRemove();
                return true;
            }
            return false;
        });
    }

    protected void OnTriggerEnter2D(Collider2D collision)
    {
        if ((gameObject.CompareTag("Enemy") && collision.CompareTag("Bullet0")) || (gameObject.CompareTag("Player") && collision.CompareTag("Bullet1")))
        {
            // 受伤
            Hurt(collision.GetComponent<Bullet>());
            Destroy(collision.gameObject);
        }
    }

    public void AddEffect(Effect effect)
    {
        effects.Add(effect);
        effect.OnAdd();
    }

    protected void Hurt(Bullet bullet)
    {
        var damage = bullet.damage*defenseRate;
        if (currentShield > 0)
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

        if (damage > 0)
        {
            currentHP -= damage;
        }
        
        anim.Play();
    }
}
