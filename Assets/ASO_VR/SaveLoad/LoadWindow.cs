using System.Net.Http;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace ASO_VR
{
    public class LoadWindow : SaveLoad
    {
        [SerializeField] protected RectTransform _nameButton;
        
        [SerializeField] protected RectTransform _nameContainer;
        [SerializeField] protected TextMeshProUGUI _textField;

        [SerializeField][TextArea(5, 20)] protected string textComment;
        
        protected NameButton _currentState;

        protected void SelectState(NameButton button)
        {
            _currentState = button;
            SetCurrenrState(button.GetName());
            SetComment(button.GetComment());
        }

        protected virtual void SetComment(string comment)
        {
            _textField.text = comment;
        }

        public virtual void DeleteState()
        {
            if (_currentState != null)
            {
                DeleteStateByName(_currentState.GetName(), TypeSave.States);
                
                Destroy(_currentState);
                _currentState = null;
            }
        }

        public void LoadState()
        {
            if (_currentState != null)
            {
                LoadStateByName(_currentState.GetName());
            }
        }
    }
}
