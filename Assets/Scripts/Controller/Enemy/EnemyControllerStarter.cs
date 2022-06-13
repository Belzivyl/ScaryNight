using UnityEngine;

internal sealed class EnemyControllerStarter
{
	//заменить на статический метод, чтобы не загаживать экземплярами класса кучу
	public EnemyControllerStarter(ControllersManager controllers)
	{
		var FlashLightView = Object.FindObjectOfType<FlashLightView>();
		var PlayerView = Object.FindObjectOfType<PlayerView>();
		var Spawner = Object.FindObjectsOfType<EnemySpawnerView>();
		var EnemyInstantiator = Object.FindObjectOfType<EnemyInstantiator>();
		var PlayerUIView = Object.FindObjectOfType<PlayerUIView>();

		new EnemySpawnerInitialization(controllers, Spawner, PlayerView, FlashLightView, EnemyInstantiator, PlayerUIView);
	}
}

