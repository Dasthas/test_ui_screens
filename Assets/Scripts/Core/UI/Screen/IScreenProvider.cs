using System;
using System.Threading;
using Cysharp.Threading.Tasks;

namespace Core.UI.Screen
{
    // implemented by controllers
    public interface IScreenProvider
    {
        UniTask ShowAsync(CancellationToken ct);
        UniTask HideAsync(CancellationToken ct);
        void HideImmediately();
    }
}