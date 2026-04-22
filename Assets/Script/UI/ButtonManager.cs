using UnityEngine;
using UnityEngine.UI;

namespace Script.UI
{
    public class ButtonManager : MonoBehaviour
    {
        [SerializeField] private UIManager _uiManager;
        [SerializeField] private Button[] _restartButtons;
        [SerializeField] private Button[] _backMenuButtons;
        [SerializeField] private Button _nextLevelButton;
        

        private void OnEnable()
        {
            foreach (var restartButton in _restartButtons)
            {
                restartButton.onClick.AddListener(RestartLevel);
            }

            foreach (var backMenuButton in _backMenuButtons)
            {
                backMenuButton.onClick.AddListener(BackMenu);
            }

            _nextLevelButton.onClick.AddListener(NextLevelClick);
        }

        private void OnDisable()
        {
            foreach (var restartButton in _restartButtons)
            {
                restartButton.onClick.RemoveListener(RestartLevel);
            }

            foreach (var backMenuButton in _backMenuButtons)
            {
                backMenuButton.onClick.RemoveListener(BackMenu);
            }
            
            _nextLevelButton.onClick.RemoveListener(NextLevelClick);
        }

        private void RestartLevel()
        {
            _uiManager.RestartLevel();
        }

        private void BackMenu()
        {
            _uiManager.SceneChanger(1);
        }

        private void NextLevelClick()
        {
            _uiManager.NextLevel();
        }
    }
}