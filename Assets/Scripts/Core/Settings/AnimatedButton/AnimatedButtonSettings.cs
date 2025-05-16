using UnityEngine;

namespace Core.Settings.AnimatedButton
{
    [CreateAssetMenu(fileName = "AnimatedButtonSettings", menuName = "Setting/AnimatedButtonSettings")]
    public class AnimatedButtonSettings : ScriptableObject
    {
        [SerializeField] private AnimatedButtonSettingsData _pointerDownData;
        [SerializeField] private AnimatedButtonSettingsData _pointerUpData;
        [SerializeField] private AnimatedButtonSettingsData _pointerEnterData;
        [SerializeField] private AnimatedButtonSettingsData _pointerExitData;

        public AnimatedButtonSettingsData PointerDownData => _pointerDownData;
        public AnimatedButtonSettingsData PointerUpData => _pointerUpData;
        public AnimatedButtonSettingsData PointerEnterData => _pointerEnterData;
        public AnimatedButtonSettingsData PointerExitData => _pointerExitData;
    }
}