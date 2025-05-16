using System.Threading;
using Core.Settings.Screen;
using Core.UI.Screen.Base;
using Core.UI.Screen.Closable;
using Cysharp.Threading.Tasks;
using Modules.Extensions;
using Modules.UI.Screens.SelectCharacterScreen.CharacterElement;
using UnityEngine;
using UnityEngine.UI;

namespace Modules.UI.Screens.SelectCharacterScreen
{
    public sealed class SelectCharacterScreenView : BaseScreenView<SelectCharacterScreenModel>, IClosableScreenView
    {
        [SerializeField] private ScreenSettings _closeButtonScreenSettings;
        [SerializeField] private Button _closeButton;
        [SerializeField] private Button _testIncreaseExpButton;
        [SerializeField] private CharacterElementView _characterElementViewPrefab;
        [SerializeField] private RectTransform _elementsContent;
        [SerializeField] private GridLayoutGroup _gridLayoutGroup;

        [Header("SelectedCharacter")] [SerializeField]
        private RectTransform _selectedCharacterContainer;

        private CharacterElementView _selectedCharacterElement;

        Button IClosableScreenView.CloseButton => _closeButton;

        #region Unity events

        private void Awake()
        {
            _closeButton.onClick.AddListener(() => Model.OnCloseButtonClicked?.Invoke());
            _testIncreaseExpButton.onClick.AddListener(() => Model.OnIncreaseExpClicked?.Invoke());
        }

        private void OnDestroy()
        {
            _closeButton.onClick.RemoveAllListeners();
            _testIncreaseExpButton.onClick.RemoveAllListeners();
        }

        #endregion

        public async UniTask ChangeSelectedCharacter(CharacterElementData newCharacterElement, CancellationToken ct)
        {
            var view = Instantiate(_characterElementViewPrefab, _selectedCharacterContainer);
            view.Initialize(newCharacterElement.Model);
            view.AnimatedButton.SetRaycaster(Raycaster);
            view.HideImmediately();
            await view.ShowAsync(ct);
            if (_selectedCharacterElement != null)
            {
                Destroy(_selectedCharacterElement.gameObject);
            }

            _selectedCharacterElement = view;
        }

        public override async UniTask ShowAsync(CancellationToken ct)
        {
            await base.ShowAsync(ct);
            var settings = _closeButtonScreenSettings;
            await UniTask.WhenAll(_testIncreaseExpButton.ShowButtonAsync(settings, ct),
                _closeButton.ShowButtonAsync(settings, ct));

            foreach (var data in Model.CharacterElements)
            {
                var view = Instantiate(_characterElementViewPrefab, _elementsContent);
                view.Initialize(data.Model);
                view.AnimatedButton.SetRaycaster(Raycaster);
                view.HideImmediately();
                view.ShowAsync(ct).Forget();
                data.View = view;
                var halfSize = _elementsContent.sizeDelta.y / 2;
                var pos = _elementsContent.position;
                pos.y = halfSize;
                _elementsContent.position = pos;
            }
        }

        public override async UniTask HideAsync(CancellationToken ct)
        {
            var settings = _closeButtonScreenSettings;
            await UniTask.WhenAll(_closeButton.HideButtonAsync(settings, ct),
                _testIncreaseExpButton.HideButtonAsync(settings, ct));
            await base.HideAsync(ct);
            
            _closeButton.HideButtonImmediately(settings);

            // can be improved using pool
            if (_selectedCharacterElement != null)
            {
                Destroy(_selectedCharacterElement.gameObject);
            }

            foreach (var element in Model.CharacterElements)
            {
                Destroy(element.View?.gameObject);
            }
        }

        public override void HideImmediately()
        {
            base.HideImmediately();
            var settings = _closeButtonScreenSettings;
            _closeButton.HideButtonImmediately(settings);
            _testIncreaseExpButton.HideButtonImmediately(settings);
        }
    }
}