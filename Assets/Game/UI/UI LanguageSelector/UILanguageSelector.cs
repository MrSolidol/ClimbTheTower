using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Localization.Settings;

public class UILanguageSelector : MonoBehaviour
{
    public UnityEvent<string> eLanguageChanged; 

    [SerializeField] private TMP_Dropdown dropdown;

    private Dictionary<int, string> languagesMap;


    public void Initialization(string languageSelected) 
    {
        languagesMap = new Dictionary<int, string>()
        {
            [0] = "en",
            [1] = "ru",
            [2] = "es",
            [3] = "de",
            [4] = "fr"
        };

        switch (languageSelected) 
        {
            case "en":
                dropdown.value = 0;
                break;
            case "ru":
                dropdown.value = 1;
                break;
            case "es":
                dropdown.value = 2;
                break;
            case "de":
                dropdown.value = 3;
                break;
            case "fr":
                dropdown.value = 4;
                break;
        }

        ChangeLanguage(languageSelected);
    }


    private void OnEnable()
    {
        dropdown.onValueChanged.AddListener(OnValueChange);
    }

    private void OnDisable()
    {
        dropdown.onValueChanged.RemoveListener(OnValueChange);
    }


    private void OnValueChange(int value) 
    {
        ChangeLanguage(languagesMap[value]);
        eLanguageChanged?.Invoke(languagesMap[value]);
    }

    private void ChangeLanguage(string code)
    {
        var locale = LocalizationSettings.AvailableLocales.GetLocale(code);
        LocalizationSettings.SelectedLocale = locale;
    }
}
