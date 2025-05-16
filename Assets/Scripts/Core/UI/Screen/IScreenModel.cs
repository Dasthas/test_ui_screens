using Core.Settings;
using Core.Settings.Screen;

namespace Core.UI.Screen
{
    public interface IScreenModel
    {
        IScreenSettings Settings { get; }
        void Initialize(IScreenSettings settings);
    }
}