using UnityEngine;

public class ColumnModel: CardPlaceModel
{
    public void Fill(DeckModel deck,int startCardCount)
    {
        for (int i = 0; i < startCardCount; i++)
        {
            if (!deck.TryGiveTopCard(this))
            {
                break;
            }
        }
    }

    public void OpenLastCard()
    {
        if (_cards.Count > 0)
        {
            _cards[_cards.Count - 1].Open();
        }
    }

    protected override bool IsCardCanBeAdded(CardModel cardModel)
    {
        if (_cards.Count == 0)
        {
            return cardModel.Rang == CardRangs.King;
        }
        else
        {
            CardModel lastCard = _cards[_cards.Count - 1];
            return (lastCard.IsBlack != cardModel.IsBlack) && (lastCard.Rang == cardModel.Rang + 1);
        }
    }

    protected override void GiveCard(CardPlaceModel cardPlaceModel, CardModel cardModel)
    {
        base.GiveCard(cardPlaceModel, cardModel);
        OpenLastCard();
    }
}
