using System;
using UnityEngine;

public class Wallet
{
    public Wallet(int value=1000)
    {
        Value = value;
    }
    
    public int Value { get; private set; } = 1000;

    public event Action ChangedValue; 
    
    public void Add(int addingValue)
    {
        if (addingValue > 0)
        {
            Value += addingValue;
            ChangedValue?.Invoke();
        }
    }

    public bool TryRemove(int removingValue)
    {
        if (removingValue < Value && removingValue > 0)
        {
            Value -= removingValue;
            ChangedValue?.Invoke();
            return true;
        }

        return false;
    }
}
