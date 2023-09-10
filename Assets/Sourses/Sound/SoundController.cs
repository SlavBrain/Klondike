using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class SoundController : MonoBehaviour
{
    [SerializeField] private AudioClip _cardMovementClip;

    private AudioSource _audioSource;
    
    public static SoundController Instance;

    public void Initialize()
    {
        if (Instance == null)
        {
            transform.parent = null;
            Instance = this;
            _audioSource = GetComponent<AudioSource>();
            SetSoundOn(Saver.Instance.SaveData.IsSoundOn);
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void PlayCardMovement()
    {
        _audioSource.clip = _cardMovementClip;
        _audioSource.Play();
    }

    public void SetSoundOn(bool value) => _audioSource.mute = !value;
    public void Mute() => _audioSource.Pause();
    public void UnMute() => _audioSource.UnPause();
}
