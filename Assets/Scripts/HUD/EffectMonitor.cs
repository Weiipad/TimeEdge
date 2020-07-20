using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EffectMonitor : MonoBehaviour
{
    public EffectSlot effectSlotPrefab;

    //[HideInInspector]
    public EffectSlot[] effectImages = new EffectSlot[12];
    // Start is called before the first frame update
    void Start()
    {
        for (var i = 0; i < effectImages.Length; i++)
        {
            effectImages[i] = Instantiate(effectSlotPrefab, transform.position, Quaternion.identity);
            effectImages[i].gameObject.transform.SetParent(transform, false);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
