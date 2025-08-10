using System.Collections;
using System.Globalization;
using System.Text;
using TMPro;
using UnityEngine;
using UnityEngine.Events;

public class DialogeWriter : MonoBehaviour
{
    [HideInInspector] public UnityEvent eDialogeEnded;
    [HideInInspector] public UnityEvent eCharacterWrited;
    [HideInInspector] public UnityEvent<bool> eDisplayTalk;

    [SerializeField] private DialogeSelector dialogeSelector;
    [SerializeField] private TextMeshProUGUI npcLabel;
    [SerializeField] private float writingSpeed = 0.5f;

    private string textToWrite;
    private StringBuilder labelBuilder;
    private int currentCharacterIndex;


    private void Awake()    
    {
        labelBuilder = new StringBuilder("", 10000);
    }

    private void OnEnable()
    {
        dialogeSelector.eDialogeSelected.AddListener(SetText);
    }

    private void OnDisable()
    {
        dialogeSelector.eDialogeSelected.RemoveListener(SetText);
    }


    private void SetText(string newText) 
    {
        if (newText == "") { return; }

        textToWrite = newText;
        currentCharacterIndex = 0;
        labelBuilder.Clear();

        StartCoroutine(TextWriter());
    }

    private void ClearLabel() 
    {
        npcLabel.text = "";
        eDialogeEnded?.Invoke();
    }


    private IEnumerator TextWriter()
    {
        while (currentCharacterIndex < textToWrite.Length) 
        {
            var character = textToWrite[currentCharacterIndex];

            if (character == '/')
            {
                string command = textToWrite.Substring(currentCharacterIndex + 1, 4);
                Debug.Log(command);
                switch (command[0]) 
                {
                    case 'c':
                        labelBuilder.Clear();
                        break;
                    case 'w':
                        yield return new WaitForSeconds(float.Parse(command.Substring(1, 3), CultureInfo.InvariantCulture.NumberFormat));
                        break;

                }
                currentCharacterIndex += 4;
            }
            else 
            { labelBuilder.Append(character); }

            currentCharacterIndex++;
            npcLabel.text = labelBuilder.ToString();
            yield return new WaitForSeconds(writingSpeed);
        }

        Invoke("ClearLabel", 0.1f);
    }

}
