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
    public int SkillName;
    public int SkillDiscription;
    public bool IsActive;
    public int Price;
}
#endregion