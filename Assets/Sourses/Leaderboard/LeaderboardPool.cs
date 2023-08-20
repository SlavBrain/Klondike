using System;
using System.Collections;
using System.Collections.Generic;
using Agava.YandexGames;
using UnityEngine;

public class LeaderboardPool : MonoBehaviour
{
    [SerializeField] private LeaderboardRecord _recordPrefab;
    [SerializeField] private LeaderboardRecord _currentPlayerRecordPrefab;

    private const int PlayerCount = 5;

    private readonly List<LeaderboardRecord> _playersRecords = new List<LeaderboardRecord>();

    private void Awake()
    {
        for (int i = 0; i < PlayerCount; i++)
        {
            LeaderboardRecord tempPlayerRecord = Instantiate(i == 0 ? _currentPlayerRecordPrefab : _recordPrefab,
                Vector3.zero, Quaternion.identity, transform);
            
            _playersRecords.Add(tempPlayerRecord);
            tempPlayerRecord.Disable();
        }
    }

    public void EnablePlayersRecords(LeaderboardEntryResponse currentPlayer, LeaderboardEntryResponse[] leaders, Panel panel)
    {
        _playersRecords[0].Initialization(currentPlayer.player.publicName, currentPlayer.score, currentPlayer.rank);
        _playersRecords[0].Enable(panel);

        for (int i = 0; i < leaders.Length; i++)
        {
            _playersRecords[i + 1].Initialization(leaders[i].player.publicName, leaders[i].score, leaders[i].rank);
            _playersRecords[i + 1].Enable(panel);
        }
    }

    public void DisableRecords()
    {
        foreach (LeaderboardRecord record in _playersRecords)
        {
            record.Disable();
        }
    }
}
