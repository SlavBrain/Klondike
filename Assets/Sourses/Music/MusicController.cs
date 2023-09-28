using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class MusicController : MonoBehaviour
{
    [SerializeField] private List<AudioClip> _music;
    private int _currentMusicIndex = 0;
    private AudioSource _audioSource;
    private Coroutine _playingMusic;
    
    public static MusicController Instance;

    public void Initialize()
    {
        if (Instance == null)
        {
            transform.parent = null;
            Instance = this;
            DontDestroyOnLoad(gameObject);
            _audioSource = GetComponent<AudioSource>();
            SetMusicOn(Saver.Instance.SaveData.IsMusicOn);
            SetMusicClip();
        }
        else
        {
            Destroy(gameObject);
        }
    }
    
    public void Mute()
    {
        Debug.Log("mute");
        _audioSource.mute = true;
        _audioSource.volume = 0;
    }

    public void UnMute()
    {
        Debug.Log("unmute");
        _audioSource.mute = false;
        _audioSource.volume = 1;
    }

    public void SetMusicOn(bool isOn)
    {
        if (isOn)
        {
            UnMute();
        }
        else
        {
            Mute();
        };
    }

    private void SetMusicClip()
    {
        
        if (_music.Count == 0)
        {
            return;
        }

        if (_currentMusicIndex >= _music.Count)
        {
            _currentMusicIndex = 0;
        }

        if (_playingMusic != null)
        {
            StopCoroutine(_playingMusic);
        }

        _playingMusic=StartCoroutine(MusicSwitch());
    }

    private IEnumerator MusicSwitch()
    {
        _audioSource.clip = _music[_currentMusicIndex];
        _audioSource.Play();
        _currentMusicIndex++;
        
        yield return new WaitUntil(() => (_audioSource.isPlaying == false && _audioSource.mute == false));
        
        SetMusicClip();
    }
}
