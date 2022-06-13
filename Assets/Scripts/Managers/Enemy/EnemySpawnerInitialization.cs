using UnityEngine;

internal sealed class EnemySpawnerInitialization
{
	public EnemySpawnerInitialization(ControllersManager controllersManager, EnemySpawnerView [] enemySpawner, PlayerView player, FlashLightView flashLight, EnemyInstantiator enemyInstantiator, PlayerUIView playerUIView)
	{
		var EnemySpawnController = new EnemySpawnController(enemySpawner, enemyInstantiator);

		controllersManager.Add(EnemySpawnController);
		EnemySpawnController.CreateEasyMonsters(controllersManager, flashLight, enemyInstantiator, player, EnemySpawnController, playerUIView);
		EnemySpawnController.CreateHardMonsters(controllersManager, flashLight,enemyInstantiator, player, EnemySpawnController, playerUIView);
	}

}
