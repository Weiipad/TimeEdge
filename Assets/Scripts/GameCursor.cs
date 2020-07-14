using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameCursor
{

    private static bool isCursorHide = false;

    /// <summary>
    /// 防止外部初始化
    /// </summary>
    private GameCursor() { }

    /// <summary>
    /// 是否隐藏鼠标指针
    /// </summary>
    /// <param name="value"></param>
    public static void HideCursor(bool value)
    {
        isCursorHide = value;
        Cursor.visible = !isCursorHide;
    }

    /// <summary>
    /// 判断物体是否在摄像机内
    /// </summary>
    /// <param name="worldPos">物体位置</param>
    /// <returns></returns>
    public static bool IsInView(Vector3 worldPos)
    {
        Transform camTransform = Camera.main.transform;
        Vector2 viewPos = Camera.main.WorldToViewportPoint(worldPos);
        Vector3 dir = (worldPos - camTransform.position).normalized;
        float dot = Vector3.Dot(camTransform.forward, dir);

        if (dot > 0 && viewPos.x >= 0 && viewPos.x <= 1 && viewPos.y >= 0 && viewPos.y <= 1)
            return true;
        else
            return false;
    }

    /// <summary>
    /// 锁住鼠标指针
    /// </summary>
    public static void LockCursor()
    {
        Cursor.lockState = CursorLockMode.Locked;
    }

    /// <summary>
    /// 解锁鼠标指针
    /// </summary>
    public static void UnLockCursor()
    {
        Cursor.lockState = CursorLockMode.None;
    }
}
