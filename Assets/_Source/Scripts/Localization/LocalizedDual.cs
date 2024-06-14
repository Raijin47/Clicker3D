using UnityEngine;
using TMPro;

namespace Assets.SimpleLocalization
{
    [RequireComponent(typeof(TextMeshProUGUI))]
    public class LocalizedDual : MonoBehaviour
    {
        [SerializeField] private string _localizationFirstKey;
        [SerializeField] private string _localizationSecondKey;
        [SerializeField] private TextMeshProUGUI _text;

        private string _secondText;

        private void Start()
        {
            Localize();
            LocalizationManager.LocalizationChanged += Localize;
        }

        private void OnDestroy()
        {
            LocalizationManager.LocalizationChanged -= Localize;
        }

        private void Localize()
        {
            _secondText = LocalizationManager.Localize(_localizationSecondKey);
            _text.text = LocalizationManager.Localize(_localizationFirstKey, _secondText);
        }

        public void SetFirstKey(string key)
        {
            _localizationFirstKey = key;
            Localize();
        }

        public void SetSecondKey(string key)
        {
            _localizationSecondKey = key;
            Localize();
        }

#if UNITY_EDITOR
        private void OnValidate()
        {
            _text ??= GetComponent<TextMeshProUGUI>();
        }
#endif
    }
}