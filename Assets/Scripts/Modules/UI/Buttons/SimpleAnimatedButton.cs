using System;
using Core.Settings.AnimatedButton;
using Core.Settings.Screen;
using Core.UI.AnimatedButton;
using Cysharp.Threading.Tasks;
using DG.Tweening;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace Modules.UI.Buttons
{
    public class SimpleAnimatedButton : BaseAnimatedButton
    {
        [SerializeField] private AnimatedButtonSettings _buttonAnimationSettings;
        [SerializeField] private Button _button;
        [SerializeField] private GraphicRaycaster _raycaster;
        
        public Action OnPointerDownCallback { get; set; }

        public override void OnPointerDown(PointerEventData eventData)
        {
            OnPointerDownCallback?.Invoke();
            ProcessAnimation(_buttonAnimationSettings.PointerDownData);
        }

        public override void OnPointerUp(PointerEventData eventData)
        {
            ProcessAnimation(_buttonAnimationSettings.PointerUpData);
        }

        public override void OnPointerEnter(PointerEventData eventData)
        {
            ProcessAnimation(_buttonAnimationSettings.PointerEnterData);
        }

        public override void OnPointerExit(PointerEventData eventData)
        {
            ProcessAnimation(_buttonAnimationSettings.PointerExitData);
        }

        public void SetRaycaster(GraphicRaycaster raycaster)
        {
            _raycaster = raycaster;
        }

        private void ProcessAnimation(AnimatedButtonSettingsData settings)
        {
            if (_raycaster == null)
            {
                return;
            }
            if (!_raycaster.enabled)
            {
                return;
            }

            _button.DOKill(true);
            switch (settings.SimpleAnimationType)
            {
                case UISimpleAnimationType.Fade:
                    _button.image
                        .DOFade(settings.EndValue, settings.Duration)
                        .SetEase(settings.Ease)
                        .SetTarget(_button)
                        .ToUniTask(cancellationToken: gameObject.GetCancellationTokenOnDestroy()).Forget();
                    break;
                case UISimpleAnimationType.Scale:
                    _button.image.rectTransform
                        .DOScale(settings.EndValue, settings.Duration)
                        .SetEase(settings.Ease)
                        .SetTarget(_button)
                        .ToUniTask(cancellationToken: gameObject.GetCancellationTokenOnDestroy()).Forget();
                    break;
            }
        }
    }
}