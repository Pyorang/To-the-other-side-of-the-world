using System;
using UnityEngine;

public class UserSettingsData : IUserData
{
    public bool IsSoundEnable { get; set; }
    public void SetDefaultData()
    {
        IsSoundEnable = true;
    }

    public bool LoadData()
    {
        bool result = false;

        try
        {
            IsSoundEnable = (PlayerPrefs.GetInt(nameof(IsSoundEnable)) == 1) ? true : false;

            result = true;
        }
        catch (Exception e)
        {
        }

        return result;
    }

    public bool SaveData()
    {
        bool result = false;
        try
        {
            PlayerPrefs.SetInt(nameof(IsSoundEnable), IsSoundEnable ? 1 : 0);
            PlayerPrefs.Save();

            result = true;
        }
        catch (Exception e)
        {
        }

        return result;
    }
}
