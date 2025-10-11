using System.IO;
using System.Linq;
using UnityEngine;

public class DataTableManager : SingletonBehaviour<DataTableManager>
{
    [SerializeField]
    class Wrapper<T>
    {
        public T[] data;
    }

    // NOTE : GameData를 담아올 수 있는 컨테이너들 추가
    private AchievementModel[] _achievments;
    private CharacterModel[] _characters;
    private BlockProbabailtyModel[] _blockProbabailtyModels;
    private BlockInfoModel[] _blockInfos;

    protected override void Init()
    {
        base.Init();

        // NOTE: 컨테이너들에 GameData들 불러오기
        _achievments = LoadDataFromJson<AchievementModel>("Achievement");
        _characters = LoadDataFromJson<CharacterModel>("Character");
        _blockProbabailtyModels = LoadDataFromJson<BlockProbabailtyModel>("BlockProbabaility");
        _blockInfos = LoadDataFromJson<BlockInfoModel>("BlockInfo");
    }

    public AchievementModel GetAchievementData(string id)
    {
        return _achievments.Where(achievement => achievement.ID == id).FirstOrDefault();
    }

    public AchievementModel[] GetAllAchievementData()
    {
        return _achievments;
    }

    public CharacterModel GetCharacterData(string id)
    {
        return _characters.Where(character => character.ID == id).FirstOrDefault();
    }
    public CharacterModel[] GetAllCharacterModelData()
    {
        return _characters;
    }

    public BlockInfoModel GetBlockInfoData(string id)
    {
        return _blockInfos.Where(block => block.ID == id).FirstOrDefault();
    }
    public BlockInfoModel[] GetAllBlockInfoData()
    {
        return _blockInfos;
    }

    public BlockType GetRandomBlock(int floorNumber)
    {
        for(int i = 0; i< _blockProbabailtyModels.Length; i++)
        {
            if (_blockProbabailtyModels[i].StartStage <= floorNumber && floorNumber <= _blockProbabailtyModels[i].EndStage)
            {
                int randomNumber = Random.Range(1, 101);

                if (randomNumber - _blockProbabailtyModels[i].CommonBlock <= 0)
                    return BlockType.CommonBlock;
                else if (randomNumber - _blockProbabailtyModels[i].CommonBlock - _blockProbabailtyModels[i]. ExplosionBlock <= 0)
                    return BlockType.ExplosionBlock;
                else if (randomNumber - _blockProbabailtyModels[i].CommonBlock - _blockProbabailtyModels[i].ExplosionBlock - _blockProbabailtyModels[i].EnhancedBlock <= 0)
                    return BlockType.EnhancedBlock;
                else
                    return BlockType.GasBlock;
            }
        }

        return BlockType.CommonBlock;   
    }

    private const string DATA_PATH = "DataTable";
    private T[] LoadDataFromJson<T>(string filename)
    {
        var path = Path.Combine(DATA_PATH, filename);
        var json = Resources.Load<TextAsset>(path);
        var wrapper = JsonUtility.FromJson<Wrapper<T>>(json.text);
        return wrapper.data;
    }
}
