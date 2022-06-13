using System;
using UnityEngine;
using UnityEngine.UI;

public sealed class FlashLightUIController : iOnDestroy
{
	private readonly FlashLightUIModel _flashLightUIModel;
	private readonly FlashLightModel _flashLightModel;
	private readonly FlashLightUIview _flashLightUIView;

	public FlashLightUIController(FlashLightUIModel flashLightUIModel, FlashLightModel flashLightModel, FlashLightUIview flashLightUIView)
	{
		_flashLightUIModel = flashLightUIModel;
		_flashLightModel = flashLightModel;
		_flashLightUIView = flashLightUIView;
	}

	public void SubscribeOnEvents()
	{
		_flashLightUIView.OnEnergyChange += ChangeBatteryEnergySlider;
	}

	private void ChangeBatteryEnergySlider(float currentBatteryEnergyFraction)
	{
		// _flashLightUIModel.BatteryLifeFillImage.color = Color.Lerp(_flashLightUIModel.EmptyBatteryColor, 
		// 	_flashLightUIModel.FullBatteryColor, currentBatteryEnergyFraction); // not working as intended
		
		_flashLightUIModel.BatteryLifeFillImage.color = Color.Lerp(Color.red, Color.green, currentBatteryEnergyFraction);
		_flashLightUIView.batteryLifeFillImage.color = _flashLightUIModel.BatteryLifeFillImage.color;
		
		_flashLightUIModel.BatteryLifeSlider.value = currentBatteryEnergyFraction;
		_flashLightUIView.batteryLifeSlider.value = _flashLightUIModel.BatteryLifeSlider.value;
	}

	public void OnDestroy()
	{
		_flashLightUIView.OnEnergyChange -= ChangeBatteryEnergySlider;
	}
}
