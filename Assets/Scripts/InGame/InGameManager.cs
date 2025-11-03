using System;
using System.Collections;
using UnityEngine;

public enum GameState
{
    Playing,
    Pause,
    GameOver
}

public class InGameManager : SingletonBehaviour<InGameManager>
{
    public int currentStage = 1;
    public int CharacterPowerLevel = 1;
    public bool HasAutoShield = false;

    private float timeLeft;
    static public readonly float playTimeLimit = 30f;

    public GameState GameState;
    public ISkillStrategy skillStrategy;

    [Space]
    [SerializeField] private Floor floor;
    [SerializeField] private InGameUIController InGameUIController;

    public static Action OnGameStageCleared;

    protected override void Init()
    {
        IsDestroyOnLoad = true;
        base.Init();
    }

    private void Start()
    {
        InGameUIController = FindAnyObjectByType<InGameUIController>();
        if (InGameUIController == null)
        {
            Debug.Log("InGameUIController does not exist.");
            return;
        }

        InGameUIController.init();
        UIManager.Instance.CurrencyUI.SetActive(false);
        EquipCharacterSkill();

        GameState = GameState.Pause;
        timeLeft = playTimeLimit;
    }

    private void Update()
    {
        CheckPlayTime();
    }

    private void OnDestroy()
    {
        UnequipCharacterSkill();
    }

    public void EquipCharacterSkill()
    {
        string CharacterInUse = UserDataManager.Instance.GetUserData<UserCharacterData>().CharacterID_InUse;
        string ChacterNum = CharacterInUse.Substring(CharacterInUse.Length - 1);

        string skillClassName = "Skill_" + ChacterNum + "Strategy";
        Type skillType = Type.GetType(skillClassName);

        if(skillType != null)
        {
            object skillInstance = Activator.CreateInstance(skillType);
            skillStrategy = skillInstance as ISkillStrategy;
        }
    }
    
    public void UnequipCharacterSkill()
    {
        skillStrategy.UnequipCharacterSkill();
    }

    public void UpdateSkillCoolTime(int currentCoolTime, int skillCoolTime)
    {
        InGameUIController.UpdateSkillCoolTimeImage(currentCoolTime, skillCoolTime);
    }

    public void CheckCurrentStageClear()
    {
        floor.ProcessStageClear();
    }

    public void ProcessGameOver()
    {
        UserDataManager.Instance.SaveUserData();
        GameState = GameState.GameOver;
        InGameUIController.ShowGameOverUI();
        Debug.Log("Game Over");
    }

    public Floor GetFloor()
    {
        return floor;
    }

    public void CheckPlayTime()
    {
        if (GameState != GameState.Playing)
            return;
        timeLeft -= Time.deltaTime;
        InGameUIController.UpdateTimerUI(timeLeft);

        if (timeLeft <= 0)
        {
            ProcessGameOver();    
        }
    }

    public void AddBonusTime(float time)
    {
        if (GameState != GameState.Playing) return;
        if (InGameUIController.isPlayingWarningSound)
        {
            AudioManager.Instance.Stop(AudioType.SFX);
            InGameUIController.isPlayingWarningSound = false;
        }

        AudioManager.Instance.Play(AudioType.SFX, "ingame_time_bonus");
        timeLeft += time;
        if(timeLeft > playTimeLimit)
            timeLeft = playTimeLimit;
        InGameUIController.UpdateTimerUI(timeLeft);
    }

    public void DeActivatePassive(string skillName)
    {
        HasAutoShield = false;
        InGameUIController.DeActivateSkillEffect("DeActivate"+skillName);
    }

    public void ActivatePassive(string skillName)
    {
        HasAutoShield = true;
        InGameUIController.ActivateSkillEffect("Activate"+skillName);
    }
}
