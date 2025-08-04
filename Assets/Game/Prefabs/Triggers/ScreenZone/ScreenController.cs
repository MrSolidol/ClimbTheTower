using System.Collections.Generic;
using UnityEngine;

public class ScreenController
{
    private const float CAMERA_SCALE = 6.1875f;

    private List<ScreenZone> screensList;
    private Transform currentCamera;
    private ScreenZone targetedZone;
    private ScreenZone currentZone;


    public ScreenController(Transform currentCamera, List<ScreenZone> screensList)
    {
        this.currentCamera = currentCamera;

        this.screensList = screensList;
        foreach (var screen in this.screensList)
        {
            screen.eScreenEntered.AddListener(OnScreenEntered);
            screen.eScreenExited.AddListener(OnScreenExited);
        }

        UnityEngine.Debug.Log("Screenes Inited! " + this.screensList.Count.ToString());
    }


    private void MoveToZone(ScreenZone screen) 
    {
        currentCamera.position = new Vector3(screen.transform.position.x, screen.transform.position.y, currentCamera.position.z);
        currentCamera.gameObject.GetComponent<Camera>().orthographicSize = CAMERA_SCALE * screen.ScreenScale;
    }

    private void OnScreenEntered(ScreenZone screen) 
    {
        targetedZone = screen;

        if (currentZone == null)
        {
            currentZone = targetedZone;
            MoveToZone(currentZone);
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
            currentZone = targetedZone;
            MoveToZone(currentZone);
            targetedZone = null;
        }
    }

}
