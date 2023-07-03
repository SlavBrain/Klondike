using System;
using System.Collections.Generic;
using UnityEngine;

public class Column:ICardPlace
{
    private ColumnView _columnView;
    private List<Card> _cards;

    public event Action<Card> CardAdded;
    public Column(ColumnView columnView)
    {
        _columnView = columnView;
        _cards = new List<Card>();
    }

    public void Fill(Deck deck,int startCardCount)
    {
        for (int i = 0; i < startCardCount; i++)
        {
            if (!deck.TryGiveTopCard(_columnView, out Card card))
            {
                break;
            }
        }

        Debug.Log(_cards.Count);
        _cards[^1].Open();
    }
    
    public void AddCard(Card newCard)
    {
        _cards.Add(newCard);
        CardAdded?.Invoke(newCard);
    }
}
