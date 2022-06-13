using System.Collections.Generic;
internal sealed class ControllersManager : iOnEnable, iAwake, iStart, iFixedUpdate, iLateUpdate, iUpdate, iOnDestroy
{
	private readonly List<iAwake> _iAwakes;
	private readonly List<iOnEnable> _iOnEnables;
	private readonly List<iStart> _iStarts;
	private readonly List<iFixedUpdate> _iFixedUpdates;
	private readonly List<iLateUpdate> _iLateUpdates;
	private readonly List<iUpdate> _iUpdates;
	private readonly List<iOnDestroy> _iOnDestroys;

	internal ControllersManager()
	{
		_iAwakes = new List<iAwake>();
		_iOnEnables = new List<iOnEnable>();
		_iStarts = new List<iStart>();
		_iFixedUpdates = new List<iFixedUpdate>();
		_iLateUpdates = new List<iLateUpdate>();
		_iUpdates = new List<iUpdate>();
		_iOnDestroys = new List<iOnDestroy>();
	}

	internal ControllersManager Add(iController controller)
	{
		if(controller is iOnEnable onEnableController)
		{
			_iOnEnables.Add(onEnableController);
		}

		if(controller is iAwake awakeController)
		{
			_iAwakes.Add(awakeController);
		}
		if(controller is iStart startController)
		{
			_iStarts.Add(startController);
		}
		if (controller is iFixedUpdate fixedUpdateController) 
		{
			_iFixedUpdates.Add(fixedUpdateController);
		}
		if(controller is iLateUpdate lateUpdateController)
		{
			_iLateUpdates.Add(lateUpdateController);
		}
		if(controller is iUpdate updateController)
		{
			_iUpdates.Add(updateController);
		}
		if(controller is iOnDestroy onDestroyController)
		{
			_iOnDestroys.Add(onDestroyController);
		}
		return this;
	}


	public void Awake()
	{
		for(var index = 0; index<_iAwakes.Count; ++index)
		{
			_iAwakes[index].Awake();
		}
	}

	public void FixedUpdate()
	{
		for (var index = 0; index < _iFixedUpdates.Count; ++index)
		{
			_iFixedUpdates[index].FixedUpdate();
		}
	}

	public void LateUpdate()
	{
		for(var index = 0; index<_iLateUpdates.Count; ++index)
		{
			_iLateUpdates[index].LateUpdate();
		}
	}

	public void OnDestroy()
	{
		for(var index = 0; index<_iOnDestroys.Count; ++index)
		{
			_iOnDestroys[index].OnDestroy();
		}
	}

	public void Start()
	{
		for(var index=0; index<_iStarts.Count; ++index)
		{
			_iStarts[index].Start();
		}
	}

	public void Update()
	{
		for(var index = 0; index < _iUpdates.Count; ++index)
		{
			_iUpdates[index].Update();
		}
	}

	public void OnEnable()
	{
		for (var index = 0; index < _iOnEnables.Count; ++index)
		{
			_iOnEnables[index].OnEnable();
		}
	}
}
