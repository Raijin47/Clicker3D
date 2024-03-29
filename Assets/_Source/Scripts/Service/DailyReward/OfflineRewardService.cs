using System;
using Cysharp.Threading.Tasks;
using UnityEngine;
using YG;

namespace ExampleYGDateTime
{
    public abstract class OfflineRewardService
    {
        public event Action onCompletedInitOfflineTimer;

        private double _localTime;

        protected int _serverTime;
        private int _timerTime;
        private int _deltaTime;
        private int _offlineTime;
        public int OfflineTime => _offlineTime;

        private async UniTask<int> GetServerTimeNow()
        {
            await SendRequest();
            return _serverTime;
        }

        protected abstract UniTask SendRequest();

        public async UniTask SaveOfflineTime()
        {
            _timerTime = await GetServerTimeNow();
            YandexGame.savesData.LastLoginTime = _timerTime;
            YandexGame.SaveProgress();
        }

        public async UniTaskVoid InitializeOfflineTimer()
        {
            if (!YandexGame.savesData.isFirstSession)
            {
                int serverTimeNow = await GetServerTimeNow();
                _timerTime = YandexGame.savesData.LastLoginTime;
                _localTime = Time.realtimeSinceStartupAsDouble;
                _deltaTime = (int)(serverTimeNow - _localTime);

                double calculateTimeLeft = _deltaTime - _timerTime;
                int minutes = Mathf.FloorToInt((float)calculateTimeLeft / 60);
                int seconds = Mathf.FloorToInt((float)calculateTimeLeft % 60);

                _offlineTime = (minutes * 60 + seconds);
            }

            onCompletedInitOfflineTimer?.Invoke();
        }
    }
}