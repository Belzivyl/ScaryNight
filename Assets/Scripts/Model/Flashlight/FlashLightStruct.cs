using System;
using UnityEngine;

[Serializable]
public struct FlashLightStruct
{
	#region Parameters 
	[Header("FlashLight type")]
	public FlashLightType FlashLightType;
	[Header("Enemy Slow down multiplier")]
	public float EnemySlowDown;
	[Header("Damage per cooldown(second)")]
	public float maxFlashLightDamage;
	public float minFlashLightDamage;
	public float AttackCooldown;
	[Header("Battery")]
	public float flashLightBatteryCapacity;
	public int MaxFlashLightBatteryCount;
	public int CurrentFlashLightBatteryCount;
	public bool autoBatteryReduce;
	public float batteryReduceSpeed;
	public bool autoBatteryIncrease;
	public float batteryIncreaseSpeed;
	public float energyChangeFrequency;
	[Header("Follow speed")]
	public float followSpeed;
	public Quaternion offset;
	[Range(0, 1)]
	public float toggleOnWaitPercentage;
	public float minBatteryLife;
	public float maxBatteryLife;
	#endregion
	#region References
	[Header("Audio")]
	public AudioClip toggleOnOff;
	public AudioClip buzzSound;
	public AudioClip reloadBattery;
	[Header("Light")]
	public Material volumetricLightMat;
	public Texture FlashlightCookie;
	public Light lightComponent;
	public Color LightColor;
	public float DefaultIntensity;
	public float DefaultOpacity;
	public float minimumIntensity;
	public float minimumOpacity;
	//public float maxLightIntensity;
	#endregion
	#region Statistics
	public float currentBatteryLife;
	public bool flashLightOn;
	public bool outOfEnergy;
	#endregion
	//add flashlight vfx: dissolving stuff

	//НАПИСАТЬ ГЕТЕРЫ И СЕТЕРЫ(OnValueChanged)

}
