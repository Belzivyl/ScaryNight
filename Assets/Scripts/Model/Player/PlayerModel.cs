using UnityEngine;

public class PlayerModel
{
    private PlayerStruct _playerStruct;

    #region Health
    public float CurrentHP { get => _playerStruct.CurrentHP; }
    public float MaxHP { get => _playerStruct.MaxHP; }
    public float HealRate { get => _playerStruct.HealRate; }
    #endregion

    #region Move
    public float CurrentTurnSpeed { get => _playerStruct.CurrentTurnSpeed; }
    public float MaxTurnSpeed { get => _playerStruct.MaxTurnSpeed; }
    public float MaxTurnAxisX { get => _playerStruct.MaxTurnAxisX; }
    public float MinTurnAxisX { get => _playerStruct.MinTurnAxisX; }
    public float MaxTurnAxisY { get => _playerStruct.MaxTurnAxisY; }
    public float MinTurnAxisY { get => _playerStruct.MinTurnAxisY; }
    #endregion
    public PlayerModel(PlayerStruct playerStruct) 
    {
        _playerStruct = playerStruct;
    }

    public bool HPChanged(float value) 
    {
        if (value >= 0) 
        {
            _playerStruct.CurrentHP = value;
            return true;
        }
        return false;
    }

    public bool TurnSpeedChange(float value) 
    { 
        if (value > 0) 
        {
            _playerStruct.CurrentTurnSpeed = value;
            return true;
        }
        return false;
    }
}
