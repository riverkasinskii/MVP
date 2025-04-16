using Modules.UI;
using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public sealed class PlanetView : MonoBehaviour
{
    public event Action OnButtonClicked;
    public event Action OnButtonHolded;

    [SerializeField] 
    private SmartButton _button;   

    [SerializeField] 
    private Image _lockImage;

    [SerializeField] 
    private Image _iconImage;

    [SerializeField] 
    private Image _coinImage;

    [SerializeField] 
    private Image _incomeImage;

    [SerializeField] 
    private Image _progressBar;

    [SerializeField] 
    private TextMeshProUGUI _timeText;   
    
    [SerializeField] 
    private TextMeshProUGUI _priceText;

    [SerializeField] 
    private GameObject _priceGO;

    private void OnEnable()
    {
        _button.OnClick += OnClick;
        _button.OnHold += OnHold;
    }

    private void OnDisable()
    {
        _button.OnClick -= OnClick;
        _button.OnHold -= OnHold;
    }

    private void OnHold()
    {
        OnButtonHolded?.Invoke();
    }

    private void OnClick()
    {
        OnButtonClicked?.Invoke();        
    }

    public void SetLock(bool state)
    {
        _lockImage.gameObject.SetActive(state);
    }

    public void SetCoin(bool state)
    {
        _coinImage.gameObject.SetActive(state);
    }

    public void SetTime(string time)
    {
        _timeText.text = time;
    }

    public void SetIcon(Sprite sprite)
    {
        _iconImage.sprite = sprite;
    }

    public void SetPriceText(string price)
    {
        _priceText.text = price;
    }

    public void SetPriceState(bool state)
    {
        _priceGO.SetActive(state);
    }

    public void SetIncome(bool state)
    {
        _incomeImage.gameObject.SetActive(state);
    }

    public void SetFillAmountProgressBar(float progress)
    {
        _incomeImage.gameObject.SetActive(true);
        _progressBar.fillAmount = progress;
    }
}
