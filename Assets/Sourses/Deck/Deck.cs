using System;
using System.Collections.Generic;
using Random = System.Random;

public class Deck: ICardPlace
{
    private DeckView _deckView;
    private List<Card> _startingCards;
    private List<Card> _currentCards;

    public event Action ChangedCurrentState;
    public event Action<Card> CreatedNewCard;
    public event Action<Card,ICardPlaceView> GaveCard;
    
    public Deck(DeckView deckView)
    {
        _deckView = deckView;
    }

    public IReadOnlyList<Card> Cards => _currentCards;
    
    public void CreateNew()
    {
        _startingCards = new List<Card>();
            
        foreach (CardSuits suit in Enum.GetValues(typeof(CardSuits)))
        {
            foreach (CardRangs rang in Enum.GetValues(typeof(CardRangs)))
            {
                Card newCard = new Card(rang, suit);
                _startingCards.Add(newCard);
                CreatedNewCard?.Invoke(newCard);
            }
        }
        Shuffle();
        RebuildCurrentState();
    }

    public bool TryGiveTopCard(ICardPlaceView cardPlaceView,out Card card)
    {
        card = null;
        
        if (_currentCards.Count > 0)
        {
            card = _currentCards[0];
            _currentCards.RemoveAt(0);
            GaveCard?.Invoke(card,cardPlaceView);
            return true;
        }
        else
        {
            return false;
        }
    }

    private void RebuildCurrentState()
    {
        _currentCards = new List<Card>();
        
        foreach (Card card in _startingCards)
        {
            _currentCards.Add(card);
        }
        
        ChangedCurrentState?.Invoke();
    }

    private void Shuffle()
    {
        Random random = new Random();
        
        for (int i = 0; i < _startingCards.Count; i++)
        {
            int newPosition = random.Next(0, _startingCards.Count);
            (_startingCards[newPosition], _startingCards[i]) = (_startingCards[i], _startingCards[newPosition]);
        }
    }
}
