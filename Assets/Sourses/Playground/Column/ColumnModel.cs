public class ColumnModel: CardPlaceModel
{
    public CardModel LastCardModel => _cards[^1];
    public bool IsAllCardOpened
    {
        get
        {
            if (_cards.Count > 0)
            {
                foreach (CardModel card in _cards)
                {
                    if (!card.IsOpen)
                        return false;
                }
            }

            return true;
        }
        
    }
    
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
            _cards[^1].OpenMove();
        }
    }

    public override bool IsCardCanBeAdded(CardModel cardModel)
    {
        if (_cards.Count == 0)
        {
            return cardModel.Rang == CardRangs.King;
        }
        else
        {
            CardModel lastCard = _cards[^1];
            return (lastCard.IsBlack != cardModel.IsBlack) && (lastCard.Rang == cardModel.Rang + 1);
        }
    }

    protected override void GiveCardsMove(CardPlaceModel cardPlaceModel, CardModel cardModel)
    {
        base.GiveCardsMove(cardPlaceModel, cardModel);
        OpenLastCard();
    }
}
