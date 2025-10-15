using UnityEngine;
using UnityEngine.UI;

public class InGameUIController : MonoBehaviour
{
    public Transform CanvasTransform;

    [SerializeField] private Image TimeBarHandleImage;
    [SerializeField] private Slider timerBar;
    [SerializeField] private GameObject gameOverUI;

    private bool isPlayingWarningSound = false;
    public void init()
    {
        string choosedCharID = UserDataManager.Instance.GetUserData<UserCharacterData>().CharacterID_InUse;

        TimeBarHandleImage.sprite = Resources.Load<Sprite>($"Textures/HandleImages/{choosedCharID}");
        TimeBarHandleImage.SetNativeSize();

        gameOverUI.SetActive(false);
    }

    public void OnClickPauseBtn()
    {
        var uiData = new BaseUIData();
        UIManager.Instance.OpenUI<PauseUI>(uiData);
        AudioManager.Instance.Play(AudioType.SFX, "ui_openUI_button_click");

        //////////////////////////////////
        /// Game Pause Code /////////////
        /// ////////////////////////////
    }

    public void UpdateTimerUI(float timeLeft)
    {
        float value = timeLeft / InGameManager.playTimeLimit;
        timerBar.value = value;

        if (value <= 0.2f && isPlayingWarningSound == false)
        {
            AudioManager.Instance.Play(AudioType.SFX, "ui_time_warning");
            isPlayingWarningSound = true;
        }

        if (isPlayingWarningSound && value > 0.2f)
        {
            AudioManager.Instance.Stop(AudioType.SFX);
            isPlayingWarningSound = false;
        }
            

        TimeBarHandleImage.GetComponent<Animator>().SetFloat("sliderValue", timerBar.value);
    }

    public void ShowGameOverUI()
    {
        AudioManager.Instance.Stop(AudioType.SFX);
        gameOverUI.SetActive(true);
    }


}
