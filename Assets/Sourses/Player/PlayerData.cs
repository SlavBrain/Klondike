using System;
using UnityEngine;

public class PlayerData : MonoBehaviour
{
    public static PlayerData Instance;
    
    private int _coinValue;
    private int _lastBetChange;
    private int _startingGameCount;
    private int _successEndedGameCount;

    public event Action ChangedValue;

    public int CoinValue => _coinValue;
    
    public void Initialize()
    {
        if (Instance == null)
        {
            transform.parent = null;
            DontDestroyOnLoad(gameObject);
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }
    
    private void OnGameStarting()
    {
        _startingGameCount++;
    }

    private void OnSuccessEndedGame()
    {
        _successEndedGameCount++;
    }

    private void OnBetChanged(int number)
    {
        _lastBetChange = number;
    }
    
    public void AddCoins(int addingValue)
    {
        if (addingValue > 0)
        {
            _coinValue += addingValue;
            ChangedValue?.Invoke();
            Saver.Instance.SavePlayerData();
        }
    }

    public bool TryRemoveCoins(int removingValue)
    {
        if (removingValue < _coinValue && removingValue > 0)
        {
            _coinValue -= removingValue;
            ChangedValue?.Invoke();
            return true;
        }

        return false;
    }
}
