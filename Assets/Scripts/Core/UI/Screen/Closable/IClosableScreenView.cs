using UnityEngine.UI;

namespace Core.UI.Screen.Closable
{
    public interface IClosableScreenView
    {
        protected Button CloseButton { get; }
    }
}