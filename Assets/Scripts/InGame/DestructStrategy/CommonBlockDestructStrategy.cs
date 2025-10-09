using UnityEngine;

public class CommonBlockDestructStrategy : IDestructStrategy
{
    public void Destruct(Block block)
    {

        block.gameObject.SetActive(false);
        GameManager.Instance.CheckCurrentStageClear();
        UserDataManager.Instance.GetUserData<UserAchievementData>().IncreaseProgress("TotalBlocksDestroyed", 1);
    }
}
