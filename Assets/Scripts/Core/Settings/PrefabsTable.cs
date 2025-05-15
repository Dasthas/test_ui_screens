using UnityEngine;

namespace Core.Settings
{
    /// <summary>
    /// in the real project would be better to use addressables instead of direct links to prefabs,
    /// cuz now all linked prefabs are loaded if some object have reference for PrefabsTable
    /// </summary>
    [CreateAssetMenu(fileName = "PrefabsTable", menuName = "Create/Setting/PrefabsTable")]
    public class PrefabsTable : ScriptableObject
    {
        /*[Header("UI")] [SerializeField] private MainScreenView _mainScreenPrefab;
        [SerializeField] private WinScreen _winScreenPrefab;
        [SerializeField] private LoseScreen _loseScreenPrefab;
        [SerializeField] private InputView _inputViewPrefab;

        [Header("Physic")] [SerializeField] private FigureView _figureViewPrefab;
        [SerializeField] private FiguresBarView _figuresBarViewPrefab;
        [SerializeField] private FiguresContainerView _figuresContainerPrefab;

        public MainScreenView MainScreenPrefab => _mainScreenPrefab;

        public LoseScreen LoseScreenPrefab => _loseScreenPrefab;

        public WinScreen WinScreenPrefab => _winScreenPrefab;

        public FigureView FigureViewPrefab => _figureViewPrefab;

        public FiguresContainerView FiguresContainerPrefab => _figuresContainerPrefab;

        public FiguresBarView FiguresBarViewPrefab => _figuresBarViewPrefab;

        public InputView InputViewPrefab => _inputViewPrefab;*/
    }
}