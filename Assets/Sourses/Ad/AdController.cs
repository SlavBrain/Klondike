using System;
using Agava.YandexGames;
using UnityEngine;

public class AdController : MonoBehaviour
{
    public static AdController Instance;

    public void Initialize()
    {
        if (Instance == null)
        {
            transform.parent = null;
            Instance = this;
            gameObject.AddComponent<InterstitialCounter>().Initialize();
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }
    
    public void ShowVideoAd(Action onRewardedCallback = null)
    {
#if UNITY_WEBGL&&!UNITY_EDITOR
        VideoAd.Show(onRewardedCallback: onRewardedCallback,onOpenCallback:OnAdStarting,onCloseCallback:OnAdEnded);
#else
        Debug.Log("ShowingVideoAd");
#endif
    }

    public void ShowInterstitial(Action onSuccess=null)
    {
        
        OnAdStarting();
#if UNITY_WEBGL&&!UNITY_EDITOR
        InterstitialAd.Show(onOpenCallback:OnInterstitialAdStarting,onCloseCallback:OnAdEnded);
#else
        Debug.Log("ShowingInterstitial");
#endif
        OnAdEnded();
    }

    private void OnInterstitialAdStarting()
    {
        OnAdStarting();
        InterstitialCounter.Instance.ResetCount();
    }

    private void OnAdStarting()
    {
        if (SoundController.Instance != null) SoundController.Instance.Mute();
        Time.timeScale = 0;
    }

    private void OnAdEnded()
    {
        if (SoundController.Instance != null) SoundController.Instance.UnMute();
        Time.timeScale = 1;
    }
    
    private void OnAdEnded(bool isClose)
    {
        if (SoundController.Instance != null) SoundController.Instance.UnMute();
        Time.timeScale = 1;
    }
}
