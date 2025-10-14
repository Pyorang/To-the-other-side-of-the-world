using UnityEngine;
using UnityEngine.UI;

public class InGameUIController : MonoBehaviour
{
    public Transform CanvasTransform;

    [SerializeField] private Image TimeBarHandleImage;
    [SerializeField] private Slider timerBar;
    [SerializeField] private GameObject gameOverUI;
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

    public void UpdateTimer(float timeLeft)
    {
        timerBar.value = timeLeft / InGameManager.Instance.playTimeLimit;
    }

    public void ShowGameOverUI()
    {
        gameOverUI.SetActive(true);
    }


}
