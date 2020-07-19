using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public abstract class Bar : MonoBehaviour
{
    public enum BarDirect
    {
        vertical,
        horizontal,
    }

    /// <summary>
    /// 信息条的变化方向
    /// </summary>
    public BarDirect barDirect = BarDirect.horizontal;

    /// <summary>
    /// 镜像显示
    /// </summary>
    public bool Mirror = false;

    /// <summary>
    /// 开始时当前值是否为最大值
    /// </summary>
    public bool IsMaxValueOnStart = true;

    protected RectTransform currentBar;

    protected Image childImage;

    protected float maxBarWidth = 0f;
    protected float maxBarHeight = 0f;

    protected float preValue;
    protected float currentValue;
    protected float maxValue;

    protected abstract void BarInit();

    protected abstract void BarStatusUpdate();
    protected abstract float GetCurrentValue();
    protected abstract void UpdateMaxValue();

    public delegate void OnValueChangeDelegate();
    public event OnValueChangeDelegate OnValueChangeEvent;

    public void AddListener(OnValueChangeDelegate method)
    {
        OnValueChangeEvent += method;
    }

    public void DeleteListener(OnValueChangeDelegate method)
    {
        OnValueChangeEvent -= method;
    }

    protected void OnValueChange()
    {
        if (OnValueChangeEvent != null)
        {
            OnValueChangeEvent();
        }
    }
}
