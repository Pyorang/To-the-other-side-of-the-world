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
        accessDay = PlayerPrefs.GetInt(nameof(accessDay));
        accessMonth = PlayerPrefs.GetInt(nameof(accessMonth));
        accessYear = PlayerPrefs.GetInt(nameof(accessYear));

        try
        {
            // date 값 오류 체크용
            var date = new DateTime(accessYear, accessMonth, accessDay);

            Debug.Log($"현재 접속 날짜 : {DateTime.Now.Year} : {DateTime.Now.Month} : {DateTime.Now.Day}");
            Debug.Log($"지난 접속 날짜 : {accessYear} : {accessMonth} : {accessDay}");

            CheckAccessDays();
            return true;
        }
        catch
        {
            Debug.Log("접속 기록이 없습니다.");
            UserDataManager.Instance.GetUserData<UserAchievementData>().TotalDayGameAccessed = 0;
            SetDefaultData();
            return false;
        }
    }

    public bool SaveData()
    {
        try
        {
            PlayerPrefs.SetInt(nameof(accessDay), accessDay);
            PlayerPrefs.SetInt(nameof(accessMonth), accessMonth);
            PlayerPrefs.SetInt(nameof(accessYear), accessYear);
            PlayerPrefs.Save();

            Debug.Log("접속 시간 저장 완료");
            return true;
        }
        catch (Exception e)
        {
            Debug.Log("접속 시간 저장 실패..");
            return false;
        }
    }

    public void SetDefaultData()
    {
        accessDay = DateTime.Now.Day;
        accessMonth = DateTime.Now.Month;
        accessYear = DateTime.Now.Year;

        Debug.Log("초기 접속");

        UserDataManager.Instance.GetUserData<UserAchievementData>().IncreaseProgress("TotalDayGameAccessed", 1);
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
            UserDataManager.Instance.GetUserData<UserAchievementData>().IncreaseProgress("TotalDayGameAccessed", 1);
            SaveData();

            Debug.Log("접속일이 변경되어 초기화됩니다.");
        }

    }

}
