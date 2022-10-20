using System.Collections.Generic;
using JetBrains.Annotations;
using UnityEngine;

namespace ASO_VR
{
    [CreateAssetMenu()]
    public class StatesList : ScriptableObject
    {
        [SerializeField]
        private bool _isLoad = false;
        
        private Dictionary<string, Lesson> _states = new Dictionary<string, Lesson>();
        private Dictionary<string, Report> _reports = new Dictionary<string, Report>();
        
        private Dictionary<string, int> _saveNames = new Dictionary<string, int>();
        private Dictionary<string, int> _reportNames = new Dictionary<string, int>();
        
        [SerializeField]
        [CanBeNull] private string _currentLesson;

        public Dictionary<string, Lesson> GetStates()
        {
            return _states;
        }

        public Dictionary<string, Report> GetReports()
        {
            return _reports;
        }

        public Lesson GetLoadDataByName(string name)
        {
            return _states[name];
        }

        public void AddState(string name, Lesson state)
        {
            _states.Add(name, state);
        }

        public void AddReport(string name, Report report)
        {
            _reports.Add(name, report);
        }

        public void DeleteStateByName(string name)
        {
            if (_states.ContainsKey(name))
            {
                _states.Remove(name);
                _saveNames.Remove(name);

                if (_currentLesson == name)
                {
                    _currentLesson = null;
                }
            }
        }

        public void DeleteReportByName(string name)
        {
            if (_reports.ContainsKey(name))
            {
                _reports.Remove(name);
                _reportNames.Remove(name);
            }
        }

        public bool GetLoadState()
        {
            return _states.Count > 0;
        }

        public bool GetLoadReport()
        {
            return _reports.Count > 0;
        }

        public void SetLoadState(bool isLoad)
        {
            _isLoad = isLoad;
        }

        public Dictionary<string, int> GetDictionaryStateNames()
        {
            return _saveNames;
        }

        public Dictionary<string, int> GetDictionaryReportNames()
        {
            return _reportNames;
        }

        public bool AddStateName(string name, int numberSave)
        {
            if (_saveNames.ContainsKey(name) || _saveNames.ContainsValue(numberSave))
            {
                return false;
            }

            _saveNames.Add(name, numberSave);
            
            return true;
        }

        public bool AddReportName(string name, int numberSave)
        {
            if (_reportNames.ContainsKey(name) || _reportNames.ContainsValue(numberSave))
            {
                return false;
            }

            _reportNames.Add(name, numberSave);
            
            return true;
        }

        public void SetCurrentLesson([CanBeNull] string name)
        {
            _currentLesson = name;
        }

        [CanBeNull]
        public string GetNameCurrentLesson()
        {
            return _currentLesson;
        }

        public Lesson GetCurrentLesson()
        {
            return _currentLesson != null ? _states[_currentLesson] : null;
        }

        #if UNITY_EDITOR
        public void ClearAfterTest()
        {
            _isLoad = false;
            _currentLesson = null;
            _states.Clear();
        }
        #endif
    }
}
