using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerData : MonoBehaviour
{
    private Wallet _wallet;
    private int _lastBetChange;
    private int _startingGameCount;
    private int _successEndedGameCount;

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
}
