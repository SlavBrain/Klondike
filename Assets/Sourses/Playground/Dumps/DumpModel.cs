using System;
using UnityEngine;

public class DumpModel : CardPlaceModel
{
    private CardSuits _currentSuit;

    public bool IsFill => _cards[^1].Rang == CardRangs.King;

    public event Action Filled;
    protected override void TakeCard(CardModel card)
    {
        if (_cards.Count == 0)
        {
            SetSuit(card.Suit);
        }

        base.TakeCard(card);

        CheckFilling();
    }
    
    protected override bool IsCardCanBeAdded(CardModel cardModel)
    {
        if (_cards.Count == 0)
        {
            return cardModel.Rang == CardRangs.Ace;
        }
        else
        {
            return cardModel.Suit == _currentSuit && cardModel.Rang == _cards[^1].Rang + 1;
        }
    }

    private void SetSuit(CardSuits suit)
    {
        _currentSuit = suit;
    }

    private void CheckFilling()
    {
        Debug.Log("CheckFillingDump");
        if (_cards[^1].Rang == CardRangs.King)
        {
            Debug.Log("DumpFilled");
            Filled?.Invoke();
        }
    }
}
