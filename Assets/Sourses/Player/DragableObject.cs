using System;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class DragableObject : MonoBehaviour
{
    [SerializeField] private Collider2D _collider;
    private bool _isDragging = false;
    private Vector3 _startPosition;
    private Collider2D[] _nearColliders;
    private float _triggerRadius = 0.55f;
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
        IsObjectInNewPlace();
        transform.position = _startPosition;
    }

    private bool IsObjectInNewPlace()
    {
        _nearColliders = Physics2D.OverlapCircleAll(transform.position, _triggerRadius);
        var nearestCardPlace = _nearColliders
            .OrderBy(collider => Vector2.Distance(transform.position, collider.transform.position))
            .Where(collider => collider.TryGetComponent<CardPlaceView>(out CardPlaceView cardPlaceView))
            //.Except(this.GetComponentInParent<Collider2D>())
            .ToList();
            
        Debug.Log(_nearColliders.Length+"  "+nearestCardPlace.Count);
        return true;
    }
}

/*
var nearestUnitCollider = nearUnits
    .OrderBy(collider => Vector3.Distance(transform.position, collider.transform.position))
    .Where(collider => collider.TryGetComponent(out Unit nearUnits)).ToList()
    .Except(new Collider[] { this.GetComponent<Collider>() }).ToList()
    .Where(unit => (unit.GetComponent<Unit>().Weapon.Id == _weapon.Id) && (unit.GetComponent<Unit>().Level == _level)).ToList()
    .FirstOrDefault();
    */