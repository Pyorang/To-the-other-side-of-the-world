using UnityEngine;

public class ExplosionBlockDestructStrategy : IDestructStrategy
{
    public void Destruct(Block block)
    {
        block.gameObject.SetActive(false);

        foreach (var nearBlock in InGameManager.Instance.GetFloor().GetNearBlocks(block))
        {
            if(nearBlock != null && nearBlock.gameObject.activeSelf != false)
            {
                nearBlock.blockDurability--;
                if (nearBlock.blockDurability <= 0)
                {
                    nearBlock.GetDestructStrategy().Destruct(nearBlock);
                }
            }
        }

        UserDataManager.Instance.GetUserData<UserAchievementData>().IncreaseProgress("TotalBlocksDestroyed", 1);
        UserDataManager.Instance.GetUserData<UserAchievementData>().IncreaseProgress("TotalExplosionBlocksDestroyed", 1);
    }
}
