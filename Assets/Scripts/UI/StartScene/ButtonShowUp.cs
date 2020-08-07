using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ButtonShowUp : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    new Animation animation;
    Button button;
    void Start()
    {
        animation = GetComponent<Animation>();
        button = GetComponent<Button>();
        button.enabled = false;
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        animation["StartSceneButton"].speed = 1f;
        animation.Play();
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        if (animation.isPlaying)
            animation["StartSceneButton"].time = animation["StartSceneButton"].time;
        else
            animation["StartSceneButton"].time = animation["StartSceneButton"].length;
        animation["StartSceneButton"].speed = -1f;
        animation.Play();
        button.enabled = false;
    }
}
