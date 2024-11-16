using TMPro;
using UnityEngine;
using UnityEngine.UI;
using YG;

public class AdsManager : MonoBehaviour
{
	[SerializeField] private Customization _customization;
	[SerializeField] private RebithManager _rebithManager;
	[SerializeField] private OfflineReward _offlineReward;
	[SerializeField] private AdsReward _adsReward;
	[SerializeField] private BonusBase[] _timeBonuses;
	[SerializeField] private TextMeshProUGUI _adsBonusText;
	[SerializeField] private Image _fillImage;
	[SerializeField] private RouletteSpin _roulette;

	private const double _adsModifier = 10;
	private const double _increasePercent = 1.1;
	private const double _increaseEveryLevel = 10;

	private void OnEnable() => YandexGame.RewardVideoEvent += Rewarded;
	private void OnDisable() => YandexGame.RewardVideoEvent -= Rewarded;


	private int Level
    {
		get => YandexGame.savesData.AdsLevel;
		set
        {
			YandexGame.savesData.AdsLevel = value;
			YandexGame.SaveProgress();
			UpdateAdsModifier();
		}
    }

    private void UpdateAdsModifier()
    {
		Modifier.ADsBoost = Level * _adsModifier * System.Math.Pow(_increasePercent, System.Math.Floor(Level / _increaseEveryLevel));
		
		_adsBonusText.text = ConvertNumber.Convert(YandexGame.savesData.AdsLevel * _adsModifier * System.Math.Pow(_increasePercent, System.Math.Floor(Level / _increaseEveryLevel))) + "%";

		double a = Level / _increaseEveryLevel;

		float b = (float)(a - System.Math.Floor(a));

		_fillImage.fillAmount = b;
	}

    public void Init()
    {
		for (int i = 0; i < _timeBonuses.Length; i++)
			_timeBonuses[i].Init();

		UpdateAdsModifier();
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
			case 37: _adsReward.ClosePunel(true); break;
			case 38: _roulette.AdsReward(); break;
		}

		Level++;
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