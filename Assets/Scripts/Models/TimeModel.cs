using System;
using System.Threading;
using Core.Utils;
using Cysharp.Threading.Tasks;
using Models.Interfaces;
using UniRx;
using Zenject;

namespace Models
{
    public sealed class TimeModel : ITimeModel, IDisposable, IInitializable
    {
        private readonly CancellationTokenSource _cancellationTokenSource;

        private readonly ReactiveProperty<int> _gameTime = new();

        public IObservable<int> GameTime => _gameTime;

        public TimeModel()
        {
            _cancellationTokenSource = new CancellationTokenSource();
        }

        public void Initialize() => CountTime(_cancellationTokenSource.Token).Forget();

        private async UniTask CountTime(CancellationToken token)
        {
            try
            {
                while (true)
                {
                    await UniTask.Delay(NumericConstants.One * 1000, cancellationToken: token);
                    if (token.IsCancellationRequested)
                        break;

                    _gameTime.Value++;
                }
            }
            catch (OperationCanceledException)
            {
                //выхода из цикла.
            }
        }

        public void Dispose()
        {
            if (!_cancellationTokenSource.IsCancellationRequested)
                _cancellationTokenSource.Cancel();

            _cancellationTokenSource.Dispose();
            _gameTime?.Dispose();                  
        }
    }

}