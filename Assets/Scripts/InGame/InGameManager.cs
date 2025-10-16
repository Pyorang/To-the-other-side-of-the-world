using UnityEngine;

public class InGameManager : SingletonBehaviour<InGameManager>
{
    public int currentStage = 1;

    [SerializeField] private Floor floor;

    public InGameUIController InGameUIController;

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
}
