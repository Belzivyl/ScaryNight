using UnityEngine;

[CreateAssetMenu(fileName = "New Enemy data", menuName = "New enemy", order = 2)]
public sealed class EnemySO : ScriptableObject
{
	[SerializeField] private EnemyStruct _enemyStruct;
	public EnemyStruct EnemyStruct => _enemyStruct;

}