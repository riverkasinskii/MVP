using UnityEngine;

public interface IMoneyPresenter
{
    public Vector2 MoneyWidgetPosition { get; }
    public void SetMoneyAfterParticleAnimation();    
}