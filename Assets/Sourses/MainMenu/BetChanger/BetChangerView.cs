using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class BetChangerView : MonoBehaviour
{
    [SerializeField] private TMP_Text _value;
    [SerializeField] private Button _downButton;
    [SerializeField] private Button _upButton;

    private BetChangerModel _model;

    public event Action Initialize;

    public Button UpButton => _upButton;
    public Button DownButton => _downButton;

    private void OnEnable()
    {
        _model = new(this);
        _model.ChangedBetValue += SetTextValue;
        Initialize?.Invoke();
    }

    private void OnDisable()
    {
        _model.ChangedBetValue -= SetTextValue;
    }

    private void SetTextValue(int value)
    {
        _value.text = value.ToString();
    }
}
