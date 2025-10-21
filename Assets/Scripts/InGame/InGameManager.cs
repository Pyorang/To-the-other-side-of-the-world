using System;
using UnityEngine;

public enum GameState
{
    Playing,
    Pause,
    GameOver
}

public class InGameManager : SingletonBehaviour<InGameManager>
{
    public int currentStage = 1;

    private float timeLeft;
    static public readonly float playTimeLimit = 30f;

    public GameState GameState;

    [Space]
    [SerializeField] private Floor floor;
    [SerializeField] private InGameUIController InGameUIController;

    public static Action OnGameStageCleared;

    protected override void Init()
    {
        IsDestroyOnLoad = true;
        base.Init();
    }

    private void Start()
    {
        InGameUIController = FindAnyObjectByType<InGameUIController>();
        if (InGameUIController == null)
        {
            Debug.Log("InGameUIController does not exist.");
            return;
        }

        InGameUIController.init();
        UIManager.Instance.CurrencyUI.SetActive(false);

        GameState = GameState.Pause;
        timeLeft = playTimeLimit;

    }

    private void Update()
    {
        CheckPlayTime();
    }

    public void CheckCurrentStageClear()
    {
        floor.ProcessStageClear();
    }

    public void ProcessGameOver()
    {
        UserDataManager.Instance.SaveUserData();
        GameState = GameState.GameOver;
        InGameUIController.ShowGameOverUI();
        Debug.Log("Game Over");
    }

    public Floor GetFloor()
    {
        return floor;
    }

    public void CheckPlayTime()
    {
        if (GameState != GameState.Playing)
            return;
        timeLeft -= Time.deltaTime;
        InGameUIController.UpdateTimerUI(timeLeft);

        if (timeLeft <= 0)
        {
            ProcessGameOver();    
        }
            
    }

    public void AddBonusTime(float time)
    {
        if (GameState != GameState.Playing) return;
        if (InGameUIController.isPlayingWarningSound)
        {
            AudioManager.Instance.Stop(AudioType.SFX);
            InGameUIController.isPlayingWarningSound = false;
        }

        AudioManager.Instance.Play(AudioType.SFX, "ingame_time_bonus");
        timeLeft += time;
        if(timeLeft > playTimeLimit)
            timeLeft = playTimeLimit;
        InGameUIController.UpdateTimerUI(timeLeft);
    }
}
