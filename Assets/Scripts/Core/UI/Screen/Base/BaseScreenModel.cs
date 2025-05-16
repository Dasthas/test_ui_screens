using Core.Settings.Screen;

namespace Core.UI.Screen.Base
{
    public abstract class BaseScreenModel : IScreenModel
    {
        public IScreenSettings Settings { get; private set; }

        public virtual void Initialize(IScreenSettings settings)
        {
            Settings = settings;
        }
    }
}