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
        [SerializeField] private BetChanger _betChanger;
        [SerializeField] private TMP_Text _nicknameLabel;
        [SerializeField] private TMP_Text _playerWalletValueText;
        [SerializeField] private MessagePanel _notEnoughCoinsPanel;

        public static MainMenuPanel Instance { get; private set; }
    
        public void Initialize()
        {
            if (Instance == null)
            {
                YandexInitialization();
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
            PlayerData.Instance.ChangedValue += SetWalletValueText;
            SetWalletValueText();
            SetNicknameLabel();
        }

        private void OnDisable()
        {
            _playButton.onClick.RemoveListener(OnPlayButtonClick);
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
                    _notEnoughCoinsPanel.gameObject.SetActive(true);
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
            Debug.Log(GetNickname());
            _nicknameLabel.SetText(GetNickname());
        }

        private string GetNickname()
        {
            name= "Anonymous";
            Debug.Log("getNickName ");
#if UNITY_WEBGL && !UNITY_EDITOR
        PlayerAccount.GetProfileData((result) =>
        {
            string name = result.publicName;
            Debug.Log("Name "+name);
            if (string.IsNullOrEmpty(name))
                name = "Anonymous";
        });
#endif
            return name;
        }

        private void SetWalletValueText()
        {
            _playerWalletValueText.text = PlayerData.Instance.CoinValue.ToString();
        }
        
        private IEnumerator YandexInitialization()
        {
#if !UNITY_WEBGL || UNITY_EDITOR
            yield break;
#endif
            yield return YandexGamesSdk.Initialize();
            }
        }
        
    

