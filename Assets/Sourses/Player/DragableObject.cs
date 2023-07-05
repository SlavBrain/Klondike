using UnityEngine;

public class DragableObject : MonoBehaviour
{
    private bool _isDragging = false;

    private void Update()
    {
        if (_isDragging)
        {
            FollowToTouch();
        }
    }

    public void ChangeDraggingStateTrue()
    {
        _isDragging = true;
    }

    public void ChangeDraggingStateFalse()
    {
        _isDragging = false;
    }

    private void FollowToTouch()
    {
        transform.position = Camera.main.ScreenToWorldPoint(Input.mousePosition);
    }
}
