using System;

namespace Core.UI.Screen.Closable
{
    public interface IClosableScreenModel
    {
        Action OnCloseButtonClicked { get; set; }
    }
}