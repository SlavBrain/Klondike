using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AutoFiller:MonoBehaviour
{
    private MoveFinder _moveFinder;
    private DeckModel _deckModel;
    private List<ColumnModel> _columnModels;
    private List<DumpModel> _dumpModels;
    private OpenedCardsModel _openedCardsModel;
    private bool IsAutoFillingActive = false;
    private Coroutine _autoFilling;
    
    public void Initialize(InputController inputController,MoveFinder moveFinder, DeckModel deckModel, List<ColumnModel> columnModels,
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
            FillAvailable();
            //Need Find bug about negative card position in Deck( then opening card from deck)
            //FillAll();
        }
        else
        {
            FillAvailable();
        }
    }
    
    private void FillAll()
    {
        if(IsAutoFillingActive)
            return;
        
        if (_autoFilling != null)
        {
            StopCoroutine(_autoFilling);
        }
        
        _autoFilling = StartCoroutine(AutoFilling());
    }
    
    private void FillAvailable()
    {
        while (_moveFinder.TryFindMoveToDump(out CardModel cardModel, out CardPlaceModel cardPlaceModel))
        {
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

    private IEnumerator AutoFilling()
    {
        int errorCounter = 0;
        int maxIteration=52;
        IsAutoFillingActive = true;
        
        while (!IsAllDumpsFilled()&&errorCounter<maxIteration)
        {
            FillAvailable();
            
            if (_deckModel.Cards.Count != 0||_openedCardsModel.Cards.Count!=0)
            {
                DeckView deckView = (DeckView)_deckModel.View;
                deckView.OnOpenCardButtonClick();
            }
            
            errorCounter++;
            yield return null;
        }
        
        IsAutoFillingActive = false;
    }
}
