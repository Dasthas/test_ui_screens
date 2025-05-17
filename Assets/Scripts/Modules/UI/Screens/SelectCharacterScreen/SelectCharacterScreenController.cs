using System.Threading;
using Core.UI;
using Core.UI.Screen.Base;
using Core.UI.Screen.Closable;
using Cysharp.Threading.Tasks;
using Modules.UI.Screens.SelectCharacterScreen.CharacterElement;

namespace Modules.UI.Screens.SelectCharacterScreen
{
    public sealed class SelectCharacterScreenController :
        BaseScreenController<SelectCharacterScreenView, SelectCharacterScreenModel>, IClosableScreenController

    {
        private const int MAX_EXP_PER_LEVEL = 100;
        private const float ANIM_DUR = 1.4f;

        #region BaseScreenController

        protected override void OnInitialize()
        {
        }

        protected override UniTask OnBeforeShowAsync(CancellationToken ct)
        {
            InitCharacterElements();
            return UniTask.CompletedTask;
        }

        protected override UniTask OnAfterShowAsync(CancellationToken ct)
        {
            InitSubscribes();
            return UniTask.CompletedTask;
        }

        protected override UniTask OnBeforeHideAsync(CancellationToken ct)
        {
            Unsubscribe();
            return UniTask.CompletedTask;
        }

        protected override UniTask OnAfterHideAsync(CancellationToken ct)
        {
            Clear();
            return UniTask.CompletedTask;
        }

        public override void Dispose()
        {
            base.Dispose();
            Unsubscribe();
            Clear();
        }

        #endregion

        private void InitCharacterElements()
        {
            for (var index = 0; index < Model.PrefabsTable.TestCharacters.Count; index++)
            {
                var settingsData = Model.PrefabsTable.TestCharacters[index];
                var characterData = new CharacterElementData()
                {
                    Model = new CharacterElementModel()
                    {
                        LevelNumber = 0,
                        Exp = 0,
                        CharacterData = settingsData,
                        Index = index
                    },
                };
                characterData.Model.Initialize(Model.Settings);
                characterData.Model.OnClicked += OnCharacterClicked;
                Model.CharacterElements.Add(characterData);
            }
        }

        private void Unsubscribe()
        {
            foreach (var characterElementData in Model.CharacterElements)
            {
                characterElementData.Model.OnClicked -= OnCharacterClicked;
            }

            Model.OnCloseButtonClicked -= OnCloseButtonClicked;
            Model.OnIncreaseExpClicked -= OnIncreaseExpClicked;
        }

        private void Clear()
        {
            Model.CharacterElements.Clear();
            Model.SelectedCharacterIndex = -1;
        }

        private void InitSubscribes()
        {
            Model.OnCloseButtonClicked += OnCloseButtonClicked;
            Model.OnIncreaseExpClicked += OnIncreaseExpClicked;
        }

        private void OnIncreaseExpClicked()
        {
            // with UniTasks will be easy to process animations for number of levels, and fill value for every new level till current level
            // but this not implemented, due to the lack of an experience system
            foreach (var characterElementData in Model.CharacterElements)
            {
                var fromExp = characterElementData.Model.Exp;
                characterElementData.Model.Exp += 30;

                if (characterElementData.Model.Exp >= MAX_EXP_PER_LEVEL)
                {
                    characterElementData.Model.Exp -= MAX_EXP_PER_LEVEL;
                    characterElementData.Model.LevelNumber++;
                    characterElementData.View.SetLvlNumberAsync(characterElementData.Model.LevelNumber, ANIM_DUR / 2)
                        .Forget();
                }

                characterElementData.View.IncreaseExpAsync(fromExp, characterElementData.Model.Exp, MAX_EXP_PER_LEVEL,
                    ANIM_DUR).Forget();
            }
        }

        private void OnCharacterClicked(int index)
        {
            if (Model.SelectedCharacterIndex == index)
            {
                return;
            }

            Model.SelectCharacterCts.Cancel();
            Model.SelectCharacterCts.Dispose();
            Model.SelectCharacterCts = CancellationTokenSource.CreateLinkedTokenSource(CancellationTokenSource.Token);
            var data = Model.CharacterElements[index];

            View.ChangeSelectedCharacter(data, Model.SelectCharacterCts.Token).Forget();
            Model.SelectedCharacterIndex = index;
        }

        private void OnCloseButtonClicked()
        {
            UIController.Instance.HideScreenAsync<SelectCharacterScreenController>().Forget();
        }
    }
}