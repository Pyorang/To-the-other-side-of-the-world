using System;
using System.Collections.Generic;
using System.IO;
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
    // Dictionary 대신 List를 사용합니다.
    public List<ProgressData> progressList = new List<ProgressData>();
    // HashSet 대신 List를 사용합니다.
    public List<string> clearedAchievementsList = new List<string>();

    // 이외에 저장하려는 모든 변수들을 여기에 추가합니다.
    public int TotalDayGameAccessed;
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
        TotalDayGameAccessed = 1;
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
            progress.Add(achievement.ID, 0);
        }
    }

    public bool SaveData()
    {
        try
        {
            // 1. 저장용 클래스의 인스턴스를 생성합니다.
            UserAchievementSaveData saveData = new UserAchievementSaveData();

            // 2. 현재 데이터를 저장용 클래스로 변환합니다.
            foreach (var kvp in progress)
            {
                saveData.progressList.Add(new ProgressData { id = kvp.Key, value = kvp.Value });
            }
            saveData.clearedAchievementsList.AddRange(clearedAchievements);

            // 다른 필드들도 옮겨 담습니다.
            saveData.TotalDayGameAccessed = this.TotalDayGameAccessed;
            saveData.MaxStageReached = this.MaxStageReached;
            // ...

            // 3. 변환된 데이터를 JSON으로 저장합니다.
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
                // 1. JSON 파일을 읽어 저장용 클래스로 변환합니다.
                string jsonData = File.ReadAllText(filePath);
                UserAchievementSaveData loadedData = JsonUtility.FromJson<UserAchievementSaveData>(jsonData);

                // 2. 저장용 클래스의 데이터를 원래의 Dictionary와 HashSet으로 복원합니다.
                progress.Clear();
                foreach (var item in loadedData.progressList)
                {
                    progress.Add(item.id, item.value);
                }

                clearedAchievements.Clear();
                foreach (var item in loadedData.clearedAchievementsList)
                {
                    clearedAchievements.Add(item);
                }

                // 다른 필드들도 복원합니다.
                this.TotalDayGameAccessed = loadedData.TotalDayGameAccessed;
                this.MaxStageReached = loadedData.MaxStageReached;
                // ...

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
        // 1. Reflection을 사용하여 relatedVariable 이름과 동일한 멤버 변수를 찾습니다.
        FieldInfo field = GetType().GetField(relatedVariable, BindingFlags.Public | BindingFlags.Instance);

        // 필드가 발견되면 값을 설정합니다.
        if (field != null)
        {
            field.SetValue(this, amount);
        }
        else
        {
            // 필드가 발견되지 않으면 Properties를 확인합니다.
            PropertyInfo property = GetType().GetProperty(relatedVariable, BindingFlags.Public | BindingFlags.Instance);
            if (property != null)
            {
                property.SetValue(this, amount);
            }
            else
            {
                // 관련 변수를 찾을 수 없는 경우 경고 메시지를 출력합니다.
                Debug.LogWarning($"UserAchievementData에 'RelatedVariable' {relatedVariable}와(과) 일치하는 변수가 없습니다.");
                return;
            }
        }

        // 2. 모든 도전 과제를 순회하며 progress를 업데이트합니다.
        foreach (var achievement in DataTableManager.Instance.GetAllAchievementData())
        {
            if (achievement.RelatedVariable == relatedVariable)
            {
                // 해당 업적의 ID에 대해 진행도를 설정합니다.
                if (progress.ContainsKey(achievement.ID))
                {
                    progress[achievement.ID] = amount;
                }
                else
                {
                    // progress 딕셔너리에 해당 ID가 없는 경우, 새로 추가합니다.
                    progress.Add(achievement.ID, amount);
                }
            }
        }
    }
}