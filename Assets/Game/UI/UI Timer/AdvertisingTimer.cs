using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Localization.Settings;
using UnityEngine.Localization.Tables;

public class AdvertisingTimer : MonoBehaviour
{
    public UnityEvent eWarningTimeout;

    [SerializeField] private TextMeshProUGUI timerLabel;

    private void OnEnable()
    {
        StartCoroutine(AdvertisingWarning());
    }

    private IEnumerator AdvertisingWarning()
    {
        StringTable localTable = localTable = LocalizationSettings.StringDatabase.GetTable("UI_Strings_Table");

        int i = 3;

        while (i >= 0)
        {
            timerLabel.text = localTable.GetEntry("ad_timer").GetLocalizedString() + ": " + i.ToString() + "...";
            i -= 1;
            
            yield return new WaitForSeconds(1f);
        }

        eWarningTimeout?.Invoke();
    }
}
