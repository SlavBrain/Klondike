using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wallet : MonoBehaviour
{
    public int Value { get; private set; }

    public void Add(int addingValue)
    {
        if (addingValue > 0)
        {
            Value += addingValue;
        }
    }

    public bool TryRemove(int removingValue)
    {
        if (removingValue < Value && removingValue > 0)
        {
            Value -= removingValue;
            return true;
        }

        return false;
    }
}
