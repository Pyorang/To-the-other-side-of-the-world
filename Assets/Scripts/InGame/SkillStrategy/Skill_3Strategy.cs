using UnityEngine;

public class Skill_3Strategy : ISkillStrategy, IAutoUseSkill
{
    private static readonly int SkillCoolTime = 20;
    public int CurrentCoolTime = 0;

    public Skill_3Strategy()
    {
        InGameManager.OnGameStageCleared += CoolTimeReduce;
        InGameManager.Instance.SetPassiveEnabled(isEnabled:false);
    }

    public void UnequipCharacterSkill()
    {
        InGameManager.OnGameStageCleared -= CoolTimeReduce;
    }

    public void UseSkill()
    {
        MakeAutoShield();
    }

    public void CoolTimeReduce()
    {
        if(!InGameManager.Instance.HasAutoShield)
        {
            if (CurrentCoolTime > 0)
            {
                CurrentCoolTime--;
                InGameManager.Instance.UpdateSkillCoolTime(CurrentCoolTime, SkillCoolTime);
            }

            if (CurrentCoolTime == 0)
            {
                UseSkill();
            }
        }
    }

    public void MakeAutoShield()
    {
        InGameManager.Instance.SetPassiveEnabled(isEnabled:false);
    }

    public void SetCoolTime()
    {
        CurrentCoolTime = SkillCoolTime;
        InGameManager.Instance.UpdateSkillCoolTime(CurrentCoolTime, SkillCoolTime);
    }
}
