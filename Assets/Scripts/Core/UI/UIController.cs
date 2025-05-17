using System;
using System.Collections.Generic;
using System.Threading;
using Core.Settings.Table;
using Core.UI.Screen;
using Cysharp.Threading.Tasks;
using Modules.UI.Screens.MainScreen;
using Modules.UI.Screens.SelectCharacterScreen;
using Modules.UI.Screens.SettingsScreen;
using UnityEngine;
using Object = UnityEngine.Object;

namespace Core.UI
{
    public class UIController : IUIController, IDisposable
    {
        public static IUIController Instance;

        private Dictionary<Type, IScreenProvider> _screens = new Dictionary<Type, IScreenProvider>();

        private PrefabsTable _prefabsTable;

        private CancellationTokenSource _cancellationTokenSource =
            CancellationTokenSource.CreateLinkedTokenSource(Application.exitCancellationToken);

        public void Initialize(PrefabsTable prefabsTable)
        {
            Instance = this;
            _prefabsTable = prefabsTable;
            TestInitializeAllScreens();
        }

        #region IUIController

        public UniTask ShowScreenAsync<T>() where T : IScreenProvider
        {
            if (_screens.TryGetValue(typeof(T), out IScreenProvider screenProvider))
            {
                return screenProvider.ShowAsync(_cancellationTokenSource.Token);
            }
            else
            {
                Debug.LogError($"Screen provider not found for {typeof(T).Name}");
                return UniTask.CompletedTask;
            }
        }

        public UniTask HideScreenAsync<T>() where T : IScreenProvider
        {
            if (_screens.TryGetValue(typeof(T), out IScreenProvider screenProvider))
            {
                return screenProvider.HideAsync(_cancellationTokenSource.Token);
            }
            else
            {
                Debug.LogError($"Screen provider not found for {typeof(T).Name}");
                return UniTask.CompletedTask;
            }
        }

        #endregion

        /// <summary>
        /// Generic method for MVP Screen structure creation
        /// </summary>
        private void InitializeScreen<TController, TModel, TView>(bool hideImmediately = false)
            where TController : IScreenController<TView, TModel>
            where TModel : IScreenModel
            where TView : IScreenView<TModel>
        {
            var screenSettingsData = _prefabsTable.GetScreenSettings<TModel>();

            var model = Activator.CreateInstance<TModel>();
            model.Initialize(screenSettingsData.ScreenSettings);

            var view = Object.Instantiate(screenSettingsData.Prefab)
                .GetComponent<TView>();
            view.Initialize(model);

            var controller = Activator.CreateInstance<TController>();
            controller.Initialize(view, model);
            if (hideImmediately)
            {
                controller.HideImmediately();
            }

            _screens.Add(typeof(TController), controller);
        }

        private void TestInitializeAllScreens()
        {
            InitializeScreen<SelectCharacterScreenController, SelectCharacterScreenModel, SelectCharacterScreenView>(
                true);

            InitializeScreen<MainScreenController, MainScreenModel, MainScreenView>(true);
            InitializeScreen<SettingsScreenController, SettingsScreenModel, SettingsScreenView>(true);
            ShowScreenAsync<MainScreenController>().Forget();
        }

        public void Dispose()
        {
            _cancellationTokenSource?.Dispose();
            foreach (var (key, value) in _screens)
            {
                if (value is IDisposable disposable)
                {
                    Debug.Log("Dispose " + value.GetType().Name);
                    disposable.Dispose();
                }
            }
        }
    }
}