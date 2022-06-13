using System;
using System.Collections;
using UnityEngine;

public class FlashLightView : MonoBehaviour
{
    
    public Light lightComponent;
    public event Action<EnemyView> OnEnemyHit;
	public event Action OnDoubleTap;
	private MeshCollider _beamCollider;

	public MeshCollider BeamCollider
	{
		get
		{
			if (!_beamCollider)
			{
				_beamCollider = gameObject.GetComponent<MeshCollider>();
			}
			return _beamCollider;
		}
	}
	public void StartFlashLightCoroutine(IEnumerator flashLightCoroutine)
	{
		StartCoroutine(flashLightCoroutine);
	}
	public void HitEnemy(EnemyView enemyView)
	{
        OnEnemyHit?.Invoke(enemyView);
	}

	public void DoubleTap() 
	{ 
		OnDoubleTap?.Invoke();
	}
}
