using UnityEngine;

public class DumpModel : CardPlaceModel
{
    [SerializeField]private CardSuits _currentSuit;
    
    protected override bool IsCardCanBeAdded(CardModel cardModel)
    {
        if (_cards.Count == 0)
        {
            return cardModel.Rang == CardRangs.Ace;
        }
        else
        {
            return cardModel.Suit == _currentSuit && cardModel.Rang == _cards[_cards.Count - 1].Rang + 1;
        }
    }
    
    public override bool TryTakeDraggingCard(CardModel cardModel)
    {
        if (IsCardCanBeAdded(cardModel))
        {
            RequiredCard(cardModel.View.GetComponentInParent<CardPlaceView>().Model, cardModel);
            return true;
        }
        else
        {
            return false;
        }
    }

    protected override void TakeCard(CardView cardView)
    {
        base.TakeCard(cardView);
        SetSuit(cardView.Card.Suit);
    }

    private void SetSuit(CardSuits suit)
    {
        _currentSuit = suit;
    }
}
