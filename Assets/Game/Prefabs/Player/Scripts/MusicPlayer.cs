using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
public class MusicPlayer : MonoBehaviour
{
    [SerializeField] private AudioSource stoneMusic;
    [SerializeField] private AudioSource woodMusic;
    [SerializeField] private AudioSource metalMusic;
    [SerializeField] private AudioSource windMusic;

    [SerializeField] private float lenght = 1f;

    private AudioSource currentMusic;
    private float time = 0;


    private void Start()
    {
        currentMusic = stoneMusic;

        stoneMusic.Play();
        stoneMusic.volume = 0;
        woodMusic.Play();
        woodMusic.volume = 0;
        metalMusic.Play();
        metalMusic.volume = 0;
        windMusic.Play();
        windMusic.volume = 0;
    }
    private AudioSource GetSource(int _nextMusic) 
    {
        switch (_nextMusic) 
        {
            case 0:
                return stoneMusic;
                break;
            case 1:
                return woodMusic;
                break;
            case 2:
                return metalMusic;
                break;
            case 3:
                return windMusic;
                break;
            default:
                return currentMusic;
                break;
        }
    }

    public void TurnOff(int _musicId) 
    {
        currentMusic = GetSource(_musicId);
        StopAllCoroutines();
        StopExcept();
        StartCoroutine(Switch(currentMusic, 1, 0));
    }

    public void TurnOn(int _musicId)
    {
        currentMusic = GetSource(_musicId);
        StopAllCoroutines();
        StopExcept();
        StartCoroutine(Switch(currentMusic, 0, 1));
    }

    private void StopExcept() 
    {
        if (currentMusic != stoneMusic) { stoneMusic.volume = 0; }
        if (currentMusic != woodMusic) { woodMusic.volume = 0; }
        if (currentMusic != metalMusic) { metalMusic.volume = 0; }
        if (currentMusic != windMusic) { windMusic.volume = 0; }
    }

    IEnumerator Switch(AudioSource _music, int start, int finish) 
    {
        for (int i = start; i <= 10 * lenght; i += 1) 
        {
            currentMusic.volume = Mathf.Lerp(start, finish, i/(10 * lenght));
            yield return new WaitForSeconds(0.025f);
        }
        if (currentMusic.volume == finish) { StopAllCoroutines(); }
    }
}
