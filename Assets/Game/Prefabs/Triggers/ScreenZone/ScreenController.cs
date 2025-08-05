using System.Collections.Generic;
using TMPro;

public class ScreenController
{
    private List<ScreenZone> screensList;

    private CameraController cameraController;
    private MusicController musicController;

    private ScreenZone targetedZone;
    private ScreenZone selectedZone;

    private bool isInitialized = false;
    

    public ScreenController(CameraController cameraController, MusicController musicController, List<ScreenZone> screensList)
    {
        this.screensList = screensList;
        this.cameraController = cameraController;
        this.musicController = musicController;

        foreach (var screen in this.screensList)
        {
            screen.eScreenEntered.AddListener(OnScreenEntered);
            screen.eScreenExited.AddListener(OnScreenExited);
        }
    }


    private void InitializeZone(ScreenZone screen) 
    {
        cameraController.SetCamera(screen.transform.position, screen.ScreenScale);
        musicController.SetScreenMusic(screen);
    }

    private void SetScreenZone(ScreenZone screen) 
    {
        if (!isInitialized) { isInitialized = true; InitializeZone(screen); return; }

        cameraController.MoveCamera(screen.transform.position, screen.ScreenScale);
        musicController.CheckScreenMusic(screen);
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
