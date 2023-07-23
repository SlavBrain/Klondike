using System;
using UnityEngine;

public class DumpModel : CardPlaceModel
{
    private CardSuits _currentSuit;

    public bool IsFill {
        get
        {
            if (Cards.Count == 0)
            {
                return false;
            }
            else
            {
               return  _cards[^1].Rang == CardRangs.King;
            }
            
        }
        
    }

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
    
    public override bool IsCardCanBeAdded(CardModel cardModel)
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
        if (_cards[^1].Rang == CardRangs.King)
        {
            Filled?.Invoke();
        }
    }
}
