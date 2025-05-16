using System;
using Core.Settings.Screen;
using UnityEngine;
using UnityEngine.Serialization;

namespace Core.Settings.Table
{
    [Serializable]
    public struct ScreenSettingsData
    {
        [FormerlySerializedAs("_uiScreenSettings")] [FormerlySerializedAs("_uiAnimationSettings")] [FormerlySerializedAs("_screenAnimationSettings")] [SerializeField] private ScreenSettings _screenSettings;
        [SerializeField] private GameObject _prefab;

        public ScreenSettings ScreenSettings => _screenSettings;
        public GameObject Prefab => _prefab;
    }
}