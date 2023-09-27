using UnityEngine;
using UnityEngine.UI;

public class SettingMenu : MonoBehaviour
{
    [SerializeField] private SoundMuteChanger _soundMuteChanger;
    [SerializeField] private MusicMuteChanger _musicMuteChanger;

    public void Initialize()
    {
        _soundMuteChanger.Initialize();
        _musicMuteChanger.Initialize();
    }
    
    public void Enable() => gameObject.SetActive(true);
    public void Disable() => gameObject.SetActive(false);
}
