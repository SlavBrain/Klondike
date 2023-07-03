using System;
using UnityEngine;

public class Card
{
    private CardView _cardView;
    private CardRangs _rang;
    private CardSuits _suit;
    private bool _isOpen=false;

    public event Action ChangedOpenState;
    
    public Card(CardRangs rang, CardSuits suit)
    {
        _rang = rang;
        _suit = suit;
    }

    public CardView CardView => _cardView;
    public CardRangs Rang => _rang;
    public CardSuits Suit => _suit;
    public bool IsOpen => _isOpen;

    public void SignToView(CardView cardView)
    {
        _cardView = cardView;
    }

    public void Open()
    {
        Debug.Log("Open");
        _isOpen = true;
        ChangedOpenState?.Invoke();
    }

    public void Close()
    {
        _isOpen = false;
        ChangedOpenState?.Invoke();
    }
}
