using UnityEngine;
using UnityEngine.Scripting;
using System;
using Agava.YandexGames;

public class Saver : MonoBehaviour
{
    public static Saver Instance { get; private set; }
    public static bool IsLoaded { get; private set; }
    public SaveData SaveData;

    public void Initialize()
    {
        if (Instance == null)
        {
            transform.parent = null;
            DontDestroyOnLoad(gameObject);
            Instance = this;
            IsLoaded = false;
            Load();
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void SaveLastRewardDay(DateTime date)
    {
        SaveData.LastRewardedDay = date.ToString();
        Debug.Log("SaveRewardDate "+SaveData.LastRewardedDay);
        Save();
    }

    public DateTime GetLastRewardDay()
    {
        DateTime.TryParse(SaveData.LastRewardedDay, out DateTime lastRewardDate);
        return lastRewardDate;
    }

    public void SavePlayerData()
    {
        SaveData.CoinValue = PlayerData.Instance.CoinValue;
        Save();
    }

    public void SaveLanguage(Languages language)
    {
        SaveData.LastLanguage = language;
        Save();
    }

    public void SaveSoundMute(bool value)
    {
        SaveData.IsSoundOn = value;
        Save();
    }

    public void Save()
    {
#if UNITY_WEBGL&&!UNITY_EDITOR
        PlayerAccount.SetCloudSaveData(JsonUtility.ToJson(SaveData));
#else
        PlayerPrefs.SetString("SaveData",JsonUtility.ToJson(SaveData));
#endif
    }

    private void Load()
    {
#if UNITY_WEBGL&& !UNITY_EDITOR
        Debug.Log("Authorized -"+PlayerAccount.IsAuthorized+"; HasPersonalProfileDataPermission-"+PlayerAccount.HasPersonalProfileDataPermission);
        Debug.Log("Loading");
        PlayerAccount.GetCloudSaveData(onSuccessCallback: OnLoaded);
#else
        if (PlayerPrefs.HasKey("SaveData"))
        {
            OnLoaded(PlayerPrefs.GetString("SaveData"));
        }
        else
        {
            Debug.Log("null savedata");
            SaveData = new SaveData();
            IsLoaded = true;
        }
#endif

        Debug.Log("Load"+ SaveData.CoinValue);
        Debug.Log("Load"+ GetLastRewardDay());
    }

    private void OnLoaded(string jsonData)
    {
        Debug.Log("OnLoading");
        SaveData = JsonUtility.FromJson<SaveData>(jsonData);
        IsLoaded = true;
    }
}

[Serializable]
public class SaveData
{
    [field: Preserve] public int CoinValue;
    [field: Preserve] public bool IsSoundOn;
    [field: Preserve] public bool IsMusicOn;
    [field: Preserve] public bool IsTrained;
    [field: Preserve] public string LastRewardedDay;
    [field: Preserve] public int LastBet;
    [field: Preserve] public int StartingGameCount;
    [field: Preserve] public int CompleteGameCount;
    [field: Preserve] public Languages LastLanguage;
}


