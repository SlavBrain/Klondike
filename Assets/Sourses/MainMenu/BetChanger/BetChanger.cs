using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class BetChanger : MonoBehaviour
{
    [SerializeField] private TMP_Text _value;
    [SerializeField] private Button _downButton;
    [SerializeField] private Button _upButton;
    
    
    private int _currentBetNumber;
    private int[] _betVariables = new[] {0, 10, 20, 50, 100, 250 , 1000, 2500, 10000};
    private bool _isMaxValue => _currentBetNumber == _betVariables.Length - 1;
    private bool _isMinValue => _currentBetNumber == 0;

    public int CurrentBet => _betVariables[_currentBetNumber];

    public void Initialize()
    {
        SetStartCurrentValue();
    }
    
    private void OnEnable()
    {
        _upButton.onClick.AddListener(UpBet);
        _downButton.onClick.AddListener(DownBet);
    }

    private void OnDisable()
    {
        _upButton.onClick.RemoveListener(UpBet);
        _downButton.onClick.RemoveListener(DownBet);
    }
    
    private void SetStartCurrentValue()
    {
        int lastBet = Saver.Instance.SaveData.LastBet;
        
        for (int i = _betVariables.Length - 1; i >= 0; i--)
        {
            if (lastBet >= _betVariables[i])
            {
                _currentBetNumber = i;
                break;
            }
        }
        
        SetTextValue();
    }

    private void SetTextValue()
    {
        _value.text = _betVariables[_currentBetNumber].ToString();
    }
    
    private void UpBet()
    {
        if (!_isMaxValue)
        {
            _currentBetNumber++;
            Saver.Instance.SaveData.LastBet = CurrentBet;
        }
        
        SetTextValue();
    }
    
    private void DownBet()
    {
        if (!_isMinValue)
        {
            _currentBetNumber--;
            Saver.Instance.SaveData.LastBet = CurrentBet;
        }
        
        SetTextValue();
    }
}
