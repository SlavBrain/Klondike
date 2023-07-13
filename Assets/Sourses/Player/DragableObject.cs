using System.Linq;
using UnityEngine;

public class DragableObject : MonoBehaviour
{
    [SerializeField] private Collider2D _collider;
    
    private bool _isDragging = false;
    private Vector3 _startPosition;
    private Collider2D[] _nearColliders;
    private float _triggerRadius = 0.55f;
    private CardPlaceView _parentCardPlace;
    
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
        _startPosition = transform.position;
    }

    public void ChangeDraggingStateFalse()
    {
        SetPosition();
        _isDragging = false;
    }

    private void FollowToTouch()
    {
        transform.position = Camera.main.ScreenToWorldPoint(Input.mousePosition);
    }

    private void SetPosition()
    {
        if (IsObjectInNewPlace(out CardPlaceView cardPlaceView))
        {
            if (!cardPlaceView.Model.TryTakeCard(this.GetComponent<CardView>().Card))
            {
                TeleportToStartPosition();  
            }
        }
        else
        {
            TeleportToStartPosition();
        }
    }

    private void TeleportToStartPosition()
    {
        transform.position = _startPosition;
    }
    
    private bool IsObjectInNewPlace(out CardPlaceView nearestCardPlaceView)
    {
        nearestCardPlaceView = null;
        _nearColliders = Physics2D.OverlapCircleAll(transform.position, _triggerRadius);
        _parentCardPlace = GetComponentInParent<CardPlaceView>();
        _parentCardPlace.TryGetComponent<Collider2D>(out Collider2D parentCollider);
        
        var nearestCardPlaceCollider = _nearColliders
            .OrderBy(collider => Vector2.Distance(transform.position, collider.transform.position))
            .Except(new Collider2D[] { parentCollider})
            .FirstOrDefault(collider => collider.TryGetComponent<CardPlaceView>(out CardPlaceView nearestCardPlaceView));

        if (nearestCardPlaceCollider != null)
        {
            Debug.Log(nearestCardPlaceCollider.gameObject.name);
            nearestCardPlaceView = nearestCardPlaceCollider.GetComponent<CardPlaceView>();
        }
        
        return nearestCardPlaceView!=null;
    }
}