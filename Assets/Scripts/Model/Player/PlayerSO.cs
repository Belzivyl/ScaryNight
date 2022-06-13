using UnityEngine;


[CreateAssetMenu(fileName = "New Player data", menuName = "New Player", order = 0)]

public class PlayerSO : ScriptableObject
{
    [SerializeField] private PlayerStruct _playerStruct;
    public PlayerStruct PlayerStruct => _playerStruct;
}
