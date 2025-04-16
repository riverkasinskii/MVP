using TMPro;
using UnityEngine;
using UnityEngine.UI;

public sealed class MoneyView : MonoBehaviour
{    
    [SerializeField] private TextMeshProUGUI _moneyCount;
    [SerializeField] private Image _icon;

    public Vector2 IconPosition => _icon.transform.position;

    public void SetMoney(string count) 
        => _moneyCount.text = count;
}
