using Core.Settings.Table;
using Core.UI;
using UnityEngine;

namespace Core.EntryPoints
{
    public class ProjectEntryPoint : MonoBehaviour
    {
        public static PrefabsTable Table;

        [SerializeField] private PrefabsTable _prefabsTable;

        private UIController _uiController;

        void Awake()
        {
            Table = _prefabsTable;
            _uiController = new UIController();
            _uiController.Initialize(_prefabsTable);
        }

        private void OnDestroy()
        {
            _uiController.Dispose();
        }
    }
}