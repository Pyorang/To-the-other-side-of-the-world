using UnityEngine;
using UnityEngine.UI;

public class InGameUIController : MonoBehaviour
{
    public Transform CanvasTransform;

    [SerializeField] private Image TimeBarHandleImage;
    public void init()
    {
        string choosedCharID = UserDataManager.Instance.GetUserData<UserCharacterData>().CharacterID_InUse;

        TimeBarHandleImage.sprite = Resources.Load<Sprite>($"Textures/HandleImages/{choosedCharID}");
        TimeBarHandleImage.SetNativeSize();
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
}
