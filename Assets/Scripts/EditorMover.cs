using UnityEngine;

namespace DefaultNamespace
{
	
	[RequireComponent(typeof(PositionSaver))]
	public class EditorMover : MonoBehaviour
	{
		private PositionSaver _save;
		private float _currentDelay;

		//todo comment: Что произойдёт, если _delay > _duration?
		// Предположу, что не будет получено ни одного значения, т.к.время между которым производитсся замер будет больше всей продолжительности, следовательно замеры прекратятся делать до того момента, когда наступит первый замер
		[Range(0.2f,1.0f)]
		private float _delay = 0.5f;
		[Min(0.2f)]
		private float _duration = 5f;

		private void Start()
		{
            if(_duration<_delay) _duration = _delay*5;
			//todo comment: Почему этот поиск производится здесь, а не в начале метода Update?
            // Получение ссылки на компонент достаточно получить один раз здесь т.к. компонент один, а также в Update() использование GetComponent очень затратно для производительности.
            // Еще нашел информацию, что иногда компонент, кот. мы хотим найти, может не полностью быть инициализирован, и тогда через Update() можно получить ошибку, а Start() позволяет дождаться, когда все будут готовы
            _save = GetComponent<PositionSaver>();
			_save.Records.Clear();
		}

		private void Update()
		{
			_duration -= Time.deltaTime;
			if (_duration <= 0f)
			{
				enabled = false;
				Debug.Log($"<b>{name}</b> finished", this);
				return;
			}
			
			//todo comment: Почему не написать (_delay -= Time.deltaTime;) по аналогии с полем _duration?
			// В таком случае значение _delay один раз дойдет до 0 и больше не восстановит свое значения для следующей итерации замера для записи
			_currentDelay -= Time.deltaTime;
			if (_currentDelay <= 0f)
			{
				_currentDelay = _delay;
				_save.Records.Add(new PositionSaver.Data
				{
					Position = transform.position,
					//todo comment: Для чего сохраняется значение игрового времени?
					// Предположу, что метка времени с привязкой к позиции
					Time = Time.time,
				});
			}
		}
	}
}