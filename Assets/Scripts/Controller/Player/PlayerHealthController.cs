using UnityEngine;

public class PlayerHealthController : iOnDestroy
{
	#region Player
	private readonly PlayerView _playerView;
	private readonly PlayerModel _playerModel;
	private readonly PlayerUIView _playerUIView;
	#endregion

	private float _minPlayerHPWhenBloodMaskAlphaStops;
	private float _twoThirdsOfPlayerHP;
	private float _damagePercentageOftwoThirdsOfPlayerHP;
	private float _tempCurrentHP;

	public PlayerHealthController(PlayerView playerView, PlayerModel playerModel, PlayerUIView playerUIView)
	{
		_playerView = playerView;
		_playerModel = playerModel;
		_minPlayerHPWhenBloodMaskAlphaStops = _playerModel.MaxHP * 0.33f;
		_playerUIView = playerUIView;
	}

	public void SubscribeOnEvents()
	{
		_playerView.OnEnemyTouch += playerDamage;
	}
	
	public void OnDestroy()
	{
		_playerView.OnEnemyTouch -= playerDamage;
	}

	private void playerDamage(EnemyModel enemyData)
	{
		_tempCurrentHP = _playerModel.CurrentHP - enemyData.EnemyAttack;
		if (_tempCurrentHP <= 0) { _playerModel.HPChanged(0); PlayerDeath(false); }
		else { _playerModel.HPChanged(_tempCurrentHP); playerHealthPercentageCheck(enemyData.EnemyAttack); }
	}

	private void playerHealthPercentageCheck(float takenDamage) 
	{
		_twoThirdsOfPlayerHP =_playerModel.MaxHP - _minPlayerHPWhenBloodMaskAlphaStops;
		_damagePercentageOftwoThirdsOfPlayerHP = takenDamage / _twoThirdsOfPlayerHP;
		_playerView.ChangeBloodMaskAlpha(_damagePercentageOftwoThirdsOfPlayerHP);
	}

	private void PlayerDeath(bool playerIsAlive) 
	{
		Time.timeScale = 0;
		_playerUIView.DefeatMenu.SetActive(true);
		_playerView.StopBlinkingBloodMask(playerIsAlive);
	}
}
