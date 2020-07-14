using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControll : MonoBehaviour
{
    public float maxDistanceDelta = 1f;

    // Start is called before the first frame update
    void Start()
    {
        GameCursor.getInstance.HidePointer(true);
    }

    // Update is called once per frame
    void Update()
    {
        FollowMouse();
    }

    private void FollowMouse()
    {
        var pos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        pos.z = 0;
        if (!GameCursor.getInstance.IsInView(pos))
            return;
        transform.position = Vector2.MoveTowards(transform.position, pos, maxDistanceDelta);
    }
}
