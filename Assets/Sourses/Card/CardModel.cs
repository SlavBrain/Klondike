using System;
using Unity.VisualScripting;
using UnityEngine;

public class CardModel
{
    private CardView _cardView;
    private readonly CardRangs _rang;
    private readonly CardSuits _suit;
    private bool _isOpen=false;
    private bool _isDraggingPermission = false;

    public event Action<CardModel> ChangedOpenState;
    public event Action<CardModel> ChangedOpenStateMove;
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
    public bool IsDraggingPermission => _isDraggingPermission;
    public bool IsBlack=> _suit is CardSuits.Clubs or CardSuits.Spades;

    public void SignToView(CardView cardView)
    {
        _cardView = cardView;
    }
    
    public void OpenMove()
    {
        Open();
        ChangedOpenStateMove?.Invoke(this);
    }

    public void CloseMove()
    {
        Close();
        ChangedOpenStateMove?.Invoke(this);
    }

    public void Open()
    {
        _isOpen = true;
        ChangedOpenState?.Invoke(this);
        AllowDragging();
    }

    public void Close()
    {
        _isOpen = false;
        ChangedOpenState?.Invoke(this);
        BlockDragging();
    }

    private void AllowDragging()
    {
        _isDraggingPermission = true;
        ChangedPermissionDragging?.Invoke();
    }

    private void BlockDragging()
    {
        _isDraggingPermission = false;
        ChangedPermissionDragging?.Invoke();
    }
}
