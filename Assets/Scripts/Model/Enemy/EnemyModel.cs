using UnityEngine;

public class EnemyModel
{
	private EnemyStruct _enemyStruct;

	public EnemyType EnemyType { get => _enemyStruct.EnemyType; }
	public EnemyState EnemyState { get => _enemyStruct.EnemyState; }

	#region Move
	public float EnemySpeed { get => _enemyStruct.EnemySpeed; }
	public float EnemyBaseSpeed { get => _enemyStruct.EnemyBaseSpeed; }
	#endregion

	#region Health
	public float EnemyHealth { get => _enemyStruct.EnemyHealth; }
	public float EnemyBaseHealth => _enemyStruct.EnemyBaseHealth;
	#endregion

	#region Attack
	public float EnemyAttack { get => _enemyStruct.EnemyDamage; }
	#endregion

	#region SoundFX
	public AudioClip[] audioClips => _enemyStruct.audioClips;
	//public AudioSource damageSound => _enemyStruct.damageSound;
	//public AudioSource deathSound => _enemyStruct.deathSound;
	//public AudioSource attackSound => _enemyStruct.attackSound;
	//public AudioSource spawnSound=>_enemyStruct.spawnSound;
	//public AudioSource walkingSound=> _enemyStruct.walkingSound;
	#endregion
	public int EnemyCount { get => _enemyStruct.EnemyCount; }
	public EnemyModel(EnemyStruct enemyStruct)
	{
		_enemyStruct = enemyStruct;
	}

	public bool EnemyStateChanged(EnemyState enemyState)
	{
		_enemyStruct.EnemyState = enemyState;
		return true;
	}

	public bool EnemySpeedChanged(float value)
	{
		if (value != 0)
		{
			_enemyStruct.EnemySpeed = value;//maybe change this to: _enemyStruct.EnemySpeed *= value;
			return true;
		}
		return false;
	}

	public bool EnemyHealthChanged(float value)
	{
		_enemyStruct.EnemyHealth = value;
		return true;
	}

	public bool EnemyAttackChanged(float value)
	{
		if(value != 0)
		{
			_enemyStruct.EnemyDamage = value;
			return true;
		}
		return false;
	}
	
}
