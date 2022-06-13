using UnityEngine;

[CreateAssetMenu(fileName = "New FlashlightUI data", menuName = "New flashlightUI", order = 4)]
public sealed class FlashLightUISO : ScriptableObject
{
	[SerializeField] private FlashLightUIStruct _flashLightUIStruct;
	public FlashLightUIStruct FlashLightUIStruct => _flashLightUIStruct;
}

