using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Localization.Settings;
using UnityEngine.Localization.Tables;
using Zenject;

public class DialogeSelector : MonoBehaviour
{
    [HideInInspector] public UnityEvent<string> eDialogeSelected;

    [SerializeField] private DialogeTrigger dialogeTrigger;
    [SerializeField] private string npcName;

    private List<string> dialogeKeys;
    private int currentDialoge = 0;


    [Inject]
    public void Construct(Dictionary<string, DialogeInfo> dialogeMap) 
    {
        dialogeKeys = dialogeMap[npcName].DialogeKeys.ToList();
    }


    private void OnEnable()
    {
        dialogeTrigger.eDialogeStarted.AddListener(NewDialoge);
    }

    private void OnDisable()
    {
        dialogeTrigger.eDialogeStarted.RemoveListener(NewDialoge);
    }


    private void NewDialoge() 
    {
        StringTable localTable = localTable = LocalizationSettings.StringDatabase.GetTable("NPC_Dialoge_Table");

        eDialogeSelected?.Invoke(localTable.GetEntry(dialogeKeys[currentDialoge]).GetLocalizedString());
        currentDialoge++;
        if (currentDialoge >= dialogeKeys.Count) 
        { currentDialoge = 1; }
    }
}
