using UnityEngine;

public class ColumnView : CardPlaceView
{
    private Vector3 _offset = new(0, -0.5f,-1);
    
    public override Vector3 GetNextCardPosition()
    {
        return transform.position + _offset * _cards.Count;
    }
}
