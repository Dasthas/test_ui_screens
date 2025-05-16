using System;
using Core.Settings.Screen;
using DG.Tweening;

namespace Core.Settings.AnimatedButton
{
    [Serializable]
    public struct AnimatedButtonSettingsData
    {
        public UISimpleAnimationType SimpleAnimationType;
        public Ease Ease;
        public float Duration;
        public float EndValue;
    }
}