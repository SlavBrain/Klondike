using System.Collections.Generic;
using Unity.Mathematics;
using Unity.VisualScripting;
using UnityEngine;

public class DeckView : MonoBehaviour,ICardPlaceView
{
    [SerializeField] private CardView _cardTemplate;
    [SerializeField] private List<CardView> _cards;
    private Vector3 _offset = new Vector3(0, 0, 0.5f);
    private Deck _deckModel;

    private void OnDisable()
    {
        _deckModel.CreatedNewCard -= AddNewCard;
    }
    
    public void Initialize(Deck deck)
    {
        _deckModel = deck;
        _deckModel.CreatedNewCard += AddNewCard;
        _deckModel.GaveCard += TryGiveCard;
    }

    private void AddNewCard(Card card)
    {
        CardView newCard = Instantiate(_cardTemplate, transform.position + _offset*_cards.Count, quaternion.identity, transform);
        newCard.Initialize(card);
        _cards.Add(newCard);
    }

    private void TryGiveCard(Card card, ICardPlaceView cardPlaceView)
    {
        if (card.CardView != null)
        {
            GiveCard(card.CardView,cardPlaceView);
        }
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
    }

    Vector3 ICardPlaceView.NextCardPosition()
    {
        return transform.position;
    }
}
