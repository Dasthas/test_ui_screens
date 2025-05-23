using System.Threading;
using Cysharp.Threading.Tasks;

namespace Core.UI.Screen
{
    public interface IScreenView<TModel>
        where TModel : IScreenModel
    {
        CancellationToken OnDestroyCancellationToken { get; }
        void Initialize(TModel model);

        UniTask ShowAsync(CancellationToken ct);
        UniTask HideAsync(CancellationToken ct);
        void HideImmediately();
    }
}