using Modules.Planets;
using System;
using UnityEngine;
using Zenject;

public sealed class PlanetPresenter : IInitializable, IDisposable
{
    public Vector2 PlanetPosition => _planetView.transform.position;
        
    private readonly PlanetView _planetView;
    private readonly IPlanet _planet;
    private readonly PlanetPopupShower _planetPopupShower;
    private readonly ParticleAnimatorShower _particleAnimatorShower;
   
    public PlanetPresenter(PlanetView planetView, IPlanet planet, PlanetPopupShower planetPopupShower, ParticleAnimatorShower particleAnimatorShower)
    {        
        _planet = planet;
        _planetView = planetView;      
        _planetPopupShower = planetPopupShower;
        _particleAnimatorShower = particleAnimatorShower;
    }

    void IInitializable.Initialize()
    {
        _planetView.SetIcon(_planet.GetIcon(false));
        _planetView.SetPriceText(_planet.Price.ToString());
        _planetView.SetCoin(false);
        _planetView.SetIncome(false);
        _planetView.OnButtonClicked += OnButtonClicked;
        _planetView.OnButtonHolded += OnButtonHolded;
        _planet.OnIncomeTimeChanged += OnIncomeTimeChanged;
        _planet.OnIncomeReady += OnIncomeReady;
    }

    void IDisposable.Dispose()
    {
        _planetView.OnButtonClicked -= OnButtonClicked;
        _planetView.OnButtonHolded -= OnButtonHolded;
        _planet.OnIncomeTimeChanged -= OnIncomeTimeChanged;
        _planet.OnIncomeReady -= OnIncomeReady;
    }

    private void OnIncomeReady(bool state)
    {
        _planetView.SetCoin(state);
        _planetView.SetIncome(!state);
        _planetView.SetIcon(_planet.GetIcon(true));
    }

    private void OnIncomeTimeChanged(float value)
    {
        int minutes = (int)(value / 60);
        int newSec = (int)(value - minutes * 60);
        _planetView.SetTime($"{minutes}m:{newSec}s");
        _planetView.SetFillAmountProgressBar(_planet.IncomeProgress);
    }

    private void OnButtonClicked()
    {        
        if (_planet.CanUnlockOrUpgrade)
        {
            _planetView.SetLock(false);
            _planet.Unlock();
            _planetView.SetCoin(false);
            _planetView.SetPriceState(false);
            _planetView.SetIcon(_planet.GetIcon(true));
        }
        if (_planet.IsIncomeReady)
        {
            _planet.GatherIncome();
            _particleAnimatorShower.ParticleEmit(PlanetPosition);
        }
    }

    private void OnButtonHolded()
    {
        _planetPopupShower.Show(_planet);
    }
}
