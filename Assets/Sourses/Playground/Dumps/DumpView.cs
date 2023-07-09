using UnityEngine;

public class DumpView : CardPlaceView
{
    private Vector3 _offset = new(0, 0,-0.1f);
    
    public override Vector3 GetNextCardPosition()
    {
        return transform.position + _offset * _cards.Count;
    }
}
