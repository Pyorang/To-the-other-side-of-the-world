using UnityEngine;

public class GameManager : SingletonBehaviour<GameManager>
{
    public int currentStage = 1;

    [SerializeField] private Floor floor;

    protected override void Init()
    {
        IsDestroyOnLoad = true;
        base.Init();
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
