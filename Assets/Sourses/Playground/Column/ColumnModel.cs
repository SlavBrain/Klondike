using System;
using UnityEngine;

public class ColumnModel: CardPlaceModel
{
    public event Action<CardModel> CardAdded;

    public void Fill(DeckModel deck,int startCardCount)
    {
        for (int i = 0; i < startCardCount; i++)
        {
            if (!deck.TryGiveTopCard(this))
            {
                break;
            }
        }
    }

    public void OpenLastCard()
    {
        if (_cards.Count > 0)
        {
            _cards[_cards.Count - 1].Open();
        }
    }
}
