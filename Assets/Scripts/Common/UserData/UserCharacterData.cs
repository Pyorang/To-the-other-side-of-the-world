using System;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

[Serializable]
public class CharacterSaveData
{
    public List<string> acquiredCharacters;
    public string characterIdInUse;
}

public class UserCharacterData : IUserData
{
    public HashSet<string> acuiredChatacter = new HashSet<string>();

    public string CharacterID_InUse { get; set; }

    private string savePath;
    private const string filename = "characterData.json";

    public UserCharacterData()
    {
        savePath = Path.Combine(Application.persistentDataPath, filename);
    }

    public void AddAcuiredCharacter(string characterID)
    {
        if (!acuiredChatacter.Contains(characterID))
        {
            acuiredChatacter.Add(characterID);
        }
    }

    public bool LoadData()
    {
        if (!File.Exists(savePath))
        {
            SetDefaultData();
            return false;
        }

        try
        {
            string json = File.ReadAllText(savePath);
            CharacterSaveData data = JsonUtility.FromJson<CharacterSaveData>(json);

            acuiredChatacter.Clear();
            foreach (var id in data.acquiredCharacters)
            {
                acuiredChatacter.Add(id);
            }
            CharacterID_InUse = data.characterIdInUse;
            return true;
        }
        catch (Exception e)
        {
            Debug.LogError("Failed to load character data: " + e.Message);
            return false;
        }
    }

    public bool SaveData()
    {
        try
        {
            CharacterSaveData data = new CharacterSaveData
            {
                acquiredCharacters = new List<string>(acuiredChatacter),
                characterIdInUse = CharacterID_InUse
            };

            string json = JsonUtility.ToJson(data, true);
            File.WriteAllText(savePath, json);
            return true;
        }
        catch (Exception e)
        {
            Debug.LogError("Failed to save character data: " + e.Message);
            return false;
        }
    }

    public void SetDefaultData()
    {
        CharacterID_InUse = "CH_1";
        acuiredChatacter.Add("CH_1");
    }
}
