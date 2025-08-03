using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using Zenject;

public class UIMainMenuBook : MonoBehaviour
{
    [SerializeField] private UIPopup[] uiPopupsToInsert;
    [SerializeField] private string sceneToMoveName;
    
    private Dictionary<string, UIPopup> popupMap;
    private bool isSetted = false;

    public UIPopup CurrentPopup { get; private set; }


    private void Awake()
    {
        InitMap(uiPopupsToInsert);
        SetNewPopup("Main");
    }


    private void InitMap(params UIPopup[] uiPopups)
    {
        this.popupMap = new Dictionary<string, UIPopup>();
        foreach (var popup in uiPopups)
        {
            this.popupMap[popup.PopupName] = popup;
        }
    }

    public void SetNewPopup(string newPopup)
    {
        ChangePopup(newPopup);
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
