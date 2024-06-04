using TMPro;
using UnityEngine;
using UnityEngine.UI;
using YG;

public class AdsManager : MonoBehaviour
{
	[SerializeField] private Customization _customization;
	[SerializeField] private RebithManager _rebithManager;
	[SerializeField] private OfflineReward _offlineReward;
	[SerializeField] private BonusBase[] _timeBonuses;
	[SerializeField] private TextMeshProUGUI _adsBonusText;
	[SerializeField] private Image _fillImage;
	[SerializeField] private double _adsModifier;

	private readonly double _increasePercent = 1.5;
	private readonly double _increaseEveryLevel = 10;

	private void OnEnable() => YandexGame.RewardVideoEvent += Rewarded;
	private void OnDisable() => YandexGame.RewardVideoEvent -= Rewarded;

    private int GetAdsModifier()
    {
		return YandexGame.savesData.AdsLevel;
    }

    private void SetAdsModifier(int level)
    {
		YandexGame.savesData.AdsLevel = level;
		Modifier.ADsBoost = YandexGame.savesData.AdsLevel * _adsModifier * System.Math.Pow(_increasePercent, System.Math.Floor(level / _increaseEveryLevel));
		
		_adsBonusText.text = ConvertNumber.Convert(YandexGame.savesData.AdsLevel * _adsModifier * System.Math.Pow(_increasePercent, System.Math.Floor(level / _increaseEveryLevel))) + "%";

		double a = level / _increaseEveryLevel;

		float b = (float)(a - System.Math.Floor(a));

		_fillImage.fillAmount = b;
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
			case <31: _customization.UnlockColor(id); break;
            case 31: _timeBonuses[0].Reward(); break;
            case 32: _timeBonuses[1].Reward(); break;
            case 35: _rebithManager.AdditionalReward(); break;
			case 36: _offlineReward.GetReward(true); break;
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