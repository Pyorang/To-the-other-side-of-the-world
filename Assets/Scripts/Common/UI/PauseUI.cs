using UnityEngine;

public class PauseUI : SettingsUI
{
    [SerializeField] private GameObject DictUI;

    private void OnEnable()
    {
        DictUI.SetActive(false);
    }

    public void OnClickExitBtn()
    {
        Debug.Log("게임 플레이를 종료합니다.");
        var frontUI = UIManager.Instance.GetFrontUI();
        if (frontUI != null) frontUI.Close();
        SceneLoader.Instance.LoadSceneAsync(ESceneType.Lobby);
    }

    public void OnClickReStartBtn()
    {
        Debug.Log("게임을 재시작합니다.");
        var frontUI = UIManager.Instance.GetFrontUI();
        if (frontUI != null) frontUI.Close();
        SceneLoader.Instance.ReloadScene();
    }

    public void OnClickOpenDict()
    {
        DictUI.SetActive(true);
    }

    public void OnClickCloseDict()
    {
        AudioManager.Instance.Play(AudioType.SFX, "ui_closeUi_button_click");
        DictUI.SetActive(false);
    }

    public override void OnClickCloseButton()
    {
        //////////////////////////////////
        ///// Game Pause Unfreeze Code ////////
        //////////////////////////////////
        base.OnClickCloseButton();
    }
}
