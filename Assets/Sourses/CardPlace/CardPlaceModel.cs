using System.Collections.Generic;

public class CardPlaceModel
{
    private CardPlaceView _cardPlaceView;
    private List<Card> _cards;

    public CardPlaceModel()
    {
        _cards = new List<Card>();
    }

    public CardPlaceView View => _cardPlaceView;
    public IReadOnlyList<Card> Card => _cards;

    public void SignToView(CardPlaceView cardPlaceView)
    {
        _cardPlaceView = cardPlaceView;
    }
    
    private void AddCard(Card newCard)
    {
        _cards.Add(newCard);
    }
}
