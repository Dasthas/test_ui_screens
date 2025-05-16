using System.Threading;
using Core.Settings.Screen;
using Core.UI.Screen.Base;
using Cysharp.Threading.Tasks;
using Modules.Extensions;
using UnityEngine;
using UnityEngine.UI;

namespace Modules.UI.Screens.MainScreen
{
    public sealed class MainScreenView : BaseScreenView<MainScreenModel>
    {
        [SerializeField] private ScreenSettings _buttonsScreenSettings;
        [SerializeField] private Button _playButton;
        [SerializeField] private Button _exitGameButton;
        [SerializeField] private Button _settingsButton;
        [SerializeField] private Button _selectCharacterButton;

        #region Unity events

        private void Awake()
        {
            _playButton.onClick.AddListener(() => Model.OnPlayButtonClicked?.Invoke());
            _exitGameButton.onClick.AddListener(() => Model.OnExitGameButtonClicked?.Invoke());
            _settingsButton.onClick.AddListener(() => Model.OnSettingsButtonClicked?.Invoke());
            _selectCharacterButton.onClick.AddListener(() => Model.OnSelectCharacterButtonClicked?.Invoke());
        }

        private void OnDestroy()
        {
            _playButton.onClick.RemoveAllListeners();
            _exitGameButton.onClick.RemoveAllListeners();
            _settingsButton.onClick.RemoveAllListeners();
            _selectCharacterButton.onClick.RemoveAllListeners();
        }

        #endregion

        public override async UniTask ShowAsync(CancellationToken ct)
        {
            await base.ShowAsync(ct);
            var settings = _buttonsScreenSettings;
            await UniTask.WhenAll(
                _playButton.ShowButtonAsync(settings, ct),
                _exitGameButton.ShowButtonAsync(settings, ct),
                _settingsButton.ShowButtonAsync(settings, ct),
                _selectCharacterButton.ShowButtonAsync(settings, ct)
            );
        }

        public override async UniTask HideAsync(CancellationToken ct)
        {
            var settings = _buttonsScreenSettings;
            await UniTask.WhenAll(
                base.HideAsync(ct),
                _playButton.HideButtonAsync(settings, ct),
                _exitGameButton.HideButtonAsync(settings, ct),
                _settingsButton.HideButtonAsync(settings, ct),
                _selectCharacterButton.HideButtonAsync(settings, ct)
            );
        }

        public override void HideImmediately()
        {
            base.HideImmediately();
            var settings = _buttonsScreenSettings;
            _playButton.HideButtonImmediately(settings);
            _exitGameButton.HideButtonImmediately(settings);
            _settingsButton.HideButtonImmediately(settings);
            _selectCharacterButton.HideButtonImmediately(settings);
        }
    }
}