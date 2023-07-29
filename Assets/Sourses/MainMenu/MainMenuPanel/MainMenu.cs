using System;
using System.Collections;
using System.Collections.Generic;
using IJunior.TypedScenes;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
    [SerializeField] private Button _playButton;
    [SerializeField] private Button _settingButton;
    [SerializeField] private Button _leaderboardButton;
    [SerializeField] private Button _rewardButton;
    [SerializeField] private BetChangerView _betChangerView;
    [SerializeField] private TMP_Text _playerWalletValueText;

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
        _rewardButton.onClick.AddListener(OnRewardButtonClick);
        PlayerData.Instance.ChangedValue += SetWalletValueText;
        SetWalletValueText();
    }

    private void OnDisable()
    {
        _playButton.onClick.RemoveListener(OnPlayButtonClick);
        _rewardButton.onClick.RemoveListener(OnRewardButtonClick);
        PlayerData.Instance.ChangedValue += SetWalletValueText;
    }

    private void OnPlayButtonClick()
    {
        KlondikeGame.Load();
    }

    private void OnRewardButtonClick()
    {
        
    }

    private void SetWalletValueText()
    {
        _playerWalletValueText.text = PlayerData.Instance.CoinValue.ToString();
    }
}
