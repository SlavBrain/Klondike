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

        private PlayerAccountProfileDataResponse _playerAccountProfileDataResponse;
        public static MainMenuPanel Instance { get; private set; }
    
        public void Initialize()
        {
            if (Instance == null)
            {
                YandexInitialization();
                transform.SetParent(null);
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
            if (YandexGamesSdk.IsInitialized)
            {
                SetWalletValueText();
                UpdateProfileData();
            }
            else
            {
                YandexInitialization();
            }
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

        private void SetUserData()
        {
            UpdateProfileData();
            SetWalletValueText();
        }

        private void UpdateProfileData()
        {
            StartCoroutine(PlayerAccountProfileDataResponseLoading());
        }

        private void SetNickname(string nickname=null)
        {
            if (string.IsNullOrEmpty(nickname))
                _nicknameLabel.SetText("Anonymous");
            else
                _nicknameLabel.SetText(nickname);
        }

        private void SetWalletValueText()
        {
            _playerWalletValueText.text = PlayerData.Instance.CoinValue.ToString();
        }
        
        private IEnumerator YandexInitialization()
        {
#if UNITY_WEBGL &&!UNITY_EDITOR
            yield return YandexGamesSdk.Initialize(SetUserData);
#else
            yield break;
#endif
        }

        private IEnumerator PlayerAccountProfileDataResponseLoading()
        {
#if UNITY_WEBGL && !UNITY_EDITOR
            bool isReceived = false;
            WaitForSeconds delay = new WaitForSeconds(1);
            
            while (!isReceived)
            {
                PlayerAccount.GetProfileData(result =>
                {
                    SetNickname(result.publicName);
                    isReceived = true;
                });
                
                yield return delay;
            }
            
#else
            SetNickname("Anonymous");
            yield break;
#endif
        }
    }
        
    

