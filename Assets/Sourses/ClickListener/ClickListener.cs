using System;
using UnityEngine;

public abstract class ClickListener : MonoBehaviour
{
    public event Action Clicked;

    public void OnClick()
    {
        Clicked?.Invoke();
    }
    
    private void OnEnable()
    {
        gameObject.layer = LayerMask.NameToLayer("ClickListenerLayer");
    }
}
