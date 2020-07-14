using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameCursor
{
    /// <summary>
    /// 防止外部初始化
    /// </summary>
    private GameCursor() { }

    /// <summary>
    /// 单例
    /// </summary>
    private static GameCursor instance;
    public static GameCursor getInstance
    {
        get
        {
            if(instance == null)
            {
                instance = new GameCursor();
            }
            return instance;
        }
    }
    private bool isPointerHide = false;
    public bool IsPointerHide { get { return isPointerHide; } }

    /// <summary>
    /// 是否隐藏鼠标指针
    /// </summary>
    /// <param name="value"></param>
    public void HidePointer(bool value)
    {
        isPointerHide = value;
        if (isPointerHide)
        {
            Cursor.visible = false;
        }
        else if (!isPointerHide)
        {
            Cursor.visible = true;
        }
    }

    /// <summary>
    /// 判断物体是否在摄像机内
    /// </summary>
    /// <param name="worldPos">物体位置</param>
    /// <returns></returns>
    public bool IsInView(Vector3 worldPos)
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
}
