using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Bar : MonoBehaviour
{
    public enum BarType
    {
        none,
        blood,
        shield,
        loadBullet,
    }

    public enum BarDirect
    {
        vertical,
        horizontal,
    }

    /// <summary>
    /// 显示的类别
    /// </summary>
    public BarType barType = BarType.none;

    /// <summary>
    /// 信息条的变化方向
    /// </summary>
    public BarDirect barDirect = BarDirect.horizontal;

    public Color BloodBarColor = Color.red;
    public Color ShieldBarColor = Color.blue;
    public Color LoadBulletColor = Color.gray;

    /// <summary>
    /// 游戏物体标签，按照标签寻找物体
    /// </summary>
    public string EntityTag;

    /// <summary>
    /// 镜像显示
    /// </summary>
    public bool Mirror = false;

    private GameEntity targetEntity;
    private Weapon weapon;
    private bool isGetEntity;

    private RectTransform currentBar;

    private Image childImage;

    private float maxBarWidth = 0f;
    private float maxBarHeight = 0f;

    private void Start()
    {
        if (EntityTag != null)
        {
            targetEntity = GameObject.FindGameObjectWithTag(EntityTag).GetComponent<GameEntity>();
            isGetEntity = true;
        }

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
        Debug.Log(rectTransformSizeDelta);

        if (rectTransformSizeDelta != Vector2.zero)
        {
            maxBarWidth = rectTransformSizeDelta.x;
            maxBarHeight = rectTransformSizeDelta.y;
            currentBar.sizeDelta = rectTransformSizeDelta;
            currentBar.anchoredPosition = new Vector2(0f, -rectTransformSizeDelta.y);
        }

        switch(barType)
        {
            case BarType.blood      :childImage.color = BloodBarColor;break;
            case BarType.shield     :childImage.color = ShieldBarColor;break;
            case BarType.loadBullet :childImage.color = LoadBulletColor;break;
            default:break;
        }

        if(barType == BarType.loadBullet && EntityTag != "Player")
        {
            throw new System.Exception("只有玩家才能使用武器装填信息条");
        }
    }

    private void FixedUpdate()
    {
        BarStatusUpdate();
    }

    /// <summary>
    /// 暂时无法获取武器数据，会出错
    /// </summary>
    private void BarStatusUpdate()
    {
        if (barType == BarType.none || targetEntity == null || currentBar == null || maxBarWidth.Equals(0f))
        {
            if(targetEntity == null)
            {
                if (isGetEntity)
                {
                    Destroy(gameObject);
                }
            }
            return;
        }
        float current;
        float max;
        if(barType == BarType.blood)
        {
            current = targetEntity.currentHP;
            max = targetEntity.maxHP;
        }
        else if(barType == BarType.shield)
        {
            current = targetEntity.currentShield;
            max = targetEntity.maxShield;
        }
        else if(barType == BarType.loadBullet)
        {
            if(weapon == null)
            {
                weapon = targetEntity.gameObject.GetComponent<Player>().GetWeapon;
                if (weapon == null)
                    return;
            }
            current = weapon.Load;
            max = weapon.FullLoad;
        }
        else
        {
            return;
        }

        float percent = current / max;
        if (current.Equals(0f))
            percent = 0f;
        if(percent.Equals(1f) || percent > 1f)
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
}
