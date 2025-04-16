using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

public sealed class PlanetPopup : MonoBehaviour
{
    [SerializeField] 
    private Button _upgradeButton;

    [SerializeField]
    private Button _closeButton;

    [SerializeField]
    private Image _avatarPlanet;

    [SerializeField]
    private TextMeshProUGUI _planetPopulationText;

    [SerializeField]
    private TextMeshProUGUI _planetLevelText;

    [SerializeField]
    private TextMeshProUGUI _planetIncomeText;

    [SerializeField]
    private TextMeshProUGUI _planetPriceText;

    [SerializeField]
    private TextMeshProUGUI _planetTitleText;

    [Inject]
    private readonly IPlanetPopupPresenter _planetPopupPresenter;

    private void OnEnable()
    {
        _planetPopupPresenter.OnStateChanged += OnStateChanged;
        _planetPopupPresenter.Initialize();
        _upgradeButton.onClick.AddListener(_planetPopupPresenter.Upgrade);
        _closeButton.onClick.AddListener(HidePopup);
        Render();
    }        

    private void OnDisable()
    {
        _planetPopupPresenter.OnStateChanged -= OnStateChanged;
        _planetPopupPresenter.Dispose();
        _upgradeButton.onClick.RemoveListener(_planetPopupPresenter.Upgrade);
        _closeButton.onClick.RemoveListener(HidePopup);
    }

    private void OnStateChanged() => Render();

    private void Render()
    {
        _avatarPlanet.sprite = _planetPopupPresenter.AvatarPlanet;
        _planetPopulationText.text = _planetPopupPresenter.PlanetPopulationText;
        _planetLevelText.text = _planetPopupPresenter.PlanetLevelText;
        _planetIncomeText.text = _planetPopupPresenter.PlanetIncomeText;
        _planetPriceText.text = _planetPopupPresenter.PlanetPriceText;
        _planetTitleText.text = _planetPopupPresenter.PlanetTitleText;
        _upgradeButton.interactable = _planetPopupPresenter.CanUpgrade();
    }

    private void HidePopup()
    {
        gameObject.SetActive(false);
    }
}
