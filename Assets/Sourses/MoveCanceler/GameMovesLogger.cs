using System.Collections.Generic;
using UnityEngine;

public class GameMovesLogger
{
    private Stack<GameChanges> _gameChanges;
    private Playground _playground;

    private DeckModel _deckModel;
    private OpenedCardsModel _openedCardsModel;
    private List<ColumnModel> _columnModels;
    private List<DumpModel> _dumpModels;

    public GameMovesLogger(Playground playground, DeckModel deck, OpenedCardsModel openedCards, List<ColumnModel> columnModels, List<DumpModel> dumpModels)
    {
        _gameChanges = new Stack<GameChanges>();
        _playground = playground;
        _deckModel = deck;
        _openedCardsModel = openedCards;
        _columnModels = columnModels;
        _dumpModels = dumpModels;
        
        SignToCardTransfers();
        SignToCardCreating();
    }

    private void SignToCardTransfers()
    {
        _playground.GameStarted += OnGameStarted;
        
        _deckModel.GaveCardsMove += OnCardsMoveTransfer;
        _openedCardsModel.GaveCardsMove += OnCardsMoveTransfer;
         
        foreach (ColumnModel column in _columnModels)
        {
            column.GaveCardsMove += OnCardsMoveTransfer;
        }
        
        foreach(DumpModel dump in _dumpModels)
        {
            dump.GaveCardsMove += OnCardsMoveTransfer;
        }
    }

    private void SignToCardCreating()
    {
        _deckModel.CreatedNewCard += SignToCardOpenChange;
    }

    private void SignToCardOpenChange(CardModel cardModel)
    {
        cardModel.ChangedOpenStateMove += OnCardOpenChanged;
    }

    private void OnCardOpenChanged(CardModel cardModel)
    {
        SaveChange(new CardOpenChange(cardModel,cardModel.IsOpen));
    }
    
    private void OnGameStarted()
    {
        Reset();
    }

    private void OnCardsMoveTransfer(CardPlaceModel oldPlace, CardPlaceModel newPlace, List<CardModel> card)
    {
        SaveChange(new CardTransferChange(oldPlace,newPlace,card));
    }

    private void SaveChange(GameChanges changes)
    {
        _gameChanges.Push(changes);
    }

    public GameChanges GetLastChange()
    {
        if (_gameChanges.Count > 0)
        {
            return _gameChanges.Pop();  
        }
        else
        {
            return null;
        }
    }

    private void Reset()
    {
        _gameChanges.Clear();
    }
}
