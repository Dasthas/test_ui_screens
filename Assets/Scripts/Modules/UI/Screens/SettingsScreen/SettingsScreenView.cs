using System.Threading;
using Core.Settings.Screen;
using Core.UI.Screen.Base;
using Core.UI.Screen.Closable;
using Cysharp.Threading.Tasks;
using Modules.Extensions;
using UnityEngine;
using UnityEngine.UI;

namespace Modules.UI.Screens.SettingsScreen
{
    public sealed class SettingsScreenView : BaseScreenView<SettingsScreenModel>, IClosableScreenView
    {
        [SerializeField] private ScreenSettings _buttonScreenSettings;
        [SerializeField] private Button _closeButton;
        Button IClosableScreenView.CloseButton => _closeButton;

        #region Unity events

        private void Awake()
        {
            _closeButton.onClick.AddListener(() => Model.OnCloseButtonClicked?.Invoke());
        }

        private void OnDestroy()
        {
            _closeButton.onClick.RemoveAllListeners();
        }

        #endregion

        public override async UniTask ShowAsync(CancellationToken ct)
        {
            await base.ShowAsync(ct);
            var settings = _buttonScreenSettings;
            await _closeButton.ShowButtonAsync(settings, ct);
        }

        public override async UniTask HideAsync(CancellationToken ct)
        {
            var settings = _buttonScreenSettings;
            UniTask elementsTask = _closeButton.HideButtonAsync(settings, ct);
            await UniTask.WhenAll(elementsTask, base.HideAsync(ct));
        }

        public override void HideImmediately()
        {
            base.HideImmediately();
            var settings = _buttonScreenSettings;
            _closeButton.HideButtonImmediately(settings);
        }
    }
}