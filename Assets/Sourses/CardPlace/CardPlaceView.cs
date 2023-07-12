using System;
using System.Collections.Generic;
using UnityEngine;

public abstract class CardPlaceView : MonoBehaviour
{
    [SerializeField] protected List<CardView> _cards;
    protected CardPlaceModel _cardPlaceModel;
    private bool _isSignedToModel;

    public CardPlaceModel Model => _cardPlaceModel;

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

    protected virtual void SignToModelAction()
    {
        if (_cardPlaceModel != null)
        {
            _cardPlaceModel.TakedCard += OnTakedCard;
            _cardPlaceModel.GaveCard += OnGiveCard;
            _cardPlaceModel.Reseted += OnModelReset;
            _isSignedToModel = true;
        }
    }

    protected virtual void UnsignToModelAction()
    {
        if (_cardPlaceModel != null)
        {
            _cardPlaceModel.GaveCard -= OnGiveCard;
            _cardPlaceModel.Reseted -= OnModelReset;
            _isSignedToModel = false;
        }
    }

    protected virtual void OnTakedCard(CardModel card)
    {
        _cards.Add(card.View);
    }

    private void OnGiveCard(CardPlaceModel cardPlaceModel, CardModel cardModel)
    {
        cardModel.View.MoveToNewPlace(cardPlaceModel.View);
        cardPlaceModel.TakeCard(cardModel);
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
