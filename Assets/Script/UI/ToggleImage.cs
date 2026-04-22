using UnityEngine;
using UnityEngine.UI;

namespace Script.UI
{
    public class ToggleImage : MonoBehaviour
    {
        [SerializeField] private Button _button;
        [SerializeField] private Image _imageComponent;
        [SerializeField] private Sprite[] _toggleSprites;

        private int _currentIndex;

        private const string SoundIconKey = "SoundIcon";

        private void Awake()
        {
            _currentIndex = PlayerPrefs.HasKey(SoundIconKey) ? PlayerPrefs.GetInt(SoundIconKey, _currentIndex) : 0;
        }

        private void Start()
        {
            _button.onClick.AddListener(Toggle);

            if (_toggleSprites.Length > 0)
            {
                _imageComponent.sprite = _toggleSprites[_currentIndex];
            }
        }

        private void Toggle()
        {
            if (_toggleSprites.Length == 0)
            {
                return;
            }

            _currentIndex = (_currentIndex + 1) % _toggleSprites.Length;
            _imageComponent.sprite = _toggleSprites[_currentIndex];

            SaveIcon();
        }

        private void SaveIcon()
        {
            PlayerPrefs.SetInt(SoundIconKey, _currentIndex);
            PlayerPrefs.Save();
        }
    }
}