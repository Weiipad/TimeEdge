using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameEntityBar : Bar
{
    public enum BarType
    {
        none,
        blood,
        shield,
        loadBullet,
    }

    /// <summary>
    /// 显示的类别
    /// </summary>
    public BarType barType = BarType.none;

    public Color BloodBarColor = Color.red;
    public Color ShieldBarColor = Color.blue;
    public Color LoadBulletColor = Color.gray;

    /// <summary>
    /// 游戏物体标签，按照标签寻找物体
    /// </summary>
    public string EntityTag;

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
        currentBar = transform.GetChild(0).GetComponent<RectTransform>();
        if (Mirror)
        {
            transform.rotation = Quaternion.Euler(new Vector3(0f, 0f, 180f));
        }
        else
        {
            transform.rotation = Quaternion.Euler(new Vector3(0f, 0f, 0f));

        }

        childImage = transform.GetChild(0).GetComponent<Image>();

        var rectTransformSizeDelta = GetComponent<RectTransform>().sizeDelta;
        if (rectTransformSizeDelta != Vector2.zero)
        {
            maxBarWidth = rectTransformSizeDelta.x;
            maxBarHeight = rectTransformSizeDelta.y;
            currentBar.sizeDelta = rectTransformSizeDelta;
            currentBar.anchoredPosition = new Vector2(0f, -rectTransformSizeDelta.y);
        }

        if (EntityTag != null)
        {
            targetEntity = GameObject.FindGameObjectWithTag(EntityTag).GetComponent<GameEntity>();
            isGetEntity = true;
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
        if (barType == BarType.none || targetEntity == null || currentBar == null || maxBarWidth.Equals(0f) || maxValue.Equals(0f))
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
        float percent = currentValue / maxValue;
        if (currentValue.Equals(0f))
            percent = 0f;
        if (percent.Equals(1f) || percent > 1f)
            percent = 1f;
        if (barDirect == BarDirect.horizontal)
        {
            float newWidth = maxBarWidth * percent;
            currentBar.sizeDelta = new Vector2(newWidth, currentBar.sizeDelta.y);
        }
        else if (barDirect == BarDirect.vertical)
        {
            float newHeight = maxBarHeight * percent;
            currentBar.sizeDelta = new Vector2(currentBar.sizeDelta.x, newHeight);
        }
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
