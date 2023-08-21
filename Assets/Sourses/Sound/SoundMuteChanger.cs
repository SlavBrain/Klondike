using UnityEngine;
using UnityEngine.UI;

public class SoundMuteChanger : MonoBehaviour
{
    [SerializeField] private Toggle _muteToggle;

    public void Initialize()
    {
        _muteToggle.isOn = Saver.Instance.SaveData.IsSoundOn;
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
        SoundController.Instance.SetSoundOn(value);
        Saver.Instance.SaveSoundMute(value);
    }
}
