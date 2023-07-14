using System;
using UnityEngine;

public class InputController : MonoBehaviour
{
    [SerializeField] private LayerMask _clickListenerLayer;
    
    private RaycastHit2D hit;
    private Vector2 _worldPoint;
    private DragableObject _currentDragableObject;

    private void Update()
    {
        if (Input.GetMouseButtonDown(0)&&_currentDragableObject==null)
        {
            _worldPoint = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            
            CheckClickDownOnDragableObject();
            CheckClickOnClickListener();
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

    private void CheckClickDownOnDragableObject()
    {
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

    private void CheckClickOnClickListener()
    {
        hit = Physics2D.Raycast(_worldPoint, Vector2.zero, 15f, _clickListenerLayer);
        
        if (hit.transform != null)
        {
            if (hit.transform.TryGetComponent<ClickListener>(out ClickListener clickListener))
            {
                clickListener.OnClick();
            }
        }
    }
}
