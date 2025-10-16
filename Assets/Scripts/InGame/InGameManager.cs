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

    [SerializeField] private Floor floor;

    public InGameUIController InGameUIController;

    static public readonly float playTimeLimit = 20f;
    private float timeLeft;

    public GameState GameState;

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
        timeLeft += time;
        InGameUIController.UpdateTimerUI(timeLeft);
    }
}
