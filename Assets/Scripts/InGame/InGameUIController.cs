using UnityEngine;

public class InGameUIController : MonoBehaviour
{
    public Transform CanvasTransform;

    public void init()
    {

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
