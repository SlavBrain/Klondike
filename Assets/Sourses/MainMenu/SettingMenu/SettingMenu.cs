using UnityEngine;
using UnityEngine.UI;

public class SettingMenu : MonoBehaviour
{
    [SerializeField] private SoundMuteChanger _muteChanger;

    public void Initialize()
    {
        _muteChanger.Initialize();
    }
    
    public void Enable() => gameObject.SetActive(true);
    public void Disable() => gameObject.SetActive(false);
}
