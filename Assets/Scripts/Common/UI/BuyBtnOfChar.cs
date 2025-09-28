using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;

public class BuyBtnOfCharData : BaseUIData
{
    public CharacterModel CharacterModel;
}


public class BuyBtnOfChar : MonoBehaviour
{
    
    public string Char_Id;
    [SerializeField] private TextMeshProUGUI BtnType;

    public void OnClickBuyBtnOfChar()
    {
        if ( BtnType.text == "보유중")
        {
            // 이미 보유중인 캐릭터를 눌렀을 경우 캐릭터 선택 진행
            UserCharacterData userCharacterData = UserDataManager.Instance.GetUserData<UserCharacterData>();
            userCharacterData.CharacterID_InUse = Char_Id;

            UserDataManager.Instance.GetUserData<UserCharacterData>().SoldOut?.Invoke();
            UserDataManager.Instance.GetUserData<UserCharacterData>().ChangeCharAction?.Invoke();

            UserDataManager.Instance.SaveUserData();


        }
        else
        {
            // 보유중이 아니므로 캐릭터 구매 진행
            ConfirmBuyUIData data = new ConfirmBuyUIData()
            {
                BuyBtnOfCharData = new BuyBtnOfCharData()
                {
                    CharacterModel = DataTableManager.Instance.GetCharacterData(Char_Id)
                }
            };

            UIManager.Instance.OpenUI<ConfirmBuyUI>(data);
        }
        
    }

}
