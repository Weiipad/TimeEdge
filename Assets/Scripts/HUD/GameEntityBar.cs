using System.Collections;
using System.Collections.Generic;
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
        hp,
        shield,
        loadBullet,
    }

    /// <summary>
    /// 显示的类别
    /// </summary>
    public BarType barType = BarType.hp;

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
    private Weapon.WeaponInterface weapon;
    private bool isGetEntity;

    private void OnEnable()
    {
        
    }

    private void FixedUpdate()
    {
        if (GameStatus.IsPauseGame())
            return;
        if(isGetEntity == false)
        {
            BarInit();
        }
        BarStatusUpdate();
    }

    protected override void BarInit()
    {
        childImage = transform.GetChild(0).GetComponent<Image>();
        if (childImage.sprite == null)
        {
            childImage.sprite = Resources.Load<Sprite>("Pictures/WhiteBoard");
            childImage.type = Image.Type.Filled;
            childImage.fillMethod = Image.FillMethod.Horizontal;
        }

        if (childImage.type != Image.Type.Filled)
        {
            childImage.type = Image.Type.Filled;
        }

        childText = transform.GetChild(1).GetComponent<Text>();

        if (entityFrom == EntityFrom.tag)
        {
            if (EntityTag != null && EntityTag != "")
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
                targetEntity = targetEntityObject.GetComponent<GameEntity>();
                if(targetEntity != null)
                    isGetEntity = true;
            }
        }

        switch (barType)
        {
            case BarType.hp: childImage.color = BloodBarColor; break;
            case BarType.shield: childImage.color = ShieldBarColor; break;
            case BarType.loadBullet: childImage.color = LoadBulletColor; break;
            default: break;
        }

        UpdateMaxValue();

        currentValue = GetCurrentValue();

        isGetEntity = true;
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
            throw new System.Exception("Can't get target!");
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
        UpdateText(currentValue, maxValue);
    }

    protected override float GetCurrentValue()
    {
        float current = 0f;
        if (barType == BarType.hp)
            current = targetEntity.currentHP;
        else if (barType == BarType.shield)
            current = targetEntity.currentShield;
        else if (barType == BarType.loadBullet)
        {
            if (weapon == null)
            {
                weapon = targetEntity.gameObject.GetComponent<WeaponHolder>().wi;
                if (weapon == null)
                    throw new System.Exception("Can't get weapon!");
            }
            current = weapon.load;
        }
        return current;
    }

    protected override void UpdateMaxValue()
    {
        if(!isGetEntity)
        {
            BarInit();
        }

        if (barType == BarType.hp)
            maxValue = targetEntity.maxHP;
        else if (barType == BarType.shield)
            maxValue = targetEntity.maxShield;
        else if (barType == BarType.loadBullet)
        {
            if (weapon == null)
            {
                weapon = targetEntity.gameObject.GetComponent<Player>().wi;
                if (weapon == null)
                    throw new System.Exception("Can't get weapon!");
            }
            maxValue = weapon.fullLoad;
        }
    }

    private void UpdateText(float currentValue, float maxValue)
    {
        switch(barType)
        {
            case BarType.hp:
                childText.text = $"HP:{currentValue:F0}/{maxValue:F0}";
                break;
            case BarType.shield:
                childText.text = $"Shield:{currentValue:F0}/{maxValue:F0}";
                break;
            case BarType.loadBullet:
                childText.text = $"Load:{currentValue:F2}/{maxValue:F0}";
                break;
        }
    }
}
