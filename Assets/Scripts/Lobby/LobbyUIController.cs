using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using TMPro;

public class LobbyUIController : MonoBehaviour
{
    public Transform CanvasTransform;

    [SerializeField] private Animator ChoosedCharAnim;
    private string CurrentChoosedCharID;
    [SerializeField] private Image ChoosedCharImage;
    private bool isSetNativeSize = false;

    [Header("Loading Objects")]
    [SerializeField] private Transform Fade;
    [SerializeField] private GameObject Loading;
    [SerializeField] private Slider _progressBar;
    [SerializeField] private TextMeshProUGUI _progressBarText;
    [SerializeField] private float FadeSpeed = 1f;


    public void init()
    {
        UIManager.Instance.CurrencyUI.SetActive(true);

        StartCoroutine(ISetChoosedCharAnimAndSize());

        UserDataManager.Instance.GetUserData<UserCharacterData>().ChangeCharAction = SetChoosedCharAnimAndSize;
;

        Debug.Log(UserDataManager.Instance.GetUserData<UserCharacterData>().CharacterID_InUse + "_AnimAct");

        Loading.SetActive(false);
        Fade.localScale = new Vector3(1, 0, 1);
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

        StartCoroutine(LoadingSequence());
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

    public IEnumerator LoadingSequence()
    {
        UIManager.Instance.CurrencyUI.SetActive(false);
        var loadingOperation = SceneLoader.Instance.LoadSceneAsync(ESceneType.InGame);
        if (loadingOperation == null)
        {
            yield break;
        }
        loadingOperation.allowSceneActivation = false;

        float value = Fade.localScale.y;
        while (value <= 1)
        {
            value += Time.deltaTime * FadeSpeed;
            Fade.localScale = new Vector3(1, value, 1);
            yield return null;
        }

        Loading.SetActive(true);

        _progressBar.value = 0.5f;
        _progressBarText.text = $"{(int)(_progressBar.value * 100.0f)}%";
        yield return new WaitForSeconds(0.5f);

        while (true)
        {
            if (loadingOperation.isDone)
                break;

            _progressBar.value = loadingOperation.progress;
            _progressBarText.text = $"{(int)(_progressBar.value * 100.0f)}%";

            if (_progressBar.value >= 0.9f)
            {
                loadingOperation.allowSceneActivation = true;
            }

            yield return null;
        }


    }
}
