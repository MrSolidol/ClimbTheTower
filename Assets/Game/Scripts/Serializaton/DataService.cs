using System;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEditor.Localization.Plugins.XLIFF.V20;

public class DataService
{
    private DataSerializer dataSerializer;
    private List<Type> allowedDataList;

    public DataService() 
    {
        dataSerializer = new DataSerializer();
        allowedDataList = new List<Type>() { typeof(SettingsData), typeof(PlayerData) };
    }


    public void Save(PlayerData playerData) 
    {
        dataSerializer.Save<PlayerData>(playerData, PlayerData.FileName);
    }

    public void Save(SettingsData settingsData) 
    {
        dataSerializer.Save<SettingsData>(settingsData, SettingsData.FileName);
    }

    public void Load(out PlayerData playerData) 
    {
        playerData = dataSerializer.Load<PlayerData>(PlayerData.FileName);
    }

    public void Load(out SettingsData settingsData)
    {
        settingsData = dataSerializer.Load<SettingsData>(SettingsData.FileName);
    }
}
