using UnityEngine;

public class ColumnView : CardPlaceView
{
    private Vector3 _offset = new(0, -0.5f,-0.1f);
    
    public override Vector3 GetNextCardPosition()
    {
        return transform.position + _offset * _cards.Count;
    }

    public override Transform GetNextCardParent()
    {

        if (_cards.Count > 1)
        {
            return _cards[^2].gameObject.transform;
        }
        else
        {
            return base.GetNextCardParent();
        }
    }
}
