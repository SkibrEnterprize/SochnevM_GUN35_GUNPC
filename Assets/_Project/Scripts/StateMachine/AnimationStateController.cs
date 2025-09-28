using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationStateController : MonoBehaviour
{
    /// <summary>Ссылка на Animator, привязанный к этому GameObject.</summary>
    [SerializeField] private Animator _animator;

    // Хэши параметров (чтобы не пересчитывать строку каждый кадр)
    private readonly Dictionary<CharacterAnimation, int> _hashes = new();

    public bool IsAnimationDone { get; private set; }
    /// <summary>
    /// Вызывается при старте. Кэшируем хэши триггеров.
    /// </summary>
    private void Awake()
    {
        if (_animator == null)
            _animator = GetComponent<Animator>();

        // Перебираем все значения перечисления и сохраняем их хеши
        foreach (CharacterAnimation anim in System.Enum.GetValues(typeof(CharacterAnimation)))
        {
            string paramName = anim.ToString();          // "Walk", "Run" …
            int hash = Animator.StringToHash(paramName);
            _hashes[anim] = hash;
        }
    }

    /// <summary>
    /// Устанавливает нужный триггер анимации.
    /// </summary>
    public void SetAnimation(CharacterAnimation animation)
    {
        if (_animator == null) return;

        // Сбросим все остальные триггеры (если нужно). В большинстве случаев
        // Animator сам переключит состояния, но иногда удобно явно сбрасывать.
        ResetAllTriggers();

        // Устанавливаем нужный триггер
        _animator.SetTrigger(_hashes[animation]);
        IsAnimationDone = false;
    }

    public void OnAttackFinished()
    {
        IsAnimationDone = true;
    }

    /// <summary>
    /// Сбрасывает все триггеры. (можно убрать, если не требуется)
    /// </summary>
    private void ResetAllTriggers()
    {
        foreach (var hash in _hashes.Values)
            _animator.ResetTrigger(hash);
    }
}
