using UnityEngine;
using UnityEngine.Scripting;
using System;
using Agava.YandexGames;

public class Saver : MonoBehaviour
{
    public static Saver Instance { get; private set; }
    public static bool IsLoaded { get; private set; }
    public SaveData SaveData { get; private set; }

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

    public void SaveMusicSetting()
    {
        
    }

    public void GetSavedMusicSetting()
    {
        
    }

    public void EndTraining()
    {
        
    }

    private void Load() => PlayerAccount.GetCloudSaveData(onSuccessCallback: OnLoaded);

    private void OnLoaded(string jsonData)
    {
        SaveData = JsonUtility.FromJson<SaveData>(jsonData);
        IsLoaded = true;
    }
}

[Serializable]
public class SaveData
{
    [field: Preserve] public PlayerData PlayerData;
    [field: Preserve] public float MusicVolumeValue;
    [field: Preserve] public bool MusicChanged;
    [field: Preserve] public bool IsTrained;
}


