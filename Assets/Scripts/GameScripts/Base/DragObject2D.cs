using UnityEngine;

public class DragObject2D : MonoBehaviour
{
    private Vector3 offset;
    private float zCoordinate;
    private bool isDragging = false;

    void OnMouseDown()
    {
        // 记录物体与鼠标点击位置的偏移量
        zCoordinate = Camera.main.WorldToScreenPoint(transform.position).z;
        offset = transform.position - GetMouseWorldPos();
        isDragging = true;
    }

    void OnMouseDrag()
    {
        if (isDragging)
        {
            // 更新物体位置，保持与鼠标的偏移
            transform.position = GetMouseWorldPos() + offset;
        }
    }

    void OnMouseUp()
    {
        isDragging = false;
    }

    private Vector3 GetMouseWorldPos()
    {
        // 获取鼠标在世界空间中的位置
        Vector3 mousePoint = Input.mousePosition;
        mousePoint.z = zCoordinate;
        return Camera.main.ScreenToWorldPoint(mousePoint);
    }
}