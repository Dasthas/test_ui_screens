using System;
using System.Threading;
using Core.EntryPoints;
using Core.UI;
using Core.UI.Screen.Base;
using Core.UI.Screen.Closable;
using Cysharp.Threading.Tasks;

namespace Modules.UI.Screens.SettingsScreen
{
    public sealed class SettingsScreenController :
        BaseScreenController<SettingsScreenView, SettingsScreenModel>, IClosableScreenController
        
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
            DisposeSubscribes();
            return UniTask.CompletedTask;
        }

        protected override UniTask OnAfterHideAsync(CancellationToken ct)
        {
            return UniTask.CompletedTask;
        }

        public override void Dispose()
        {
            DisposeSubscribes();
        }

        #endregion

        private void DisposeSubscribes()
        {
            Model.OnCloseButtonClicked -= OnCloseButtonClicked;
        }

        private void InitSubscribes()
        {
            Model.OnCloseButtonClicked += OnCloseButtonClicked;
        }

        private void OnCloseButtonClicked()
        {
            UIController.Instance.HideScreenAsync<SettingsScreenController>().Forget();
        }
    }
}