using System;
using UnityEngine;
using UnityEngine.UI;

public class GetItemUI : BaseUI
{
    [SerializeField] private Image ItemImage;

    public override void SetData(BaseUIData data)
    {
        base.SetData(data);

        ConfirmBuyUIData confirmBuyUIData = data as ConfirmBuyUIData;

        if (confirmBuyUIData.BuyBtnOfCharData == null && confirmBuyUIData.BuyBtnOfGoldData == null)
        {
            Debug.Log("ConfirmBuyUIData is Null");
        }
        else if (confirmBuyUIData.BuyBtnOfCharData != null && confirmBuyUIData.BuyBtnOfGoldData != null)
        {
            Debug.Log("ConfirmBuyUIData에 두 값이 동시에 들어 있음");
        }
        else if (confirmBuyUIData.BuyBtnOfGoldData != null)
        {
            BuyBtnOfGoldData buyBtnOfGoldData = confirmBuyUIData.BuyBtnOfGoldData;
            if (buyBtnOfGoldData.GoldAmountImage != null) ItemImage.sprite = buyBtnOfGoldData.GoldAmountImage.sprite;
        }
        else
        {
            BuyBtnOfCharData buyBtnOfCharData = confirmBuyUIData.BuyBtnOfCharData;
            if (buyBtnOfCharData.CharacterModel.ID != null) ItemImage.sprite = Resources.Load<Sprite>($"Textures/{buyBtnOfCharData.CharacterModel.ID}");
        }
   
    }
}
