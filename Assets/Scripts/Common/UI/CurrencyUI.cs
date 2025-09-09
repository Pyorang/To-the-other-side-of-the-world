using TMPro;
using UnityEngine;

public class CurrencyUI : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _goldAmountText;

    private void OnEnable()
    {
        var data = UserDataManager.Instance.GetUserData<UserCurrencyData>();

        _goldAmountText.text = data.Gold.ToString("N0");
    }
}
