using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class BuyBtnOfGoldData : BaseUIData 
{
    // 아래 변수는 각 구매 버튼마다 얼마의 골드를 할당할 건지에 대한 데이터
    // 대신 임시로 정해놓은 변수임
    // 원래는 public GameData GameData { get; set; }이런식으로 설정하는게 맞음
    public int GoldAmount { get; set; }
    public Image GoldAmountImage { get; set; }

}


public class BuyBtnOfGold : MonoBehaviour
{
    // 버튼을 통해 구매할 골드의 양을 임시로 이 변수에 저장
    [SerializeField] private int goldAmount;
    [SerializeField] private Image goldAmountImage;
    [SerializeField] private TextMeshProUGUI goldAmountText;
    [SerializeField] private int btnNum;

    private void Awake()
    {
        goldAmountText.text = goldAmount.ToString();
    }

    public void UpdateData()
    {
        // 각 버튼 마다 구매할 골드 양을 개발자가 변경하면 ui에 업데이트 하는 함수
        // GoldAmount = GameData.Instance.GetBuyBtnOfGoldNum(BtnNum);

    }
    public void OnClickBuyBtnOfGold()
    {
        ConfirmBuyUIData data = new ConfirmBuyUIData()
        {
            BuyBtnOfGoldData = new BuyBtnOfGoldData()
            {
                GoldAmount = goldAmount,
                GoldAmountImage = goldAmountImage
            }
        };
        UIManager.Instance.OpenUI<ConfirmBuyUI>(data);
    }
}
