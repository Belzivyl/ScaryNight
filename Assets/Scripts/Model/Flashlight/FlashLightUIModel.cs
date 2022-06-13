using UnityEngine.UI;
using UnityEngine;
using TMPro;

public class FlashLightUIModel
{
	private FlashLightUIStruct _flashLightUIStruct;

	public Image StateIconImage => _flashLightUIStruct.stateIcon;
	public Slider BatteryLifeSlider => _flashLightUIStruct.batteryLifeSlider;
	public Image BatteryLifeFillImage => _flashLightUIStruct.batteryLifeFillImage;
	public TextMeshProUGUI ReloadText => _flashLightUIStruct.reloadText;
	public TextMeshProUGUI BatteryCountText => _flashLightUIStruct.batteryCountText;
	public Color FullBatteryColor => _flashLightUIStruct.fullBatteryColor;
	public Color EmptyBatteryColor => _flashLightUIStruct.emptyBatteryColor;

	public FlashLightUIModel(FlashLightUIStruct flashLightUIStruct)
	{
		_flashLightUIStruct = flashLightUIStruct;
	}

	public void ChangeEnergyBatterySliderValue(Slider energyBatterySlider)
	{
		_flashLightUIStruct.batteryLifeSlider = energyBatterySlider;
	}

}
