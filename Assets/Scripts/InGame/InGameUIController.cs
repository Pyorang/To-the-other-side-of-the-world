using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using TMPro;

public class InGameUIController : MonoBehaviour
{
    public Transform CanvasTransform;

    [Header("Timer")]
    [Space]
    [SerializeField] private Image TimeBarHandleImage;
    [SerializeField] private Slider timerBar;

    [Header("Stage Text")]
    [Space]
    [SerializeField] private TextMeshProUGUI stageText;

    [Header("Character Skill")]
    [Space]
    [SerializeField] private Button skillButton;
    [SerializeField] private Image skillCoolDownImage;

    [SerializeField] private GameObject _skillAnimObj;
    [SerializeField] private GameObject _skillIConEffect;

    [Header("Start Cutscene Object")]
    [Space]
    [SerializeField] private GameObject InGameUI;
    [SerializeField] private GameObject StartCutsceneUI;
    [SerializeField] private TextMeshProUGUI countText;
    [SerializeField] private Animator FadeInUpAnim;
    [SerializeField] private Animator FadeInDownAnim;

    [Header("GameOverUI")]
    [Space]
    [SerializeField] private GameObject gameOverUI;
    [SerializeField] private TextMeshProUGUI gameResultText;

    private readonly int maxCount = 3;
    public bool isPlayingWarningSound = false;

    public void init()
    {
        string choosedCharID = UserDataManager.Instance.GetUserData<UserCharacterData>().CharacterID_InUse;
        TimeBarHandleImage.sprite = Resources.Load<Sprite>($"Textures/HandleImages/{choosedCharID}");
        TimeBarHandleImage.SetNativeSize();

        UpdateStageText();
        SetSkillButtonActive();
        InGameManager.OnGameStageCleared += UpdateStageText;

        _skillAnimObj.SetActive(false);
        gameOverUI.SetActive(false);

        StartCoroutine(StartCutScene());
    }

    public void SetSkillButtonActive()
    {
        skillButton.image.sprite = Resources.Load<Sprite>($"Textures/SkillIcons/{UserDataManager.Instance.GetUserData<UserCharacterData>().CharacterID_InUse}");
        skillCoolDownImage.sprite = Resources.Load<Sprite>($"Textures/SkillIcons/{UserDataManager.Instance.GetUserData<UserCharacterData>().CharacterID_InUse}");

        if (UserDataManager.Instance.GetUserData<UserCharacterData>().IsSkillUnlockedInUseChar() == true)
            skillButton.interactable = true;

        else
            skillButton.interactable = false;
    }

    public void OnDestroy()
    {
        InGameManager.OnGameStageCleared -= UpdateStageText;
    }

    public void UpdateStageText()
    {
        stageText.text = $"{InGameManager.Instance.currentStage}";
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

    public void UpdateSkillCoolTimeImage(int currentCoolTime, int skillCoolTime)
    {
        skillCoolDownImage.fillAmount = (float)currentCoolTime / skillCoolTime;

        if(currentCoolTime == 0)
        {
            skillButton.interactable = true;
            _skillIConEffect.SetActive(true);
        }
        else
        {
            skillButton.interactable = false;
            _skillIConEffect.SetActive(false);
        }
    }

    public void ShowGameOverUI()
    {
        AudioManager.Instance.Stop(AudioType.SFX);
        gameResultText.text = $"결과 : {InGameManager.Instance.currentStage}층";
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

    public void OnClickSkillButton()
    {
        InGameManager.Instance.skillStrategy.UseSkill();
        ShowSkillEffect(isEnabled: true);
    }

    public IEnumerator ShowSkillAnimation()
    {
        string choosedCharID = UserDataManager.Instance.GetUserData<UserCharacterData>().CharacterID_InUse;
        Debug.Log("현재 캐릭터 ID : " + choosedCharID);
        _skillAnimObj.SetActive(true);

        Animator anim = _skillAnimObj.GetComponent<Animator>();
        anim.Play(choosedCharID+"_Skill_Anim");
        AudioManager.Instance.Play(AudioType.SFX, choosedCharID + "_Skill_SFX");
        while (anim.GetCurrentAnimatorStateInfo(0).normalizedTime < 1f)
        {
            yield return null;
        }
        //float animDuration = anim.GetCurrentAnimatorStateInfo(0).length;


        //yield return new WaitForSeconds(animDuration);
        _skillAnimObj.SetActive(false);
    }

    public void ShowSkillEffect(bool isEnabled)
    {
        _skillIConEffect.SetActive(!isEnabled);

        if (isEnabled == true)
            StartCoroutine(ShowSkillAnimation());
    }
}
