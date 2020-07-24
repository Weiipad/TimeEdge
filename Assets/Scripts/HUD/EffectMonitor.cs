using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EffectMonitor : MonoBehaviour
{
    public EffectSlot effectSlotPrefab;
    public GameEntity target;

    public EffectSlot[] effectImages;
    
    
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
        if (target == null) return;

        var end = target.effects.Count - 1;
        for (var i = 0; i < effectImages.Length; i++)
        {
            effectImages[i].data = end >= i ? target.effects[end - i].data : null;
            effectImages[i].InitSlot();
            if (end >= i) effectImages[i].timeElapsed = target.effects[end - i].timeCount;
        }
    }
}
