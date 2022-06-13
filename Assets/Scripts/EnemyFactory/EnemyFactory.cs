using System;
using UnityEngine;

internal abstract class EnemyFactory : MonoBehaviour
{
	public static EnemyView CreateEasyMonster(ControllersManager controllers, FlashLightView flashlight, PlayerView playerView, Transform spawnTransform, Action<EnemyType, EnemyView> onEnemyDeath, EnemySpawnController enemySpawnController, PlayerUIView playerUIView)
	{
		var easyMonster = Instantiate(Resources.Load<EnemyView>("Enemies/EasyMonster_test"), spawnTransform);
		var easyModel = new EnemyModel(Resources.Load<EnemySO>("Enemies/EasyMonster").EnemyStruct);
		easyMonster.InitializeEnemy(controllers, flashlight, onEnemyDeath, easyModel, playerView, enemySpawnController, playerUIView);
		return easyMonster;
	}
	public static EnemyView CreateHardMonster(ControllersManager controllers, FlashLightView flashlight, PlayerView playerView, Transform spawnTransform, Action<EnemyType,EnemyView> onEnemyDeath, EnemySpawnController enemySpawnController, PlayerUIView playerUIView)
	{
		var hardMonster = Instantiate(Resources.Load<EnemyView>("Enemies/StrongMonster_Test"), spawnTransform);
		var strongModel = new EnemyModel(Resources.Load<EnemySO>("Enemies/StrongEnemy").EnemyStruct);
		hardMonster.InitializeEnemy(controllers, flashlight, onEnemyDeath, strongModel, playerView, enemySpawnController, playerUIView);
		return hardMonster;
	}
}

