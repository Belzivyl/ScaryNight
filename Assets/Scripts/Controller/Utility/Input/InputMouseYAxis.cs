using System;
using UnityEngine;


public class InputMouseYAxis : iPlayerAxisInputProxy
{
    private Joystick _joystick;
    public event Action<float> OnMouseAxisChange = delegate (float f) { };

    public InputMouseYAxis(Joystick joystick)
    {
        _joystick = joystick;
    }

    public void GetAxis()
    {
        //OnMouseAxisChange.Invoke(_joystick.Vertical);
        OnMouseAxisChange.Invoke(Input.GetAxis(InputData.MouseY));
    }
}

