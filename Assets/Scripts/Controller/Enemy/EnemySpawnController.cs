using System.Collections.Generic;
using System;
using UnityEngine;

internal sealed class EnemySpawnController : iAwake, iStart, iOnDestroy
{
	private Queue<EnemyView> _easyEnemyQueue = new Queue<EnemyView>();
	private Queue<EnemyView> _hardEnemyQueue = new Queue<EnemyView>();
	private Queue<EnemyView>[] _enemyQueues = new Queue<EnemyView>[2];
	private Queue<EnemyView> _enemyPool = new Queue<EnemyView>();
	private int _enemyCount;
	private readonly EnemySpawnerView[] _enemySpawnerViews;
	private EnemyInstantiator _enemyInstantiator;
	private EnemySpawnController _enemySpawnController;
	private int _sceneEnemyCount;


	public EnemySpawnController(EnemySpawnerView [] enemySpawnerViews, EnemyInstantiator enemyInstantiator)
	{
		_enemySpawnerViews = enemySpawnerViews;
		_enemyCount = _enemySpawnerViews.Length;
		_enemyInstantiator = enemyInstantiator;
		_enemySpawnController = this;
		SubscribeSpawnerViewsOnEvents();
	}

	#region Enemy creation (factory)
	public void CreateEasyMonsters(ControllersManager controllers, FlashLightView flashlight, EnemyInstantiator enemyInstantiator, PlayerView playerView, EnemySpawnController enemySpawnController, PlayerUIView playerUIView)
	{
		for (int i = 0; i < _enemyCount*2; i++)
		{
			EnemyView enemy = EnemyFactory.CreateEasyMonster(controllers, flashlight, playerView, enemyInstantiator.transform, DeactivateEnemy, _enemySpawnController, playerUIView);//<---прокидываем метод, который вызывается при смерти врага в EnemyHealthController
			enemy.gameObject.SetActive(false);
			_easyEnemyQueue.Enqueue(enemy);
			SubscribeOnEvents(enemy);
		}
	}
	public void CreateHardMonsters(ControllersManager controllers, FlashLightView flashlight, EnemyInstantiator enemyInstantiator, PlayerView playerView, EnemySpawnController enemySpawnController, PlayerUIView playerUIView)
	{
		for(int i = 0; i < _enemyCount*2; i++)
		{
			EnemyView enemy = EnemyFactory.CreateHardMonster(controllers, flashlight, playerView, enemyInstantiator.transform, DeactivateEnemy, _enemySpawnController, playerUIView);
			enemy.gameObject.SetActive(false);
			_hardEnemyQueue.Enqueue(enemy);
			SubscribeOnEvents(enemy);
		}
	}
	#endregion

	#region Subscriptions and desubscriptions
	public void SubscribeSpawnerViewsOnEvents()
	{
		foreach(EnemySpawnerView spawnerView in _enemySpawnerViews)
		{
			spawnerView.onEnemySpawnAnimationAndEffectsStop += SpawnEffectsAreStopped;
		}
	}
	public void SubscribeOnEvents(EnemyView enemyView)
	{
		enemyView.onEnemyDeath += DeactivateEnemy;
		enemyView.onEnemyDeath += EnqueueEnemy;
		enemyView.onEnemyDeathActivateNewEnemy += ActivateNextRandomEnemy;
	}
	public void OnDestroy()
	{
		foreach(EnemyView enemyView in _easyEnemyQueue)
		{
			enemyView.onEnemyDeath -= EnqueueEnemy;
			enemyView.onEnemyDeath -= DeactivateEnemy;
			enemyView.onEnemyDeathActivateNewEnemy -= ActivateNextRandomEnemy;
		}
		foreach (EnemyView enemyView in _hardEnemyQueue)
		{
			enemyView.onEnemyDeath -= EnqueueEnemy;
			enemyView.onEnemyDeath -= DeactivateEnemy;
			enemyView.onEnemyDeathActivateNewEnemy -= ActivateNextRandomEnemy;
		}
		foreach (EnemyView enemyView in _enemyPool)
		{
			enemyView.onEnemyDeath -= EnqueueEnemy;
			enemyView.onEnemyDeath -= DeactivateEnemy;
			enemyView.onEnemyDeathActivateNewEnemy -= ActivateNextRandomEnemy;
		}
		foreach(EnemySpawnerView enemySpawnerView in _enemySpawnerViews)
		{
			enemySpawnerView.onEnemySpawnAnimationAndEffectsStop -= SpawnEffectsAreStopped;
		}
	}
	#endregion

