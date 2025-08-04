using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class ScreenController
{
    private const float CAMERA_SCALE = 6.1875f;

    private List<ScreenZone> screensList;
    private Dictionary<int, MusicInfo> musicMap;
    private Transform currentCamera;
    private GameObject musicPlayer;

    private ScreenZone targetedZone;
    private ScreenZone selectedZone;

    private MusicInfo currentMusic;

    
    public ScreenController(
        Transform currentCamera, GameObject musicPlayer,
        List<ScreenZone> screensList, List<MusicInfo> musicInfo)
    {
        this.currentCamera = currentCamera;
        this.musicPlayer = musicPlayer;
        this.screensList = screensList;

        foreach (var screen in this.screensList)
        {
            screen.eScreenEntered.AddListener(OnScreenEntered);
            screen.eScreenExited.AddListener(OnScreenExited);
        }

        musicMap = new Dictionary<int, MusicInfo>();
        foreach (var info in musicInfo) 
        {
            musicMap[info.PlayIndex] = info;
        }

    }
    
    
    private void SetScreenZone(ScreenZone screen) 
    {
        ChangeCameraTransform(screen);
        CheckScreenMusic(screen);
    }

    private void CheckScreenMusic(ScreenZone screen) 
    {
        var index = screen.ScreenIndex;
        var keys = musicMap.Keys.ToList();
        keys.Sort();

        for (int i = 0; i < keys.Count; i++)
        {
            if (index < keys[i])
            {
                index = keys[i - 1]; break;
            }

        }

        if (currentMusic == null || currentMusic.PlayIndex != index)
        {
            currentMusic = musicMap[index];
            var source = musicPlayer.GetComponent<AudioSource>();

            source.Stop();
            source.clip = currentMusic.AudioClip;
            source.volume = currentMusic.MaxVolume;
            source.Play();
        }
    }

    private void ChangeCameraTransform(ScreenZone screen) 
    {
        currentCamera.position = new Vector3(screen.transform.position.x, screen.transform.position.y, currentCamera.position.z);
        currentCamera.gameObject.GetComponent<Camera>().orthographicSize = CAMERA_SCALE * screen.ScreenScale;
    }

    private void OnScreenEntered(ScreenZone screen) 
    {
        targetedZone = screen;

        if (selectedZone == null)
        {
            selectedZone = targetedZone;
            SetScreenZone(selectedZone);
            targetedZone = null;
        }
    }

    private void OnScreenExited(ScreenZone screen) 
    {
        if (screen == targetedZone) 
        {
            targetedZone = null;
        }

        if (targetedZone != null)
        {
            selectedZone = targetedZone;
            SetScreenZone(selectedZone);
            targetedZone = null;
        }
    }

}
