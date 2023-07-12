using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Playground : MonoBehaviour
{

    private DeckModel _deckModel;
    private List<ColumnModel> _columnModel;

    public event Action StartingGame;

    public void Initialize(DeckModel deckModel,List<ColumnModel> columnModels, Button startButton, Button restartButton)
    {
        _deckModel = deckModel;
        _columnModel = columnModels;
        startButton.onClick.AddListener(StartGame);
        restartButton.onClick.AddListener(RestartGame);
    }

    public void StartGame()
    {
        StartingGame?.Invoke();
        _deckModel.CreateNew();
        SpreadOutCardToColumns();
    }

    public void RestartGame()
    {
        StartingGame?.Invoke();
        _deckModel.Recover();
        SpreadOutCardToColumns();
    }

    private void SpreadOutCardToColumns()
    {
        Debug.Log("columns "+_columnModel.Count);
        for (int i = 0; i < _columnModel.Count; i++)
        {
            _columnModel[i].Fill(_deckModel,i+1);
            _columnModel[i].OpenLastCard();
        }
    }
}
