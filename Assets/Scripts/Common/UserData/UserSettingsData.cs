using System;
using UnityEngine;

public class UserSettingsData : IUserData
{
    public float BGMvalue { get; set; }
    public float SFXvalue { get; set; }

    public void SetDefaultData()
    {
        BGMvalue = 0.5f;
        SFXvalue = 0.5f;
    }

    public bool LoadData()
    {
        bool result = false;

        try
        {
            BGMvalue = (PlayerPrefs.GetFloat(nameof(BGMvalue)));
            SFXvalue = (PlayerPrefs.GetFloat(nameof(SFXvalue)));

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
            PlayerPrefs.SetFloat(nameof(BGMvalue), AudioManager.Instance.GetVolume(AudioType.BGM));
            PlayerPrefs.SetFloat(nameof(SFXvalue), AudioManager.Instance.GetVolume(AudioType.SFX));
            PlayerPrefs.Save();

            result = true;
        }
        catch (Exception e)
        {
        }

        return result;
    }
}
