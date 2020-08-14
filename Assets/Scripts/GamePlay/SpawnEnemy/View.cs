using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class View : MonoBehaviour
{
    public Text targetText;

    // Update is called once per frame
    protected virtual void Update()
    {
        targetText.text = GetContain();
    }

    public virtual string GetContain()
    {
        return null;
    }
}
