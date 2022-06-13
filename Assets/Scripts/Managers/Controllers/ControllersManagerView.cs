using UnityEngine;

public sealed class ControllersManagerView : MonoBehaviour
{
    private ControllersManager _controllersManager;
    private void Awake()
	{
        SceneManageController.SetSceneMagerController();
        _controllersManager = new ControllersManager();
        //initialize controllers
        new PlayerInitialization(_controllersManager);
        new FlashLightInitialization(_controllersManager);
        new EnemyControllerStarter(_controllersManager);
        
        //add scene/level manager+setting enemy spawn for each scene

        _controllersManager.Awake();
    }

	private void OnEnable()
	{
        _controllersManager.OnEnable();
	}

	private void Start()
    {
        _controllersManager.Start();
    }

    private void Update()
    {
        _controllersManager.Update();
    }

    private void FixedUpdate()
    {
        _controllersManager.FixedUpdate();
    }

    private void OnDestroy()
	{
        _controllersManager.OnDestroy();
        StopAllCoroutines();
    }
}
