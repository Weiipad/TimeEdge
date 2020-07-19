﻿using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class GameEntityBar : Bar
{
    public enum EntityFrom
    {
        tag,
        gameObject,
    }

    public enum BarType
    {
        blood,
        shield,
        loadBullet,
    }

    /// <summary>
    /// 显示的类别
    /// </summary>
    public BarType barType = BarType.blood;

    /// <summary>
    /// 实体数据来自标签或游戏物体
    /// </summary>
    public EntityFrom entityFrom = EntityFrom.tag;

    /// <summary>
    /// 游戏物体标签，按照标签寻找物体
    /// </summary>
    public string EntityTag;

    /// <summary>
    /// 指定的游戏物体
    /// </summary>
    public GameObject targetEntityObject;

    public Color BloodBarColor = Color.red;
    public Color ShieldBarColor = Color.blue;
    public Color LoadBulletColor = Color.gray;

    private GameEntity targetEntity;
    private Weapon weapon;
    private bool isGetEntity;

    private void Start()
    {
        BarInit();
    }

    private void FixedUpdate()
    {
        BarStatusUpdate();
    }

    protected override void BarInit()
    {
        childImage = transform.GetChild(0).GetComponent<Image>();
        if (childImage.sprite == null)
            childImage.sprite = Resources.Load<Sprite>("Pictures/WhiteBoard");

        if (childImage.type != Image.Type.Filled)
        {
            childImage.type = Image.Type.Filled;
        }
        if (entityFrom == EntityFrom.tag)
        {
            if (EntityTag != null)
            {
                targetEntity = GameObject.FindGameObjectWithTag(EntityTag).GetComponent<GameEntity>();
                if (targetEntity != null)
                    isGetEntity = true;
            }
        }
        else if(entityFrom == EntityFrom.gameObject)
        {
            if(targetEntityObject != null)
            {
                targetEntity = GameObject.FindGameObjectWithTag(EntityTag).GetComponent<GameEntity>();
                if(targetEntity != null)
                    isGetEntity = true;
            }
        }

        switch (barType)
        {
            case BarType.blood: childImage.color = BloodBarColor; break;
            case BarType.shield: childImage.color = ShieldBarColor; break;
            case BarType.loadBullet: childImage.color = LoadBulletColor; break;
            default: break;
        }

        if (barType == BarType.loadBullet && EntityTag != "Player")
        {
            throw new System.Exception("只有玩家才能使用武器装填信息条");
        }

        UpdateMaxValue();

        currentValue = GetCurrentValue();
    }

    protected override void BarStatusUpdate()
    {
        if (targetEntity == null || maxValue.Equals(0f) || !isGetEntity)
        {
            if (targetEntity == null)
            {
                if (isGetEntity)
                {
                    Destroy(gameObject);
                }
            }
            return;
        }

        UpdateMaxValue();
        preValue = currentValue;
        currentValue = GetCurrentValue();

        if (!preValue.Equals(currentValue))
        {
            OnValueChange();
        }
        float percent = 0f;
        if (!maxValue.Equals(0f))
            percent = Mathf.Clamp(currentValue / maxValue, 0f, 1f);
        childImage.fillAmount = percent;
    }

    protected override float GetCurrentValue()
    {
        float current = 0f;
        if (barType == BarType.blood)
            current = targetEntity.currentHP;
        else if (barType == BarType.shield)
            current = targetEntity.currentShield;
        else if (barType == BarType.loadBullet)
        {
            if (weapon == null)
            {
                weapon = targetEntity.gameObject.GetComponent<Player>().GetWeapon;
                if (weapon == null)
                    return current;
            }
            current = weapon.Load;
        }
        return current;
    }

    protected override void UpdateMaxValue()
    {
        if(!isGetEntity)
        {
            BarInit();
        }

        if (barType == BarType.blood)
            maxValue = targetEntity.maxHP;
        else if (barType == BarType.shield)
            maxValue = targetEntity.maxShield;
        else if (barType == BarType.loadBullet)
        {
            if (weapon == null)
            {
                weapon = targetEntity.gameObject.GetComponent<Player>().GetWeapon;
                if (weapon == null)
                    return;
            }
            maxValue = weapon.FullLoad;
        }
    }
}