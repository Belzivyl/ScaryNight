using System;
using UnityEngine;
internal sealed class EnemyAttackController: iOnDestroy
{
	private readonly EnemyModel _enemyModel;
	private readonly EnemyView _enemyView;
	private readonly PlayerView _playerView;

	public EnemyAttackController (PlayerView playerView, EnemyView enemyView, EnemyModel enemyModel)
	{
		_enemyView = enemyView;
		_enemyModel = enemyModel;
		_playerView = playerView;
	}

	public void OnDestroy()
	{
		_enemyView.OnHitPlayer -= DealDamage;
	}

	public void SubscribeOnEvents()
	{
		_enemyView.OnHitPlayer += DealDamage;
	}

	public void DealDamage()
	{
		_enemyModel.EnemyStateChanged(EnemyState.Attacking);
		_playerView.TakeDamage(_enemyModel);
		_enemyView.TellAudioControllerToPlaySound(_enemyModel.EnemyState);
	}
}