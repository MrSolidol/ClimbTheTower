using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class WindowsController : MonoBehaviour, IController
{
    [SerializeField]
    private SwapCalculation swapCalculation;

    private List<IControlable> controlablesList;


    private void Awake()
    {
        controlablesList = new List<IControlable>();
        controlablesList.Add(swapCalculation);
    }
    

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
            PressOn();
        else if (Input.GetMouseButtonUp(0))
            PressUp();
        else if (Input.GetMouseButton(0))
            Press();
    }


    private void PressOn() 
    {
        if (controlablesList.Count == 0) { return; }

        var value = (Vector2)Camera.main.ScreenToWorldPoint(Input.mousePosition);

        foreach (var controlGetter in controlablesList) 
        {
            controlGetter.PressOn(value);
        } 
    }

    private void Press()
    {
        if (controlablesList.Count == 0) { return; }

        var value = (Vector2)Camera.main.ScreenToWorldPoint(Input.mousePosition);

        foreach (var controlGetter in controlablesList)
        {
            controlGetter.Press(value);
        }
    }

    private void PressUp() 
    {
        if (controlablesList.Count == 0) { return; }

        var value = (Vector2)Camera.main.ScreenToWorldPoint(Input.mousePosition);

        foreach (var controlGetter in controlablesList)
        {
            controlGetter.PressUp(value);
        }
    }

}
