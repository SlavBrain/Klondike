using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class BetChangerView : MonoBehaviour
{
    [SerializeField] private TMP_Text _value;
    [SerializeField] private Button _downButton;
    [SerializeField] private Button _upButton;
    
    
    private int _currentBetNumber;
    private int[] _betVariables = new[] { 10, 20, 50, 100, 250 };

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
    
    private void SetStartCurrentValue()
    {
        _currentBetNumber = 0;
    }

    private void SetTextValue(int value)
    {
        _value.text = value.ToString();
    }
    
    private void UpBet()
    {
        if (!IsMaxValue)
        {
            _currentBetNumber++;
        }
        
        ChangedBetValue?.Invoke(CurrentBet);
    }
    
    private void DownBet()
    {
        if (!IsMinValue)
        {
            _currentBetNumber--;
        }
        
        ChangedBetValue?.Invoke(CurrentBet);
    }
}
