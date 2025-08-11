using System.Collections.Generic;
using UnityEngine;

public class WindowsController : InputController
{
    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
            PressOn();
        else if (Input.GetMouseButtonUp(0))
            PressUp();
        else if (Input.GetMouseButton(0))
            Press();
    }


    public override void PressOn() 
    {
        var value = (Vector2)Camera.main.ScreenToWorldPoint(Input.mousePosition);

        ePressOn?.Invoke(value);
    }

    public override void Press()
    {
        var value = (Vector2)Camera.main.ScreenToWorldPoint(Input.mousePosition);

        ePress?.Invoke(value);
    }

    public override void PressUp() 
    {
        var value = (Vector2)Camera.main.ScreenToWorldPoint(Input.mousePosition);

        ePressUp?.Invoke(value);
    }

}
