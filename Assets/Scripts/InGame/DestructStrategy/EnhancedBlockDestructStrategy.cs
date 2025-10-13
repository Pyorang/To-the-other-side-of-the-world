using UnityEngine;

public class EnhancedBlockDestructStrategy : IDestructStrategy
{
    public void Destruct(Block block)
    {
        block.gameObject.SetActive(false);
        InGameManager.Instance.CheckCurrentStageClear();
        UserDataManager.Instance.GetUserData<UserAchievementData>().IncreaseProgress("TotalBlocksDestroyed", 1);
        UserDataManager.Instance.GetUserData<UserAchievementData>().IncreaseProgress("TotalEnhancedBlocksDestroyed", 1);
    }
}
