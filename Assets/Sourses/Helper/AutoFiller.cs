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
            FillAll();
        }
        else
        {
            FillAvailable();
        }
    }
    
    private void FillAll()
    {
        Debug.Log("1)StartFillingAll");
        if(IsAutoFillingActive)
            return;
        Debug.Log("2)StartFillingAll");
        if (_autoFilling != null)
        {
            Debug.Log("3)StartFillingAll");
            StopCoroutine(_autoFilling);
        }
        Debug.Log("4)StartFillingAll");
        _autoFilling = StartCoroutine(AutoFilling());
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
                Debug.Log("Not all card opened");
                return false;
            }
        }
        
        Debug.Log("all card opened");
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
        Debug.Log("5)StartFillingAll");
        IsAutoFillingActive = true;
        Debug.Log("6)StartFillingAll");
        
        while (!IsAllDumpsFilled()&&errorCounter<maxIteration)
        {
            Debug.Log("7)StartFillingAll");
            
            FillAvailable();
            Debug.Log("8)StartFillingAll");
            
            if (_deckModel.Cards.Count != 0)
            {
                Debug.Log("9)StartFillingAll");
                
                DeckView deckView = (DeckView)_deckModel.View;
                
                Debug.Log("10)StartFillingAll");
                
                deckView.OnOpenCardButtonClick();
            }

            Debug.Log("11)StartFillingAll");
            errorCounter++;
            yield return null;
        }
        
        Debug.Log("12)StartFillingAll");
        
        IsAutoFillingActive = false;
    }
}
