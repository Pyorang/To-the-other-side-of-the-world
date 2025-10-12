using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

public class MissionUI : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI MissionNameText;
    [SerializeField] private TextMeshProUGUI MissionDescText;
    [SerializeField] private TextMeshProUGUI RewardAmountText;
    [SerializeField] private GameObject GetRewardMarker;

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
        if (!UserDataManager.Instance.GetUserData<UserAchievementData>().progress.ContainsKey(missionData.ID))
        {
            Debug.Log($"{missionData.ID} 데이터 없음");
            return;
        }

        if (UserDataManager.Instance.GetUserData<UserAchievementData>().clearedAchievements.Contains(missionData.ID))
        {
            GetRewardMarker.SetActive(true);
            RewardButton.interactable = false;
        }
        else
        {
            GetRewardMarker.SetActive(false);

            if (missionData.TargetValue <= UserDataManager.Instance.GetUserData<UserAchievementData>().progress[missionData.ID])
                RewardButton.interactable = true;
            else
                RewardButton.interactable = false;
        }
    }

    public void SetMissionName(AchievementModel missionData)
    {
        MissionNameText.text = missionData.Name;
        /*foreach (var text in MissonNameText)
        {
            text.text = missionData.Name;
        }*/
    }

    public void SetMissionDesc(AchievementModel missionData)
    {
        if (!UserDataManager.Instance.GetUserData<UserAchievementData>().progress.ContainsKey(missionData.ID))
        {
            Debug.Log($"{missionData.ID} 데이터 없음");
            return;
        }

        string showProgress = " (" + Math.Min(UserDataManager.Instance.GetUserData<UserAchievementData>().progress[missionData.ID], missionData.TargetValue) + "/" + missionData.TargetValue + ")";

        MissionDescText.text = missionData.Description + showProgress;
        /*foreach (var text in MissionDescText)
        {
            text.text = missionData.Description + showProgress;
        }*/
    }

    public void SetRewardAmount(AchievementModel missionData)
    {
        RewardAmountText.text = missionData.RewardGold.ToString() + " G";
    }

    public void OnClickRewardButton()
    {
        AudioManager.Instance.Play(AudioType.SFX, "ui_button_click");

        UserDataManager.Instance.GetUserData<UserAchievementData>().clearedAchievements.Add(missionData.ID);
        UserDataManager.Instance.GetUserData<UserCurrencyData>().Gold += missionData.RewardGold;
        RewardButton.interactable = false;
        GetRewardMarker.SetActive(true);
        UserDataManager.Instance.SaveUserData();
    }
}
