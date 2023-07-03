using System;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class ColumnView : MonoBehaviour,ICardPlaceView
{
    [SerializeField] private List<CardView> _cards;
    private Vector3 _offset = new Vector3(0, -0.5f,-1);
    private Column _columnModel;

    public event Action<Card> CardAdded;

    private void OnDisable()
    {
        _columnModel.CardAdded -= AddCard;
    }
    
    public void Initialize(Column column, CardView template)
    {
        _columnModel = column;
        _columnModel.CardAdded += AddCard;
    }

    private void AddCard(Card card)
    {
        TakeCard(card.CardView);
    }

    public void GiveCard(CardView cardView,ICardPlaceView newCardPlaceView)
    {
        if (newCardPlaceView is Transform)
        {
            cardView.MoveToNewPlace(newCardPlaceView);
            newCardPlaceView.TakeCard(cardView);
            _cards.Remove(cardView);
        }
        //_cards.Remove(cardView);
    }

    public void TakeCard(CardView newCardView)
    {
        _cards.Add(newCardView);
        CardAdded?.Invoke(newCardView.Card);
        /*
        if(newCardView.transform.parent.TryGetComponent<ICardPlaceView>(out ICardPlaceView cardPlaceView))
        {
            newCardView.MoveToNewPlace(this);
            cardPlaceView.GiveCard(newCardView);
            _cards.Add(newCardView);
        }
        */
    }

    public Vector3 NextCardPosition()
    {
        return transform.position + _offset * _cards.Count;
    }
}
