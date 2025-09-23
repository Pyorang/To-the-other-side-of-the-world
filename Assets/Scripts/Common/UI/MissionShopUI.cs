using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
enum UiType
{
    DM = 0,
    CM = 1,
    Shop = 2
}
public class MissionShopUI : BaseUI
{

    [SerializeField] private List<GameObject> uis = new List<GameObject>();
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
        /*for (int i = 0; i < 4; i++)
        {
            _dailyAchievements[i] = DataTableManager.Instance.GetAchievementData($"DM_{i + 1}");
        }*/

        // 상점에 필요한 캐릭터 데이터 불러오기
        //_characterModel = DataTableManager.Instance.GetAllChracterData();

    }
    public void OnClickShowDailyMissionUI()
    {
        ChangeUI((int)UiType.DM);
    }

    public void OnClickShowChallengeMissionUI()
    {
        ChangeUI((int)UiType.CM);
    }

    public void OnClickShowShopUI()
    {
        ChangeUI((int)UiType.Shop);
    }

    public void OnClickChangeShopPage()
    {
        Debug.Log($"{GetType()}::{nameof(OnClickChangeShopPage)}");
    }

    private void ChangeUI(int UiIndex)
    {
        uis[currentUiIndex].SetActive(false);
        btns[currentUiIndex].GetComponent<Image>().color = new Color(1, 1, 1, 1);
        btns[currentUiIndex].interactable = true;

        uis[UiIndex].SetActive(true);
        btns[UiIndex].GetComponent<Image>().color = new Color(1, 1, 1, 0);
        btns[UiIndex].interactable = false;

        currentUiIndex = UiIndex;
    }

}
