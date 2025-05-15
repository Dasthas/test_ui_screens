using Core.Settings;
using Core.Settings.Screen;

namespace Core.UI.Screen
{
    public class BaseScreenModel : IScreenModel
    {
        public IScreenSettings Settings { get; private set; }

        public BaseScreenModel(IScreenSettings screenSettings)
        {
            Settings = screenSettings;
        }
    }
}