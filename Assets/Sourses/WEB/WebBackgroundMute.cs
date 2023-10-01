using System;
using System.Collections;
using System.Collections.Generic;
using Agava.WebUtility;
using UnityEngine;

public class WebBackgroundMute : MonoBehaviour
{
    private void Awake()
    {
#if!UNITY_WEBGL||UNITY_EDITOR
        Destroy(this);
#else 
        DontDestroyOnLoad(gameObject);
#endif
    }

    private void OnEnable()
    {
        WebApplication.InBackgroundChangeEvent += OnInBackgroundChange;
    }

    private void OnDisable()
    {
        WebApplication.InBackgroundChangeEvent -= OnInBackgroundChange;
    }

    private void OnInBackgroundChange(bool isBackground)
    {
        Debug.Log("onBackground");
        if (isBackground)
        {
            SoundController.Instance.Mute();
            MusicController.Instance.Mute();
        }
        else
        {
            if (Saver.Instance.SaveData.IsSoundOn)
            {
                SoundController.Instance.UnMute();
            }

            if (Saver.Instance.SaveData.IsMusicOn)
            {
                MusicController.Instance.UnMute();
            }
        }
    }
}
