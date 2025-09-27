using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class MissionUI : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI[] MissonNameText;
    [SerializeField] private TextMeshProUGUI[] MissionDescText;
    [SerializeField] private TextMeshProUGUI RewardAmountText;

    private AchievementModel missionData;
    [SerializeField] private Button RewardButton;

    public void SetContents(string missionID)
    {
        missionData = DataTableManager.Instance.GetAchievementData(missionID);
        SetMissionName(missionData);
        SetMissionDesc(missionData);
        SetRewardAmount(missionData);
        CheckMissionSuccess();
    }

    public void CheckMissionSuccess()
    {
        if(UserDataManager.Instance.GetUserData<UserAchievementData>().clearedAchievements.Contains(missionData.ID))
        {
            RewardButton.interactable = false;
        }
        else
        {
            if (missionData.TargetValue <= UserDataManager.Instance.GetUserData<UserAchievementData>().progress[missionData.ID])
                RewardButton.interactable = true;
            else
                RewardButton.interactable = false;
        }
    }

    public void SetMissionName(AchievementModel missionData)
    {
        foreach (var text in MissonNameText)
        {
            text.text = missionData.Name;
        }
    }

    public void SetMissionDesc(AchievementModel missionData)
    {
        string showProgress = " (" + Math.Min(UserDataManager.Instance.GetUserData<UserAchievementData>().progress[missionData.ID], missionData.TargetValue) + "/" + missionData.TargetValue + ")";

        foreach (var text in MissionDescText)
        {
            text.text = missionData.Description + showProgress;
        }
    }

    public void SetRewardAmount(AchievementModel missionData)
    {
        RewardAmountText.text = missionData.RewardGold.ToString() + " G";
    }

    public void OnClickRewardButton()
    {
        UserDataManager.Instance.GetUserData<UserAchievementData>().clearedAchievements.Add(missionData.ID);
        UserDataManager.Instance.GetUserData<UserCurrencyData>().Gold += missionData.RewardGold;
        RewardButton.interactable = false;
        UserDataManager.Instance.SaveUserData();
    }
}
