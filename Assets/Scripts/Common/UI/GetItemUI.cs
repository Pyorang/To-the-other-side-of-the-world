using UnityEngine;
using UnityEngine.UI;

public class GetItemUI : BaseUI
{
    [SerializeField] private Image ItemImage;

    public override void SetData(BaseUIData data)
    {
        base.SetData(data);

        ConfirmBuyUIData confirmBuyUIData = data as ConfirmBuyUIData;
        BuyBtnOfGoldData buyBtnOfGoldData = confirmBuyUIData.BuyBtnOfGoldData;
        if (buyBtnOfGoldData != null && buyBtnOfGoldData.GoldAmountImage != null)
            ItemImage.sprite = buyBtnOfGoldData.GoldAmountImage.sprite;
    }
}
