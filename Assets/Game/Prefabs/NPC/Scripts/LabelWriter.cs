using System.Collections;
using System.Collections.Generic;
using System.Reflection.Emit;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using TMPro;
using UnityEngine.UIElements;

public class LabelWriter : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI label;
    private string textToWrite;
    private float readSpeed = 0.5f;

    public UnityEvent labelWrited;
    public UnityEvent letterWrited;

    public void Clean()
    {
        label.text = "";
        StopAllCoroutines();
    }

    public void TypeNewText(string _text, float _speed) 
    {
        readSpeed = _speed;
        textToWrite = _text;
        label.text = "";
        StartCoroutine(TypeLetter());
    }

    IEnumerator TypeLetter() 
    {
        for (int i = 0; i < textToWrite.Length; i++) 
        {
            label.text = label.text + textToWrite[i];
            letterWrited.Invoke();
            if (i + 2 == textToWrite.Length) { labelWrited.Invoke(); }
            yield return new WaitForSeconds(readSpeed);
        }
    }

}
