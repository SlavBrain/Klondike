using TMPro;
using UnityEngine;

public class LeaderboardRecord : MonoBehaviour
{
    [SerializeField] private TMP_Text _nick;
    [SerializeField] private TMP_Text _score;
    [SerializeField] private TMP_Text _rank;
    private string _nullName = "Anonymous";
    
    public void Initialization(string playerName, int score, int rank)
    {
        Debug.Log(playerName);
        if (playerName == null)
        {
            Debug.Log("null name "+ rank);
        }
        if (!string.IsNullOrEmpty(playerName))
        {
            _nick.text = playerName;
        }
        else
        {
            _nick.text = _nullName;
        }
        
        _score.text = score.ToString();
        _rank.text = rank.ToString();
    }

    public void Enable(Panel panel)
    {
        transform.SetParent(panel.transform);
        gameObject.SetActive(true);
    }

    public void Disable()
    {
        gameObject.SetActive(false);
    }
}
