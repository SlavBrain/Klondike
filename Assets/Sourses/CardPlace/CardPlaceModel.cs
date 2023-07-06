using System;
using System.Collections.Generic;

public abstract class CardPlaceModel
{
    private CardPlaceView _cardPlaceView;
    protected List<CardModel> _cards;

    public CardPlaceModel()
    {
        _cards = new List<CardModel>();
    }

    public event Action<CardPlaceModel,CardModel> GaveCard;

    public CardPlaceView View => _cardPlaceView;

    public IReadOnlyList<CardModel> Cards => _cards;

    public void SignToView(CardPlaceView cardPlaceView)
    {
        _cardPlaceView = cardPlaceView;
        _cardPlaceView.TakedCard += TakeCard;
    }

    public bool TryGiveTopCard(CardPlaceModel cardPlaceModel)
    {
        if (_cards.Count > 0)
        {
            GiveTopCard(cardPlaceModel);
            return true;
        }
        else
        {
            return false;
        }
    }

    private void GiveTopCard(CardPlaceModel cardPlaceModel)
    {
        CardModel card = _cards[_cards.Count-1];
        _cards.RemoveAt(_cards.Count - 1);
        GaveCard?.Invoke(cardPlaceModel,card);
    }

    protected virtual void TakeCard(CardView cardView)
    {
       _cards.Add(cardView.Card); 
    }

    private void RequestCard(CardPlaceModel cardPlaceModel)
    {
        cardPlaceModel.TryGiveTopCard(this);
    }    
}
