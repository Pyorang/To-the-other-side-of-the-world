using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
enum UiType
{
    DM = 0,
    CM = 1,
    Shop = 2
}

public class MissionShopUIData : BaseUIData
{
    public bool isClickGoldAddBtn = false;
}

public class MissionShopUI : BaseUI
{

    [SerializeField] private List<GameObject> uis = new List<GameObject>();
    [SerializeField] private List<Button> btns = new List<Button>();

    private AchievementModel[] _dailyAchievements;
    private CharacterModel[] _characterModel;

    [SerializeField] private ShopUI ShopUI;

    private int currentUiIndex = 0;

    public override void SetData(BaseUIData data)
    {
        base.SetData(data);

        MissionShopUIData missionShopUIData = data as MissionShopUIData;
        if (missionShopUIData != null && missionShopUIData.isClickGoldAddBtn)
        {
            OnClickShowShopUI();
            ShopUI.ShowGoldPage();
        }
            

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
