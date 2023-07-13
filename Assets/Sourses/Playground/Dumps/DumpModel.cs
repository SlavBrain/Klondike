public class DumpModel : CardPlaceModel
{
    private CardSuits _currentSuit;
    
    protected override void TakeCard(CardModel card)
    {
        base.TakeCard(card);
        SetSuit(card.Suit);
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
}
