using UnityEngine;
using TMPro;

namespace Assets.SimpleLocalization
{
    [RequireComponent(typeof(TextMeshProUGUI))]
    public class LocalizedDynamic : MonoBehaviour
    {
        [SerializeField] private string _localizationKey;    
        [SerializeField] private TextMeshProUGUI _text;

        private string _value;

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
            _text.text = LocalizationManager.Localize(_localizationKey, _value);
        }

        public void SetKey(string key)
        {
            _localizationKey = key;
            Localize();
        }

        public void SetValue(string value)
        {
            _value = value;
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