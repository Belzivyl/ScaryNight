using UnityEngine;

internal sealed class EnemyAnimationController: iOnDestroy
{
    private readonly EnemyView _enemyView;
    private Animator _enemyAnimator;
    private float _animationBaseSpeed;
    private float _slowDownSpeed;

    public EnemyAnimationController(EnemyView enemyView, EnemyModel enemyModel) 
    {
        _enemyView = enemyView;
        _enemyAnimator = enemyView.EnemyAnimator;
        _animationBaseSpeed = _enemyView.EnemyAnimator.speed;
    }

    public void SubscribeOnEvents()
    {
        _enemyView.OnPlayAnimation += ActivateAnimation;
        _enemyView.OnAnimationSlowDown += SlowDownWalkingAnimation;
        _enemyView.OnAnimationBaseSpeedReturn += SpeedUpWalkingAnimation;
    }

    public void OnDestroy()
    {
        _enemyView.OnPlayAnimation -= ActivateAnimation;
        _enemyView.OnAnimationSlowDown -= SlowDownWalkingAnimation;
        _enemyView.OnAnimationBaseSpeedReturn -= SpeedUpWalkingAnimation;
    }

    public void ActivateAnimation(string animState, bool value)
    {
        _enemyAnimator.SetBool(animState, value);
    }

    private void SlowDownWalkingAnimation(float slowDownValue) 
    {
        _slowDownSpeed = _animationBaseSpeed * slowDownValue;
        _enemyAnimator.SetFloat("WalkingSpeed", _slowDownSpeed);
    }

    private void SpeedUpWalkingAnimation()
    {
        _enemyAnimator.SetFloat("WalkingSpeed", _animationBaseSpeed);
    }
}
