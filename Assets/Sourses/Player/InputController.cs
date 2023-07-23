using System;
using UnityEngine;

public class InputController : MonoBehaviour
{
    [SerializeField] private LayerMask _clickListenerLayer;
    
    private RaycastHit2D hit;
    private Vector2 _worldPoint;
    private DragableObject _currentDragableObject;

    private float _doubleClickTime = 0.5f;
    private float _lastClickTime;

    public event Action<CardModel> DoubleClickedOnCard;
    public event Action DoubleClickedOnEmpty;
    
    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            if (Time.time - _doubleClickTime <= _lastClickTime)
            {
                OnDoubleClicked();
            }
            else
            {
                OnSingleClicked();
            }

            _lastClickTime = Time.time;
        }

        if (Input.GetMouseButtonUp(0))
        {
            OnClickOut();
        }
    }

    private void OnSingleClicked()
    {
        Debug.Log("SingleClick");
        if (_currentDragableObject==null)
        {
            _worldPoint = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            
            CheckClickDownOnDragableObject();
            CheckClickOnClickListener();
        }
    }

    private void OnDoubleClicked()
    {
        Debug.Log("DoubleClick");
        if (IsClickDownOnCardView(out CardModel cardModel))
        {
            DoubleClickedOnCard?.Invoke(cardModel);
        }
        else
        {
            DoubleClickedOnEmpty?.Invoke();
        }
    }

    private void OnClickOut()
    {
        if (_currentDragableObject != null)
        {
            _currentDragableObject.ChangeDraggingStateFalse();
            _currentDragableObject = null;
        }
    }

    private bool IsClickDownOnCardView(out CardModel cardModel)
    {
        cardModel = null;
        
        hit = Physics2D.Raycast(_worldPoint, Vector2.zero);

        if (hit.transform != null)
        {
            if (hit.transform.TryGetComponent<CardView>(out CardView cardView))
            {
                cardModel = cardView.Model;
                return true;
            }
        }

        return false;
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
                    Debug.Log("drag");
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
