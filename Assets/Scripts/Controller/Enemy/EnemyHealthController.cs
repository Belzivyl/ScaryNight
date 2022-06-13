using System;
using UnityEngine;
internal sealed class EnemyHealthController : iOnDestroy
{
	private readonly EnemyModel _enemyModel;
	private readonly EnemyView _enemyView;
	private readonly FlashLightView _flashLightView;
	private float _lastTimeAttacked;
	public Action<EnemyType, EnemyView> onEnemyDeath;
	public Action<bool> onRestoreValues;
	private PlayerUIView _playerUIView;

	public EnemyHealthController(FlashLightView flashLightView, EnemyView enemyView, EnemyModel enemyModel, PlayerUIView playerUIView)
	{
		_enemyView = enemyView;
		_flashLightView = flashLightView;
		_enemyModel = enemyModel;
		_playerUIView = playerUIView;
	}

	public void SubscribeOnEvents()
	{
		_enemyView.OnTakingDamageChangeHealth += TakeDamage;
		_enemyView.OnHitPlayer += Die;
	}
	public void OnDestroy()
	{
		_enemyView.OnTakingDamageChangeHealth -= TakeDamage;
		_enemyView.OnHitPlayer -= Die;
	}


	private void TakeDamage(FlashLightModel flashLightModel)
	{
		if (Time.time > _lastTimeAttacked + flashLightModel.attackCooldown)
		{
			if (_enemyModel.EnemyHealth > 0)
			{
				_enemyModel.EnemyHealthChanged(_enemyModel.EnemyHealth - flashLightModel.CurrentFlashLightDamage);
				_enemyModel.EnemyStateChanged(EnemyState.Damaged);
				_enemyView.TellAnimationControllerToPlayAnimation("Damaged", true);
				_enemyView.TellAudioControllerToPlaySound(_enemyModel.EnemyState);
			}
			else { Die(); }
			_lastTimeAttacked = Time.time;
		}
	}

	private void Die()
	{
		SceneManageController.ChangeKillCount(1);
		_playerUIView.ChangeKillCounterText();
		//_playerUIView.ActivateLevelCompleteMenu();
		if (_enemyModel.EnemyState != EnemyState.Died)
		{
			_enemyModel.EnemyStateChanged(EnemyState.Died);
			_enemyView.TellAudioControllerToPlaySound(_enemyModel.EnemyState);
			_enemyView.Death(_enemyModel.EnemyType);
			_enemyView.TellAnimationControllerToPlayAnimation("Died", true);
			RestoreModelValuesBeforePuttingInThePool();
			//add coroutine to wait for death effects/animation
		}
	}

	private void RestoreModelValuesBeforePuttingInThePool() 
	{
		onRestoreValues?.Invoke(true);
		_enemyModel.EnemyHealthChanged(_enemyModel.EnemyBaseHealth);
	}
}
