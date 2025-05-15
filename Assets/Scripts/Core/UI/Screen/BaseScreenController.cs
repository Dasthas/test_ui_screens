using System.Threading;
using Cysharp.Threading.Tasks;

namespace Core.UI.Screen
{
    public abstract class BaseScreenController<TView, TModel> : IScreenController
        where TView : IScreenView<TModel>
        where TModel : IScreenModel
    {
        protected TModel Model { get; private set; }
        protected TView View { get; private set; }

        protected CancellationTokenSource CancellationTokenSource { get; set; }

        protected BaseScreenController(TModel model, TView view)
        {
            Model = model;
            View = view;
            View.LinkModel(model);
            CancellationTokenSource = CancellationTokenSource.CreateLinkedTokenSource(View.OnDestroyCancellationToken);
        }

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

        protected abstract UniTask OnBeforeShowAsync(CancellationToken ct);
        protected abstract UniTask OnAfterShowAsync(CancellationToken ct);
        protected abstract UniTask OnBeforeHideAsync(CancellationToken ct);
        protected abstract UniTask OnAfterHideAsync(CancellationToken ct);
    }
}