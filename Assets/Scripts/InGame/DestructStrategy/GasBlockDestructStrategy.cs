using UnityEngine;

public class GasBlockDestructStrategy : IDestructStrategy
{
    public void Destruct(Block block)
    {
        block.gameObject.SetActive(false);
        InGameManager.Instance.ProcessGameOver();
        UserDataManager.Instance.GetUserData<UserAchievementData>().IncreaseProgress("TotalBlocksDestroyed", 1);
    }
}
