using System.Threading;
using Cysharp.Threading.Tasks;

namespace Core.UI.Screen
{
    public interface IScreenController<TView, TModel> : IScreenProvider
        where TView : IScreenView<TModel>
        where TModel : IScreenModel
    {
        void Initialize(TView view, TModel model);
    }
}