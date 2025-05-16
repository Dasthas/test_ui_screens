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

        public virtual UniTask ShowAsync(CancellationToken ct)
        {
            _graphicRaycaster.enabled = true;
            var settings = Model.Settings;

            switch (settings.SimpleAnimationType)
            {
                case UISimpleAnimationType.Fade:
                    _canvasGroup.DOKill(true);
                    return _canvasGroup
                        .DOFade(1, settings.ShowTime)
                        .SetEase(settings.ShowEase)
                        .ToUniTask(cancellationToken: OnDestroyCancellationToken);
                case UISimpleAnimationType.Scale:
                    _mainRectTransform.DOKill(true);
                    return _mainRectTransform
                        .DOScale(1, settings.ShowTime)
                        .SetEase(settings.ShowEase)
                        .ToUniTask(cancellationToken: OnDestroyCancellationToken);
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        public virtual UniTask HideAsync(CancellationToken ct)
        {
            _graphicRaycaster.enabled = false;
            var settings = Model.Settings;

            switch (settings.SimpleAnimationType)
            {
                case UISimpleAnimationType.Fade:
                    _canvasGroup.DOKill(true);
                    return _canvasGroup
                        .DOFade(0, settings.HideTime)
                        .SetEase(settings.HideEase)
                        .ToUniTask(cancellationToken: OnDestroyCancellationToken);
                case UISimpleAnimationType.Scale:
                    _mainRectTransform.DOKill(true);
                    return _mainRectTransform
                        .DOScale(0, settings.HideTime)
                        .SetEase(settings.HideEase)
                        .ToUniTask(cancellationToken: OnDestroyCancellationToken);
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
                    _canvasGroup.DOKill(true);
                    _canvasGroup.alpha = 0;
                    break;
                case UISimpleAnimationType.Scale:
                    _mainRectTransform.DOKill(true);
                    _mainRectTransform.localScale = Vector3.zero;
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }
    }
}