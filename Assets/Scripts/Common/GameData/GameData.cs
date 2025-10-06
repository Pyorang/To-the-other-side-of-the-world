using UnityEngine;
using System;

#region Achievment
[Serializable]
public class AchievementModel
{
    public string ID;
    public string Name;
    public string Description;
    public int TargetValue;
    public int RewardGold;
    public string RelatedVariable;
}
#endregion

#region Character
[Serializable]
public class CharacterModel
{
    public string ID;
    public string Name;
    public string SkillName;
    public string SkillDescription;
    public bool IsActive;
    public int Price;
}
#endregion

#region BlockProbability
[Serializable]
public class BlockProbabailtyModel
{
    public int StartStage;
    public int EndStage;
    public int CommonBlock;
    public int ExplosionBlock;
    public int EnhancedBlock;
    public int GasBlock;
}
#endregion