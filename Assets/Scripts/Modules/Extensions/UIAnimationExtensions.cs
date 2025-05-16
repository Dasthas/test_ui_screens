using System;
using System.Threading;
using Core.Settings.Screen;
using Cysharp.Threading.Tasks;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

namespace Modules.Extensions
{
    public static class UIAnimationExtensions
    {
        public static UniTask ShowButtonAsync(this Button button, IScreenSettings settings, CancellationToken ct)
        {
            ct.ThrowIfCancellationRequested();
            switch (settings.SimpleAnimationType)
            {
                case UISimpleAnimationType.Fade:
                    button.DOKill();
                    return button.image
                        .DOFade(1, settings.ShowTime)
                        .SetEase(settings.ShowEase)
                        .SetTarget(button)
                        .ToUniTask(cancellationToken: button.GetCancellationTokenOnDestroy());
                case UISimpleAnimationType.Scale:
                    button.DOKill();
                    return button.image.rectTransform
                        .DOScale(1, settings.ShowTime)
                        .SetEase(settings.ShowEase)
                        .SetTarget(button)
                        .ToUniTask(cancellationToken: button.GetCancellationTokenOnDestroy());
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        public static UniTask HideButtonAsync(this Button button, IScreenSettings settings, CancellationToken ct)
        {
            ct.ThrowIfCancellationRequested();
            switch (settings.SimpleAnimationType)
            {
                case UISimpleAnimationType.Fade:
                    button.DOKill();
                    return button.image
                        .DOFade(0, settings.HideTime)
                        .SetEase(settings.HideEase)
                        .SetTarget(button)
                        .ToUniTask(cancellationToken: button.GetCancellationTokenOnDestroy());
                case UISimpleAnimationType.Scale:
                    button.DOKill();
                    return button.image.rectTransform
                        .DOScale(0, settings.HideTime)
                        .SetEase(settings.HideEase)
                        .SetTarget(button)
                        .ToUniTask(cancellationToken: button.GetCancellationTokenOnDestroy());
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        public static void HideButtonImmediately(this Button button, IScreenSettings settings)
        {
            switch (settings.SimpleAnimationType)
            {
                case UISimpleAnimationType.Fade:
                    button.DOKill();
                    var color = button.image.color;
                    color.a = 0;
                    button.image.color = color;
                    break;
                case UISimpleAnimationType.Scale:
                    button.DOKill();
                    button.image.rectTransform.localScale = Vector3.zero;
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }
    }
}