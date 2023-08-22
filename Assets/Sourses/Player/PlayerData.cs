using System;
using Agava.YandexGames;
using UnityEngine;

public class PlayerData : MonoBehaviour
{
    public static PlayerData Instance;
    
    [SerializeField]private int _coinValue;
    [SerializeField]private int _startingGameCount;
    [SerializeField]private int _successEndedGameCount;

    public event Action ChangedValue;

    public int CoinValue => _coinValue;
    
    public void Initialize()
    {
        if (Instance == null)
        {
            transform.parent = null;
            DontDestroyOnLoad(gameObject);
            Instance = this;
            _coinValue = Saver.Instance.SaveData.CoinValue;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void OnGameStarting()
    {
        Saver.Instance.SaveData.StartingGameCount++;
        Saver.Instance.Save();
    }

    public void OnSuccessEndedGame()
    {
        Saver.Instance.SaveData.CompleteGameCount++;
        Saver.Instance.Save();
    }
    
    public void AddCoins(int addingValue)
    {
        if (addingValue > 0)
        {
            _coinValue += addingValue;
            ChangedValue?.Invoke();
            Saver.Instance.SavePlayerData();
            RefreshCoinLeaderboard();
        }
    }

    public bool HaveEnoughMoney(int value)
    {
        return value <= _coinValue;
    }

    public bool TryRemoveCoins(int removingValue)
    {
        if (HaveEnoughMoney(removingValue) && removingValue > 0)
        {
            _coinValue -= removingValue;
            ChangedValue?.Invoke();
            Saver.Instance.SavePlayerData();
            RefreshCoinLeaderboard();
            return true;
        }

        if (removingValue == 0)
        {
            return true;
        }

        return false;
    }

    private void RefreshCoinLeaderboard()
    {
#if UNITY_WEBGL && !UNITY_EDITOR
        Leaderboard.SetScore("CoinValue", _coinValue);
#endif
    }
}
