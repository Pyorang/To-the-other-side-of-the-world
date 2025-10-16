using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using TMPro;

public class InGameUIController : MonoBehaviour
{
    public Transform CanvasTransform;

    [SerializeField] private Image TimeBarHandleImage;
    [SerializeField] private Slider timerBar;
    [SerializeField] private GameObject gameOverUI;

    [Header("Start Cutscene Object")]
    [SerializeField] private GameObject InGameUI;
    [SerializeField] private GameObject StartCutsceneUI;
    [SerializeField] private TextMeshProUGUI countText;
    [SerializeField] private Animator FadeInUpAnim;
    [SerializeField] private Animator FadeInDownAnim;

    private readonly int maxCount = 3;

    private bool isPlayingWarningSound = false;
    public void init()
    {
        string choosedCharID = UserDataManager.Instance.GetUserData<UserCharacterData>().CharacterID_InUse;
        TimeBarHandleImage.sprite = Resources.Load<Sprite>($"Textures/HandleImages/{choosedCharID}");
        TimeBarHandleImage.SetNativeSize();
        gameOverUI.SetActive(false);

        StartCoroutine(StartCutScene());
    }

    public void OnClickPauseBtn()
    {
        var uiData = new BaseUIData();
        UIManager.Instance.OpenUI<PauseUI>(uiData);
        AudioManager.Instance.Play(AudioType.SFX, "ui_openUI_button_click");
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

    public IEnumerator StartCutScene()
    {
        StartCutsceneUI.SetActive(true);
        InGameUI.SetActive(false);

        countText.color = Color.blue;

        for (int count = maxCount; count > 1; count--)
        {
            countText.text = count.ToString();
            countText.GetComponent<Animator>().Play("Count");
            AudioManager.Instance.Play(AudioType.SFX, "ui_count");
            yield return new WaitForSeconds(1f);
        }

        countText.text = "1";
        countText.color = Color.red;
        FadeInUpAnim.Play("FadeIn", -1, 0f);
        FadeInDownAnim.Play("FadeIn", -1, 0f);
        AudioManager.Instance.Play(AudioType.SFX, "ui_count");

        yield return new WaitForSeconds(1f);

        countText.text = "Start!";
        AudioManager.Instance.Play(AudioType.SFX, "ui_count");

        yield return new WaitForSeconds(0.5f);

        InGameManager.Instance.GameState = GameState.Playing;

        StartCutsceneUI.SetActive(false);
        InGameUI.SetActive(true);
        AudioManager.Instance.Play(AudioType.BGM, "InGame");

    }
}
