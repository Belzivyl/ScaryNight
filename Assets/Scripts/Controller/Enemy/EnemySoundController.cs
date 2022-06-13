using System;
using System.Collections.Generic;
using UnityEngine;

internal sealed class EnemySoundController: iOnDestroy
{
	//redo this
	private readonly EnemyView _enemyView;
	private readonly EnemyModel _enemyModel;
	private AudioSource _enemyAudioSource;
	private readonly Dictionary<EnemyState, Action<bool>> _enemyStateDictionary;

	public EnemySoundController(EnemyView enemyView, EnemyModel enemyModel)
	{
		_enemyView = enemyView;
		_enemyModel = enemyModel;
		_enemyAudioSource = _enemyView.GetComponent<AudioSource>();
		
		_enemyStateDictionary = new Dictionary<EnemyState, Action<bool>>()
		{
			{ EnemyState.Spawned, (bool SpawnSound) => {if(_enemyView.enemySounds.Count!=0) _enemyAudioSource.clip =enemyView.enemySounds[0].clip;  _enemyAudioSource.Play();} }
			,
			{ EnemyState.Damaged, (bool DamagedSound) => {if(_enemyView.enemySounds.Count!=0) _enemyAudioSource.clip = enemyView.enemySounds[1].clip; _enemyAudioSource.Play();} }
			,
			{ EnemyState.Walking, (bool WalkingSound) => {if(_enemyView.enemySounds.Count!=0)Debug.Log("WalkSound");/*_enemyView.enemySounds[x].source.Play();*/} }
			,
			{ EnemyState.Attacking, (bool AttackingSound) => {if(_enemyView.enemySounds.Count!=0) _enemyAudioSource.clip = enemyView.enemySounds[2].clip; _enemyAudioSource.Play(); } }
			,
			{ EnemyState.Died, (bool DeathSound) => {if(_enemyView.enemySounds.Count!=0)Debug.Log("DieSound");/*_enemyView.enemySounds[x].source.Play();*/} }
		};
	}

	public void SubscribeOnEvents()
	{
		_enemyView.OnPlaySound += PlaySound;
	}
	public void OnDestroy()
	{
		_enemyView.OnPlaySound -= PlaySound;
	}

	public void PlaySound(EnemyState enemyState)
	{
		if (_enemyStateDictionary.ContainsKey(enemyState))
		{
			_enemyStateDictionary[enemyState].Invoke(true);
		}
	}
}
