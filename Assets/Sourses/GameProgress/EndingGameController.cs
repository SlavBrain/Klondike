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
        Debug.Log("StartEnding");
        _particleSystem.Play();
    }

}
