using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Localization;
using UnityEngine.Localization.Settings;

public class DialogeWriter : MonoBehaviour
{
    public UnityEvent dialogWrited;

    [SerializeField] private int npcIndex;

    [SerializeField] private LabelWriter writer;
    [SerializeField] private float pause;
    [SerializeField] private float speed;

    private List<List<string>> dialogs;

    private int currentDialog = -1;
    private int currentPhrase = 0;

    private void Start()
    {
        writer.labelWrited.AddListener(delegate () { Invoke("NextPhrase", pause); });
        dialogs = PhraseDataBase.GetDialogs(npcIndex);
    }

    private string ReturnLocalString(int _currentDialog, int _currentPhrase) 
    {
        var _result = new LocalizedString(PhraseDataBase.GetTables(npcIndex), dialogs[_currentDialog][_currentPhrase]);
        return _result.GetLocalizedString();
    }

    public void NextDialoge() 
    {
        currentDialog++;
        currentPhrase = 0;
        if (currentDialog == dialogs.Count) { currentDialog = 1; }
        NextPhrase();
    }

    public void NextPhrase() 
    {
        if (dialogs[currentDialog].Count == currentPhrase) {  writer.Clean(); dialogWrited.Invoke(); return; }
        writer.TypeNewText(ReturnLocalString(currentDialog, currentPhrase), speed);
        currentPhrase++;
    }
}
