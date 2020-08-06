using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AboutWindow : MonoBehaviour
{
    public delegate void OnAboutWindowActiveFalse();
    public event OnAboutWindowActiveFalse OnAboutWindowActive;

    public void AboutWindowOK()
    {
        gameObject.SetActive(false);
        if (OnAboutWindowActive != null)
            OnAboutWindowActive();
    }
}
