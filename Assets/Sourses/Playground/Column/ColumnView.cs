using UnityEngine;

public class ColumnView : CardPlaceView
{
    protected override Vector3 Offset { get; } = new(0, -0.7f,-0.1f);
    private Vector3 _currentOffset;
    
    public override Vector3 GetNextCardPosition()
    {
        if (_cards.Count > 1)
        {
            return _cards[^1].transform.position+Offset;
        }
        else
        {
            return base.GetNextCardPosition()+_defaultOffset;
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

    protected override void Refresh()
    {
        RecalculateOffset();
        
        for (int i = 0; i < _cards.Count; i++)
        {
            _cards[i].MoveToDot(transform.position+_defaultOffset + _currentOffset * i);
            
            if (i == 0)
            {
                _cards[i].transform.parent = transform;
            }
            else
            {
                _cards[i].transform.parent = _cards[i-1].transform;
            }
            
        }
        
    }

    private void RecalculateOffset()
    {
        if (_cards.Count <= 7)
        {
            _currentOffset = Offset;
        }
        else
        {
            _currentOffset = new Vector3(Offset.x, Offset.y/ (_cards.Count/7f),Offset.z );
            Debug.Log(_cards.Count+" "+ _currentOffset);
        }
    }
}
