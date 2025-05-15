using System.Collections.Generic;
using UnityEngine;

namespace Core.Settings
{
    [CreateAssetMenu(fileName = "GameplaySettings", menuName = "Create/Setting/GameplaySettings")]
    public class GameplaySettings : ScriptableObject
    {
        [SerializeField] private int _minFiguresToMatch = 3;
        [SerializeField] private int _minFiguresToLose = 7;
        [SerializeField] private int _figuresMaxCount = 25;
        [SerializeField] private LayerMask _figuresLayerMask;
        [SerializeField] private float _timeBetweenSpawnFigure = 0.18f;
        [SerializeField] private List<Sprite> _figureIconSprites = new List<Sprite>();
        [SerializeField] private List<Sprite> _figureSprites = new List<Sprite>();
        [SerializeField] private List<Color> _figureColors = new List<Color>();

        public IReadOnlyList<Sprite> FigureIconSprites => _figureIconSprites;

        public IReadOnlyList<Sprite> FigureSprites => _figureSprites;

        public IReadOnlyList<Color> FigureColors => _figureColors;

        public int FiguresMaxCount => _figuresMaxCount;

        public float TimeBetweenSpawnFigure => _timeBetweenSpawnFigure;

        public LayerMask FiguresLayerMask => _figuresLayerMask;

        public int MinFiguresToLose => _minFiguresToLose;

        public int MinFiguresToMatch => _minFiguresToMatch;
    }
}