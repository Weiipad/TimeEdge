using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ButtonShowUp : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    Animation anim;
    void Start()
    {
        anim = GetComponent<Animation>();
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        anim["StartSceneButton"].speed = 1f;
        anim.Play();
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        if (anim.isPlaying)
            anim["StartSceneButton"].time = anim["StartSceneButton"].time;
        else
            anim["StartSceneButton"].time = anim["StartSceneButton"].length;
        anim["StartSceneButton"].speed = -1f;
        anim.Play();
    }
}
