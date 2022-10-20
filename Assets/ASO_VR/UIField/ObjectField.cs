using System.Collections.Generic;

namespace ASO_VR
{
    using System;
    using UnityEngine;
    using TMPro;
    using UnityEngine.UI;

    public class ObjectField : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI _text;
        [SerializeField] private Button _button;
        [SerializeField] private GameObject _separator;
        [SerializeField] private RectTransform _operationPrefab;

        private OPAObject _opaObject;

        private List<OperationField> _operations = new List<OperationField>();
        private bool _show = false;

        public void SetParameters(OPAObject opaObject)
        {
            _opaObject = opaObject;
            _opaObject.DataChanged += DataChanged;
            _text.text = opaObject.GetLocalName();
            AddOperations();
            ShowOperation(_show);
            SetButtonColor();
        }

        public void OnButtonClick()
        {
            _show = !_show;
            ShowOperation(_show);
        }

        private void DataChanged()
        {
            SetButtonColor();
        }
        
        private void SetButtonColor()
        {
            var buttonColors = _button.colors;
            
            buttonColors.normalColor =
                _opaObject.GetWork() ? buttonColors.pressedColor : buttonColors.disabledColor;
            
            _button.colors = buttonColors;
        }

        private void ShowOperation(bool show)
        {
            _separator.SetActive(show);
            foreach (var operation in _operations)
            {
                operation.SetActive(show);
            }
        }

        private void AddOperations()
        {
            foreach (var operation in _opaObject.GetOperationsList())
            {
                AddOperationField(operation);
            }
        }

        private void AddOperationField(ObjectOperation operation)
        {
            GameObject instance = Instantiate(_operationPrefab.gameObject);
            instance.transform.SetParent(gameObject.transform, false);
            
            OperationField operationField = instance.GetComponent<OperationField>();
            operationField.SetParameters(operation);
            _operations.Add(operationField);
        }

        private void OnDestroy()
        {
            _opaObject.DataChanged -= DataChanged;
        }

    }
}
