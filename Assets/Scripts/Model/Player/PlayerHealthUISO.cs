using UnityEngine;

[CreateAssetMenu(fileName = "New PlayerHealthUI data", menuName = "New PlayerHealthUI", order = 1)]

public class PlayerHealthUISO: ScriptableObject
{
    [SerializeField] private PlayerHealthUIStruct _playerHealthUIStruct;
    public PlayerHealthUIStruct PlayerHealthUIStructer => _playerHealthUIStruct;
}

