using UnityEngine;
using TMPro;

namespace Assets.SimpleLocalization
{
    [RequireComponent(typeof(TextMeshProUGUI))]
    public class LocalizedDual : MonoBehaviour
    {
        [SerializeField] private string _localizationKey;
        [SerializeField] private string _localizationAdditionalKey;
        [SerializeField] private TextMeshProUGUI _text;

        private string _additionalKey;

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
            _additionalKey = LocalizationManager.Localize(_localizationAdditionalKey);
            _text.text = LocalizationManager.Localize(_localizationKey, _additionalKey);
        }

        public void SetKeyName(string key)
        {
            _localizationKey = key;
            Localize();
        }

        public void SetAdditionalKey(string key)
        {
            _localizationAdditionalKey = key;
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