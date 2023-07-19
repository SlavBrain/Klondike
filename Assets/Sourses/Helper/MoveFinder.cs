using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class MoveFinder
{
    private OpenedCardsModel _openedCardsModel;
    private List<ColumnModel> _columnModels;
    private List<DumpModel> _dumpModels;

    private bool _isAllCardInColumnOpen;
    
    public MoveFinder(OpenedCardsModel openedCardsModel,List<ColumnModel> columnModels, List<DumpModel> dumpModels)
    {
        _openedCardsModel = openedCardsModel;
        _columnModels = columnModels;
        _dumpModels = dumpModels;
    }

    public void OnHelpInvoke()
    {
        Debug.Log("Help Invoke");
        if (TryFindAvailableMoves(out CardModel cardModel, out CardPlaceModel cardPlaceModel))
        {
            cardModel.View.MoveBoomerang(cardPlaceModel.View);
            Debug.Log("Card "+cardModel.Rang+" "+cardModel.Suit+";Column "+cardPlaceModel.View.name);
        }
        else
        {
            Debug.Log("Not available moves");
        }
    }

    private bool TryFindAvailableMoves(out CardModel cardModel, out CardPlaceModel cardPlaceModel)
    {
        cardModel = null;
        cardPlaceModel = null;
        
        if (TryFindMovesColumnToDump(out cardModel, out cardPlaceModel))
            return true;
        if (TryFindMovesOpenCardToDump(out cardModel, out cardPlaceModel))
            return true;
        if (TryFindMovesColumnToColumn(out cardModel, out cardPlaceModel))
            return true;
        if (TryFindMovesOpenCardToColumn(out cardModel, out cardPlaceModel))
            return true;

        return false;

    }

    private bool TryFindMovesColumnToColumn(out CardModel cardModel,out CardPlaceModel cardPlaceModel)
    {
        cardModel = null;
        cardPlaceModel = null;
        
        for (int i = 0; i < _columnModels.Count; i++)
        {
            if (_columnModels[i].Cards.Count > 0)
            {
                for (int j = 0; j < _columnModels.Count; j++)
                {
                    if (j == i)
                    {
                        continue;
                    }

                    foreach (CardModel card in _columnModels[i].Cards)
                    {
                        if (card.IsOpen)
                            if (_columnModels[j].IsCardCanBeAdded(card))
                            {
                                cardModel = card;
                                cardPlaceModel = _columnModels[j];
                                return true;
                            }

                    }
                }
            }
        }

        return false;
    }

    private bool TryFindMovesOpenCardToColumn(out CardModel cardModel,out CardPlaceModel cardPlaceModel)
    {
        cardModel = null;
        cardPlaceModel = null;

        if (_openedCardsModel.Cards.Count > 0)
        {
            foreach (ColumnModel columnModel in _columnModels)
            {
                if (columnModel.IsCardCanBeAdded(_openedCardsModel.Cards[^1]))
                {
                    cardModel = _openedCardsModel.Cards[^1];
                    cardPlaceModel = columnModel;
                    return true;
                }
            }
        }

        return false;
    }

    private bool TryFindMovesOpenCardToDump(out CardModel cardModel,out CardPlaceModel cardPlaceModel)
    {
        cardModel = null;
        cardPlaceModel = null;
        
        if(_openedCardsModel.Cards.Count>0)
            
            foreach (DumpModel dump in _dumpModels)
            {
                if (dump.IsCardCanBeAdded(_openedCardsModel.Cards[^1]))
                {
                    cardModel = _openedCardsModel.Cards[^1];
                    cardPlaceModel = dump;
                    return true;
                }
            }

        return false;
    }

    private bool TryFindMovesColumnToDump(out CardModel cardModel,out CardPlaceModel cardPlaceModel)
    {
        cardModel = null;
        cardPlaceModel = null;

        foreach (ColumnModel columnModel in _columnModels)
        {
            if (columnModel.Cards.Count > 0)
            {
                foreach (DumpModel dump in _dumpModels)
                {
                    if (dump.IsCardCanBeAdded(columnModel.LastCardModel))
                    {
                        cardModel = columnModel.LastCardModel;
                        cardPlaceModel = dump;
                        return true;
                    }
                }
            }
        }

        return false;
    }
}
