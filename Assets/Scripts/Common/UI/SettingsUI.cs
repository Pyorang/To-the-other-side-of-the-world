using NUnit.Framework;
using UnityEngine;
using UnityEngine.UI;

public class SettingsUI : BaseUI
{
    [SerializeField] private Slider _bgmSlider;
    [SerializeField] private Slider _sfxSlider;

    public override void SetData(BaseUIData data)
    {
        base.SetData(data);

        var userSettingsData = UserDataManager.Instance.GetUserData<UserSettingsData>();
        Debug.Assert(userSettingsData != null);
        SetSoundSetting(userSettingsData);
    }

    public void OnBGMValueChanged()
    {
        AudioManager.Instance.SetVolume(AudioType.BGM, _bgmSlider.value);
        UserDataManager.Instance.GetUserData<UserSettingsData>().BGMvalue = _bgmSlider.value;
        UserDataManager.Instance.SaveUserData();
    }

    public void OnSFXValueChanged()
    {
        AudioManager.Instance.SetVolume(AudioType.SFX, _sfxSlider.value);
        UserDataManager.Instance.GetUserData<UserSettingsData>().SFXvalue = _sfxSlider.value;
        UserDataManager.Instance.SaveUserData();
    }

    private void SetSoundSetting(UserSettingsData data)
    {
        _bgmSlider.value = data.BGMvalue;
        _sfxSlider.value = data.SFXvalue;
    }
}
