using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Weapon : ScriptableObject
{
    public Bullet ammunition;
    public float fullLoad;
    public float baseLoadSpeed;

    public float bulletVelocity;
    public float bulletDamage;
    public float bulletDuration;
    protected abstract void TryShoot(WeaponInterface weaponInterface);

    public class WeaponInterface 
    {
        private Weapon weapon;
        internal GameEntity owner;
        internal float load;

        internal bool lastShoot = false;
        internal bool shot = false;

        internal float fullLoad
        {
            get => weapon.fullLoad;
        }

        internal bool Continuous
        {
            get => lastShoot == shot;
        }
    
        public WeaponInterface(GameEntity owner, Weapon weapon)
        {
            this.owner = owner;
            this.weapon = weapon;
            load = 0;
        }

        public void Update()
        {
            if (load < weapon.fullLoad)
            {
                load += weapon.baseLoadSpeed * owner.loadSpeedScale * Time.deltaTime;
            }
            else
            {
                load = weapon.fullLoad;
            }
        }

        public void LateUpdate()
        {
            lastShoot = shot;
        }

        public void Shoot()
        {
            weapon.TryShoot(this);
            shot = true;
        }
    }
}