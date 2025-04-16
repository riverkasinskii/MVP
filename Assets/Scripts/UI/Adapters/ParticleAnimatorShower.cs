using Modules.UI;
using UnityEngine;

public sealed class ParticleAnimatorShower
{        
    private readonly ParticleAnimator _particleAnimator;
    private readonly IMoneyPresenter _moneyPresenter;        
    
    public ParticleAnimatorShower(ParticleAnimator particleAnimator, MoneyPresenter moneyPresenter)
    {        
        _particleAnimator = particleAnimator;        
        _moneyPresenter = moneyPresenter;        
    }
    
    public void ParticleEmit(Vector2 position)
    {
        _particleAnimator.Emit(position, _moneyPresenter.MoneyWidgetPosition, 1f, () =>
        {
            _moneyPresenter.SetMoneyAfterParticleAnimation();
        });
    }
}
