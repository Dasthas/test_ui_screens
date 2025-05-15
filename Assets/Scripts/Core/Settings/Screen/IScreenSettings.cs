using DG.Tweening;

namespace Core.Settings.Screen
{
    public interface IScreenSettings
    {
        UISimpleAnimationType SimpleAnimationType { get; }
        float ShowTime { get; }
        float HideTime { get; }
        Ease ShowEase { get; }
        Ease HideEase { get; }
    }
}