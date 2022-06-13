using System;
using UnityEngine;
using UnityEngine.UI;

public sealed class FlashLightUIview: MonoBehaviour
{
	
	public Slider batteryLifeSlider;
	public Image batteryLifeFillImage;
	public event Action<float> OnEnergyChange;

	public void FlashLightEnergyChange(float energy)
	{
		OnEnergyChange?.Invoke(energy);
	}

	// public void InstantiateSlider()
	// {
	// 	batteryLifeSlider = Instantiate(Resources.Load<Slider>("UI/FlashLightUI/BatteryEnergySlider"), gameObject.transform);
	// }
}

