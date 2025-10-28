using System;
using UnityEngine;

public class Skill_2Strategy : ISkillStrategy
{
    private static readonly int SkillCoolTime = 20;
    public int CurrentCoolTime = 0;

    public Skill_2Strategy()
    {
        InGameManager.OnGameStageCleared += CoolTimeReduce;
    }

    public void UnequipCharacterSkill()
    {
        InGameManager.OnGameStageCleared -= CoolTimeReduce;
    }

    public void UseSkill()
    {
        InGameManager.Instance.GetFloor().ChangeAllBlocksToCommonBlock();
        CurrentCoolTime = SkillCoolTime;
        InGameManager.Instance.UpdateSkillCoolTime(CurrentCoolTime, SkillCoolTime);
    }

    public void CoolTimeReduce()
    {
        if(CurrentCoolTime > 0)
        {
            CurrentCoolTime--;
            InGameManager.Instance.UpdateSkillCoolTime(CurrentCoolTime, SkillCoolTime);
        }
    }
}
