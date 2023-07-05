using System;
using UnityEngine;

public class CardModel
{
    private CardView _cardView;
    readonly private CardRangs _rang;
    readonly private CardSuits _suit;
    private bool _isOpen=false;
    private bool _isDraggingAllow = false;
    private bool _isOpeningAllow = false;

    public event Action ChangedOpenState;
    public event Action AllowedDragging;
    
    public CardModel(CardRangs rang, CardSuits suit)
    {
        _rang = rang;
        _suit = suit;
    }

    public CardView View => _cardView;
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
        AllowedDragging?.Invoke();
    }
}
