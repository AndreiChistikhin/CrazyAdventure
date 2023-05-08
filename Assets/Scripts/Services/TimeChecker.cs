using Cysharp.Threading.Tasks;

namespace Services
{
    public class TimeChecker : ITimeChecker
    {
        private int _secondsPassed;
        private bool _timerIsOn;
        
        public async UniTask StartTimer()
        {
            _timerIsOn = true;

            while (_timerIsOn)
            {
                await UniTask.Delay(1000);
                _secondsPassed++;
            }
        }

        public int GetTime()
        {
            return _secondsPassed;
        }

        public void StopTimer()
        {
            _timerIsOn = false;
            _secondsPassed = 0;
        }
    }
}