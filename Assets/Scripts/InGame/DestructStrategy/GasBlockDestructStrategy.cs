using UnityEngine;

public class GasBlockDestructStrategy : IDestructStrategy
{
    public void Destruct(Block block)
    {
        block.gameObject.SetActive(false);
        if (InGameManager.Instance.HasAutoShield)
        {
            // 실드 깨지는 애니메이션 추가
            InGameManager.Instance.SetPassiveEnabled(isEnabled:true);
            if (InGameManager.Instance.skillStrategy is IAutoUseSkill autoUseSkillStrategy)
            {
                autoUseSkillStrategy.SetCoolTime();
            }
            return;
        }
        else
        {
            InGameManager.Instance.ProcessGameOver();
        }
        UserDataManager.Instance.GetUserData<UserAchievementData>().IncreaseProgress("TotalBlocksDestroyed", 1);
    }
}
