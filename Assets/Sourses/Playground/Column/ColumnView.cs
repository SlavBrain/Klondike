using UnityEngine;

public class ColumnView : CardPlaceView
{
    private Vector3 _offset = new(0, -0.7f,-0.1f);
    
    public override Vector3 GetNextCardPosition()
    {
        if (_cards.Count > 1)
        {
            return transform.position + _offset * (_cards.Count-1);
        }
        else
        {
            return base.GetNextCardPosition()+new Vector3(0,0,-0.05f);
        }
        
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
