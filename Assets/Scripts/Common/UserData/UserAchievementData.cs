using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using UnityEngine;

[Serializable]
public class ProgressData
{
    public string id;
    public int value;
}

[Serializable]
public class UserAchievementSaveData
{
    public List<ProgressData> progressList = new List<ProgressData>();
    public List<string> clearedAchievementsList = new List<string>();

    public int TotalDayGameAccessed;
    public int ToatlDailyMissionCleared;
    public bool GotExtraDailyMissionReward;
    public int MaxStageReached;
    public int TotalBlocksDestroyed;
    public int TotalBombBlocksDestroyed;
    public int TotalEnhancedBlocksDestroyed;
    public int TotalCharactersAcquired;
    public int dwarf_200_floor_challenge;
    public int earth_mage_block_conversion;
    public int tamer_shield_consumption;
    public int ancient_dwarf_block_destruction;
    public int low_stage_game_over;
    public int one_tap_stage_clear;
}

public class UserAchievementData : IUserData
{
    // 각 도전과제 ID에 대한 현재 진행도
    public Dictionary<string, int> progress = new Dictionary<string, int>();
    // 이미 클리어한 도전과제 ID 목록
    public HashSet<string> clearedAchievements = new HashSet<string>();


    // NOTE : 업적 달성 여부를 확인하는 누적 데이터
    public int TotalDayGameAccessed { get; set; }
    public int TotalDailyMissionCleared { get; set; }
    public bool GotExtraDailyMissionReward { get; set; }

    public int MaxStageReached { get; set; }


    public int TotalBlocksDestroyed { get; set; }
    public int TotalBombBlocksDestroyed { get; set; }
    public int TotalEnhancedBlocksDestroyed { get; set; }
    
    public int TotalCharactersAcquired { get; set; }

    public int dwarf_200_floor_challenge { get; set; }
    public int earth_mage_block_conversion { get; set; }
    public int tamer_shield_consumption { get; set; }
    public int ancient_dwarf_block_destruction { get; set; }

    public int low_stage_game_over { get; set; }
    public int one_tap_stage_clear { get; set; }

    public void SetDefaultData()
    {
        TotalDayGameAccessed = 0;
        TotalDailyMissionCleared = 0;
        GotExtraDailyMissionReward = false;
        MaxStageReached = 0;
        TotalBlocksDestroyed = 0;
        TotalBombBlocksDestroyed = 0;
        TotalEnhancedBlocksDestroyed = 0;
        TotalCharactersAcquired = 0;
        dwarf_200_floor_challenge = 0;
        earth_mage_block_conversion = 0;
        tamer_shield_consumption = 0;
        ancient_dwarf_block_destruction = 0;
        low_stage_game_over = 0;
        one_tap_stage_clear = 0;

        foreach (var achievement in DataTableManager.Instance.GetAllAchievementData())
        {
            Debug.Log($"저장되는 ID : {achievement.ID}");
            progress.Add(achievement.ID, 0);
        }
    }

    public bool SaveData()
    {
        try
        {
            UserAchievementSaveData saveData = new UserAchievementSaveData();

            foreach (var kvp in progress)
            {
                saveData.progressList.Add(new ProgressData { id = kvp.Key, value = kvp.Value });
            }
            saveData.clearedAchievementsList.AddRange(clearedAchievements);

            saveData.TotalDayGameAccessed = this.TotalDayGameAccessed;
            saveData.ToatlDailyMissionCleared = this.TotalDailyMissionCleared;
            saveData.GotExtraDailyMissionReward = this.GotExtraDailyMissionReward;
            saveData.MaxStageReached = this.MaxStageReached;
            saveData.TotalBlocksDestroyed = this.TotalBlocksDestroyed;
            saveData.TotalBombBlocksDestroyed = this.TotalBombBlocksDestroyed;
            saveData.TotalEnhancedBlocksDestroyed = this.TotalEnhancedBlocksDestroyed;
            saveData.TotalCharactersAcquired = this.TotalCharactersAcquired;
            saveData.dwarf_200_floor_challenge = this.dwarf_200_floor_challenge;
            saveData.earth_mage_block_conversion = this.earth_mage_block_conversion;
            saveData.tamer_shield_consumption = this.tamer_shield_consumption;
            saveData.ancient_dwarf_block_destruction = this.ancient_dwarf_block_destruction;
            saveData.low_stage_game_over = this.low_stage_game_over;
            saveData.one_tap_stage_clear = this.one_tap_stage_clear;

            string jsonData = JsonUtility.ToJson(saveData, true);
            string filePath = Path.Combine(Application.persistentDataPath, "userAchievement.json");
            File.WriteAllText(filePath, jsonData);

            Debug.Log($"업적 데이터 저장 성공: {filePath}");
            return true;
        }
        catch (System.Exception e)
        {
            Debug.LogError($"업적 데이터 저장 실패: {e.Message}");
            return false;
        }
    }

