using System;
using UnityEngine;
using UnityEngine.Events;

public class UserCurrencyData : IUserData
{
    public UnityEvent GoldChanged = new UnityEvent();
    
    private long _gold;
    public long Gold
    {
        get => _gold;
        set
        {
            _gold = value;
            GoldChanged.Invoke();
        }
    }

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
