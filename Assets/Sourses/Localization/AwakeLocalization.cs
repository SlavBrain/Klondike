using Agava.YandexGames;
using Lean.Localization;
using UnityEngine;

public class AwakeLocalization : MonoBehaviour
{
        private void Awake() => SetLanguageAll();

        private void SetLanguageAll()
        {
            switch (DefaultLanguage())
            {
                case "ru":
                    LeanLocalization.SetCurrentLanguageAll(Languages.Russian.ToString());
                    break;
                case "en":
                    LeanLocalization.SetCurrentLanguageAll(Languages.English.ToString());
                    break;
                case "tr":
                    LeanLocalization.SetCurrentLanguageAll(Languages.Turkish.ToString());
                    break;
            }
        }

        private string DefaultLanguage()
        {
#if UNITY_WEBGL && !UNITY_EDITOR
            return YandexGamesSdk.Environment.i18n.lang;
#else
            return "ru";
#endif
        }
}
