using UnityEngine;
using UnityEngine.UI;

public class SettingMenu : MonoBehaviour
{
    [SerializeField] private Toggle _soundToggle;

    public void Initialize()
    {
        //_soundToggle.isOn = Saver.Instance.SaveData.MusicChanged;
    }
    
    public void Enable() => gameObject.SetActive(true);
    public void Disable() => gameObject.SetActive(false);
}
