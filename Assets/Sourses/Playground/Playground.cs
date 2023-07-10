using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Playground : MonoBehaviour
{

    private DeckModel _deckModel;
    private List<ColumnModel> _columnModel;

    public void Initialize(DeckModel deckModel,List<ColumnModel> columnModels, Button startButton)
    {
        _deckModel = deckModel;
        _columnModel = columnModels;
        startButton.onClick.AddListener(StartGame);
    }

    public void StartGame()
    {
        _deckModel.CreateNew();
        SpreadOutCardToColumns();
    }

    private void SpreadOutCardToColumns()
    {
        for (int i = 0; i < _columnModel.Count; i++)
        {
            _columnModel[i].Fill(_deckModel,i+1);
            _columnModel[i].OpenLastCard();
        }
    }
}
