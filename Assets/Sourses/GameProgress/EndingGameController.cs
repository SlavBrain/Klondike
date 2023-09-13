using UnityEngine;

public class EndingGameController : MonoBehaviour
{
    [SerializeField] private ParticleSystem _particleSystem;

    private EndGameInspector _endGameInspector;

    public void Initialize(EndGameInspector endGameInspector)
    {
        _endGameInspector = endGameInspector;
        _endGameInspector.GameEnded += StartEndingGameActions;
    }


    private void StartEndingGameActions()
    {
        PlayerData.Instance.AddCoins(Saver.Instance.SaveData.LastBet*2);
        _particleSystem.Play();
    }

}
