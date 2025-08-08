using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class UIMenuBook : MonoBehaviour
{
    [SerializeField] private UIPopup[] uiPopupsToInsert;
    [SerializeField] private string originalPopup;

    [Inject] private SceneLoader sceneLoader;
    private Dictionary<string, UIPopup> popupMap;
    private bool isSetted = false;


    public UIPopup CurrentPopup { get; private set; }


    private void Awake()
    {
        InitMap(uiPopupsToInsert);
        SetNewPopup(originalPopup);
    }


    public void SetNewPopup(string newPopup)
    {
        ChangePopup(newPopup);
    }

    public void ChangeScene(string sceneName) 
    {
        sceneLoader.LoadSceneWithFade(sceneName);
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

}
