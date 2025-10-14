using UnityEngine;

public class InGameManager : SingletonBehaviour<InGameManager>
{
    public int currentStage = 1;

    [SerializeField] private Floor floor;

    public InGameUIController InGameUIController;

    public float playTimeLimit = 20f;
    private float timeLeft;

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
        AudioManager.Instance.Play(AudioType.BGM, "InGame");

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
        Debug.Log("Game Over");
    }

    public Floor GetFloor()
    {
        return floor;
    }

    public void CheckPlayTime()
    {
        timeLeft -= Time.deltaTime;
        InGameUIController.UpdateTimer(timeLeft);

        if (timeLeft <= 0)
        {
            Debug.Log("Time Over");
            InGameUIController.ShowGameOverUI();
            // 게임 종료 UI 출력 및 게임 조작 불가 기능 구현
        }
            
    }
}
