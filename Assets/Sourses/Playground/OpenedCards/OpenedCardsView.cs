using UnityEngine;

public class OpenedCardsView : CardPlaceView
{
    protected override Vector3 Offset { get; } = new(0, 0, -0.1f);
        
        
    public override Vector3 GetNextCardPosition()
    {
        return transform.position+_defaultOffset + Offset * _cards.Count;
    }

    public override void OnTakedCard(CardModel card)
    {
        card.OpenMove();
        base.OnTakedCard(card);
    }
}
