using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public abstract class Bar : MonoBehaviour
{
    protected Image childImage;

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
