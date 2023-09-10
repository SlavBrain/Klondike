using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MessagePanel : MonoBehaviour
{
    [SerializeField] private float _showTime = 1f;
    private Coroutine _showing;

    private void OnEnable()
    {
        Show();
    }

    public void Show()
    {
        if (_showing != null)
        {
            StopCoroutine(_showing);
        }

        _showing = StartCoroutine(WaitShowingTime());
    }

    private IEnumerator WaitShowingTime()
    {
        yield return new WaitForSeconds(_showTime);
        gameObject.SetActive(false);
    }
}
