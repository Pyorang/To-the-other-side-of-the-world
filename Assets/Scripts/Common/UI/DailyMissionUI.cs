using UnityEngine;

public class DailyMissionUI : MonoBehaviour
{
    private const string DAILY_MISSION_ID = "DM_";
    [SerializeField] private MissionUI[] _missionUIPrefab;

    private void OnEnable()
    {
        for(int i = 1; i<= _missionUIPrefab.Length; i++)
        {
            _missionUIPrefab[i-1].SetContents(DAILY_MISSION_ID + i.ToString());
        }
    }
}
