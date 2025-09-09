using UnityEngine;

public class LobbyUIController : MonoBehaviour
{

    public Transform CanvasTransform;
    public void init()
    {
        //UIManager.Instance.CurrencyUI.SetActive(true);
    }

    private void Update()
    {
        //HandleInput();
    }

    private void HandleInput()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            //AudioManager.Instance.Play(AudioType.SFX, "ui_button_click");

        /*    var frontUI = UIManager.Instance.GetFrontUI();
            if (frontUI != null)
            {
                frontUI.Close();
            }
            else
            {
                ShowQuitConfirmUI();
            }*/
        }
    }

    private void ShowQuitConfirmUI()
    {
        /*var data = new ConfirmUIData()
        {
            ConfirmType = EConfirmType.OK_CANCEL,
            TitleText = "Quit",
            DescriptionText = "Do you want to quit the game?",
            OKButtonText = "Quit",
            CancleButtonText = "Cancle",
            ActionOnClickOKButton = () => Application.Quit()
        };
        UIManager.Instance.OpenUI<ConfirmUI>(data);*/
    }

    public void OnClickSettingsButton()
    {
        Debug.Log($"{GetType()}::{nameof(OnClickSettingsButton)}");

        var uiData = new BaseUIData();
        //UIManager.Instance.OpenUI<SettingsUI>(uiData);
    }

    public void OnClickShopButton()
    {
        Debug.Log($"{GetType()}::{nameof(OnClickShopButton)}");
        /*var ui = Resources.Load<BaseUI>("UI/ShopUI");
        var result = Instantiate(ui);
        result.gameObject.SetActive(true);
        result.transform.SetParent(CanvasTransform);
        result.transform.SetSiblingIndex(CanvasTransform.childCount - 1);
        result.Init(CanvasTransform);*/
        var uiData = new BaseUIData();
        //UIManager.Instance.OpenUI<ShopUI>(uiData);
    }

    public void OnClickStartButton()
    {
        //Logger.Log($"{GetType()}::{nameof(OnClickStartButton)}");
        //SceneLoader.Instance.LoadScene(ESceneType.InGame);

    }
}
