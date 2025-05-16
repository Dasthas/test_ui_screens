using System;
using System.Collections.Generic;
using System.Threading;
using Core.EntryPoints;
using Core.Settings.Screen;
using Core.Settings.Table;
using Core.UI.Screen.Base;
using Core.UI.Screen.Closable;
using Modules.UI.Screens.SelectCharacterScreen.CharacterElement;

namespace Modules.UI.Screens.SelectCharacterScreen
{
    public sealed class SelectCharacterScreenModel : BaseScreenModel, IClosableScreenModel
    {
        public Action OnCloseButtonClicked { get; set; }
        public Action OnIncreaseExpClicked { get; set; }
        public PrefabsTable PrefabsTable { get; private set; }
        
        public List<CharacterElementData> CharacterElements { get; private set; } = new List<CharacterElementData>();

        public int SelectedCharacterIndex { get; set; } = -1;
        
        public CancellationTokenSource SelectCharacterCts { get; set; } = new CancellationTokenSource();

        public override void Initialize(IScreenSettings settings)
        {
            base.Initialize(settings);
            PrefabsTable = ProjectEntryPoint.Table;
        }
    }
}