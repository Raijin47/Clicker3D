using System;
using Cysharp.Threading.Tasks;
using UnityEngine;
using YG;

namespace ExampleYGDateTime
{
    public abstract class TimeBonusService
    {
        public event Action<int> onCompletedInitBonusTimer;

        protected int _serverTime;
        private int _timerTime;
        private int _deltaTime;

        private double _localTime;

        private string _timeLeft;

        private bool _isActiveTimeData;
        private bool _isActiveTimer;

        private async UniTask<int> GetServerTimeNow()
        {
            await SendRequest();
            return _serverTime;
        }
        protected abstract UniTask SendRequest();

        public void SetTimerData(int id)
        {
            YandexGame.savesData.LastReceiveTime[id] = _timerTime;
            YandexGame.savesData.IsActiveTimer[id] = _isActiveTimer;
        }

        public async UniTaskVoid InitializeTimerReceived(int id)
        {
            _isActiveTimeData = YandexGame.savesData.IsActiveTimer[id];
            if (_isActiveTimeData)
            {
                int serverTimeNow = await GetServerTimeNow();
                _timerTime = YandexGame.savesData.LastReceiveTime[id];
                _localTime = Time.realtimeSinceStartupAsDouble;
                _deltaTime = (int)(serverTimeNow - _localTime);
                _isActiveTimer = true;
            }
            else
            {
                _isActiveTimer = false;
            }

            onCompletedInitBonusTimer?.Invoke(id);
        }

        public bool CheckTimerEnded()
        {
            return Time.realtimeSinceStartupAsDouble + _deltaTime > _timerTime;
        }

        public async UniTask StartTimerReceived(int time)
        {
            int serverTimeNow = await GetServerTimeNow();
            _timerTime = time + serverTimeNow;
            _localTime = Time.realtimeSinceStartupAsDouble;
            _deltaTime = (int)(serverTimeNow - _localTime);
            _isActiveTimeData = true;
            _isActiveTimer = true;
        }

        public bool IsActiveTimer
        {
            get => _isActiveTimer;
            set => _isActiveTimer = value;
        }

        public string TimeLeft()
        {
            double calculateTimeLeft = _timerTime - Time.realtimeSinceStartupAsDouble - _deltaTime;
            int minutes = Mathf.FloorToInt((float)calculateTimeLeft / 60);
            int seconds = Mathf.FloorToInt((float)calculateTimeLeft % 60);
            _timeLeft = $"{minutes:00}:{seconds:00}";
            return _timeLeft;
        }
    }
}