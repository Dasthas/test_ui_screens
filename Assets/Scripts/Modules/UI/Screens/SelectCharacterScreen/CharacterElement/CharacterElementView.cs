using System;
using System.Threading;
using Core.Settings.Screen;
using Core.UI.Screen;
using Cysharp.Threading.Tasks;
using DG.Tweening;
using Modules.UI.Buttons;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace Modules.UI.Screens.SelectCharacterScreen.CharacterElement
{
    public class CharacterElementView : MonoBehaviour, IScreenView<CharacterElementModel>
    {
        private const int MAX_EXP_PER_LEVEL = 100;
        [SerializeField] private Image _iconImage;
        [SerializeField] private Image _bgImage;
        [SerializeField] private TextMeshProUGUI _titleText;
        [SerializeField] private CanvasGroup _canvasGroup;
        [SerializeField] private RectTransform _rectTransform;
        [Header("Exp")] [SerializeField] private TextMeshProUGUI _levelNumberText;
        [SerializeField] private Image _levelNumberImage;
        [SerializeField] private Image _levelExpFillImage;
        [SerializeField] private SimpleAnimatedButton _simpleAnimatedButton;

        private CharacterElementModel _model;

        public CancellationToken OnDestroyCancellationToken => gameObject.GetCancellationTokenOnDestroy();

        public SimpleAnimatedButton AnimatedButton => _simpleAnimatedButton;

        public void Initialize(CharacterElementModel model)
        {
            _model = model;
        }

        private void Awake()
        {
            _simpleAnimatedButton.OnPointerDownCallback += OnPointerDown;
        }

        private void OnDestroy()
        {
            _simpleAnimatedButton.OnPointerDownCallback -= OnPointerDown;
        }

        public UniTask ShowAsync(CancellationToken ct)
        {
            var characterSettingData = _model.CharacterData;
            _iconImage.sprite = characterSettingData.CharacterIcon;
            _bgImage.color = characterSettingData.BGColor;
            _titleText.text = characterSettingData.CharacterName;

            var settings = _model.Settings;
            switch (settings.SimpleAnimationType)
            {
                case UISimpleAnimationType.Fade:
                    return _canvasGroup
                        .DOFade(1, settings.ShowTime)
                        .SetEase(settings.ShowEase)
                        .ToUniTask(cancellationToken: OnDestroyCancellationToken);
                case UISimpleAnimationType.Scale:
                    return _rectTransform
                        .DOScale(1, settings.ShowTime)
                        .SetEase(settings.ShowEase)
                        .ToUniTask(cancellationToken: OnDestroyCancellationToken);
            }

            return UniTask.CompletedTask;
        }

        public UniTask HideAsync(CancellationToken ct)
        {
            _simpleAnimatedButton.OnPointerDownCallback -= OnPointerDown;
            return UniTask.CompletedTask;
        }

        public void HideImmediately()
        {
            var settings = _model.Settings;
            switch (settings.SimpleAnimationType)
            {
                case UISimpleAnimationType.Fade:
                    _canvasGroup.alpha = 0;
                    break;
                case UISimpleAnimationType.Scale:
                    _rectTransform.localScale = Vector3.zero;
                    break;
            }
        }

        private void OnPointerDown()
        {
            _model.OnClicked?.Invoke(_model.Index);
        }

        public void SetLvlNumber(int value, float duration)
        {
            _levelNumberImage.DOKill(true);
            var sequence = DOTween.Sequence(_levelNumberText);
            sequence.Append(_levelNumberImage.rectTransform.DOScale(1.5f, duration / 2))
                .AppendCallback(() => _levelNumberText.text = _model.LevelNumber.ToString())
                .Append(_levelNumberImage.rectTransform.DOScale(1, duration / 2))
                .SetEase(Ease.OutBounce);
        }

        public void IncreaseExp(float toExp, float maxValue, float duration)
        {
            _levelExpFillImage.DOKill(true);
            _levelExpFillImage.DOFillAmount(toExp / maxValue, duration);
        }
    }
}