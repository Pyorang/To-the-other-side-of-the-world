using UnityEngine;

public class ChallengeMissionUI : MonoBehaviour
{
    private const string CHALLENGE_MISSION_ID = "CM_";

    [SerializeField] private GameObject _viewport;
    [SerializeField] private GameObject _missionUIPrefab;

    private void Start()
    {
        foreach(var achievement in DataTableManager.Instance.GetAllAchievementData())
        {
            if(achievement.ID.StartsWith(CHALLENGE_MISSION_ID))
            {
                var missionUIObj = Instantiate(_missionUIPrefab, _viewport.transform);
                var missionUI = missionUIObj.GetComponent<MissionUI>();
                missionUI.SetContents(achievement.ID);
            }
        }
    }
}
