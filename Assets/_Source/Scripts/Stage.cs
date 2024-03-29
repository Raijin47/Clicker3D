using UnityEngine;
using YG;
using Assets.SimpleLocalization;

public class Stage : MonoBehaviour
{
    [SerializeField] private LocalizedDynamic _textStage;
    [SerializeField] private Health _health;

    private int _currentStage;
    private const int SkipStageValue = 2;
    private const string Leaderboard_Name = "Best";

    public int CurrentStage
    {
        get => _currentStage;
        set
        {
            if (Random.value < Modifier.ChanceSkipStage) _currentStage = value + SkipStageValue;
            else _currentStage = value;

            _textStage.SetValue(_currentStage.ToString());
            GlobalEvent.SendChangeStage();
            YandexGame.savesData.CurrentStage = _currentStage;

            if(_currentStage > YandexGame.savesData.RecordStage)
            {
                YandexGame.savesData.RecordStage = _currentStage;
                YandexGame.SaveProgress();
                YandexGame.NewLeaderboardScores(Leaderboard_Name, YandexGame.savesData.RecordStage);
            }
        }
    }
    public void Init()
    {
        CurrentStage = YandexGame.savesData.CurrentStage;
        GlobalEvent.OnRebith.AddListener(OnReset);
    }

    private void OnReset()
    {
        CurrentStage = (int)(CurrentStage * Modifier.ReductionLostDays);
        _health.OnReset();
    }
}