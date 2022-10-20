using System;

namespace ASO_VR
{
    [Serializable]
    public class OperationWorkCorrector : IWorkCorrector
    {
        private int _maxCountOperations = 1;
        private int _currentCountOperations = 0;

        public OperationWorkCorrector(int maxOperations)
        {
            _maxCountOperations = maxOperations;
        }
        
        public bool CorrectWork(bool workState)
        {
            if (workState)
            {
                if (_currentCountOperations < _maxCountOperations)
                {
                    _currentCountOperations++;
                }
                else
                {
                    workState = false;
                }
            }
            else
            {
                if (_currentCountOperations != 0)
                {
                    _currentCountOperations--;
                }
            }
            
            return workState;
        }
    }
}
