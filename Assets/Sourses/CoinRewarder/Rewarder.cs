using System;
using UnityEngine;
using Debug = UnityEngine.Debug;

public class Rewarder : MonoBehaviour
{
    [SerializeField] private int _dailyReward=100;
    [SerializeField] private int _videoReward=50;
    
    public void Initialize()
    {
        TryGiveDailyReward();
    }

    public void OnVideoRewardButtonClick()
    {
        Debug.Log("RewardButtonClick");
        AdController.Instance.ShowVideoAd(onRewardedCallback:GiveVideoReward);
    }

    private void GiveVideoReward()
    {
        Debug.Log("GiveVideoReward");
        PlayerData.Instance.AddCoins(_videoReward);
    }
    
    private void TryGiveDailyReward()
    {
        if (Saver.Instance.GetLastRewardDay() < DateTime.Now.Date || Saver.Instance.GetLastRewardDay() == DateTime.MinValue)
        {
            Saver.Instance.SaveLastRewardDay(DateTime.Now.Date);
            PlayerData.Instance.AddCoins(_dailyReward);
        }
        else
        {
            Debug.Log("Reward took today");
        }
    }
}
