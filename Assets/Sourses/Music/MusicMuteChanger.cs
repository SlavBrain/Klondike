using UnityEngine;
using UnityEngine.UI;

public class MusicMuteChanger : MonoBehaviour
{
    [SerializeField] private Toggle _muteToggle;

    public void Initialize()
    {
        _muteToggle.isOn = Saver.Instance.SaveData.IsMusicOn;
    }
    
    private void OnEnable()
    {
        _muteToggle.onValueChanged.AddListener(ChangeMute);
    }

    private void OnDisable()
    {
        _muteToggle.onValueChanged.RemoveListener(ChangeMute);
    }

    private void ChangeMute(bool value)
    {
        MusicController.Instance.SetMusicOn(value);
        Saver.Instance.SaveMusicMute(value);
    }
}
