using System.Collections.Generic;
using UnityEngine;

public class AutoFiller
{
    private MoveFinder _moveFinder;
    private DeckModel _deckModel;
    private List<ColumnModel> _columnModels;
    private List<DumpModel> _dumpModels;
    private OpenedCardsModel _openedCardsModel;

    public AutoFiller(InputController inputController,MoveFinder moveFinder, DeckModel deckModel, List<ColumnModel> columnModels,
        OpenedCardsModel openedCardsModel,List<DumpModel> dumpModels)
    {
        _moveFinder = moveFinder;
        _deckModel = deckModel;
        _columnModels = columnModels;
        _openedCardsModel = openedCardsModel;
        _dumpModels = dumpModels;
        
        SignToInputs(inputController);
    }

    private void SignToInputs(InputController inputController)
    {
        inputController.DoubleClickedOnCard += OnDoubleClickOnCard;
        inputController.DoubleClickedOnEmpty += OnDoubleClick;
    }

    private void OnDoubleClickOnCard(CardModel cardModel)
    {
        TryMoveCardToDump(cardModel);
    }

    private void OnDoubleClick()
    {
        if (IsAllCardInColumnOpened())
        {
            FillAll();
        }
        else
        {
            FillAvailable();
        }
    }
    
    private void FillAll()
    {
        while (!IsAllDumpsFilled())
        {
            
            FillAvailable();
            
            if (_deckModel.Cards.Count == 0)
            {
                DeckView deckView = (DeckView)_deckModel.View;
                deckView.OnOpenCardButtonClick();
            }
        }
    }
    
    private void FillAvailable()
    {
        Debug.Log("FillAvailable");
        while (_moveFinder.TryFindMoveToDump(out CardModel cardModel, out CardPlaceModel cardPlaceModel))
        {
            Debug.Log(cardModel.View.gameObject.name);
            Debug.Log(cardPlaceModel.View.gameObject.name);
            cardPlaceModel.TryTakeCard(cardModel);
        }
    }
    
    private bool TryMoveCardToDump(CardModel cardModel)
    {
        if (_moveFinder.TryFindCardMoveToDump(cardModel, out CardPlaceModel cardPlaceModel))
        {
            cardPlaceModel.TryTakeCard(cardModel);
            return true;
        }

        return false;
    }

    private bool IsAllCardInColumnOpened()
    {
        foreach (ColumnModel columnModel in _columnModels)
        {
            if (!columnModel.IsAllCardOpened)
            {
                return false;
            }
        }

        return true;
    }

    private bool IsAllDumpsFilled()
    {
        foreach (DumpModel dumpModel in _dumpModels)
        {
            if ((!dumpModel.IsFill)&&dumpModel.Cards.Count>0)
            {
                return false;
            }
        }

        return true;
    }
}
