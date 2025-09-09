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

    protected override void Init()
    {
        base.Init();

        // NOTE: 컨테이너들에 GameData들 불러오기
        _achievments = LoadDataFromJson<AchievementModel>("Achievement");
        _characters = LoadDataFromJson<CharacterModel>("Character");
    }

    public AchievementModel GetAchievementData(string id)
    {
        return _achievments.Where(achievement => achievement.ID == id).FirstOrDefault();
    }

    public AchievementModel[] GetAllAchievementData()
    {
        return _achievments;
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
