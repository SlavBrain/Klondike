using System;
using System.Collections;
using System.Collections.Generic;
using Lean.Localization;
using UnityEngine;
using UnityEngine.UI;

public class LanguageChanger : MonoBehaviour
{
    [SerializeField] private LeanLocalization _leanLocalization;
    [SerializeField] private Toggle _russianToggle;
    [SerializeField] private Toggle _englishToggle;
    [SerializeField] private Toggle _turkishToggle;

    private void Awake()
    {
        SetLanguageToggle();
    }

    private void OnEnable()
    {
        _russianToggle.onValueChanged.AddListener(ChangeToRussian);
        _englishToggle.onValueChanged.AddListener(ChangeToEnglish);
        _turkishToggle.onValueChanged.AddListener(ChangeToTurkish);
    }

    private void OnDisable()
    {
        _russianToggle.onValueChanged.RemoveListener(ChangeToRussian);
        _englishToggle.onValueChanged.RemoveListener(ChangeToEnglish);
        _turkishToggle.onValueChanged.RemoveListener(ChangeToTurkish);
    }

    private void SetLanguageToggle()
    {
        if (_leanLocalization.CurrentLanguage == Languages.Russian.ToString())
        {
            SetRussianToggle();
            _russianToggle.isOn = true;
        }
        else if (_leanLocalization.CurrentLanguage == Languages.English.ToString())
        {
            SetEnglishToggle();
            _englishToggle.isOn = true;
        }
        else if (_leanLocalization.CurrentLanguage == Languages.Turkish.ToString())
        {
            SetTurkishToggle();
            _turkishToggle.isOn = true;
        }
    }

    private void ChangeToRussian(bool value)
    {
        if (value == true)
        {
            LeanLocalization.SetCurrentLanguageAll(Languages.Russian.ToString());
            SetLanguageToggle(); 
            Saver.Instance.SaveLanguage(Languages.Russian);
        }
    }

    private void ChangeToEnglish(bool value)
    {
        if (value == true)
        {
            LeanLocalization.SetCurrentLanguageAll(Languages.English.ToString());
            SetLanguageToggle();
            Saver.Instance.SaveLanguage(Languages.English);
        }
    }

    private void ChangeToTurkish(bool value)
    {
        if (value == true)
        {
            LeanLocalization.SetCurrentLanguageAll(Languages.Turkish.ToString());
            SetLanguageToggle();
            Saver.Instance.SaveLanguage(Languages.Turkish);
        }
    }

    private void SetRussianToggle()
    {
        _russianToggle.interactable = false;
        _englishToggle.interactable = true;
        _turkishToggle.interactable = true;
        
        _englishToggle.isOn = false;
        _turkishToggle.isOn = false;
    }
    
    private void SetEnglishToggle()
    {
        _russianToggle.interactable = true;
        _englishToggle.interactable = false;
        _turkishToggle.interactable = true;
        
        _russianToggle.isOn = false;
        _turkishToggle.isOn = false;
    }
    
    private void SetTurkishToggle()
    {
        _russianToggle.interactable = true;
        _englishToggle.interactable = true;
        _turkishToggle.interactable = false;
        
        _englishToggle.isOn = false;
        _russianToggle.isOn = false;
    }
}
