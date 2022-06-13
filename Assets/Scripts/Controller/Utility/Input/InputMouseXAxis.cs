using System;
using UnityEngine;


public class InputMouseXAxis : iPlayerAxisInputProxy
{
    private Joystick _joystick;
    public event Action<float> OnMouseAxisChange = delegate (float f) { };

    public InputMouseXAxis(Joystick joystick) 
    {
        _joystick = joystick;
    }

    public void GetAxis()
    {
        //OnMouseAxisChange.Invoke(_joystick.Horizontal);
        OnMouseAxisChange.Invoke(Input.GetAxis(InputData.MouseX));
    }
}
