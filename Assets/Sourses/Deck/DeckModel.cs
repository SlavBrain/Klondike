using System;
using System.Collections.Generic;
using Random = System.Random;

public class DeckModel: CardPlaceModel
{
    private readonly List<CardModel> _startingCards=new List<CardModel>();

    public event Action<CardModel> CreatedNewCard;

    public void CreateNew()
    {
            
        foreach (CardSuits suit in Enum.GetValues(typeof(CardSuits)))
        {
            foreach (CardRangs rang in Enum.GetValues(typeof(CardRangs)))
            {
                CardModel newCard = new(rang, suit);
                _cards.Add(newCard);
                CreatedNewCard?.Invoke(newCard);
            }
        }

        Shuffle();
        OverrideStartState();
    }

    private void Shuffle()
    {
        Random random = new();
        
        for (int i = 0; i < _cards.Count; i++)
        {
            int newPosition = random.Next(0, _cards.Count);
            (_cards[newPosition], _cards[i]) = (_cards[i], _cards[newPosition]);
        }
    }

    private void OverrideStartState()
    {
        _startingCards.Clear();

        foreach(CardModel card in _cards)
        {
            _startingCards.Add(card);
        }
    }
}
