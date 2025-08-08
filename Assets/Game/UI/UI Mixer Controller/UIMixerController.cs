using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;
using static UnityEngine.Rendering.DebugUI;

public class UIMixerController : MonoBehaviour
{
    [SerializeField] private AudioMixer gameMixer;

    [SerializeField] private AnimationCurve volumeCurve;

    [SerializeField] private Slider musicSlider;
    [SerializeField] private Slider soundSlider;
    [SerializeField] private Slider speechSlider;


    private void Awake()
    {
        Initialization();
    }

    private void OnEnable()
    {
        musicSlider.onValueChanged.AddListener(SetMusicVolume);
        soundSlider.onValueChanged.AddListener(SetSoundVolume);
        speechSlider.onValueChanged.AddListener(SetSpeechVolume);
    }

    private void OnDisable()
    {
        musicSlider.onValueChanged.RemoveListener(SetMusicVolume);
        soundSlider.onValueChanged.RemoveListener(SetSoundVolume);
        speechSlider.onValueChanged.RemoveListener(SetSpeechVolume);
    }


    private void Initialization() 
    {
        SetMusicVolume(0.5f);
        SetSoundVolume(0.5f);
        SetSpeechVolume(0.5f);
    }

    private void SetMusicVolume(float value)
    {
        gameMixer.SetFloat("music_volume", Mathf.Lerp(-80, 0, volumeCurve.Evaluate(value)));
    }

    private void SetSoundVolume(float value)
    {
        gameMixer.SetFloat("sound_volume", Mathf.Lerp(-80, 0, volumeCurve.Evaluate(value)));
    }

    private void SetSpeechVolume(float value)
    {
        gameMixer.SetFloat("speech_volume", Mathf.Lerp(-80, 0, volumeCurve.Evaluate(value)));
    }
}
