
using Modules.Planets;

public sealed class PlanetPopupShower
{    
    private readonly PlanetPopup _planetPopup;
    private readonly PlanetPopupPresenter _planetPopupPresenter;

    public PlanetPopupShower(PlanetPopup planetPopup, PlanetPopupPresenter planetPopupPresenter)
    {
        _planetPopup = planetPopup;
        _planetPopupPresenter = planetPopupPresenter;
    }

    public void Show(IPlanet planet)
    {
        _planetPopupPresenter.ChangePlanet(planet);
        _planetPopup.gameObject.SetActive(true);
    }
}
