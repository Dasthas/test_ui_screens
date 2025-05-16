using System;
using System.Threading;
using Core.Settings.Screen;
using Cysharp.Threading.Tasks;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

namespace Core.UI.Screen.Base
{
    public abstract class BaseScreenView<TModel> : MonoBehaviour, IScreenView<TModel>
        where TModel : IScreenModel
    {
        [Header("Base Settings")] [SerializeField]
        private CanvasGroup _canvasGroup;

        [SerializeField] private RectTransform _mainRectTransform;
        [SerializeField] private GraphicRaycaster _graphicRaycaster;

        protected TModel Model { get; private set; }

        public CancellationToken OnDestroyCancellationToken => gameObject.GetCancellationTokenOnDestroy();

        protected GraphicRaycaster Raycaster => _graphicRaycaster;

        public virtual void Initialize(TModel model)
        {
            Model = model;
        }

        public virtual async UniTask ShowAsync(CancellationToken ct)
        {
            _graphicRaycaster.enabled = true;
            var settings = Model.Settings;
            switch (settings.SimpleAnimationType)
            {
                case UISimpleAnimationType.Fade:
                    await _canvasGroup
                        .DOFade(1, settings.ShowTime)
                        .SetEase(settings.ShowEase)
                        .ToUniTask(cancellationToken: OnDestroyCancellationToken);
                    break;
                case UISimpleAnimationType.Scale:
                    await _mainRectTransform
                        .DOScale(1, settings.ShowTime)
                        .SetEase(settings.ShowEase)
                        .ToUniTask(cancellationToken: OnDestroyCancellationToken);
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        public virtual async UniTask HideAsync(CancellationToken ct)
        {
            _graphicRaycaster.enabled = false;
            var settings = Model.Settings;
            switch (settings.SimpleAnimationType)
            {
                case UISimpleAnimationType.Fade:
                    await _canvasGroup
                        .DOFade(0, settings.HideTime)
                        .SetEase(settings.HideEase)
                        .ToUniTask(cancellationToken: OnDestroyCancellationToken);
                    break;
                case UISimpleAnimationType.Scale:
                    await _mainRectTransform
                        .DOScale(0, settings.HideTime)
                        .SetEase(settings.HideEase)
                        .ToUniTask(cancellationToken: OnDestroyCancellationToken);
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        public virtual void HideImmediately()
        {
            var settings = Model.Settings;
            _graphicRaycaster.enabled = false;
            switch (settings.SimpleAnimationType)
            {
                case UISimpleAnimationType.Fade:
                    _canvasGroup.alpha = 0;
                    break;
                case UISimpleAnimationType.Scale:
                    _mainRectTransform.localScale = Vector3.zero;
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }
    }
}