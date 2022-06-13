using System;
using UnityEngine;

[Serializable]
public struct EnemyStruct
{
	[Header("Enemy type")]
	public EnemyType EnemyType;
	[Header("Enemy state")]
	public EnemyState EnemyState;
	[Header("Move data")]
	public float EnemySpeed;
	public float EnemyBaseSpeed;
	[Header("Health data")]
	public float EnemyHealth;
	public float EnemyBaseHealth;
	[Header("Attack data")]
	public float EnemyDamage;
	[Header("Enemy count")]
	public int EnemyCount;
	[Header("Sounds")]
	public AudioClip[] audioClips;
	
}

