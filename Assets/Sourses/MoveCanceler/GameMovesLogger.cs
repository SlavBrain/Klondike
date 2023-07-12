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
    }

    private void SaveChange(GameChanges changes)
    {
        _gameChanges.Push(changes);
    }

    private GameChanges GetLastChange()
    {
        return _gameChanges.Pop();
    }

    private void Reset()
    {
        _gameChanges.Clear();
    }
}
