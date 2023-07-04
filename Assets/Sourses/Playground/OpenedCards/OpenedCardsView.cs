using UnityEngine;

public class OpenedCardsView : CardPlaceView
{
    private Vector3 _offset = new(0, 0, -0.1f);
        
        
    public override Vector3 GetNextCardPosition()
    {
        return transform.position + _offset * _cards.Count;
    }

    public override void TakeCard(CardView cardView)
    {
        cardView.Card.Open();
        base.TakeCard(cardView);
    }
}
