using UnityEngine;

public class MainMenuBootstrap : MonoBehaviour
{
    [SerializeField] private MainMenuPanel _mainMenuPanel;
    [SerializeField] private SettingMenu _settingMenu;
    [SerializeField] private LeaderboardView _leaderboardMenu;
    [SerializeField] private PlayerData _playerData;
    [SerializeField] private Rewarder _rewarder;
    [SerializeField] private Saver _saver;
    [SerializeField] private SoundController _soundController;

    private void Awake()
    {
        _saver.Initialize();
        _playerData.Initialize();
        _soundController.Initialize();
        _mainMenuPanel.Initialize();
        _settingMenu.Initialize();
        _leaderboardMenu.Initialize();
        _rewarder.Initialize();
    }
}
