using System;
using UnityEngine;

[RequireComponent(typeof(BoxCollider))]
public class PlayerView : MonoBehaviour
{
    public event Action<EnemyModel> OnEnemyTouch;
    public event Action<float> OnHPChangeTrack;
    public event Action<bool> OnPlayerDeath;
    public FlashLightView FlashLightView;

    public void TakeDamage(EnemyModel enemyModel)
    {
       OnEnemyTouch?.Invoke(enemyModel);
    }

    public void ChangeBloodMaskAlpha(float damagePercentageOftwoThirdsOfPlayerHP)
    {
        OnHPChangeTrack?.Invoke(damagePercentageOftwoThirdsOfPlayerHP);
    }

    public void StopBlinkingBloodMask(bool value) 
    {
        OnPlayerDeath?.Invoke(value);
    }
}
