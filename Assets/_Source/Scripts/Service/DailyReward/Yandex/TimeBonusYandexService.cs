using System;
using System.Globalization;
using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.Networking;

namespace ExampleYGDateTime
{
    public class TimeBonusYandexService : TimeBonusService
    {
        protected override async UniTask SendRequest()
        {
            try
            {
                using UnityWebRequest webRequest = UnityWebRequest.Head(Application.absoluteURL);
                webRequest.SetRequestHeader("cache-control", "no-cache");
                await webRequest.SendWebRequest();

                switch (webRequest.result)
                {
                    case UnityWebRequest.Result.ConnectionError:
                        break;
                    case UnityWebRequest.Result.DataProcessingError:
                        break;
                    case UnityWebRequest.Result.ProtocolError:
                        break;
                    case UnityWebRequest.Result.Success:
                        string dateString = webRequest.GetResponseHeader("date");
                        DateTimeOffset date = DateTimeOffset.ParseExact(dateString, "ddd, dd MMM yyyy HH:mm:ss 'GMT'",
                            CultureInfo.InvariantCulture,
                            DateTimeStyles.AssumeUniversal);
                        _serverTime = (int)date.ToUnixTimeSeconds();
                        break;
                }
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}