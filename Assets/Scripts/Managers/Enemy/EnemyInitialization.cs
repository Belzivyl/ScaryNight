using System;
internal static class EnemyInitialization
{
	public static void InitializeEnemy(this EnemyView enemy, ControllersManager controllersManager, FlashLightView flashLight, Action<EnemyType, EnemyView> onEnemyDeath, EnemyModel enemyModel, PlayerView playerView, EnemySpawnController enemySpawnController, PlayerUIView playerUIView)
	{
		var EnemyMoveController = new EnemyMoveController(enemy, enemyModel);
		var EnemyHealthController = new EnemyHealthController(flashLight, enemy, enemyModel, playerUIView);
		var EnemyAttackController = new EnemyAttackController(playerView, enemy, enemyModel);
		var EnemySoundController = new EnemySoundController(enemy, enemyModel);
		var EnemyAnimationController = new EnemyAnimationController(enemy, enemyModel);
		//EnemyHealthController.onEnemyDeath += onEnemyDeath;

		controllersManager.Add(EnemyMoveController);
		controllersManager.Add(EnemyHealthController);
		controllersManager.Add(EnemyAttackController);
		controllersManager.Add(EnemySoundController);
		controllersManager.Add(EnemyAnimationController);
		
		var GeneralEnemyController = new GeneralEnemyController(enemy, EnemyAttackController, EnemyHealthController, EnemyMoveController, EnemySoundController, enemySpawnController, EnemyAnimationController);
		controllersManager.Add(GeneralEnemyController);

		EnemyMoveController.SubscribeOnEvents();
		EnemyHealthController.SubscribeOnEvents();
		EnemyAttackController.SubscribeOnEvents();
		EnemySoundController.SubscribeOnEvents();
		EnemyAnimationController.SubscribeOnEvents();

	}
}
