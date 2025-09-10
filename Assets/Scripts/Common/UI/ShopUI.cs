using System.Linq;
using System.Runtime.CompilerServices;
using NUnit.Framework;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
using Unity.VisualScripting;

public class ShopUIData : BaseUIData
{
    //public UserItemData UserItemData { get; set; }
    public bool IsPossesed { get; set; }
}
public class ShopUI : BaseUI
{
    [SerializeField] private GameObject _dailyMissionUI;
    [SerializeField] private GameObject _challengeUI;
    [SerializeField] private GameObject _characterShopUI;
    [SerializeField] private Button _dailyMissionButton;
    [SerializeField] private Button _challengeMissionButton;
    [SerializeField] private Button _characterShopButton;

    [SerializeField] private List<GameObject> uis= new List<GameObject>();
    [SerializeField] private List<Button> btns = new List<Button>();

    private AchievementModel[] _dailyAchievements;
    private CharacterModel[] _characterModel;

    private int currentUiIndex = 0;

    public override void SetData(BaseUIData data)
    {
        base.SetData(data);

        // UserDataManager에서 접속날짜와 기록된 날짜를 비교하여 미션 초기화 여부 결정

        //ShopUI, DMUI, CMUI, CSUI 분리해서 스크립트 구성해야겠다.
        //얘네들은 별개의 UI 창이라고 생각하고 하는게 맞는듯

        // DM 불러오기
        /*for (int i=0; i<4; i++)
        {
            _dailyAchievements[i] = DataTableManager.Instance.GetAchievementData($"DM_{i + 1}");
        }*/

        // 상점에 필요한 캐릭터 데이터 불러오기
        //_characterModel = DataTableManager.Instance.GetAllChracterData();


    }
    public void OnClickShowDailyMissionUI()
    {
        ChangeUI((int)UiType.DM);
        /*_dailyMissionUI.SetActive(true);
        _challengeUI.SetActive(false);
        _characterShopUI.SetActive(false);
        _dailyMissionButton.GetComponent<Image>().color = new Color(1, 1, 1, 0);
        _challengeMissionButton.GetComponent<Image>().color = new Color(1, 1, 1, 1);
        _characterShopButton.GetComponent<Image>().color = new Color(1, 1, 1, 1);*/

    }

    public void OnClickShowChallengeMissionUI()
    {
        ChangeUI((int)UiType.CM);
        /*_dailyMissionUI.SetActive(false);
        _challengeUI.SetActive(true);
        _characterShopUI.SetActive(false);
        _dailyMissionButton.GetComponent<Image>().color = new Color(1, 1, 1, 1);
        _challengeMissionButton.GetComponent<Image>().color = new Color(1, 1, 1, 0);
        _characterShopButton.GetComponent<Image>().color = new Color(1, 1, 1, 1);*/
    }

    public void OnClickShowShopUI()
    {
        ChangeUI((int)UiType.Shop);
        /*_dailyMissionUI.SetActive(false);
        _challengeUI.SetActive(false);
        _characterShopUI.SetActive(true);
        _dailyMissionButton.GetComponent<Image>().color = new Color(1, 1, 1, 1);
        _challengeMissionButton.GetComponent<Image>().color = new Color(1, 1, 1, 1);
        _characterShopButton.GetComponent<Image>().color = new Color(1, 1, 1, 0);*/


    }

    public void OnClickChangeShopPage()
    {
        Debug.Log($"{GetType()}::{nameof(OnClickChangeShopPage)}");

    }

    private void ChangeUI(int UiIndex)
    {
        Debug.Log($"curentUiIndex : {currentUiIndex}, UiIndex : {UiIndex}");
        uis[currentUiIndex].SetActive(false);
        btns[currentUiIndex].GetComponent<Image>().color = new Color(1, 1, 1, 1);

        uis[UiIndex].SetActive(true);
        btns[UiIndex].GetComponent<Image>().color = new Color(1, 1, 1, 0);

        currentUiIndex = UiIndex;
    }



}
