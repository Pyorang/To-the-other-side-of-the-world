using TMPro;
using UnityEngine;

public class CurrencyUI : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _goldAmountText;

    public void Start()
    {
        var data = UserDataManager.Instance.GetUserData<UserCurrencyData>();
        data.GoldChanged.AddListener(UpdateCurrencyText);
        UpdateCurrencyText();
    }

    private void OnEnable()
    {
        UpdateCurrencyText();
    }

    public void UpdateCurrencyText()
    {
        var data = UserDataManager.Instance.GetUserData<UserCurrencyData>();
        _goldAmountText.text = data.Gold.ToString("N0");
    }

    public void OnClickGoldAddBtn()
    {
        var data = new MissionShopUIData()
        {
            isClickGoldAddBtn = true
        };
        
        UIManager.Instance.OpenUI<MissionShopUI>(data);
    }
}
