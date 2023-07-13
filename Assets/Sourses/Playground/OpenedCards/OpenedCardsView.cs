using UnityEngine;

public class OpenedCardsView : CardPlaceView
{
    private Vector3 _offset = new(0, 0, -0.1f);
        
        
    public override Vector3 GetNextCardPosition()
    {
        return transform.position + _offset * _cards.Count;
    }

    protected override void OnTakedCard(CardModel card)
    {
        card.OpenMove();
        base.OnTakedCard(card);
    }
}
