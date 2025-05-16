using System;
using System.Collections.Generic;
using Core.UI.Screen;
using UnityEngine;

namespace Core.Settings.Table
{
    /// <summary>
    /// in the real project would be better to use Addressables instead of direct links to prefabs,
    /// cuz now all linked prefabs are loaded if some object have reference for PrefabsTable
    /// </summary>
    [CreateAssetMenu(fileName = "PrefabsTable", menuName = "Setting/PrefabsTable")]
    public class PrefabsTable : ScriptableObject
    {
        [SerializeField] private List<ScreenSettingsData> _screenPrefabs = new List<ScreenSettingsData>();
        
        [SerializeField] private List<CharacterSettingData> _testCharacters = new List<CharacterSettingData>();

        public List<CharacterSettingData> TestCharacters => _testCharacters;

        public ScreenSettingsData GetScreenSettings<TModel>() where TModel : IScreenModel
        {
            foreach (var screenSettings in _screenPrefabs)
            {
                // its not good practice, in the real project would be better to use Odin Inspector, to serialize generic objects
                if (screenSettings.Prefab.TryGetComponent<IScreenView<TModel>>(out _))
                {
                    return screenSettings;
                }
            }

            throw new Exception($"Screen View prefab not found for {typeof(TModel).Name}");
        }
    }
}