using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class UIMenuBook : MonoBehaviour
{
    [SerializeField] private UIPopup[] uiPopupsToInsert;
    [SerializeField] private string originalPopup;

    [Inject] private SceneLoader sceneLoader;
    [Inject] private PauseService pauseService;
    
    private Dictionary<string, UIPopup> popupMap;
    private bool isSetted = false;
    private bool isLocked = false;

    public UIPopup CurrentPopup { get; private set; }


    private void Awake()
    {
        InitMap(uiPopupsToInsert);
        SetNewPopup(originalPopup);
        pauseService.ResumeGame();
    }

    private void OnEnable()
    {
        pauseService.eInterfaceLocked.AddListener(OnInterfaceLocked);
    }

    private void OnDisable()
    {
        pauseService.eInterfaceLocked.RemoveListener(OnInterfaceLocked);
    }


    public void SetNewPopup(string newPopup)
    {
        if (isLocked) { return; }
        ChangePopup(newPopup);
    }

    public void ChangeScene(string sceneName) 
    {
        if (isLocked) { return; }
        sceneLoader.LoadSceneWithFade(sceneName);
    }

    public void SetPause(bool flag) 
    {
        if (flag) { pauseService.StopGame(); }
        else { pauseService.ResumeGame(); }
    }


    private void InitMap(params UIPopup[] uiPopups)
    {
        this.popupMap = new Dictionary<string, UIPopup>();
        foreach (var popup in uiPopups)
        {
            this.popupMap[popup.PopupName] = popup;
        }
    }

    private async void ChangePopup(string newPopup)
    {
        if (isLocked) 
        { return; }

        if (isSetted)
        { return; }

        if (CurrentPopup != null)
        {
            if (CurrentPopup == this.popupMap[newPopup])
            { return; }

            isSetted = true;

            var task = CurrentPopup.Hide();
            await task;
        }

        isSetted = true;

        CurrentPopup = this.popupMap[newPopup];
        await CurrentPopup.Show();

        isSetted = false;
    }

    private void OnInterfaceLocked(bool flag)
    {
        isLocked = flag;
    }
}