    public bool LoadData()
    {
        string filePath = Path.Combine(Application.persistentDataPath, "userAchievement.json");

        if (File.Exists(filePath))
        {
            try
            {
                string jsonData = File.ReadAllText(filePath);
                UserAchievementSaveData loadedData = JsonUtility.FromJson<UserAchievementSaveData>(jsonData);

                progress.Clear();
                foreach (var item in loadedData.progressList)
                {
                    Debug.Log($"ID : {item.id}, value : {item.value}");
                    progress.Add(item.id, item.value);
                }

                clearedAchievements.Clear();
                foreach (var item in loadedData.clearedAchievementsList)
                {
                    clearedAchievements.Add(item);
                }

                this.TotalDayGameAccessed = loadedData.TotalDayGameAccessed;
                this.TotalDailyMissionCleared = loadedData.ToatlDailyMissionCleared;
                this.GotExtraDailyMissionReward = loadedData.GotExtraDailyMissionReward;
                this.MaxStageReached = loadedData.MaxStageReached;
                this.TotalBlocksDestroyed = loadedData.TotalBlocksDestroyed;
                this.TotalBombBlocksDestroyed = loadedData.TotalBombBlocksDestroyed;
                this.TotalEnhancedBlocksDestroyed = loadedData.TotalEnhancedBlocksDestroyed;
                this.TotalCharactersAcquired = loadedData.TotalCharactersAcquired;
                this.dwarf_200_floor_challenge = loadedData.dwarf_200_floor_challenge;
                this.earth_mage_block_conversion = loadedData.earth_mage_block_conversion;
                this.tamer_shield_consumption = loadedData.tamer_shield_consumption;
                this.ancient_dwarf_block_destruction = loadedData.ancient_dwarf_block_destruction;
                this.low_stage_game_over = loadedData.low_stage_game_over;
                this.one_tap_stage_clear = loadedData.one_tap_stage_clear;

                Debug.Log($"업적 데이터 불러오기 성공: {filePath}");
                return true;
            }
            catch (System.Exception e)
            {
                Debug.LogError($"업적 데이터 불러오기 실패: {e.Message}");
                SetDefaultData();
                return false;
            }
        }
        else
        {
            Debug.Log("저장된 파일이 없습니다. 기본 데이터로 시작합니다.");
            SetDefaultData();
            return false;
        }
    }

    public void IncreaseProgress(string relatedVariable, int amount)
    {
        PropertyInfo property = GetType().GetProperty(relatedVariable, BindingFlags.Public | BindingFlags.Instance);
        if (property != null)
        {
            property.SetValue(this, (int)property.GetValue(this) + amount);
        }

        foreach (var achievement in DataTableManager.Instance.GetAllAchievementData())
        {
            if (achievement.RelatedVariable == relatedVariable)
            {
                if (progress.ContainsKey(achievement.ID))
                {
                    progress[achievement.ID] += amount;
                }
                else
                {
                    progress.Add(achievement.ID, amount);
                }
            }
        }

        SaveData();
    }
}