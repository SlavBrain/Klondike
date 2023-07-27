using System;

public class BetChangerModel
{
    private int _currentBetNumber;
    private int[] _betVariables = new[] { 10, 20, 50, 100, 250 };
    private BetChangerView _view;

    public BetChangerModel(BetChangerView view)
    {
        _view = view;
        SignToView();
        SetStartCurrentValue();
    }

    public event Action<int> ChangedBetValue;

    public int CurrentBet
    {
        get
        {
            _currentBetNumber = Math.Clamp(_currentBetNumber, 0, _betVariables.Length - 1);
            return _betVariables[_currentBetNumber];
        }
    }

    public bool IsMinValue => _currentBetNumber == 0;
    public bool IsMaxValue => _currentBetNumber == _betVariables.Length-1;

    private void SignToView()
    {
        _view.UpButton.onClick.AddListener(UpBet);
        _view.DownButton.onClick.AddListener(DownBet);
        _view.Initialize += SetStartCurrentValue;
    }

    private void SetStartCurrentValue()
    {
        _currentBetNumber = 0;
        ChangedBetValue?.Invoke(CurrentBet);
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
