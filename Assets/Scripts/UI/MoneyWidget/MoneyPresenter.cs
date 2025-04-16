using DG.Tweening;
using Modules.Money;
using System;
using UnityEngine;
using Zenject;

public sealed class MoneyPresenter : IInitializable, IDisposable, IMoneyPresenter
{
    public Vector2 MoneyWidgetPosition => _moneyView.IconPosition;
    public int NewValueMoney { get; private set; }
    public int PrevValueMoney { get; private set; }

    private readonly MoneyView _moneyView;
    private readonly IMoneyStorage _moneyStorage;

    public MoneyPresenter(MoneyView moneyView, IMoneyStorage moneyStorage)
    {
        _moneyView = moneyView;
        _moneyStorage = moneyStorage;
    }

    public void SetMoneyAfterParticleAnimation()
    {
        DoAnimation(PrevValueMoney, NewValueMoney, 1f);
    }

    void IInitializable.Initialize()
    {
        _moneyView.SetMoney(_moneyStorage.Money.ToString());
        _moneyStorage.OnMoneySpent += OnMoneySpent;
        _moneyStorage.OnMoneyChanged += OnMoneyChanged;
    }

    void IDisposable.Dispose()
    {                        
        _moneyStorage.OnMoneySpent -= OnMoneySpent;
        _moneyStorage.OnMoneyChanged -= OnMoneyChanged;
    }

    private void OnMoneyChanged(int newValue, int prevValue)
    {
        NewValueMoney = newValue;
        PrevValueMoney = prevValue;
    }

    private void OnMoneySpent(int newValue, int range)
    {
        DoAnimation(range, newValue, 0.5f);
    }
    private void DoAnimation(int prevValue, int newValue, float duration)
    {
        DOVirtual.Int(prevValue, newValue, duration, (x) =>
        {
            _moneyView.SetMoney(x.ToString());
        });
    }
}
