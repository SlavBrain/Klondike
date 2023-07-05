using UnityEngine;

public class Dragger : MonoBehaviour
{
    private Ray2D ray;
    private RaycastHit2D hit;
    private Vector2 _worldPoint;
    private DragableObject _currentDragableObject;

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            _worldPoint = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            hit = Physics2D.Raycast(_worldPoint, Vector2.zero);
            if (hit.transform != null)
            {
                if (hit.transform.TryGetComponent<DragableObject>(out DragableObject dragableObject))
                {
                    if (dragableObject.isActiveAndEnabled)
                    {
                        _currentDragableObject = dragableObject;
                        dragableObject.ChangeDraggingStateTrue();
                    }
                }
            }
        }

        if (Input.GetMouseButtonUp(0))
        {
            if (_currentDragableObject != null)
            {
                _currentDragableObject.ChangeDraggingStateFalse();
                _currentDragableObject = null;
            }
        }
        
    }
}
