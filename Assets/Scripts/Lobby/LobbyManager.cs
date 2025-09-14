using NUnit.Framework;
using UnityEngine;

public class LobbyManager : SingletonBehaviour<LobbyManager>
{
    public LobbyUIController LobbyUIController { get; private set; }

    protected override void Init()
    {
        IsDestroyOnLoad = true;
        base.Init();
    }

    private void Start()
    {
        LobbyUIController = FindAnyObjectByType<LobbyUIController>();
        if (LobbyUIController == null)
        {
            Debug.Log("LobbyUIController does not exist.");
            return;
        }

        LobbyUIController.init();
        UIManager.Instance.CurrencyUI.SetActive(true);
        //AudioManager.Instance.Play(AudioType.BGM, "lobby");
    }
}
