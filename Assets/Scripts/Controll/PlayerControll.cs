using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class PlayerControll : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        FollowMouse();
    }
    
    private void FollowMouse()
    {
        //获取相机右上的点
        Vector2 cameraUpRightPoint = Camera.main.ViewportToWorldPoint(Vector2.one);
        //获取相机左下的点
        Vector2 cameraBottomLeftPoint = Camera.main.ViewportToWorldPoint(Vector2.zero);

        var mouseMovement = new Vector3(Input.GetAxis("Mouse X"), Input.GetAxis("Mouse Y"));
        var currentPosition = transform.position;

        currentPosition += mouseMovement;

        if (currentPosition.x < cameraBottomLeftPoint.x || currentPosition.x > cameraUpRightPoint.x)
        {
            currentPosition.x = transform.position.x;
        }

        if (currentPosition.y < cameraBottomLeftPoint.y || currentPosition.y > cameraUpRightPoint.y)
        {
            currentPosition.y = transform.position.y;
        }

        transform.position = currentPosition;
    }
}
