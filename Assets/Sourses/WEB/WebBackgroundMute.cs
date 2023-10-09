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
        if (isBackground)
        {
            SoundController.Instance.Mute();
            MusicController.Instance.Mute();
        }
        else
        {
            if (Saver.Instance.SaveData.IsSoundOn&&!AdController.Instance.IsAdShowing)
            {
                SoundController.Instance.UnMute();
            }

            if (Saver.Instance.SaveData.IsMusicOn&&!AdController.Instance.IsAdShowing)
            {
                MusicController.Instance.UnMute();
            }
        }
    }
}
