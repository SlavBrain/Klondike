using System;
using System.Collections.Generic;
using UnityEngine;

public abstract class CardPlaceModel
{
    private CardPlaceView _cardPlaceView;
    protected List<CardModel> _cards;

    protected CardPlaceModel()
    {
        _cards = new List<CardModel>();
    }

    public event Action<CardPlaceModel,CardModel> GaveCard;
    public event Action Reseted;

    public CardPlaceView View => _cardPlaceView;

    public IReadOnlyList<CardModel> Cards => _cards;

    public void SignToView(CardPlaceView cardPlaceView)
    {
        _cardPlaceView = cardPlaceView;
        _cardPlaceView.TakedCard += TakeCard;
    }

    public void Reset()
    {
        _cards = new List<CardModel>();
        Reseted?.Invoke();
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

    public void GiveCards(CardPlaceModel cardPlaceModel,CardModel cardModel)
    {
        int cardPosition = _cards.FindIndex(card=>card==cardModel);
        
        while(_cards.Count>cardPosition)
        {
            GiveCard(cardPlaceModel,_cards[cardPosition]);
        }
    }

    private void GiveTopCard(CardPlaceModel cardPlaceModel)
    {
        GiveCard(cardPlaceModel,_cards[_cards.Count-1]);
    }

    protected virtual void GiveCard(CardPlaceModel cardPlaceModel, CardModel cardModel)
    {
        _cards.Remove(cardModel);
        GaveCard?.Invoke(cardPlaceModel,cardModel);
    }

    protected virtual void TakeCard(CardView cardView)
    {
       _cards.Add(cardView.Card); 
    }

    public virtual bool TryTakeDraggingCard(CardModel cardModel)
    {
        return false;
    }

    protected virtual bool IsCardCanBeAdded(CardModel cardModel)
    {
        return false;
    }

    protected void RequiredCard(CardPlaceModel cardPlaceModel,CardModel cardModel)
    {
        cardPlaceModel.GiveCards(this,cardModel);
    }   
}
