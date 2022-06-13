using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public sealed class FlashLightController : iStart, iOnDestroy
{
    private readonly FlashLightModel _flashLightModel;
    private readonly FlashLightUIview _flashLightUIView;
    private readonly FlashLightView _flashLightView;
    private List <EnemyView> _currentlyHittedEnemies = new List<EnemyView>();
    public FlashLightController(FlashLightModel flashLightModel, FlashLightUIview flashLightUIView,
        FlashLightView flashLightView)
    {
        _flashLightModel = flashLightModel;
        _flashLightUIView = flashLightUIView;
        _flashLightView = flashLightView;
    }

    public void SubscribeOnEvents()
    {
        _flashLightView.OnDoubleTap += OnFlashLightToggle;
        _flashLightView.OnEnemyHit += HitEnemy;
    }

    private void HitEnemy(EnemyView enemyView)
    {
        enemyView.GotHitByPlayer(_flashLightModel);
        _currentlyHittedEnemies.Add(enemyView);
    }

    private void TellEnemyViewColliderIsOff()
	{
		if (_currentlyHittedEnemies.Count >0)
		{
            foreach(EnemyView enemyView in _currentlyHittedEnemies.ToList())
		    {
			    if (enemyView != null)
			    {
                    enemyView.OnTriggerExit(_flashLightView.BeamCollider);
                    _currentlyHittedEnemies.Remove(enemyView);
			    }
		    }
		}
	}
    public void Start()
    {
        _flashLightView.StartFlashLightCoroutine(FlashLightControllerCoroutine());
    }

    public void SetFlashLightParameters()
    {
        _flashLightView.lightComponent = _flashLightView.GetComponentInParent<Light>();
        _flashLightView.lightComponent.cookie = _flashLightModel.FlashLightCookie;
        _flashLightView.lightComponent.color = _flashLightModel.LightColor;
    }

    private void OnFlashLightToggle()
    {
        if (_flashLightModel.FlashLightOn)
        {
            _flashLightModel.FlashLightIsOn(false);
        }
        else
        {
            _flashLightModel.FlashLightIsOn(true);
        }
    }

    public void BeamParametersSetting()
    {
        _flashLightModel.FlashLightIsOn(true);
        _flashLightView.lightComponent.intensity = _flashLightModel.CurrentLightIntensity;
    }

    private IEnumerator FlashLightControllerCoroutine()
    {
        while (true)
        {
            if (_flashLightModel.FlashLightOn)
            {
                ChangeFlashLightStats(_flashLightModel.BatteryReduceSpeed);
                _flashLightView.BeamCollider.enabled = true;
            }
            else
            {
                ChangeFlashLightStats(-_flashLightModel.BatteryIncreaseSpeed);
                TellEnemyViewColliderIsOff();
                _flashLightView.BeamCollider.enabled = false;
            }

            yield return new WaitForSeconds(_flashLightModel.EnergyChangeFrequency);
        }
    }

    private void ChangeFlashLightStats(float batteryChangeSpeed)
    {
        if (ChangeAndGetCurrentBatteryLife(batteryChangeSpeed) <= 0f)
        {
            _flashLightModel.OutOfAllEnergy(true);
            _flashLightModel.FlashLightIsOn(false);
        }
        else
        {
            _flashLightModel.OutOfAllEnergy(false);
        }

        LightIntensityChange();
        FlashLightDamageChange();

        _flashLightView.lightComponent.intensity = _flashLightModel.CurrentLightIntensity;

        _flashLightUIView.FlashLightEnergyChange(_flashLightModel.CurrentBatteryLife / _flashLightModel.MaxBatteryLife);
    }

    private float ChangeAndGetCurrentBatteryLife(float energyLoss)
    {
        _flashLightModel.SetBatteryEnergy(_flashLightModel.CurrentBatteryLife - energyLoss);
        if (_flashLightModel.CurrentBatteryLife >= _flashLightModel.MaxBatteryLife)
        {
            _flashLightModel.SetBatteryEnergy(_flashLightModel.MaxBatteryLife);
        }
        return _flashLightModel.CurrentBatteryLife;
    }

    private void LightIntensityChange()
    {
        if (_flashLightModel.FlashLightOn)
        {
            _flashLightModel.SetFlashLightIntensity(_flashLightModel.CurrentBatteryLife / _flashLightModel.MaxBatteryLife *
                (_flashLightModel.DefaultIntensity - _flashLightModel.MinimumIntensity) + _flashLightModel.MinimumIntensity);
        }
        else
        {
            _flashLightModel.SetFlashLightIntensity(0f);
        }
    }

    private void FlashLightDamageChange()
    {
        if (_flashLightModel.FlashLightOn)
        {
            _flashLightModel.SetFlashLightDamage(_flashLightModel.CurrentBatteryLife / _flashLightModel.MaxBatteryLife *
                (_flashLightModel.DefaultIntensity - _flashLightModel.MinimumIntensity) + _flashLightModel.MinimumIntensity);
        }
        else
        {
            _flashLightModel.SetFlashLightDamage(0f);
        }
    }

    public void OnDestroy()
    {
        _flashLightView.OnEnemyHit -= HitEnemy;
        _flashLightView.OnDoubleTap -= OnFlashLightToggle;
    }
}