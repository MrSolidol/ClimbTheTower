using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Localization;
using UnityEngine.Localization.Settings;


public class LocalizeManager : MonoBehaviour
{
    private static LocalizeManager Instance;

    private int selectedLocale = 0;

    private bool active = false;
    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }



    private void Start()
    {
    }

    private void Load()
    {
        //ProgressSave.Instance.Load();
        //selectedLocale = ProgressSave.Instance.localizationID;
    }

    private void Save() 
    {
        //ProgressSave.Instance.localizationID = selectedLocale;
        //ProgressSave.Instance.Save();
    }

    public void ChangeLocale(int localeID) 
    {
        if (active) { return; }
        StartCoroutine(SetLocale(localeID));
    }


    IEnumerator SetLocale(int _localID) 
    {
        active = true;
        yield return LocalizationSettings.InitializationOperation;
        LocalizationSettings.SelectedLocale = LocalizationSettings.AvailableLocales.Locales[_localID];
        active = false;
        selectedLocale = _localID;
        Save();
    }
}
