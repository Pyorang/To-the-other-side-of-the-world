using UnityEngine;
using UnityEngine.UI;

public class LobbyUIController : MonoBehaviour
{
    public Transform CanvasTransform;

    [SerializeField] private Animator ChoosedCharAnim;
    private string CurrentChoosedCharID;
    [SerializeField] private Image ChoosedCharImage;
    private bool isSetNativeSize = false;

    public void init()
    {
        UIManager.Instance.CurrencyUI.SetActive(true);

        SetChoosedCharAnim();

        Debug.Log(UserDataManager.Instance.GetUserData<UserCharacterData>().CharacterID_InUse + "_AnimAct");

    }

    private void Update()
    {
        HandleInput();
        SetChoosedCharAnim();
    }

    private void LateUpdate()
    {
        SetNativeSize();
    }

    private void HandleInput()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            //AudioManager.Instance.Play(AudioType.SFX, "ui_button_click");

            var frontUI = UIManager.Instance.GetFrontUI();
            if (frontUI != null)
            {
                frontUI.Close();
            }
            else
            {
                //ShowQuitConfirmUI();
            }
        }
    }

    /*private void ShowQuitConfirmUI()
    {
        var data = new ConfirmUIData()
        {
            ConfirmType = EConfirmType.OK_CANCEL,
            TitleText = "Quit",
            DescriptionText = "Do you want to quit the game?",
            OKButtonText = "Quit",
            CancleButtonText = "Cancle",
            ActionOnClickOKButton = () => Application.Quit()
        };
        UIManager.Instance.OpenUI<ConfirmUI>(data);
    }*/

    public void OnClickSettingsButton()
    {
        Debug.Log($"{GetType()}::{nameof(OnClickSettingsButton)}");

        var uiData = new BaseUIData();
        UIManager.Instance.OpenUI<SettingsUI>(uiData);
    }

    public void OnClickMissionShopButton()
    {
        Debug.Log($"{GetType()}::{nameof(OnClickMissionShopButton)}");

        var uiData = new BaseUIData();
        UIManager.Instance.OpenUI<MissionShopUI>(uiData);
    }

    public void OnClickStartButton()
    {
        Debug.Log($"{GetType()}::{nameof(OnClickStartButton)}");
        SceneLoader.Instance.LoadScene(ESceneType.InGame);
        
    }
    public void SetChoosedCharAnim()
    {
        string choosedCharID = UserDataManager.Instance.GetUserData<UserCharacterData>().CharacterID_InUse;

        if (choosedCharID == CurrentChoosedCharID) return;
        CurrentChoosedCharID = choosedCharID;
        isSetNativeSize = true;

        ChoosedCharAnim.Play(choosedCharID);
        
    }

    public void SetNativeSize()
    {
        if (!isSetNativeSize) return;

        ChoosedCharImage.SetNativeSize();
        isSetNativeSize = false;
    }
}
