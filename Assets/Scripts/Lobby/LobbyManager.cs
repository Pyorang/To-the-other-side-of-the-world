using NUnit.Framework;
using UnityEngine;

public class LobbyManager : SingletonBehaviour<LobbyManager>
{
    public LobbyUIController LobbyUIController { get; private set; }

    protected override void Init()
    {
        IsDestroyOnLoad = false;
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
        //AudioManager.Instance.Play(AudioType.BGM, "lobby");
    }
}
