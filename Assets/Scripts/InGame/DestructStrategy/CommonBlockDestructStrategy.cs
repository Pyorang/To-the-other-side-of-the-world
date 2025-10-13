using UnityEngine;

public class CommonBlockDestructStrategy : IDestructStrategy
{
    public void Destruct(Block block)
    {

        block.gameObject.SetActive(false);
        InGameManager.Instance.CheckCurrentStageClear();
        UserDataManager.Instance.GetUserData<UserAchievementData>().IncreaseProgress("TotalBlocksDestroyed", 1);
    }
}
