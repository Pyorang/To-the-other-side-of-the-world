using UnityEngine;

public class Skill_4Strategy : ISkillStrategy
{
    public Skill_4Strategy()
    {
        UseSkill();
    }

    public void UseSkill()
    {
        InGameManager.Instance.CharacterPowerLevel *= 2;
    }
}
