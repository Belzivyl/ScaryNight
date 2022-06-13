using System.Collections;
using UnityEngine;
using System;
public class EnemySpawnerView: MonoBehaviour
{
    public ParticleSystem SpawnEffect;
    public float ParticleSystemEffectDuration;
	private bool _canSpawn = true;
    public bool canSpawn { get => _canSpawn; }
    public GameObject destinationPoint;
    public event Action<EnemyView> onEnemySpawnAnimationAndEffectsStop;
    
	public void Start()
	{
        ParticleSystemEffectDuration = 1.2f;
	}
	public void BoolReset(float duration)
	{
        StartCoroutine(boolResetter(duration));
	}
	IEnumerator boolResetter(float duration)
	{
        _canSpawn = false;
       float elapsed = 0.0f;
       while (elapsed < duration)
       {
            elapsed += Time.deltaTime;
            yield return null;
       }
        _canSpawn = true;
    }

    IEnumerator spawnEffectStopper(float timeToWait, EnemyView enemy)
	{
        float elapsed = 0.0f;
        while (elapsed < timeToWait)
		{
           elapsed += Time.deltaTime;
            yield return null;
		}
        onEnemySpawnAnimationAndEffectsStop?.Invoke(enemy);
	}
    public void TurnOnSpawnEffect(EnemyView enemyView)
	{
        SpawnEffect.Play();
        StartCoroutine(spawnEffectStopper(ParticleSystemEffectDuration, enemyView));
	}
}