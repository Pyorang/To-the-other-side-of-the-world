using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class DailyMissionUI : MonoBehaviour
{
    private static readonly int MAX_DAILY_MISSION_COUNT = 3;
    private static readonly string DAILY_MISSION_ID = "DM_";

    [Header("Daily Mission Clear Reward Gold Amount")]
    [Space]
    public int RewardGold = 100;

    [Space]
    [SerializeField] private MissionUI[] _missionUIPrefab;

    [Header("Daily Mission Progress Bar")]
    [Space]
    [SerializeField] private Image _progressBar;

    [Header("Daily Mission Clear Reward Button")]
    [Space]
    [SerializeField] private Button _rewardButton;
    [SerializeField] private TextMeshProUGUI _rewardButtonText;

    [Header("Daily Mission Clear Text")]
    [Space]
    [SerializeField] private TextMeshProUGUI _clearText;

    public void Start()
    {
        _rewardButtonText.text = $"{RewardGold} G";
    }

    public void OnClickMissionClear()
    {
        if (UserDataManager.Instance.GetUserData<UserAchievementData>().TotalDailyMissionCleared < MAX_DAILY_MISSION_COUNT)
        {
            UserDataManager.Instance.GetUserData<UserAchievementData>().TotalDailyMissionCleared++;
            UserDataManager.Instance.SaveUserData();
        }
        UpdateUI();
    }

    public void ResetDailyMissionClearCount()
    {
        UserDataManager.Instance.GetUserData<UserAchievementData>().TotalDailyMissionCleared = 0;
        UpdateUI();
        UserDataManager.Instance.GetUserData<UserAchievementData>().GotExtraDailyMissionReward = false;
        _rewardButton.interactable = false;
    }

    public void UpdateUI()
    {
        if (UserDataManager.Instance.GetUserData<UserAchievementData>().TotalDailyMissionCleared == MAX_DAILY_MISSION_COUNT && !UserDataManager.Instance.GetUserData<UserAchievementData>().GotExtraDailyMissionReward)
            _rewardButton.interactable = true;
        _progressBar.fillAmount = (float)UserDataManager.Instance.GetUserData<UserAchievementData>().TotalDailyMissionCleared / MAX_DAILY_MISSION_COUNT;
        _clearText.text = $"일일과제 현황 ({UserDataManager.Instance.GetUserData<UserAchievementData>().TotalDailyMissionCleared} / {MAX_DAILY_MISSION_COUNT})";
    }

    public void OnClickRewardButton()
    {
        UserDataManager.Instance.GetUserData<UserCurrencyData>().Gold += RewardGold;
        UserDataManager.Instance.GetUserData<UserAchievementData>().GotExtraDailyMissionReward = true;
        _rewardButton.interactable = false;
        UserDataManager.Instance.SaveUserData();
    }

    private void OnEnable()
    {
        for(int i = 1; i<= _missionUIPrefab.Length; i++)
        {
            _missionUIPrefab[i-1].SetContents(DAILY_MISSION_ID + i.ToString());
        }

        UpdateUI();
    }
}
