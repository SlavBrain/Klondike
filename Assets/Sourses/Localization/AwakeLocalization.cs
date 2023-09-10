using Agava.YandexGames;
using Lean.Localization;
using UnityEngine;

public class AwakeLocalization : MonoBehaviour
{
        private void Awake() => SetLanguageAll();

        private void SetLanguageAll()
        {
            LeanLocalization.SetCurrentLanguageAll(DefaultLanguage().ToString());
        }

        private Languages DefaultLanguage()
        {
            if (Saver.Instance.SaveData.LastLanguage == Languages.None)
            {
#if UNITY_WEBGL && !UNITY_EDITOR
            switch(YandexGamesSdk.Environment.i18n.lang)
            {
                case "ru":
                    return Languages.Russian;
                    break;
                case "en":
                    return Languages.English;
                    break;
                case "tr":
                    return Languages.Turkish;
                    break;
            }
#else
            return Languages.Russian;
#endif
            }
            else
            {
                return Saver.Instance.SaveData.LastLanguage;
            }
            
            return Languages.Russian;
        }
}
