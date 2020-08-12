using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Level : MonoBehaviour
{
    public float StartTime;
    public float Duration;
    public abstract void StartLevel();
    public abstract void EndLevel();
}
