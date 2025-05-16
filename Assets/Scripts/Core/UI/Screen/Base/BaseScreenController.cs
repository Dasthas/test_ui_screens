using System;
using System.Threading;
using Cysharp.Threading.Tasks;

namespace Core.UI.Screen.Base
{
    public abstract class BaseScreenController<TView, TModel> : IScreenController<TView, TModel>, IDisposable
        where TView : IScreenView<TModel>
        where TModel : IScreenModel
    {
        protected TModel Model { get; private set; }
        protected TView View { get; private set; }

        protected CancellationTokenSource CancellationTokenSource { get; set; }

        public void Initialize(TView view, TModel model)
        {
            Model = model;
            View = view;
            View.Initialize(model);
            CancellationTokenSource = CancellationTokenSource.CreateLinkedTokenSource(View.OnDestroyCancellationToken);
            OnInitialize();
        }
        
        protected abstract void OnInitialize();

        public virtual async UniTask ShowAsync(CancellationToken ct)
        {
            ct.ThrowIfCancellationRequested();
            await OnBeforeShowAsync(ct);
            ct.ThrowIfCancellationRequested();
            await View.ShowAsync(ct);
            ct.ThrowIfCancellationRequested();
            await OnAfterShowAsync(ct);
        }

        public virtual async UniTask HideAsync(CancellationToken ct)
        {
            ct.ThrowIfCancellationRequested();
            await OnBeforeHideAsync(ct);
            ct.ThrowIfCancellationRequested();
            await View.HideAsync(ct);
            ct.ThrowIfCancellationRequested();
            await OnAfterHideAsync(ct);
        }

        /// <summary>
        /// when you call this method and object have subscribes to events, you need to dispose them manually
        /// </summary>
        public virtual void HideImmediately()
        {
            View.HideImmediately();
        }

        protected abstract UniTask OnBeforeShowAsync(CancellationToken ct);
        protected abstract UniTask OnAfterShowAsync(CancellationToken ct);
        protected abstract UniTask OnBeforeHideAsync(CancellationToken ct);
        protected abstract UniTask OnAfterHideAsync(CancellationToken ct);

        public virtual void Dispose()
        {
            CancellationTokenSource?.Dispose();
        }
    }
}