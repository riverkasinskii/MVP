using System;
using UnityEngine;

public interface IPlanetPopupPresenter
{
    public event Action OnStateChanged;
    Sprite AvatarPlanet { get; }
    string PlanetPopulationText { get; }
    string PlanetLevelText { get; }
    string PlanetIncomeText { get; }
    string PlanetPriceText { get; }
    string PlanetTitleText { get; }

    void Upgrade();
    bool CanUpgrade();
    void Initialize();
    void Dispose();
}