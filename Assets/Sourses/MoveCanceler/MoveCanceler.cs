using System;
using UnityEngine.UI;

public class MoveCanceler
{
    private GameMovesLogger _moveLogger;
    private Button _cancelButton;

    public MoveCanceler(GameMovesLogger moveLogger, Button cancelButton)
    {
        _moveLogger = moveLogger;
        _cancelButton = cancelButton;
        
        _cancelButton.onClick.AddListener(TryCancelMove);
    }

    private void TryCancelMove()
    {
        var lastChanges = _moveLogger.GetLastChange();
        if (lastChanges != null)
        {
            CancelMove(lastChanges);
        }
    }

    private void CancelMove(GameChanges changes)
    {
        switch (changes)
        {
            case CardTransferChange cardTransferChange:
                CardTransferCancel(cardTransferChange);
                break;
            case CardOpenChange cardOpenChange:
                CardOpenCancel(cardOpenChange);
                break;
            default:
                throw new Exception("Try cancel unregistered change");
                break;
        }
    }

    private void CardTransferCancel(CardTransferChange cardTransferChange)
    {
        foreach (CardModel card in cardTransferChange.CardModel)
        {
            cardTransferChange.NewCardPlaceModel.GiveCard(cardTransferChange.OldCardPlaceModel,card);
        }
    }

    private void CardOpenCancel(CardOpenChange cardOpenChange)
    {
        if (cardOpenChange.IsOpen)
        {
            cardOpenChange.CardModel.Close();
        }
        else
        {
            cardOpenChange.CardModel.Open();
        }
    }
}
