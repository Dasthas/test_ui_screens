using System;
using Core.UI.Screen;
using Core.UI.Screen.Base;
using Core.UI.Screen.Closable;

namespace Modules.UI.Screens.SettingsScreen
{
    public sealed class SettingsScreenModel : BaseScreenModel, IClosableScreenModel
    {
        public Action OnCloseButtonClicked { get; set; }
    }
}