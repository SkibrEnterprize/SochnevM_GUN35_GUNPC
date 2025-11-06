using Netologia.Quest.Characters.Player;
using Netologia.Quest.Interfaces;
using Netologia.Quest.Objects;
using UnityEngine;
using static UnityEditor.Experimental.GraphView.GraphView;

namespace Netologia.Quest.Talks
{

    public class MessagePusher : MonoBehaviour
    {
        private Transform _player;
        private Camera _camera;
        //переключатель true - пауза между фразами / false - отображение фразы
        private bool _silence;
        private bool _lock;
        private float _delay;
        [SerializeField]
        private MessageElement _message;
        [SerializeField, Space(15f)]
        private WorkerFeatureFlag _workerFeature;
        [SerializeField]
        private CharacterFeatureFlag _characterFeature;
        [SerializeField, Space] private float _talkDelay = 7f;
        [SerializeField]
        private Interval _silenceDelay = new(5f, 10f);
        [SerializeField]
        private float _talkRadius = 2f;

        private void Awake()
        {
            //todo костыльно, но допустимо
            _player = FindObjectOfType<PlayerController>().transform;
            _message = Instantiate(_message, FindObjectOfType<MessageRootTag>().transform);
            _message.Disable();


            _silence = true;
            //first special long
            _delay = _silenceDelay.Random * 2;
            //set sqr
            _talkRadius *= _talkRadius;
            _camera = Camera.main;
        }
        public void LockMessage(bool value)
        {
            if (value)
                _message.Disable();
            else
            {
                _delay = TimeManager.Time + _silenceDelay.Random;
                _silence = true;
            }
        }
        private void Update()
        {
            //print ("Delay =" + _delay);
            if (!TimeManager.IsGame) return;
            if (_lock) return;
            var time = TimeManager.Time;

            //print ("Time =" + time);
            if (_delay < time) //or _delay - time <= 0
            {
                //push new message
                if (_silence)
                {
                    _silence = false;
                    _delay = time + _talkDelay;

                    if (Vector3.SqrMagnitude(transform.position - _player.transform.position) < _talkRadius)
                    {
                        _message.Enable();
                        _message.WorkPush(Director.Work[(int)_workerFeature]);
                    }
                    //может бликовать сообщение так как персонаж может быть в другой точке, a "move push" часть не return;
                    return;
                }
                //delay silence
                _silence = true;
                _delay = time + _silenceDelay.Random;
                _message.Disable();
            }

            //Move push
            if (!_silence)
                _message.Transform.position = _camera.WorldToScreenPoint(transform.position);//offset*
        }
        public void CharacterPush()
        {
            _message.Enable();
            _delay = TimeManager.Time + _talkDelay;
            _silence = false;
            _message.CharacterPush(Director.Personal[(int)_characterFeature]);
        }
        private void OnDrawGizmos()
        {
            Gizmos.color = Color.magenta;
            Gizmos.DrawWireSphere(transform.position, _talkRadius);
        }
    }
}

