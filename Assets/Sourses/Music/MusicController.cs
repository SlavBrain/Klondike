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
    
    public void Mute() => _audioSource.mute=true;
    public void UnMute() => _audioSource.mute=false;
    
    public void SetMusicOn(bool isOn)=>_audioSource.mute = !isOn;

    private void SetMusicClip()
    {
        Debug.Log("set music clip "+(_audioSource.isPlaying == false) + " " + (_audioSource.mute == false));
        
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
        Debug.Log("coroutine "+(_audioSource.isPlaying == false) + " " + (_audioSource.mute == false));
        
        yield return new WaitUntil(() => (_audioSource.isPlaying == false && _audioSource.mute == false));
        Debug.Log("coroutine2 "+(_audioSource.isPlaying == false) + " " + (_audioSource.mute == false));
        
        SetMusicClip();
    }
}
