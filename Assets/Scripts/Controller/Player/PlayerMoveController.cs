using System;
using UnityEngine;

//create controllers for each enemy and main enemy controller that controls all enemy controllers
public class PlayerMoveController : iFixedUpdate, iOnDestroy
{
    #region Player
    private readonly PlayerViewMovable _playerViewMovable;
    private PlayerModel _playerModel;
    #endregion

    #region Input
    private float _mouseX;
    private float _mouseY;
    private iPlayerAxisInputProxy _inputMouseXAxis;
    private iPlayerAxisInputProxy _inputMouseYAxis;
    #endregion

    #region Move
    private float _turnAxisX;
    private float _turnAxisY;
    private Quaternion _moveAxisX;
    private Quaternion _moveAxisY;
    private float _maxTurnAxisX;
    private float _minTutnAsixX;
    private float _maxTurnAxisY;
    private float _minTutnAsixY;
    #endregion

    public PlayerMoveController((iPlayerAxisInputProxy inputMouseXAxis, iPlayerAxisInputProxy inputMouseYAxis) input, PlayerViewMovable playerViewMovable, PlayerModel playerModel) 
    {
        _playerViewMovable = playerViewMovable;
        _playerModel = playerModel;
        _inputMouseXAxis = input.inputMouseXAxis;
        _inputMouseYAxis = input.inputMouseYAxis;
    }

    public void SubscribeOnEvents()
    {
        _inputMouseXAxis.OnMouseAxisChange += MouseXAxisChange;
		_inputMouseYAxis.OnMouseAxisChange += MouseYAxisChange;
	}

    public void SetTurnRange()
	{
        _maxTurnAxisX = _playerModel.MaxTurnAxisX;
        _minTutnAsixX = _playerModel.MinTurnAxisX;
        _maxTurnAxisY = _playerModel.MaxTurnAxisY;
        _minTutnAsixY = _playerModel.MinTurnAxisY;
    }

    public void FixedUpdate()
    {
        PlayerTurn();
    }

    public void OnDestroy()
    {
        _inputMouseXAxis.OnMouseAxisChange -= MouseXAxisChange;
        _inputMouseYAxis.OnMouseAxisChange -= MouseYAxisChange;
    }

    private void MouseXAxisChange( float mouseXAxisValue) 
    {
        _mouseX = mouseXAxisValue;
    }

    private void MouseYAxisChange(float mouseYAxisValue)
    {
        _mouseY = mouseYAxisValue;
    }

    private void PlayerTurn() 
    {
        _turnAxisX += _mouseX * _playerModel.CurrentTurnSpeed;
		_turnAxisY += _mouseY * _playerModel.CurrentTurnSpeed;
		_moveAxisX = Quaternion.AngleAxis(_turnAxisX, Vector3.up);
		_moveAxisY = Quaternion.AngleAxis(_turnAxisY, Vector3.right);
		_turnAxisX = Mathf.Clamp(_turnAxisX, _minTutnAsixX, _maxTurnAxisX);
		_turnAxisY = Mathf.Clamp(_turnAxisY, _minTutnAsixY, _maxTurnAxisY);
		_playerViewMovable.transform.rotation = _moveAxisX * _moveAxisY;
	}
}

