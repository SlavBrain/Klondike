using UnityEngine;

public class MainMenuBootstrap : MonoBehaviour
{
    [SerializeField] private MainMenu _mainMenu;
    [SerializeField] private SettingMenu _settingMenu;
    [SerializeField] private LeaderboardMenu _leaderboardMenu;
    [SerializeField] private PlayerData _playerData;
    [SerializeField] private Rewarder _rewarder;
    [SerializeField] private Saver _saver;

    private void Awake()
    {
        _saver.Initialize();
        _playerData.Initialize();
        _mainMenu.Initialize();
        _settingMenu.Initialize();
        _leaderboardMenu.Initialize();
        _rewarder.Initialize();
    }
}
