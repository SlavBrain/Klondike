using System;
using System.Collections.Generic;
using UnityEngine;

public abstract class CardPlaceView : MonoBehaviour
{
    [SerializeField] protected List<CardView> _cards;
    protected CardPlaceModel _cardPlaceModel;
    private bool _isSignedToModel;

    public CardPlaceModel Model => _cardPlaceModel;

    public event Action<CardView> TakedCard;

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
        _cardPlaceModel = cardPlaceModel;
        _cardPlaceModel.SignToView(this);
        SignToModelAction();
    }

    public virtual Vector3 GetNextCardPosition()
    {
        return transform.position;
    }

    public virtual void TakeCard(CardView cardView)
    {
        _cards.Add(cardView);
        TakedCard?.Invoke(cardView);
    }

    protected virtual void SignToModelAction()
    {
        if (_cardPlaceModel != null)
        {
            _cardPlaceModel.GaveCard += GiveCardView;
            _isSignedToModel = true;
        }
    }

    protected virtual void UnsignToModelAction()
    {
        if (_cardPlaceModel != null)
        {
            _cardPlaceModel.GaveCard -= GiveCardView;
            _isSignedToModel = false;
        }
    }

    private void GiveCardView(CardPlaceModel cardPlaceModel, CardModel cardModel)
    {
        cardModel.View.MoveToNewPlace(cardPlaceModel.View);
        cardPlaceModel.View.TakeCard(cardModel.View);
        _cards.Remove(cardModel.View);
    }
}
