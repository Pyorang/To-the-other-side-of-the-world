using UnityEngine;
using UnityEngine.UI;
using System.Collections;

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

        StartCoroutine(ISetChoosedCharAnimAndSize());

        UserDataManager.Instance.GetUserData<UserCharacterData>().ChangeCharAction = SetChoosedCharAnimAndSize;
;

    Debug.Log(UserDataManager.Instance.GetUserData<UserCharacterData>().CharacterID_InUse + "_AnimAct");

    }

    private void Update()
    {
        HandleInput();
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
        AudioManager.Instance.Play(AudioType.SFX, "ui_openUI_button_click");
    }

    public void OnClickMissionShopButton()
    {
        Debug.Log($"{GetType()}::{nameof(OnClickMissionShopButton)}");

        var uiData = new BaseUIData();
        UIManager.Instance.OpenUI<MissionShopUI>(uiData);
        AudioManager.Instance.Play(AudioType.SFX, "ui_openUI_button_click");
    }

    public void OnClickStartButton()
    {
        Debug.Log($"{GetType()}::{nameof(OnClickStartButton)}");
        AudioManager.Instance.Play(AudioType.SFX, "ui_start_button_click");
        AudioManager.Instance.Stop(AudioType.BGM);
        SceneLoader.Instance.LoadScene(ESceneType.InGame);
        
    }

    public void SetChoosedCharAnimAndSize()
    {
        StartCoroutine(ISetChoosedCharAnimAndSize());
    }
    private IEnumerator ISetChoosedCharAnimAndSize()
    {
        string choosedCharID = UserDataManager.Instance.GetUserData<UserCharacterData>().CharacterID_InUse;

        if (choosedCharID == CurrentChoosedCharID) yield break;
        CurrentChoosedCharID = choosedCharID;

        ChoosedCharAnim.Play(choosedCharID);
        yield return null;

        ChoosedCharImage.SetNativeSize();

    }
}
