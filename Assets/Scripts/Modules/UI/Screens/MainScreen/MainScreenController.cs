using System;
using System.Threading;
using Core.UI;
using Core.UI.Screen.Base;
using Cysharp.Threading.Tasks;
using Modules.UI.Screens.SelectCharacterScreen;
using Modules.UI.Screens.SettingsScreen;
using UnityEngine;
#if UNITY_EDITOR
using UnityEditor;
#endif

namespace Modules.UI.Screens.MainScreen
{
    public sealed class MainScreenController : BaseScreenController<MainScreenView, MainScreenModel>, IDisposable
    {
        #region BaseScreenController

        protected override void OnInitialize()
        {
        }

        protected override UniTask OnBeforeShowAsync(CancellationToken ct)
        {
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
            return UniTask.CompletedTask;
        }


        public override void Dispose()
        {
            base.Dispose();
            Unsubscribe();
        }

        #endregion

        private void Unsubscribe()
        {
            Model.OnPlayButtonClicked -= OnPlayButtonClicked;
            Model.OnExitGameButtonClicked -= OnExitGameButtonClicked;
            Model.OnSettingsButtonClicked -= OnSettingsButtonClicked;
            Model.OnSelectCharacterButtonClicked -= OnSelectCharacterButtonClicked;
        }

        private void InitSubscribes()
        {
            Model.OnPlayButtonClicked += OnPlayButtonClicked;
            Model.OnExitGameButtonClicked += OnExitGameButtonClicked;
            Model.OnSettingsButtonClicked += OnSettingsButtonClicked;
            Model.OnSelectCharacterButtonClicked += OnSelectCharacterButtonClicked;
        }

        private void OnExitGameButtonClicked()
        {
#if UNITY_EDITOR
            EditorApplication.ExitPlaymode();
#else
            Application.Quit();
#endif
        }

        private void OnSelectCharacterButtonClicked()
        {
            if (Model.IsUIBlocked)
            {
                return;
            }

            BlockUIUntilUniTaskAsync(UIController.Instance.ShowScreenAsync<SelectCharacterScreenController>()).Forget();
        }

        private void OnPlayButtonClicked()
        {
            Debug.Log("Play mode is not implemented");
        }

        private void OnSettingsButtonClicked()
        {
            if (Model.IsUIBlocked)
            {
                return;
            }

            BlockUIUntilUniTaskAsync(UIController.Instance.ShowScreenAsync<SettingsScreenController>()).Forget();
        }

        private async UniTask BlockUIUntilUniTaskAsync(UniTask task)
        {
            Model.IsUIBlocked = true;
            await task;
            Model.IsUIBlocked = false;
        }
    }
}