using UnityEngine;
internal sealed class EnemyMoveController : iOnDestroy
{
	private readonly EnemyModel _enemyModel;
	private readonly EnemyView _enemyView;
	public bool canMove;
	public EnemyMoveController(EnemyView enemyView, EnemyModel enemyModel)
	{
		_enemyView = enemyView;
		_enemyModel = enemyModel;
		_enemyView.NavMeshAgent.speed = 0;
		canMove = false;
	}
	public void OnDestroy()
	{
		_enemyView.OnHitSlowDown -= SlowDown;
		_enemyView.OnLightBeamExit -= SpeedUp;
	}

	public void SubscribeOnEvents()
	{
		_enemyView.OnHitSlowDown += SlowDown;
		_enemyView.OnLightBeamExit += SpeedUp;
	}

	public void RestoreValues()
	{
		_enemyModel.EnemySpeedChanged(_enemyModel.EnemyBaseSpeed);
		canMove = false;
	}
	public void SetEnemySpeed()
	{
		canMove = true;
		_enemyView.NavMeshAgent.speed = _enemyModel.EnemyBaseSpeed;
	}

	public void SlowDown(FlashLightModel flashLightModel)
	{
		//add corutine to make it smooth
		if (_enemyView.IsInTheLightBeam == true && _enemyView.NavMeshAgent.speed >(_enemyModel.EnemyBaseSpeed * flashLightModel.EnemySlowDown)&&canMove==true)
		{
			_enemyModel.EnemySpeedChanged(_enemyModel.EnemySpeed*flashLightModel.EnemySlowDown);
			_enemyView.NavMeshAgent.speed = _enemyModel.EnemySpeed;
			_enemyView.SlowDownAnimation(flashLightModel.EnemySlowDown);
		}
		
	}

	public void SpeedUp()
	{ 
		//add corutine to make it smooth
		
			if (_enemyView.IsInTheLightBeam == false&&canMove==true)
			{
				_enemyModel.EnemySpeedChanged(_enemyModel.EnemyBaseSpeed);
				_enemyView.NavMeshAgent.speed = _enemyModel.EnemyBaseSpeed;
				_enemyView.SpeedUpAnimation();
			}
	}
}
