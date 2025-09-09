using System;
using UnityEngine;

public class UserCurrencyData : IUserData
{
    public long Gold { get; set; }

    public void SetDefaultData()
    {
        Gold = 0L;
    }

    public bool LoadData()
    {
        bool result = false;
        try
        {
            Gold = long.Parse(PlayerPrefs.GetString(nameof(Gold)));
            result = true;
        }
        catch (Exception e)
        {
            Debug.LogError(e);
        }
        return result;
    }

    public bool SaveData()
    {
        bool result = false;
        try
        {
            PlayerPrefs.SetString(nameof(Gold), Gold.ToString());
            PlayerPrefs.Save();
            result = true;
        }
        catch (Exception e)
        {
            Debug.LogError(e);
        }
        return result;
    }
}
