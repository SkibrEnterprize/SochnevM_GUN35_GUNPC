using UnityEngine;

namespace DefaultNamespace
{
	[RequireComponent(typeof(PositionSaver))]
	public class ReplayMover : MonoBehaviour
	{
		private PositionSaver _save;

		private int _index;
		private PositionSaver.Data _prev;
		private float _duration;

		private void Start()
		{
            ////todo comment: зачем нужны эти проверки?
            /// Чтобы исключить ошибку NullReferenceException и вариант, когда в списке нет ни одного значения, т.е. это меры предостарожности 
            if (!TryGetComponent(out _save) || _save.Records.Count == 0)
			{
				Debug.LogError("Records incorrect value", this);
				//todo comment: Для чего выключается этот компонент?
				// Т.к. если условие верно, то нечего проигрывать и компонент отключается после сообщения в консоль
				enabled = false;
			}
		}

		private void Update()
		{
			var curr = _save.Records[_index];
			//todo comment: Что проверяет это условие (с какой целью)?
			// Если время с запуска сцены больше текущего времени, полученного из записи, то текущие данные для перемещения становятся предыдущими 
			if (Time.time > curr.Time)
			{
				_prev = curr;
				_index++;
				//todo comment: Для чего нужна эта проверка?
				// Если элементы закончились, выключаем компонент, чтобы не получить ошибку выхода за пределы границ
				if (_index >= _save.Records.Count)
				{
					enabled = false;
					Debug.Log($"<b>{name}</b> finished", this);
				}
			}
			//todo comment: Для чего производятся эти вычисления (как в дальнейшем они применяются)?
			// Так вычисляется коэф.смещения для определения скорости смещения далее прирасчете transform.position
			var delta = (Time.time - _prev.Time) / (curr.Time - _prev.Time);
            //todo comment: Зачем нужна эта проверка?
            // Тут проверка, если delta будет "Not a Number" (NaN), например при знаменателе равном нулю в вычислениях, т.е.если теоретически curr.Time и _prev.Time будут равны. 
			// Если так, то в итоге будет присвоение значения, чтобы не было ошибки ппри выполнении
            if (float.IsNaN(delta)) delta = 0f;
			//todo comment: Опишите, что происходит в этой строчке так подробно, насколько это возможно
			// Тут перемещение объекта, к которому прикреплен компонент, через Transform(телепортация) методом линейной интерполяции между предыдущим положением и текущим со скоростью смещения - delta 
			transform.position = Vector3.Lerp(_prev.Position, curr.Position, delta);
		}
	}
}