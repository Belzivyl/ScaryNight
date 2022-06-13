using System;
internal class GeneralEnemyController: iOnDestroy
{
	private EnemyAttackController _enemyAttackController;
	private EnemyHealthController _enemyHealthController;
	private EnemyMoveController _enemyMoveController;
	private EnemySoundController _enemySoundController;
	private EnemySpawnController _enemySpawnController;
	private EnemyAnimationController _enemyAnimationController;
	private EnemyView _enemyView;
	public GeneralEnemyController(EnemyView enemyView, EnemyAttackController enemyAttackController, EnemyHealthController enemyHealthController, EnemyMoveController enemyMoveController, EnemySoundController enemySoundController, EnemySpawnController enemySpawnController, EnemyAnimationController enemyAnimationController)
	{
		_enemyAttackController = enemyAttackController;
		_enemyHealthController = enemyHealthController;
		_enemyMoveController = enemyMoveController;
		_enemySoundController = enemySoundController;
		_enemySpawnController = enemySpawnController;
		_enemyAnimationController = enemyAnimationController;
		_enemyView = enemyView;
		SubscribeOnEvents();
	}

	public void SubscribeOnEvents()
	{
		//_enemySpawnController.onEnemyShouldMove += AllowEnemyToMove;
		_enemyView.OnEnemyCanMove += AllowEnemyToMove;
		_enemyHealthController.onRestoreValues += RestoreValues;
	}
	public void OnDestroy()
	{
		//_enemySpawnController.onEnemyShouldMove -= AllowEnemyToMove;
		_enemyView.OnEnemyCanMove -= AllowEnemyToMove;
		_enemyHealthController.onRestoreValues -= RestoreValues;
	}

	public void RestoreValues(bool state)
	{
		_enemyMoveController.RestoreValues();

	}
	public void AllowEnemyToMove()
	{
		//fix 64 calls. Call not from SpawnController
		_enemyMoveController.SetEnemySpeed();
	}
}
