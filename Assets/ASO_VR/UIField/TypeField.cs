
namespace ASO_VR
{
    using System;
    using UnityEngine;
    using TMPro;
    using UnityEngine.UI;

    public class TypeField : MonoBehaviour
    {
        public event Action<TypeObject> click;
        
        [SerializeField] private TextMeshProUGUI _text;
        [SerializeField] private Button _button;

        private TypeObject _typeObject;

        public void SetParameters(TypeObject typeObject)
        {
            _typeObject = typeObject;
            _typeObject.DataChanged += DataChanged;
            _text.text = typeObject.GetLocalName();
            SetButtonColor();
        }

        public void OnButtonClick()
        {
            click?.Invoke(_typeObject);
        }

        private void DataChanged()
        {
            SetButtonColor();
        }

        private void SetButtonColor()
        {
            var buttonColors = _button.colors;
            
            buttonColors.normalColor =
                _typeObject.GetWork() ? buttonColors.pressedColor : buttonColors.disabledColor;
            
            _button.colors = buttonColors;
        }
        
        private void OnDestroy()
        {
            _typeObject.DataChanged -= DataChanged;
        }
    }
}
