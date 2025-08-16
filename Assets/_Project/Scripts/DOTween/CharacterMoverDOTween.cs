using DG.Tweening;
using UnityEngine;

[RequireComponent(typeof(Renderer))]
public class CharacterMoverDOTween : MonoBehaviour
{
    [Header("Настройки DOTween")]

    [Header("Пути движения")]
    [SerializeField] private MovementPath[] _paths;   // Ссылки на ScriptableObject‑путы
    [SerializeField] private float _duration = 5f;
    [Tooltip("Кривая изменения скорости")]
    [SerializeField] private AnimationCurve _easeCurve = AnimationCurve.Linear(0, 0, 1, 1);

    [Header("Эффект следов/пыли")]
    [SerializeField] private bool _isTrailEffect = true;
    [SerializeField] private ParticleSystem _dustTrail;

    [Header("Масштаб при движении")]
    [SerializeField] private bool _isScaleDuringMove = true;
    [SerializeField] private Vector3 _targetScale = new Vector3(1.5f, 1.5f, 1.5f);

    [Header("Цвет персонажа")]
    [SerializeField] private bool _isColorChange = false;
    [SerializeField] private Color _startColor = Color.white; 
    [SerializeField] private Color _endColor = Color.red;

    public MovementPath[] Paths => _paths;

    private int _currentPathIndex;
    private Vector3[] _movePoints;
    private Sequence _moveSequence;
    private Renderer _renderer;

    private void Awake()
    {
        InitiateMovePoints();
        _renderer = GetComponent<Renderer>();
        if (_dustTrail != null)
        {
            //_dustTrail = Instantiate(_dustTrailPrefab, transform);
            _dustTrail.Play();
        }

    }

    private void InitiateMovePoints() // получаем из скриптабла координаты точек
    {
        
        MovementPath path = _paths[_currentPathIndex]; // берём выбранный путь
               
        _movePoints = new Vector3[path.Points.Length]; // переносим координаты всех точек в массив для DOTween
        for (int i = 0; i < path.Points.Length; i++)
        {            
            _movePoints[i] = path.Points[i];
        }
    }

    private void Start()
    {
        CreateMoveSequance();
        _moveSequence.Play();
    }

    [ContextMenu("CreateMoveSequance")]
    private void CreateMoveSequance()
    {
        // Блок перемещения
        if (_moveSequence != null) _moveSequence.Kill(); // проверяем нет ли уже существующей последовательности(исключаем утечку памяти)

        _moveSequence = DOTween.Sequence(); // создаем последовательность для перемещения вдоль заданного массива точек

        _moveSequence
            .Append(transform.DOPath(
                _movePoints, //массив трансформов для передвижения
                _duration,  // полное время прохождения всего пути
                PathType.Linear)) // перемещение по прямой
            .SetEase(_easeCurve)
            .SetLoops(-1, LoopType.Restart); // бесконечный цикл по окончании действий

        // Блок изменения цвета
        if (_isColorChange && _renderer != null)
        {
            _moveSequence.Insert(               // добавляем в последовательность задачу
                0f,                             // выполнение сразу
                _renderer.material.DOColor(     // задача по смене цвета
                    _endColor,                  // конечный цвет
                    _duration)                  // полное время для смены цвета
                .From(_startColor));            // начальный цвет
        }

        // Блок изменения размера
        if (_isScaleDuringMove)                     
        {
            _moveSequence
                .Insert(
                    0f,                         // выполнение сразу    
                    transform.DOScale(
                        _targetScale,           // конечный размер объекта 
                        _duration)              // полное время для изменения размера
                .SetEase(Ease.InOutSine));      // плавная анимация «вход‑выход»
        }
    }

    public void SetPath(int index)
    {
        if (index < 0 || index >= _paths.Length) return;
        _currentPathIndex = index;
        InitiateMovePoints();
        CreateMoveSequance();
    }
}

