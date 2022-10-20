using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;



namespace ASO_VR
{
    public class NameButton : MonoBehaviour
    {
        public event Action<NameButton> ButtonClick;
        
        [SerializeField] private TextMeshProUGUI _nameField;
        
        private string _name;
        private string _comment;
    
        public void SetParameters(string name, string comment)
        {
            _name = name;
            _nameField.text = name;
            _comment = comment;
        }
    
        public string GetName()
        {
            return _name;
        }
    
        public string GetComment()
        {
            return _comment;
        }

        public void OnButtonClick()
        {
            ButtonClick?.Invoke(this);
        }
    
        private void OnDestroy()
        {
            Destroy(gameObject);
        }
    }
}