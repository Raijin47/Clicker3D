using System;
using Cysharp.Threading.Tasks;
using UnityEngine;
using YG;

namespace ExampleYGDateTime
{
    public abstract class DailyRewardService
    {
        public event Action onCompletedGetDateTime;

        protected int _serverTime;
        private int _timerTime;
        private int _deltaDateTime;

        private string _dateTime;

        private bool _isDailyLogin;

        public bool IsDailyLogin => _isDailyLogin;

        protected abstract UniTask SendRequest();

        private async UniTask<int> GetServerTimeNow()
        {
            await SendRequest();
            return _serverTime;
        }

        public async UniTaskVoid GetDateTimeServerYandex()
        {
            await GetServerTimeNow();
            int serverTimeNow = await GetServerTimeNow();
            double localDateTime = Time.realtimeSinceStartupAsDouble;
            _deltaDateTime = (int)(serverTimeNow - localDateTime);

            onCompletedGetDateTime?.Invoke();
        }

        public string DateTime
        {
            get
            {
                UpdateDateTime();
                return _dateTime;
            }
        }

        private void UpdateDateTime()
        {
            double calculateTime = _deltaDateTime + Time.realtimeSinceStartupAsDouble;
            DateTimeOffset dateTimeOffset = DateTimeOffset.FromUnixTimeSeconds((int)calculateTime);
            _dateTime = dateTimeOffset.ToString("HH:mm:ss");
        }

        public async UniTask SetDate()
        {
            _timerTime = await GetServerTimeNow();
            YandexGame.savesData.LastLoginDay = _timerTime;
        }

        public bool IsNewDay()
        {
            double currentTime = _deltaDateTime + Time.realtimeSinceStartupAsDouble;

            DateTimeOffset saveTimeOffset = DateTimeOffset.FromUnixTimeSeconds(YandexGame.savesData.LastLoginDay);
            DateTimeOffset currentTimeOffset = DateTimeOffset.FromUnixTimeSeconds((int)currentTime);

            DateTime day1 = new DateTime(saveTimeOffset.Year, saveTimeOffset.Month, saveTimeOffset.Day);
            DateTime day2 = new DateTime(currentTimeOffset.Year, currentTimeOffset.Month, currentTimeOffset.Day);

            TimeSpan span = day2.Subtract(day1);

            _isDailyLogin = span.Days == 1;

            if (span.Days > 0) return true;
            else return false;
        }
    }
}