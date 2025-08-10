using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogeAnimation : MonoBehaviour
{
    [SerializeField] private Animator dialogeAnimator;
    [SerializeField] private DialogeWriter dialogeWriter;


    private void Awake()
    {
        dialogeWriter.eDisplayTalk.AddListener(SetParameters);
    }


    private void SetParameters(bool flag) 
    {
        dialogeAnimator.SetBool("is_talk", flag);
    }
}
