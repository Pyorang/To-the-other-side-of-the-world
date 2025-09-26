using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ConfirmBuyUIData : BaseUIData
{
    // 원래는 GameData에서 골드 정보를 불러오는게 맞는데, 임시로 BuyBtnOfGOldData에서 불러옴

    public BuyBtnOfGoldData BuyBtnOfGoldData { get; set; }

    public BuyBtnOfCharData BuyBtnOfCharData { get; set; }

}


public class ConfirmBuyUI : BaseUI
{
    [SerializeField] private TextMeshProUGUI ConfirmText;

    [SerializeField] private GameObject _confirmBuyUI;
    [SerializeField] private GameObject _failBuyUI;

    private ConfirmBuyUIData ConfirmBuyUIData;
    private BuyBtnOfGoldData BuyBtnOfGoldData = null;
    private BuyBtnOfCharData BuyBtnOfCharData = null;
    public override void SetData(BaseUIData data)
    {
        base.SetData(data);

        _confirmBuyUI.SetActive(true);
        _failBuyUI.SetActive(false);

        ConfirmBuyUIData = data as ConfirmBuyUIData;

        if (ConfirmBuyUIData.BuyBtnOfGoldData != null)
        {
            BuyBtnOfGoldData = ConfirmBuyUIData.BuyBtnOfGoldData;
            ConfirmText.text = BuyBtnOfGoldData.GoldAmountText + " 골드를 구매하시겠습니까?";
        }
        else if (ConfirmBuyUIData.BuyBtnOfCharData != null)
        {
            BuyBtnOfCharData = ConfirmBuyUIData.BuyBtnOfCharData;
            ConfirmText.text = $"<color=red>{BuyBtnOfCharData.CharacterModel.Price.ToString()}</color>" + " 골드로 \n"
                + BuyBtnOfCharData.CharacterModel.Name + " 캐릭터를 구매하시겠습니까?";
        }

    }

    public void OnClickBuyBtn()
    {

        ConfirmBuyUIData data = ConfirmBuyUIData;

        if (ConfirmBuyUIData.BuyBtnOfGoldData != null)
        {
            if (ConfirmPayment())
            {
                UIManager.Instance.OpenUI<GetItemUI>(data);

                ////////////////////////////////
                // 골드 획득 함수 실행
                ////////////////////////////////
                
                OnClickCloseButton();
            }
            else
            {
                _confirmBuyUI.SetActive(false);
                _failBuyUI.SetActive(true);
            }
        }
        else
        {
            if (ConfirmUserCurrencyData())
            {
                // 구매 성공이므로, 캐릭터 보유 정보 변경
                UserCharacterData userCharacterData = new UserCharacterData();
                userCharacterData.AddAcuiredCharacter(ConfirmBuyUIData.BuyBtnOfCharData.CharacterModel.ID);
                
                /////////////////////////////////////////////
                // 구매 성공이므로, 골드 감소 함수 실행
                /////////////////////////////////////////////

                UIManager.Instance.OpenUI<GetItemUI>(data);
                OnClickCloseButton();

            }
            else
            {
                _confirmBuyUI.SetActive(false);
                _failBuyUI.SetActive(true);
            }
        }

        /*if (ConfirmUserCurrencyData())
        {
            // 구매 성공이므로, 골드 감소 ( 함수 작성 필요 )
            UIManager.Instance.OpenUI<GetItemUI>(data);
            OnClickCloseButton();
        }
        else
        {
            _confirmBuyUI.SetActive(false);
            _failBuyUI.SetActive(true);
        }*/
        
    }

    private bool ConfirmPayment()
    {
        // 결제가 성공하면 true, 실패하면 false 반환
        return true;
    }

    private bool ConfirmUserCurrencyData()
    {
        // 구매 작업 처리 코드
        // 자원이 있으면 아이템을 획득하고, 없으면 구매 실패 메시지 출력


        UserCurrencyData UserCurrencyData = UserDataManager.Instance.GetUserData<UserCurrencyData>();
        /*if (UserCurrencyData == null || ConfirmBuyUIData == null || ConfirmBuyUIData.BuyBtnOfGoldData == null)
        {
            Debug.LogError("UserCurrencyData or ConfirmBuyUIData or BuyBtnOfGoldData가 없음");
            return false;
        }*/
        //return UserCurrencyData.Gold >= ConfirmBuyUIData.BuyBtnOfGoldData.GoldAmount;
        return UserCurrencyData.Gold >= BuyBtnOfCharData.CharacterModel.Price;
    }
}
