using System;
using Core.Settings.Screen;
using Core.Settings.Table;
using Core.UI.Screen;

namespace Modules.UI.Screens.SelectCharacterScreen.CharacterElement
{
    public class CharacterElementModel : IScreenModel
    {
        public IScreenSettings Settings { get; private set; }

        public CharacterSettingData CharacterData { get; set; }
        
        public Action<int> OnClicked { get; set; }

        public int LevelNumber { get; set; }
        public float Exp { get; set; }
        public int Index { get; set; }
        
        public void Initialize(IScreenSettings settings)
        {
            Settings = settings;
        }
    }
}