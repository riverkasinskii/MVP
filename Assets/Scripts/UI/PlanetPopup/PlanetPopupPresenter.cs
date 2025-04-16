using Modules.Planets;
using System;
using UnityEngine;

public sealed class PlanetPopupPresenter : IPlanetPopupPresenter
{
    public event Action OnStateChanged;

    public Sprite AvatarPlanet => _planet != null ? _planet.GetIcon(true) : null;
    public string PlanetPopulationText => _planet != null ? $"Population: {_planet.Population}" : string.Empty;
    public string PlanetLevelText => _planet != null ? $"Level: {_planet.Level}" : string.Empty;
    public string PlanetIncomeText => _planet != null ? $"Income: {_planet.MinuteIncome}$" : string.Empty;
    public string PlanetPriceText => _planet != null ? _planet.Price.ToString() : string.Empty;
    public string PlanetTitleText => _planet != null ? _planet.Name : string.Empty;

    private IPlanet _planet;

    public void ChangePlanet(IPlanet planet)
    {
        _planet = planet;
    }
    public void Initialize()
    {
        _planet.OnPopulationChanged += OnPopulationChanged;
    }

    public void Dispose()
    {
        _planet.OnPopulationChanged -= OnPopulationChanged;
    }

    private void OnPopulationChanged(int value)
    {
        OnStateChanged?.Invoke();
    }

    public void Upgrade()
    {
        if (_planet.CanUpgrade)
        {
            _planet.Upgrade();
            OnStateChanged?.Invoke();
        }
    }

    public bool CanUpgrade() => _planet.CanUpgrade;          
}
