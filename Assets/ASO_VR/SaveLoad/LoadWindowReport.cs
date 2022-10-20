using System.Text.RegularExpressions;
using UnityEngine;

namespace ASO_VR
{
    public class LoadWindowReport : LoadWindow
    {
        void Start()
        {
            base.Start();
            _textField.text = "";

            if (_loadStates.GetReports().Count > 0)
            {
                CreateButtonName();
            }
        }
        
        protected void CreateButtonName()
        {
            foreach (var state in _loadStates.GetReports())
            {
                GameObject button = Instantiate(_nameButton.gameObject);
                button.transform.SetParent(_nameContainer);
                
                string a = state.Value.GetComment();
                
                button.GetComponent<NameButton>().SetParameters(state.Key, a);
                button.GetComponent<NameButton>().ButtonClick += SelectState;
            }
        }

        protected override void SetComment(string comment)
        {
            string patternState = @"(<lesson.state>)((.*?\n*)*)(</lesson.state>)";
            string patternComment = @"(<lesson.comment>)((.*?\n*)*)(</lesson.comment>)";
            string patternReport = @"(<report.comment>)((.*?\n*)*)(</report.comment>)";
            
            _textField.text = textComment.Replace("<lesson.state>", Regex.Match(comment, patternState).Groups[2].Value).
                                          Replace("<lesson.comment>", Regex.Match(comment, patternComment).Groups[2].Value).
                                          Replace("<report.comment>", Regex.Match(comment, patternReport).Groups[2].Value);
        }
        
        public override void DeleteState()
        {
            if (_currentState != null)
            {
                DeleteStateByName(_currentState.GetName(), TypeSave.Reports);
                
                Destroy(_currentState);
                _currentState = null;
            }
        }


    }
}
