using System;
using UnityEngine;

public class InputController : iUpdate
{
    private readonly iPlayerAxisInputProxy _mouseX;
    private readonly iPlayerAxisInputProxy _mouseY;
    private float lastClickTime;
    private float timeSinceLastClick;
    private float doubleClickTime = 0.3f;
    public Action OnDoubleClick;
    private PlayerView _playerView;

    public InputController(Joystick joystick, PlayerView playerView) 
    {
        _mouseX = new InputMouseXAxis(joystick);
        _mouseY = new InputMouseYAxis(joystick);
        _playerView = playerView;
        SubscribeOnEvents();
    }

    private void SubscribeOnEvents() 
    {
        OnDoubleClick += _playerView.FlashLightView.DoubleTap;
    }
    public void Update()
    {
        if (Input.GetMouseButton(0))
        {
            _mouseX.GetAxis();
            _mouseY.GetAxis();
        }
        DoubleClick();
        //// With joystick
        //_mouseX.GetAxis();
        //_mouseY.GetAxis();
    }

    public (iPlayerAxisInputProxy inputMouseXAxis, iPlayerAxisInputProxy inputMouseYAxis) GetInput() 
    {
        (iPlayerAxisInputProxy inputMouseXAxis, iPlayerAxisInputProxy inputMouseYAxis) result = (_mouseX, _mouseY);
        return result;
    }

    private void DoubleClick() 
    {
        if (Input.GetMouseButtonDown(0)) 
        {
            timeSinceLastClick = Time.time - lastClickTime;
            if (timeSinceLastClick <= doubleClickTime)
            {
                MakeDoubleClick();
            }
            lastClickTime = Time.time;
        }
    }

    private void MakeDoubleClick() 
    {
        OnDoubleClick?.Invoke();
    }
}

