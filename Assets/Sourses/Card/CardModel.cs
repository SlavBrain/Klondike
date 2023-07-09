using System;
using Unity.VisualScripting;
using UnityEngine;

public class CardModel
{
    private CardView _cardView;
    readonly private CardRangs _rang;
    readonly private CardSuits _suit;
    private bool _isOpen=false;
    private bool _isDraggingPermission = false;
    private bool _isOpeningPermission = false;

    public event Action ChangedOpenState;
    public event Action ChangedPermissionDragging;
    
    public CardModel(CardRangs rang, CardSuits suit)
    {
        _rang = rang;
        _suit = suit;
    }

    public CardView View => _cardView;
    public CardRangs Rang => _rang;
    public CardSuits Suit => _suit;
    public bool IsOpen => _isOpen;
    public bool IsOpeningPermission => _isOpeningPermission;
    public bool IsDraggingPermission => _isDraggingPermission;

    public bool IsBlack=> _suit == CardSuits.Clubs || _suit == CardSuits.Spades;

    public void SignToView(CardView cardView)
    {
        _cardView = cardView;
    }

    public void Open()
    {
        _isOpen = true;
        _isDraggingPermission = true;
        ChangedOpenState?.Invoke();
        ChangedPermissionDragging?.Invoke();
    }

    public void Close()
    {
        _isOpen = false;
        _isDraggingPermission = false;
        ChangedOpenState?.Invoke();
        ChangedPermissionDragging?.Invoke();
    }
}
