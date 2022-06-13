using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(Animator))]
[RequireComponent(typeof(AudioSource))]
public class EnemyView : MonoBehaviour
{
    #region Animation
    [Header("Animation")]
    public Animator EnemyAnimator;
    public event Action<string, bool> OnPlayAnimation;
    public event Action<float> OnAnimationSlowDown;
    public event Action OnAnimationBaseSpeedReturn;
    #endregion

    #region Components
    [Header("Components")]
    public Rigidbody EnemyRigidBody;
    public NavMeshAgent NavMeshAgent;
    public ParticleSystem DamageEffect;
    public ParticleSystem DeathEffect;
    public List<EnemySound> enemySounds;
    public float TimeToWaitBeforeSpawningNewEnemy;
    public float TimeToWaitBeforeDeactivatingEnemy;
    public GameObject MeshGO;
    public GameObject eyeLight;
    #endregion

    #region Movement actions
    public event Action<FlashLightModel> OnHitSlowDown;
    public event Action OnLightBeamExit;
    public event Action OnEnemyCanMove;
    #endregion

    #region Health actions
    public event Action<FlashLightModel> OnTakingDamageChangeHealth;
    public event Action OnHitPlayer;
    public event Action OnStopTakingDamage;
    public Action<EnemyType, EnemyView> onEnemyDeath;
    public Action onEnemyDeathActivateNewEnemy;
    #endregion

    public event Action<EnemyState> OnPlaySound;
    public bool IsInTheLightBeam;


    void Start()
    {
      EnemyAnimator = GetComponent<Animator>();
      EnemyRigidBody = GetComponent<Rigidbody>();
    }

	#region Movement
	public void EnemyCanMove()
	{
        OnEnemyCanMove?.Invoke();
	}
	#endregion

	#region OnTrigger methods
	private void OnTriggerEnter(Collider other)
	{
        if (other.TryGetComponent<FlashLightView>(out var flashlightView)) //FlashLightView? GetComponentInParent?
		{
            IsInTheLightBeam = true;
            flashlightView.HitEnemy(this);
            DamageEffect.Play();
		}
		if (other.TryGetComponent<PlayerView>(out _))
		{
            TellAnimationControllerToPlayAnimation("Attacking", true);
            OnHitPlayer?.Invoke();
		}
    }

    private void OnTriggerStay(Collider other)
	{
        if (other.TryGetComponent<FlashLightView>(out var flashlightView)) //FlashLightView? GetComponentInParent?
        {
            flashlightView.HitEnemy(this);
        }
    }

	public void OnTriggerExit(Collider other)
	{
        //add another event That triggers animation slow down
        if (other.TryGetComponent<FlashLightView>(out _)) //FlashLightView? GetComponentInParent?
        {
            TellAnimationControllerToPlayAnimation("Damaged", false);
            IsInTheLightBeam = false;
            OnLightBeamExit?.Invoke();
            OnStopTakingDamage?.Invoke();
            DamageEffect.Stop();
        }
    }
	#endregion

	#region Audio
	public void TellAudioControllerToPlaySound(EnemyState enemyState)
	{
        OnPlaySound?.Invoke(enemyState);
    }
    #endregion

	#region Animation
    public void TellAnimationControllerToPlayAnimation(string animState, bool value)
    {
        OnPlayAnimation?.Invoke(animState, value);
    }

    public void SlowDownAnimation(float slowDownValue)
    {
        OnAnimationSlowDown?.Invoke(slowDownValue);
    }

    public void SpeedUpAnimation()
    {
        OnAnimationBaseSpeedReturn?.Invoke();
    }
	#endregion

	#region Health
	public void GotHitByPlayer(FlashLightModel flashLightModel)
	{
        OnHitSlowDown?.Invoke(flashLightModel);
        OnTakingDamageChangeHealth?.Invoke(flashLightModel);
    }

    public void Death(EnemyType enemyType)
	{
        NavMeshAgent.enabled = false;
        MeshGO.SetActive(false);
        eyeLight.SetActive(false);
        DeathEffect.Play();
        StartCoroutine(timer(TimeToWaitBeforeDeactivatingEnemy, enemyType));
    }
	public IEnumerator timer(float timeToWait, EnemyType enemyType)
    {
        float elapsed = 0.0f;
        while (elapsed < timeToWait)
        {
            elapsed += Time.deltaTime;
            yield return null;
        }
        onEnemyDeath?.Invoke(enemyType, this);
        Invoke("SpawnNewEnemy", TimeToWaitBeforeSpawningNewEnemy);
    }
	#endregion

	#region Spawning
	private void SpawnNewEnemy()
	{
        onEnemyDeathActivateNewEnemy?.Invoke();
    }
	#endregion
}
