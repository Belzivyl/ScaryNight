using UnityEngine.UI;

public class PlayerHealthUIModel
{
    private PlayerHealthUIStruct _playerHealthUIStruct;

    public Image HPImage { get => _playerHealthUIStruct.HPImage; }
    public float MaxValueBloodMaskColorAlpha { get => _playerHealthUIStruct.MaxValueBloodMaskColorAlpha; }
    public bool MaxAlphaReached { get => _playerHealthUIStruct.MaxAlphaReached; }

    public bool MaxAlphaIsReached(bool value)
    {
        _playerHealthUIStruct.MaxAlphaReached = value;
        return value;
    }

    public PlayerHealthUIModel(PlayerHealthUIStruct playerHealthUIStruct)
    {
        _playerHealthUIStruct = playerHealthUIStruct;
    }
}

