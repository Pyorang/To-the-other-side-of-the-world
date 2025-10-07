using UnityEngine;

public class CommonBlockDestructStrategy : IDestructStrategy
{
    public void Destruct(Block block)
    {
        if (block.hasDestroyed)
        {
            block._ManagedPool.Release(block);
            GameManager.Instance.CheckCurrentStageClear();
            //UserDataManager.Instance.GetUserData<UserAchievementData>().TotalBlocksDestroyed++;
        }
    }
}
