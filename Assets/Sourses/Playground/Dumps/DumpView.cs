using UnityEngine;

public class DumpView : CardPlaceView
{
    protected override Vector3 Offset { get; } = new(0, 0,-0.2f);
    
    public override Vector3 GetNextCardPosition()
    {
        return transform.position+_defaultOffset + Offset * (_cards.Count);
    }
}
