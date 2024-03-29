using System;
using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.Networking;

namespace ExampleYGDateTime
{
    public class TimeBonusUnityService : TimeBonusService
    {
        private const string SERVER_URI = "https://worldtimeapi.org/api/timezone/Europe/Moscow";

        protected override async UniTask SendRequest()
        {
            try
            {
                UnityWebRequest webRequest = UnityWebRequest.Get(SERVER_URI);
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
                        string json = webRequest.downloadHandler.text;
                        ServerTimeResponse response = JsonUtility.FromJson<ServerTimeResponse>(json);
                        _serverTime = response.unixtime;
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