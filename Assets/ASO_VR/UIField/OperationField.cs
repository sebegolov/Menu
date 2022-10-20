using TMPro;

namespace ASO_VR
{
    using System;
    using UnityEngine;
    using TMPro;
    using UnityEngine.UI;

    public class OperationField : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI _text;
        [SerializeField] private Button _button;

        private ObjectOperation _objectOperation;
        
        public void SetParameters(ObjectOperation objectOperation)
        {
            _objectOperation = objectOperation;
            objectOperation.DataChanged += DataChanged;
            _text.text = objectOperation.GetLocalName();
            SetButtonColor();
        }
        
        public void OnButtonClick()
        {
            _objectOperation.SetWork(!_objectOperation.GetWork());
        }

        private void DataChanged()
        {
            SetButtonColor();
        }

        private void SetButtonColor()
        {
            var buttonColors = _button.colors;
            
            buttonColors.normalColor =
                _objectOperation.GetWork() ? buttonColors.pressedColor : buttonColors.disabledColor;
            
            _button.colors = buttonColors;
        }

        private void OnDestroy()
        {
            _objectOperation.DataChanged -= DataChanged;
        }

        public void SetActive(bool show)
        {
            gameObject.SetActive(show);
        }
    }
}
