using UnityEngine;

[CreateAssetMenu(fileName = "New Flashlight data", menuName = "New flashlight", order = 3)]
public sealed class FlashLightSO: ScriptableObject
{
	[SerializeField] private FlashLightStruct _flashLightStruct;
	public FlashLightStruct FlashLightStruct => _flashLightStruct;
}
