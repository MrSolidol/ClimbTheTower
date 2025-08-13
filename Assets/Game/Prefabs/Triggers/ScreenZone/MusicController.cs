using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using UnityEngine;

public class MusicController : MonoBehaviour
{
    [SerializeField] private AudioSource audioSource;

    [SerializeField] private AnimationCurve musicFadeCurve;
    [SerializeField] private float musicFade = 1;

    private CancellationTokenSource token;
    private Dictionary<int, MusicInfo> musicMap;
    private MusicInfo currentMusic;


    private void Awake()
    {
        token = new CancellationTokenSource();

        List<MusicInfo> allMusicInfo = Resources.LoadAll<MusicInfo>("").ToList();
        musicMap = new Dictionary<int, MusicInfo>();

        foreach (var info in allMusicInfo) 
        {
            musicMap[info.PlayIndex] = info;
        }
    }


    public void CheckScreenMusic(ScreenZone screen)
    {
        var index = GetMusicIndex(screen.ScreenIndex);

        if (currentMusic.PlayIndex != index)
        {
            token.Cancel();
            token = new CancellationTokenSource();
            var musTask = FadeMusic(musicMap[index], token);
        }
    }

    public void SetScreenMusic(ScreenZone screen)
    {
        currentMusic = musicMap[GetMusicIndex(screen.ScreenIndex)];

        audioSource.clip = currentMusic.AudioClip;
        audioSource.volume = currentMusic.MaxVolume;
        audioSource.Play();
    }

    public async void RemoveMusic(float removeTime)
    {
        float oldVolume = audioSource.volume;

        float time = 0f;
        float unit = 0f;

        while (unit < 1)
        {
            audioSource.volume = Mathf.Lerp(oldVolume, 0, musicFadeCurve.Evaluate(unit));

            time += Time.deltaTime;
            unit = time / removeTime;
            await Task.Yield();
        }
        audioSource.volume = 0f;
        return;
    }


    private int GetMusicIndex(int index) 
    {
        var keys = musicMap.Keys.ToList();
        keys.Sort();

        if (index >= keys[keys.Count - 1]) { index = keys[keys.Count - 1]; }
        else
        {
            for (int i = 0; i < keys.Count; i++)
            {
                if (index < keys[i])
                {
                    index = keys[i - 1]; break;
                }
            }
        }
        return index;
    }

    private async Task FadeMusic(MusicInfo newMusic, CancellationTokenSource token) 
    {
        try
        {
            currentMusic = newMusic;

            float oldVolume = audioSource.volume;

            float time = 0f;
            float unit = 0f;

            while (unit < 1)
            {
                audioSource.volume = Mathf.Lerp(oldVolume, 0, musicFadeCurve.Evaluate(unit));

                time += Time.deltaTime;
                unit = time / musicFade;
                await Task.Yield();
            }
            audioSource.volume = 0f;


            audioSource.Stop();
            audioSource.clip = currentMusic.AudioClip;
            audioSource.Play();


            time = 0f;
            unit = 0f;

            while (unit < 1)
            {
                audioSource.volume = Mathf.Lerp(currentMusic.MaxVolume, 0, musicFadeCurve.Evaluate(1 - unit));

                time += Time.deltaTime;
                unit = time / musicFade;
                await Task.Yield();
            }
            audioSource.volume = currentMusic.MaxVolume;
            return;
        }
        catch (TaskCanceledException)
        {
            Debug.Log("Music change cancelled");
        }
    }

}
