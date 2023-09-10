using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Playground : MonoBehaviour
{

    private DeckModel _deckModel;
    private List<ColumnModel> _columnModel;
    private MessagePanel _notEnoughCoinsPanel;

    public event Action GameStarting;
    public event Action GameStarted;

    public void Initialize(DeckModel deckModel,List<ColumnModel> columnModels, Button startButton, Button restartButton,MessagePanel notEnoughCoinsPanel)
    {
        _deckModel = deckModel;
        _columnModel = columnModels;
        _notEnoughCoinsPanel = notEnoughCoinsPanel;
        startButton.onClick.AddListener(StartGame);
        restartButton.onClick.AddListener(RestartGame);
    }

    private void StartGame()
    {
        if (PlayerData.Instance.TryRemoveCoins(Saver.Instance.SaveData.LastBet))
        {
            InterstitialCounter.Instance.Increment();
            PlayerData.Instance.OnGameStarting();
            GameStarting?.Invoke();
            _deckModel.CreateNew();
            SpreadOutCardToColumns();
            GameStarted?.Invoke();
        }
        else
        {
            _notEnoughCoinsPanel.gameObject.SetActive(true);
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
