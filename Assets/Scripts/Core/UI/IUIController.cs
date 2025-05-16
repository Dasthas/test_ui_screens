using System;
using Core.UI.Screen;
using Cysharp.Threading.Tasks;

namespace Core.UI
{
    public interface IUIController
    {
        /// <summary>
        /// Show screen by its Controller type
        /// </summary>
        public UniTask ShowScreenAsync<T>() where T : IScreenProvider;

        /// <summary>
        /// Hide screen by its Controller type
        /// </summary>
        public UniTask HideScreenAsync<T>() where T : IScreenProvider;
    }
}