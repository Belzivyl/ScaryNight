using UnityEngine;

public sealed class FlashLightModel
{
	#region Parameteres
	private FlashLightStruct _flashLightStruct;

	public FlashLightType FlashLightType { get => _flashLightStruct.FlashLightType; }
	#endregion

	#region Slow Enemy Down
	public float EnemySlowDown { get => _flashLightStruct.EnemySlowDown; }
	#endregion

	#region Battery
	public float FlashLightBatteryCapacity { get => _flashLightStruct.flashLightBatteryCapacity; }
	public int MaxFlashLightBatteryCount { get => _flashLightStruct.MaxFlashLightBatteryCount; }
	public int CurrentFlashLightBatteryCount { get => _flashLightStruct.CurrentFlashLightBatteryCount; }
	public bool AutoBatteryReduce { get => _flashLightStruct.autoBatteryReduce; }
	public float BatteryReduceSpeed { get => _flashLightStruct.batteryReduceSpeed; }
	public bool AutoBatteryIncrease { get => _flashLightStruct.autoBatteryIncrease; }
	public float BatteryIncreaseSpeed { get => _flashLightStruct.batteryIncreaseSpeed; }
	public float EnergyChangeFrequency { get => _flashLightStruct.energyChangeFrequency; }
	public float ToggleOnWaitPercentage { get => _flashLightStruct.toggleOnWaitPercentage; }
	public float MinBatteryLife { get => _flashLightStruct.minBatteryLife; }
	public float MaxBatteryLife { get => _flashLightStruct.maxBatteryLife; }

	#endregion
	#region FollowSpeed
	public float FollowSpeed { get => _flashLightStruct.followSpeed; }
	public Quaternion Offset { get => _flashLightStruct.offset; }
	#endregion
	#region Attack
	public float FlashLightDamage
	{
		get => _flashLightStruct.minFlashLightDamage;
	}

	public float MinimumFlashLightDamage
	{
		get => _flashLightStruct.minFlashLightDamage;
	}

	public float MaximumFlashLightDamage
	{
		get => _flashLightStruct.maxFlashLightDamage;
	}

	public float attackCooldown { get => _flashLightStruct.AttackCooldown; }
	#endregion
	#region Light
	public Light LightComponent { get => _flashLightStruct.lightComponent; }
	public Color LightColor { get => _flashLightStruct.LightColor; }
	public Material VolumetricLightMat { get => _flashLightStruct.volumetricLightMat; }
	public float DefaultIntensity { get => _flashLightStruct.DefaultIntensity; }
	public float DefaultOpacity { get => _flashLightStruct.DefaultOpacity; }
	public float MinimumIntensity { get => _flashLightStruct.minimumIntensity; }
	public float MinimumOpacity { get => _flashLightStruct.minimumOpacity; }

	#endregion
	#region References
	public AudioClip ToggleOnOff { get => _flashLightStruct.toggleOnOff; }
	public AudioClip BuzzSound { get => _flashLightStruct.buzzSound; }
	public Texture FlashLightCookie { get => _flashLightStruct.FlashlightCookie; }
	public AudioClip ReloadBattery { get=> _flashLightStruct.reloadBattery; }
	#endregion
	#region Statistics
	public float CurrentFlashLightDamage { get; private set; }
	public float CurrentBatteryLife { get => _flashLightStruct.currentBatteryLife; }
	public float CurrentLightOpacity { get; private set; }
	public float CurrentLightIntensity { get; private set; }
	public bool FlashLightOn { get => _flashLightStruct.flashLightOn; }
	public bool OutOfEnergy { get => _flashLightStruct.outOfEnergy; }
	#endregion

	public FlashLightModel(FlashLightStruct flashLightStruct)
	{
		_flashLightStruct = flashLightStruct;
		flashLightStruct.offset = Quaternion.identity;
	}

	public void SetFlashLightIntensity(float value)
	{
		CurrentLightIntensity = value;
	}

	public void SetFlashLightDamage(float value)
	{
		CurrentFlashLightDamage = value;
	}

	public void SetFlashLightOpacity(float value)
	{
		CurrentLightOpacity = value;
	}

	public bool SetBatteryEnergy(float value)
	{
		if (value != 0)
		{
			_flashLightStruct.currentBatteryLife = value;
			return true;
		}

		return false;
	}
	public bool FlashLightIsOn(bool value)
	{
		_flashLightStruct.flashLightOn = value;
		return true;
	}
	public bool OutOfAllEnergy(bool value)
	{
		_flashLightStruct.outOfEnergy = value;
		return value;
	}
	
	public void LightIntensityChange()
	{
		if (FlashLightOn)
		{
			CurrentLightIntensity = CurrentBatteryLife / MaxBatteryLife * DefaultIntensity + MinimumIntensity;
		}
		else
		{
			CurrentLightIntensity = 0f;
		}
	}
	
	public bool BatteryCapacityChanged(float value)
	{
		if (value != 0)
		{
			_flashLightStruct.flashLightBatteryCapacity = value;
			return true;
		}
		return false;
	}

	public float ChangeAndGetCurrentBatteryLife(float energyLoss)
	{
		_flashLightStruct.currentBatteryLife -= energyLoss;
		if (_flashLightStruct.currentBatteryLife >= MaxBatteryLife)
		{
			_flashLightStruct.currentBatteryLife = MaxBatteryLife;
		}
		//Debug.Log($"Current Energy : {CurrentBatteryLife}");
		return CurrentBatteryLife;
	}

	public void MaterialOpacityChange()
	{
		if (FlashLightOn)
		{
			CurrentLightOpacity = CurrentBatteryLife / MaxBatteryLife * DefaultOpacity + MinimumOpacity;
		}
		else
		{
			CurrentLightOpacity = 0f;
		}
	}
}
