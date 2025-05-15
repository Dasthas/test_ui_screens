using System;
using System.Threading;
using Core.Settings;
using Core.Settings.Screen;
using Cysharp.Threading.Tasks;
using DG.Tweening;
using UnityEngine;

namespace Core.UI.Screen
{
    public abstract class BaseScreenView<TModel> : MonoBehaviour, IScreenView<TModel>
        where TModel : IScreenModel
    {
        [Header("Base Settings")] [SerializeField]
        private CanvasGroup _canvasGroup;
        private RectTransform _mainRectTransform;

        protected TModel Model { get; set; }

        public CancellationToken OnDestroyCancellationToken => gameObject.GetCancellationTokenOnDestroy();

        public virtual void LinkModel(TModel model)
        {
            Model = model;
        }

        public virtual async UniTask ShowAsync(CancellationToken ct)
        {
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
            var settings = Model.Settings;
            switch (settings.SimpleAnimationType)
            {
                case UISimpleAnimationType.Fade:
                    await _canvasGroup
                        .DOFade(0, settings.ShowTime)
                        .SetEase(settings.ShowEase)
                        .ToUniTask(cancellationToken: OnDestroyCancellationToken);
                    break;
                case UISimpleAnimationType.Scale:
                    await _mainRectTransform
                        .DOScale(0, settings.ShowTime)
                        .SetEase(settings.ShowEase)
                        .ToUniTask(cancellationToken: OnDestroyCancellationToken);
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }
    }
}