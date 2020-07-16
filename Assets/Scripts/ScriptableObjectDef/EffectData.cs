using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NewEffectData", menuName = "Time Edge/EffectData")]
public class EffectData : ScriptableObject
{
    // if the duration <= 0 then it is a permanent effect
    public float duration;
}
