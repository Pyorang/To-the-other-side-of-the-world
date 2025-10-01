using UnityEngine;
using System;
using System.Data;

public class UserAccountData : IUserData
{

    public int accessDay { get; set; }
    public int accessMonth {get; set; } 
    public int accessYear { get; set; }   

    public bool LoadData()
    {
        bool result = false;

        accessDay = PlayerPrefs.GetInt(nameof(accessDay));
        accessMonth = PlayerPrefs.GetInt(nameof(accessMonth));
        accessYear = PlayerPrefs.GetInt(nameof(accessYear));

        if (accessDay == 0 || accessMonth == 0 || accessYear == 0)
        {
            Debug.Log("접속 기록이 없습니다.");
        }
        else
        {
            Debug.Log($"현재 접속 시간 : {DateTime.Now.Year} : {DateTime.Now.Month} : {DateTime.Now.Day}");
            Debug.Log($"지난 접속 시간 : {accessYear} : {accessMonth} : {accessDay}");

            CheckAccessDays();

            result = true;
        }

        return result;
    }

    public bool SaveData()
    {
        bool result = false;
        try
        {
            PlayerPrefs.SetInt(nameof(accessDay), accessDay);
            PlayerPrefs.SetInt(nameof(accessMonth), accessMonth);
            PlayerPrefs.SetInt(nameof(accessYear), accessYear);
            PlayerPrefs.Save();

            result = true;

            Debug.Log("접속 시간 저장 완료");
        }
        catch (Exception e)
        {
        }

        return result;
    }

    public void SetDefaultData()
    {
        accessDay = DateTime.Now.Day;
        accessMonth = DateTime.Now.Month;
        accessYear = DateTime.Now.Year;

        Debug.Log("초기 접속");

        UserDataManager.Instance.GetUserData<UserAchievementData>().ResetDailyMissionData();
        SaveData();
    }

    

    public void CheckAccessDays()
    {
        DateTime currentTime = new DateTime(accessYear, accessMonth, accessDay);
        if (currentTime.Date < DateTime.Now.Date)
        {
            accessDay = DateTime.Now.Day;
            accessMonth = DateTime.Now.Month;
            accessYear = DateTime.Now.Year;

            UserDataManager.Instance.GetUserData<UserAchievementData>().ResetDailyMissionData();
            SaveData();

            Debug.Log("접속일이 변경되어 초기화됩니다.");
        }

    }

}
