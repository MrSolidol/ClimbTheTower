using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class MixerOptions : MonoBehaviour
{
    [SerializeField] private AudioMixer GameMixer;

    [SerializeField] private Slider MusicSlider;
    [SerializeField] private Slider SoundsSlider;

    private void Start()
    {
        //LoadParameters();
    }

    public void SetMusicVolume()
    {
        float volume = MusicSlider.value;
        GameMixer.SetFloat("Music", Mathf.Log10(volume) * 20);
    }

    public void SetSoundsVolume()
    {
        float volume = SoundsSlider.value;
        GameMixer.SetFloat("Sounds", Mathf.Log10(volume) * 20);
    }

    public void SaveParameters() 
    {
        //ProgressSave.Instance.musicValue = MusicSlider.value;
        //ProgressSave.Instance.soundValue = SoundsSlider.value;
        //ProgressSave.Instance.Save();
    }
    public void LoadParameters() 
    {
        //ProgressSave.Instance.Load();
        //SoundsSlider.value = ProgressSave.Instance.soundValue;
        //MusicSlider.value = ProgressSave.Instance.musicValue;
        //SetMusicVolume();
        //SetSoundsVolume();
    }
}
