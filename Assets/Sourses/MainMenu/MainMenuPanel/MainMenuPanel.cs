using System;
using System.Collections;
using System.Collections.Generic;
using Agava.YandexGames;
using IJunior.TypedScenes;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class MainMenuPanel : MonoBehaviour
{
    [SerializeField] private Button _playButton;
    [SerializeField] private Button _settingButton;
    [SerializeField] private Button _leaderboardButton;
    [SerializeField] private Button _rewardButton;
    [SerializeField] private BetChanger _betChanger;
    [SerializeField] private TMP_Text _nicknameLabel;
    [SerializeField] private TMP_Text _playerWalletValueText;

    [SerializeField] private LeaderboardView _leaderboardMenu;
    [SerializeField] private SettingMenu _settingMenu;
    

    public static MainMenuPanel Instance { get; private set; }
    
    public void Initialize()
    {
        if (Instance == null)
        {
            transform.parent = null;
            Instance = this;
            _betChanger.Initialize();
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
        SetNicknameLabel();
    }

    private void OnDisable()
    {
        _playButton.onClick.RemoveListener(OnPlayButtonClick);
        _rewardButton.onClick.RemoveListener(OnRewardButtonClick);
        PlayerData.Instance.ChangedValue -= SetWalletValueText;
    }

    private void OnPlayButtonClick()
    {
        if (_betChanger.CurrentBet > 0)
        {
            if (PlayerData.Instance.HaveEnoughMoney(Saver.Instance.SaveData.LastBet))
            {
                KlondikeGame.Load();
            }
            else
            {
                Debug.Log("Not enough money");
            }
        }
        else if(_betChanger.CurrentBet==0)
        {
            KlondikeGame.Load();
        }
        else
        {
            throw new Exception("Negative bet value");
        }
    }

    private void SetNicknameLabel()
    {
        _nicknameLabel.SetText(GetNickname());
    }

    private string GetNickname()
    {
        
#if !UNITY_WEBGL || UNITY_EDITOR
        return "Anonymous";
#endif
        
        PlayerAccount.GetProfileData((result) =>
        {
            string name = result.publicName;
            if (string.IsNullOrEmpty(name))
                name = "Anonymous";
        });
        return name;
    }

    private void OnRewardButtonClick()
    {
        
    }

    private void SetWalletValueText()
    {
        _playerWalletValueText.text = PlayerData.Instance.CoinValue.ToString();
    }
}
