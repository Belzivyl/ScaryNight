using UnityEngine;

internal sealed class FlashLightInitialization
{
	//заменить на статический метод, чтобы не загаживать экземплярами класса кучу
	public FlashLightInitialization(ControllersManager controllersManager)
	{
		var flashLightView = Object.FindObjectOfType<FlashLightView>();
		var flashLightUIView = Object.FindObjectOfType<FlashLightUIview>();


		var flashLightModel = new FlashLightModel(Resources.Load<FlashLightSO>("Flashlight/FlashLight_type 1").FlashLightStruct);

		var flashLightUIModel = new FlashLightUIModel(Resources.Load<FlashLightUISO>("Flashlight/FlashlightUI data").FlashLightUIStruct);

		var flashLightController = new FlashLightController(flashLightModel, flashLightUIView, flashLightView);

		var flashLightUIController = new FlashLightUIController(flashLightUIModel, flashLightModel, flashLightUIView);

		controllersManager.Add(flashLightController);
		controllersManager.Add(flashLightUIController);

		flashLightController.SetFlashLightParameters();
		flashLightController.SubscribeOnEvents();
		//flashLightController.BeamParametersSetting();
		
		flashLightUIController.SubscribeOnEvents();
		// flashLightUIController.SetBatteryEnergySlider();

	}
	
	
	//public static void FlashLightInitialize(ControllersManager controllersManager)
	//{

	//}
}
