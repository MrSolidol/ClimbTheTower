using System;
using System.Globalization;

[Serializable]
public class SettingsData
{
    private static string FILE_NAME = "settings.save";

    public float musicVolume;
    public float soundVolume;
    public string languageSelected;

    public static string FileName { get { return FILE_NAME; } }


    public SettingsData() 
    {
        musicVolume = 0.5f;
        soundVolume = 0.5f;

        languageSelected = CultureInfo.CurrentCulture.TwoLetterISOLanguageName;
    }
}
