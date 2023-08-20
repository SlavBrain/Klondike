using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Playground : MonoBehaviour
{

    private DeckModel _deckModel;
    private List<ColumnModel> _columnModel;

    public event Action GameStarting;
    public event Action GameStarted;

    public void Initialize(DeckModel deckModel,List<ColumnModel> columnModels, Button startButton, Button restartButton)
    {
        _deckModel = deckModel;
        _columnModel = columnModels;
        startButton.onClick.AddListener(StartGame);
        restartButton.onClick.AddListener(RestartGame);
    }

    private void StartGame()
    {
        if (PlayerData.Instance.TryRemoveCoins(Saver.Instance.SaveData.LastBet))
        {
            PlayerData.Instance.OnGameStarting();
            GameStarting?.Invoke();
            _deckModel.CreateNew();
            SpreadOutCardToColumns();
            GameStarted?.Invoke();
        }
        else
        {
            Debug.Log("Not enough money");
        }
        
    }

    private void RestartGame()
    {
        GameStarting?.Invoke();
        _deckModel.Recover();
        SpreadOutCardToColumns();
        GameStarted?.Invoke();
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
