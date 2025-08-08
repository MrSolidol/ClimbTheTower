using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIFinalController : MonoBehaviour
{
    [SerializeField] private UIFader finalFader;
    [SerializeField] private UIMenuBook finalBook;
    [SerializeField] private Animator finalAnimator;

    private bool isWaited = false;


    private void Update()
    {
        if (Input.GetMouseButtonDown(0)) 
        {
            OnMousePressed();
        }
    }


    public void HandlerWaited()
    {
        isWaited = true;
    }

    public void HandlerTitleShowed()
    {
        finalBook.SetNewPopup("Exit");
    }


    private void OnMousePressed()
    {
        if (!isWaited) 
        {
            isWaited = true;
            finalAnimator.Play("ShowTitle");            
        }
    }

    
}
