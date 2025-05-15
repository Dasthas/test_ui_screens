using DG.Tweening;
using UnityEngine;

namespace Core.Settings.Screen
{
    [CreateAssetMenu(fileName = "ScreenSetting", menuName = "Create/Setting/ScreenSetting")]
    public class ScreenSettings : ScriptableObject, IScreenSettings
    {
        [SerializeField] private UISimpleAnimationType _simpleAnimationType;
        [SerializeField] private float _showTime;
        [SerializeField] private float _hideTime;
        [SerializeField] private Ease _showEase;
        [SerializeField] private Ease _hideEase;

        public UISimpleAnimationType SimpleAnimationType => _simpleAnimationType;
        public float ShowTime => _showTime;
        public float HideTime => _hideTime;
        public Ease ShowEase => _showEase;
        public Ease HideEase => _hideEase;
    }
}