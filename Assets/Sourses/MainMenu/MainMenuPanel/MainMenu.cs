using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
    [SerializeField] private Button _playButton;
    [SerializeField] private Button _settingButton;
    [SerializeField] private Button _leaderboardButton;
    [SerializeField] private Button _rewardButton;
    [SerializeField] private BetChangerView _betChangerView;

    [SerializeField] private LeaderboardMenu _leaderboardMenu;
    [SerializeField] private SettingMenu _settingMenu;
    

    public static MainMenu Instance { get; private set; }
    
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

    private void OnEnable()
    {
        _playButton.onClick.AddListener(OnPlayButtonClick);
        _settingButton.onClick.AddListener(OnSettingButtonClick);
        _leaderboardButton.onClick.AddListener(OnLeaderboardButtonClick);
        _rewardButton.onClick.AddListener(OnRewardButtonClick);
    }

    private void OnDisable()
    {
        _playButton.onClick.RemoveListener(OnPlayButtonClick);
        _settingButton.onClick.RemoveListener(OnSettingButtonClick);
        _leaderboardButton.onClick.RemoveListener(OnLeaderboardButtonClick);
        _rewardButton.onClick.RemoveListener(OnRewardButtonClick);
    }

    private void OnPlayButtonClick()
    {
        
    }

    private void OnSettingButtonClick()
    {
        
    }

    private void OnLeaderboardButtonClick()
    {
        
    }

    private void OnRewardButtonClick()
    {
        
    }
}
