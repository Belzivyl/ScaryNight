using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

[Serializable]
public struct FlashLightUIStruct
{
	#region References
	public Image stateIcon;
	public Slider batteryLifeSlider;
	public Image batteryLifeFillImage;
	public TextMeshProUGUI reloadText;
	public TextMeshProUGUI batteryCountText;
	public Color fullBatteryColor;
	public Color emptyBatteryColor;


	#endregion

}
