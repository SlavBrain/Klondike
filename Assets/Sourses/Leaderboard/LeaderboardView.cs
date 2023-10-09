using System;
using System.Collections;
using System.Collections.Generic;
using Agava.YandexGames;
using UnityEngine;
using UnityEngine.UI;

public class LeaderboardView : MonoBehaviour
{
    [SerializeField] private Panel _panel;
    [SerializeField] private Panel _content;
    [SerializeField] private Panel _authorizedPanel;
    [SerializeField] private Panel _unauthorizedPanel;
    [SerializeField] private Button _acceptButton;
    [SerializeField] private Button _rejectButton;
    [SerializeField] private LeaderboardPool _pool;
    [SerializeField] private Button _openButton;
    [SerializeField] private Button _closeButton;

    public static LeaderboardView Instance { get; private set; }
    
    public void Initialize()
    {
        Enable();

        if (Instance == null)
        {
            transform.SetParent(null);
            Instance = this;
            
            Disable();
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void OnEnable()
    {
        _acceptButton.onClick.AddListener(AcceptAuthorize);
        _rejectButton.onClick.AddListener(Disable);
        _openButton.onClick.AddListener(Enable);
        _closeButton.onClick.AddListener(Disable);
    }

    private void OnDisable()
    {
        _openButton.onClick.RemoveListener(Enable);
        _acceptButton.onClick.RemoveListener(AcceptAuthorize);
        _rejectButton.onClick.RemoveListener(Disable);
        _closeButton.onClick.RemoveListener(Disable);
    }

    private void Enable()
    {
        _panel.Enable();
#if UNITY_WEBGL && !UNITY_EDITOR
        if (PlayerAccount.HasPersonalProfileDataPermission == false)
        {
            _unauthorizedPanel.Enable();
        }
        else
        {
            Leaderboard.GetPlayerEntry("CoinValue", currentPlayer =>
                {
                    Leaderboard.GetEntries("CoinValue", players =>
                    {
                        _authorizedPanel.Enable();
                        _pool.EnablePlayersRecords(currentPlayer,players.entries,_content);
                    });
                }
                );
        }
#endif
    }

    private void Disable()
    {
        _authorizedPanel.Disable();
        _unauthorizedPanel.Disable();
        _pool.DisableRecords();
        _panel.Disable();
    }

    private void AcceptAuthorize()
    {
        PlayerAccount.Authorize();
        PlayerAccount.RequestPersonalProfileDataPermission();
        Disable();
        Enable();
    }
}
