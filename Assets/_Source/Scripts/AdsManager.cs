using TMPro;
using UnityEngine;
using YG;

public class AdsManager : MonoBehaviour
{
	[SerializeField] private Customization _customization;
	[SerializeField] private RebithManager _rebithManager;
	[SerializeField] private OfflineReward _offlineReward;
	[SerializeField] private BonusBase[] _timeBonuses;
	[SerializeField] private TextMeshProUGUI _adsBonusText;
	[SerializeField] private double _adsModifier;

	private void OnEnable() => YandexGame.RewardVideoEvent += Rewarded;
	private void OnDisable() => YandexGame.RewardVideoEvent -= Rewarded;

    private int GetAdsModifier()
    {
		return YandexGame.savesData.AdsLevel;
    }

    private void SetAdsModifier(int level)
    {
		YandexGame.savesData.AdsLevel = level;
		Modifier.ADsBoost = YandexGame.savesData.AdsLevel * _adsModifier;

		_adsBonusText.text = ConvertNumber.Convert(YandexGame.savesData.AdsLevel * _adsModifier) + "%";
    }

    public void Init()
    {
		for (int i = 0; i < _timeBonuses.Length; i++)
			_timeBonuses[i].Init();

        SetAdsModifier(YandexGame.savesData.AdsLevel);
	}

    private void Rewarded(int id)
	{
		switch (id)
		{
			case <21: _customization.UnlockColor(id); break;
            case 21: _timeBonuses[0].Reward(); break;
            case 22: _timeBonuses[1].Reward(); break;
            case 23: _timeBonuses[2].Reward(); break;
            case 24: _timeBonuses[3].Reward(); break;
            case 25: _rebithManager.AdditionalReward(); break;
			case 26: _offlineReward.GetReward(true); break;
		}

        SetAdsModifier(GetAdsModifier() + 1);
	}

	public void OpenRewardAd(int id)
	{
		YandexGame.RewVideoShow(id);
	}

    #region OnValidate
#if UNITY_EDITOR
    private void OnValidate()
    {
		_customization ??= GetComponent<Customization>();
		_rebithManager ??= GetComponent<RebithManager>();
	}
#endif
    #endregion
}