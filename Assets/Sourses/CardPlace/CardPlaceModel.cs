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
        Debug.Log("15)StartFillingAll");
        if (IsCardCanBeAdded(cardModel))
        {
            Debug.Log("16)StartFillingAll");
            Debug.Log(cardModel.View.GetComponentInParent<CardPlaceView>().gameObject.name);
            RequiredCard(cardModel.View.GetComponentInParent<CardPlaceView>().Model, cardModel);
            return true;
        }
        else
        {
            Debug.Log("17)StartFillingAll");
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
        Debug.Log("18)StartFillingAll");
        cardPlaceModel.GiveCardsMove(this,cardModel);
    }
    
    protected virtual void GiveCardsMove(CardPlaceModel cardPlaceModel,CardModel cardModel)
    {
        Debug.Log("19)StartFillingAll");
        List<CardModel> giftingCards = new List<CardModel>();
        Debug.Log("20)StartFillingAll");
        int cardPosition = _cards.FindIndex(card=>card==cardModel);

        Debug.Log("21)StartFillingAll");
        while(_cards.Count>cardPosition)
        {
            Debug.Log("22)StartFillingAll");
            Debug.Log(_cards.Count+" "+cardPosition);
            if (this is DeckModel)
            {
                Debug.Log("DeckModel");
            }

            if (this is OpenedCardsModel)
            {
                Debug.Log("DeckModel");
            }
            
            giftingCards.Add(_cards[cardPosition]);
            Debug.Log("23)StartFillingAll");
            GiveCard(cardPlaceModel,_cards[cardPosition]);
        }
        
        Debug.Log("24)StartFillingAll");
        InvokeGaveCardsMoveAction(this,cardPlaceModel,giftingCards);
    }

    protected void InvokeGaveCardsMoveAction(CardPlaceModel oldPlaceModel, CardPlaceModel newPlaceModel,
        List<CardModel> cardModelList)
    {
        Debug.Log("25)StartFillingAll");
        GaveCardsMove?.Invoke(oldPlaceModel,newPlaceModel,cardModelList);
    }
}
