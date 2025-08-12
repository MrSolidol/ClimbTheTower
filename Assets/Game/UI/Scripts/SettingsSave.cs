using UnityEngine;
using Zenject;

public class SettingsSave : MonoBehaviour
{
    [SerializeField] private UIMixerController mixerController;
    [SerializeField] private UILanguageSelector languageSelector;
    [SerializeField] private bool UseSave = true;
    [SerializeField] private bool ResetSave = false;

    [Inject] private DataService dataService;

    private SettingsData settingsData;


    private void Awake()
    {
        if (ResetSave)
            dataService.Save(new SettingsData());
        if (UseSave)
            Initialization();

        mixerController.Initialization(settingsData.musicVolume, settingsData.soundVolume);
        if (languageSelector != null)
        { languageSelector.Initialization(settingsData.languageSelected); }
    }

    private void OnEnable()
    {
        mixerController.eMixerUpdate.AddListener(MixerSave);
        if (languageSelector != null) { languageSelector.eLanguageChanged.AddListener(LanguageSave); }
    }

    private void OnDisable()
    {
        mixerController.eMixerUpdate.RemoveListener(MixerSave);
        if (languageSelector != null) { languageSelector.eLanguageChanged.RemoveListener(LanguageSave); }
    }


    public void Save()
    {
        dataService.Save(settingsData);
    }


    private void Initialization()
    {
        settingsData = new SettingsData();
        dataService.Load(out settingsData);
    }

    private void MixerSave(float musVal, float soundVal) 
    {
        settingsData.musicVolume = musVal;
        settingsData.soundVolume = soundVal;
    }

    private void LanguageSave(string language)
    {
        settingsData.languageSelected = language;
    }
}
