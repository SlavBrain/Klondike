using System;
using UnityEngine;
using Debug = UnityEngine.Debug;

public class Rewarder : MonoBehaviour
{
    [SerializeField] private int _dailyReward;
    [SerializeField] private int _videoReward;
    
    public void Initialize()
    {
        TryGiveDailyReward();
    }

    public void GiveVideoReward()
    {
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
