using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using UnityEngine;

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
            // 전체 데이터를 JSON 문자열로 변환합니다.
            string jsonData = JsonUtility.ToJson(this, true); // 두 번째 인자는 읽기 좋게 들여쓰기(pretty print)를 해줍니다.

            // 저장 경로를 설정합니다.
            string filePath = Path.Combine(Application.persistentDataPath, "userAchievement.json");

            // 파일에 JSON 데이터를 씁니다.
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
                // 파일에서 JSON 문자열을 읽어옵니다.
                string jsonData = File.ReadAllText(filePath);

                // JSON 데이터를 이 클래스의 객체로 변환합니다.
                JsonUtility.FromJsonOverwrite(jsonData, this);

                Debug.Log($"업적 데이터 불러오기 성공: {filePath}");
                return true;
            }
            catch (System.Exception e)
            {
                Debug.LogError($"업적 데이터 불러오기 실패: {e.Message}");
                SetDefaultData(); // 실패 시 기본 데이터로 초기화합니다.
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

}