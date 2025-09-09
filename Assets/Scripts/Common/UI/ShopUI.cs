using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ShopUIData : BaseUIData
{
    //public UserItemData UserItemData { get; set; }
    public bool IsPossesed { get; set; }
}
public class ShopUI : BaseUI
{
    [SerializeField] private TextMeshProUGUI _menuNameText;
    //[SerializeField] private TextMeshProUGUI[]  _dailyMissionTexts;
    //[SerializeField] private TextMeshProUGUI _dailyMissionProgressText;
    [SerializeField] private GameObject _dailyMissionUI;
    [SerializeField] private GameObject _challengeUI;
    [SerializeField] private GameObject _characterShopUI;
    [SerializeField] private Button _dailyMissionButton;
    [SerializeField] private Button _challengeMissionButton;
    [SerializeField] private Button _characterShopButton;


    public override void SetData(BaseUIData data)
    {
        base.SetData(data);

        // UserDataManager에서 접속날짜와 기록된 날짜를 비교하여 미션 초기화 여부 결정

        //ShopUI, DMUI, CMUI, CSUI 분리해서 스크립트 구성해야겠다.
        //얘네들은 별개의 UI 창이라고 생각하고 하는게 맞는듯
        
    }
    public void OnClickShowDailyMissionUI()
    {
        _dailyMissionUI.SetActive(true);
        _challengeUI.SetActive(false);
        _characterShopUI.SetActive(false);
        _dailyMissionButton.GetComponent<Image>().color = new Color(1, 1, 1, 0);
        _challengeMissionButton.GetComponent<Image>().color = new Color(1, 1, 1, 1);
        _characterShopButton.GetComponent<Image>().color = new Color(1, 1, 1, 1);

    }

    public void OnClickShowChallengeMissionUI()
    {
        _dailyMissionUI.SetActive(false);
        _challengeUI.SetActive(true);
        _characterShopUI.SetActive(false);
        _dailyMissionButton.GetComponent<Image>().color = new Color(1, 1, 1, 1);
        _challengeMissionButton.GetComponent<Image>().color = new Color(1, 1, 1, 0);
        _characterShopButton.GetComponent<Image>().color = new Color(1, 1, 1, 1);
    }

    public void OnClickShowChracterShopUI()
    {
        _dailyMissionUI.SetActive(false);
        _challengeUI.SetActive(false);
        _characterShopUI.SetActive(true);
        _dailyMissionButton.GetComponent<Image>().color = new Color(1, 1, 1, 1);
        _challengeMissionButton.GetComponent<Image>().color = new Color(1, 1, 1, 1);
        _characterShopButton.GetComponent<Image>().color = new Color(1, 1, 1, 0);
    }

    



}