	#region Enemy activation/deactivation
	public void DeactivateEnemy(EnemyType enemyType, EnemyView enemy)
	{
		enemy.gameObject.SetActive(false);
		enemy.transform.position = _enemyInstantiator.transform.position;
		enemy.MeshGO.SetActive(true);
		enemy.eyeLight.SetActive(true);
	}
	public void ActivateRandomEnemy(int enemies)
	{
		for (int i = 1; i <= enemies; i++)
		{
			int randomNumber = UnityEngine.Random.Range(0, _enemySpawnerViews.Length);
			MoveEnemyToSpawnPoint(randomNumber);
		}
	}
	public void ActivateNextRandomEnemy()
	{
		_sceneEnemyCount += 1;
		if (_sceneEnemyCount <= SceneManageController.sceneManagerSO.SceneMaxKillCount)
			ActivateRandomEnemy(1);
	}
	private void MoveEnemyToSpawnPoint(int randomNumber)
	{
		if (_enemySpawnerViews[randomNumber].canSpawn)
		{
			var enemy = DequeueEnemy();
			enemy.transform.position = _enemySpawnerViews[randomNumber].transform.position;
			enemy.transform.rotation = _enemySpawnerViews[randomNumber].transform.rotation;
			enemy.transform.parent = _enemySpawnerViews[randomNumber].transform;
			_enemySpawnerViews[randomNumber].BoolReset(enemy.TimeToWaitBeforeSpawningNewEnemy-1f);//<<<---- Fix FUCKING MAGIC NUMBERS ALREADY
			enemy.gameObject.SetActive(true);
			_enemySpawnerViews[randomNumber].TurnOnSpawnEffect(enemy);
			//add couroutine waitfor (spawn animation length) seconds
			enemy.TellAudioControllerToPlaySound(EnemyState.Spawned);
			enemy.NavMeshAgent.enabled = true;
			enemy.NavMeshAgent.SetDestination(_enemySpawnerViews[randomNumber].destinationPoint.transform.position);
		}
		else
		{
			MoveEnemyToSpawnPoint(UnityEngine.Random.Range(0, _enemySpawnerViews.Length));
		}
	}
	#endregion

	#region Enemy Queueing / Dequeueing 
	public void CreateEnemyPool()
	{
		//add weights to increase/decrease possibility of spawning enemies of each kind
		_enemyQueues[0] = _easyEnemyQueue;
		_enemyQueues[1] = _hardEnemyQueue;
		EnemyView enemy;
		for(int i =0; i<=_enemyCount; i++)
		{
			var randomNumber = UnityEngine.Random.Range(0, _enemyQueues.Length);
			enemy = _enemyQueues[randomNumber].Dequeue();
			_enemyPool.Enqueue(enemy);
		}
	}
	public EnemyView DequeueEnemy()
	{
		EnemyView enemy;
		if (_enemyPool.Count > 0)
		{
			enemy = _enemyPool.Dequeue();
			return enemy;
		}
		else
		{
			CreateEnemyPool();
			enemy = _enemyPool.Dequeue();
			return enemy;
		}
	}
	public void EnqueueEnemy(EnemyType enemyType, EnemyView enemyView)
	{
		if (enemyType == EnemyType.Easy)
		{
			_easyEnemyQueue.Enqueue(enemyView);
		}
		else if (enemyType == EnemyType.Hard)
		{
			_hardEnemyQueue.Enqueue(enemyView);
		}
	}
	#endregion

	//Stopper of enemy movement for spawn animation to end
	public void SpawnEffectsAreStopped(EnemyView enemy)
	{
		enemy.EnemyCanMove();
		enemy.SpeedUpAnimation();
		enemy.TellAnimationControllerToPlayAnimation("Walking", true);
	}
	//=====
	public void Start()
	{
		ActivateRandomEnemy(_enemySpawnerViews.Length);
		_sceneEnemyCount = _enemySpawnerViews.Length;
	}

	public void Awake()
	{
		CreateEnemyPool();
	}
}

