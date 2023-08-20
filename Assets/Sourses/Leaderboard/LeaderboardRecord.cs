using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class LeaderboardRecord : MonoBehaviour
{
    [SerializeField] private TMP_Text _nick;
    [SerializeField] private TMP_Text _score;
    [SerializeField] private TMP_Text _rank;

    public void Initialization(string playerName, int score, int rank)
    {
        _nick.text = playerName;
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
