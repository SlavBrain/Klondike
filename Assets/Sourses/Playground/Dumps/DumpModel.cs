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

    public override void TakeCard(CardModel card)
    {
        base.TakeCard(card);
        SetSuit(card.Suit);
    }

    private void SetSuit(CardSuits suit)
    {
        _currentSuit = suit;
    }
}
