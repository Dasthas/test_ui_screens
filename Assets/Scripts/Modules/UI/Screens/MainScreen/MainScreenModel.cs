using System;
using Core.UI.Screen.Base;

namespace Modules.UI.Screens.MainScreen
{
    public sealed class MainScreenModel : BaseScreenModel
    {
        public Action OnPlayButtonClicked;
        public Action OnExitGameButtonClicked;
        public Action OnSettingsButtonClicked;
        public Action OnSelectCharacterButtonClicked;

        public bool IsUIBlocked { get; set; }
    }
}