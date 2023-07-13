using UnityEngine;

public class ColumnView : CardPlaceView
{
    private Vector3 _offset = new(0, -0.5f,-1);
    
    public override Vector3 GetNextCardPosition()
    {
        return transform.position + _offset * _cards.Count;
    }

    public override Transform GetNextCardParent()
    {
        if (_cards.Count > 1)
        {
            Debug.Log("to card");
            return _cards[^1].transform;
        }
        else
        {
            Debug.Log("to column");

            return base.GetNextCardParent();
        }
    }
}
