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
    public event Action<CardModel> TakedCard;
    public event Action Reseted;

    public CardPlaceView View => _cardPlaceView;

    public IReadOnlyList<CardModel> Cards => _cards;

    public void SignToView(CardPlaceView cardPlaceView)
    {
        _cardPlaceView = cardPlaceView;
        //_cardPlaceView.TakedCard += TakeCard;
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
            GiveCard(cardPlaceModel,_cards[^1]);
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

    public bool TryTakeCard(CardModel cardModel)
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

    public virtual void TakeCard(CardModel card)
    {
        _cards.Add(card);
        TakedCard?.Invoke(card);
    }

    protected virtual void GiveCard(CardPlaceModel cardPlaceModel, CardModel cardModel)
    {
        _cards.Remove(cardModel);
        GaveCard?.Invoke(cardPlaceModel,cardModel);
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
