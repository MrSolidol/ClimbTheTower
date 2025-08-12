using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.Events;
using UnityEngine.UI;

public class UIMixerController : MonoBehaviour
{
    public UnityEvent<float, float> eMixerUpdate;

    [SerializeField] private AudioMixer gameMixer;

    [SerializeField] private AnimationCurve volumeCurve;

    [SerializeField] private Slider musicSlider;
    [SerializeField] private Slider soundSlider;


    private void OnEnable()
    {
        musicSlider.onValueChanged.AddListener(SetMusicVolume);
        musicSlider.onValueChanged.AddListener(SliderChange);
        soundSlider.onValueChanged.AddListener(SetSoundVolume);
        soundSlider.onValueChanged.AddListener(SliderChange);

    }

    private void OnDisable()
    {
        musicSlider.onValueChanged.RemoveListener(SetMusicVolume);
        musicSlider.onValueChanged.RemoveListener(SliderChange);
        soundSlider.onValueChanged.RemoveListener(SetSoundVolume);
        soundSlider.onValueChanged.RemoveListener(SliderChange);
    }


    public void Initialization(float musVal, float souVal) 
    {
        SetMusicVolume(musVal);
        musicSlider.value = musVal;
        SetSoundVolume(souVal);
        soundSlider.value = souVal;
    }

    private void SetMusicVolume(float value)
    {
        gameMixer.SetFloat("music_volume", Mathf.Lerp(-80, 0, volumeCurve.Evaluate(value)));
    }

    private void SetSoundVolume(float value)
    {
        gameMixer.SetFloat("sound_volume", Mathf.Lerp(-80, 0, volumeCurve.Evaluate(value)));
    }

    private void SliderChange(float value) 
    {
        eMixerUpdate?.Invoke(musicSlider.value, soundSlider.value);
    }
}
