using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EffectSlot : MonoBehaviour
{
    [HideInInspector]
    public EffectData data;
    [HideInInspector]
    public float timeElapsed = 0;
    public Image image;
    
    public void ResetSlot()
    {
        image.sprite = data.image;
        timeElapsed = 0;
    }

    void Start()
    {
        if (data != null) ResetSlot();
        else gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (data == null) return;
        image.fillAmount = 1 - timeElapsed / data.duration;
    }
}
