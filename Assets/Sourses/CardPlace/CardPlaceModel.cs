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

    public event Action<CardPlaceModel,CardPlaceModel,List<CardModel>> GaveCardsMove;
    public event Action<CardPlaceModel,CardModel> GaveCard;
    public event Action Reseted;

    public CardPlaceView View => _cardPlaceView;

    public IReadOnlyList<CardModel> Cards => _cards;

    public void SignToView(CardPlaceView cardPlaceView)
    {
        _cardPlaceView = cardPlaceView;
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
            GiveCardsMove(cardPlaceModel,_cards[^1]);
            return true;
        }
        else
        {
            return false;
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
    
    //Use only for cancel move
    public void GiveCard(CardPlaceModel cardPlaceModel, CardModel cardModel)
    {
        _cards.Remove(cardModel);
        cardPlaceModel.TakeCard(cardModel);
        GaveCard?.Invoke(cardPlaceModel,cardModel);
    }
    
    public virtual bool IsCardCanBeAdded(CardModel cardModel)
    {
        return false;
    }

    protected virtual void TakeCard(CardModel card)
    {
        _cards.Add(card);
    }
    
    private void RequiredCard(CardPlaceModel cardPlaceModel,CardModel cardModel)
    {
        cardPlaceModel.GiveCardsMove(this,cardModel);
    }
    
    protected virtual void GiveCardsMove(CardPlaceModel cardPlaceModel,CardModel cardModel)
    {
        List<CardModel> giftingCards = new List<CardModel>();

        int cardPosition = _cards.FindIndex(card=>card==cardModel);

        while(_cards.Count>cardPosition)
        {
            giftingCards.Add(_cards[cardPosition]);
            GiveCard(cardPlaceModel,_cards[cardPosition]);
        }
        
        InvokeGaveCardsMoveAction(this,cardPlaceModel,giftingCards);
    }

    protected void InvokeGaveCardsMoveAction(CardPlaceModel oldPlaceModel, CardPlaceModel newPlaceModel,
        List<CardModel> cardModelList)
    {
        GaveCardsMove?.Invoke(oldPlaceModel,newPlaceModel,cardModelList);
    }
}
