using System.Collections.Generic;
using UnityEngine;

public abstract class CardPlaceView : MonoBehaviour
{
    [SerializeField] protected List<CardView> _cards;
    
    protected CardPlaceModel CardPlaceModel;
    private bool _isSignedToModel;

    public CardPlaceModel Model => CardPlaceModel;

    private void OnEnable()
    {
        if (!_isSignedToModel)
        {
            SignToModelAction();
        }
    }

    private void OnDisable()
    {
        if(_isSignedToModel)
            UnsignToModelAction();
    }

    public void Initialize(CardPlaceModel cardPlaceModel)
    {
        CardPlaceModel = cardPlaceModel;
        CardPlaceModel.SignToView(this);
        SignToModelAction();
    }

    public virtual Vector3 GetNextCardPosition()
    {
        return transform.position;
    }

    public virtual Transform GetNextCardParent()
    {
        return this.transform;
    }

    protected virtual void SignToModelAction()
    {
        if (CardPlaceModel != null)
        {
            CardPlaceModel.TakedCard += OnTakedCard;
            CardPlaceModel.GaveCard += OnGiveCardMove;
            CardPlaceModel.Reseted += OnModelReset;
            _isSignedToModel = true;
        }
    }

    protected virtual void UnsignToModelAction()
    {
        if (CardPlaceModel != null)
        {
            CardPlaceModel.TakedCard += OnTakedCard;
            CardPlaceModel.GaveCard -= OnGiveCardMove;
            CardPlaceModel.Reseted -= OnModelReset;
            _isSignedToModel = false;
        }
    }

    protected virtual void OnTakedCard(CardModel card)
    {
        _cards.Add(card.View);
    }

    private void OnGiveCardMove(CardPlaceModel newCardPlaceModel, CardModel cardModel)
    {
        cardModel.View.MoveToNewPlace(newCardPlaceModel.View,newCardPlaceModel.View.GetNextCardParent());
        _cards.Remove(cardModel.View);
    }

    private void OnModelReset()
    {
        foreach(CardView card in _cards)
        {
            Destroy(card.gameObject);
        }

        _cards.Clear();
    }
}
